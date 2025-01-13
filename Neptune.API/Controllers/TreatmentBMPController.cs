﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neptune.Models.DataTransferObjects;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.Common.GeoSpatial;
using NetTopologySuite.Features;

namespace Neptune.API.Controllers
{
    [ApiController]
    public class TreatmentBMPController(
        NeptuneDbContext dbContext,
        ILogger<TreatmentBMPController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration,
        Person callingUser)
        : SitkaController<TreatmentBMPController>(dbContext, logger, keystoneService, neptuneConfiguration, callingUser)
    {
        [HttpGet("treatment-bmps/{projectID}/getByProjectID")]
        [UserViewFeature]
        public ActionResult<List<TreatmentBMPUpsertDto>> GetByProjectID([FromRoute] int projectID)
        {
            var treatmentBMPUpsertDtos = TreatmentBMPs.ListByProjectIDAsUpsertDto(DbContext, projectID);
            return Ok(treatmentBMPUpsertDtos);
        }

        [HttpGet("treatment-bmps")]
        [JurisdictionEditFeature]
        public ActionResult<List<TreatmentBMPDisplayDto>> ListByPersonID()
        {
            var treatmentBMPDisplayDtos = TreatmentBMPs.ListByPersonIDAsDisplayDto(DbContext, CallingUser);
            return Ok(treatmentBMPDisplayDtos);
        }

        [HttpGet("treatment-bmps/verified/feature-collection")]
        [JurisdictionEditFeature]
        public ActionResult<FeatureCollection> ListTreatmentBMPsAsFeatureCollection()
        {
            var featureCollection = TreatmentBMPs.ListInventoryIsVerifiedByPersonIDAsFeatureCollection(DbContext, CallingUser);
            return Ok(featureCollection);
        }

        [HttpGet("treatmentBMPTypes")]
        [UserViewFeature]
        public ActionResult<List<TreatmentBMPTypeWithModelingAttributesDto>> ListTypes()
        {
            var treatmentBMPTypeWithModelingAttributesDtos = TreatmentBMPs.ListWithModelingAttributesAsDto(DbContext);
            return Ok(treatmentBMPTypeWithModelingAttributesDtos);
        }

        [HttpPut("treatmentBMPs/{treatmentBMPID}/treatmentBMPType/{treatmentBMPTypeID}")]
        [UserViewFeature]
        public ActionResult<int> ChangeTreatmentBMPType([FromRoute] int treatmentBMPID, int treatmentBMPTypeID)
        {
            var updatedTreatmentBMPModelingTypeID = TreatmentBMPs.ChangeTreatmentBMPType(DbContext, treatmentBMPID, treatmentBMPTypeID);
            var projectID = TreatmentBMPs.GetByTreatmentBMPID(DbContext, treatmentBMPID).ProjectID;
            if (projectID != null)
            {
                Projects.SetUpdatePersonAndDate(DbContext, (int)projectID, CallingUser.PersonID);
            }
            
            return Ok(updatedTreatmentBMPModelingTypeID);
        }

        [HttpGet("treatmentBMPModelingAttributeDropdownItems")]
        [UserViewFeature]
        public ActionResult<List<TreatmentBMPModelingAttributeDropdownItemDto>> GetModelingAttributeDropdownItems()
        {
            var treatmentBMPModelingAttributeDropdownItemDtos = TreatmentBMPs.GetModelingAttributeDropdownItemsAsDto(DbContext);
            return Ok(treatmentBMPModelingAttributeDropdownItemDtos);
        }

        [HttpPut("treatmentBMPs/{projectID}/updateLocations")]
        [JurisdictionEditFeature]
        public async Task<ActionResult> MergeTreatmentBMPLocations([FromRoute] int projectID, List<TreatmentBMPDisplayDto> treatmentBMPDisplayDtos)
        {
            var project = DbContext.Projects.SingleOrDefault(x => x.ProjectID == projectID);
            if (project == null)
            {
                return BadRequest();
            }

            var existingProjectTreatmentBMPs = DbContext.TreatmentBMPs.Where(x => x.ProjectID == project.ProjectID).ToList();
            foreach (var treatmentBMPDisplayDto in treatmentBMPDisplayDtos)
            {
                var treatmentBMP = existingProjectTreatmentBMPs.SingleOrDefault(x =>
                    x.TreatmentBMPID == treatmentBMPDisplayDto.TreatmentBMPID);
                if (treatmentBMP != null)
                {
                    var locationPointGeometry4326 = TreatmentBMPs.CreateLocationPoint4326FromLatLong(treatmentBMPDisplayDto.Latitude, treatmentBMPDisplayDto.Longitude);
                    var locationPoint = locationPointGeometry4326.ProjectTo2771();
                    treatmentBMP.LocationPoint = locationPoint;
                    treatmentBMP.LocationPoint4326 = locationPointGeometry4326;
                }
            }
            await DbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("treatmentBMPs/{projectID}")]
        [JurisdictionEditFeature]
        public async Task<ActionResult> MergeTreatmentBMPs([FromRoute] int projectID, List<TreatmentBMPUpsertDto> treatmentBMPUpsertDtos)
        {
            var project = DbContext.Projects.SingleOrDefault(x => x.ProjectID == projectID);
            if (project == null)
            {
                return BadRequest();
            }

            var namingConflicts = treatmentBMPUpsertDtos.GroupBy(x => x.TreatmentBMPName).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
            if (namingConflicts.Any())
            {
                ModelState.AddModelError("TreatmentBMPName",
                    $"Treatment BMP names need to be unique within a project.  The following names are used more than once: {string.Join(", ", namingConflicts)}");
            }
            else
            {
                var existingTreatmentBMPs = DbContext.TreatmentBMPs.Where(x => x.ProjectID == projectID).AsNoTracking().ToList();
                foreach (var treatmentBMPUpsertDto in treatmentBMPUpsertDtos.Where(treatmentBMPUpsertDto =>
                             existingTreatmentBMPs
                                 .Any(x => x.TreatmentBMPName == treatmentBMPUpsertDto.TreatmentBMPName &&
                                           x.TreatmentBMPID != treatmentBMPUpsertDto.TreatmentBMPID)))
                {
                    ModelState.AddModelError("TreatmentBMPName",
                        $"A Treatment BMP with the name {treatmentBMPUpsertDto.TreatmentBMPName} already exists for this project. Treatment BMP names must be unique within a project.");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Projects.DeleteProjectNereidResultsAndGrantScores(DbContext, projectID);
            var existingProjectTreatmentBMPs = DbContext.TreatmentBMPs.Include(x => x.TreatmentBMPModelingAttributeTreatmentBMP).Where(x => x.ProjectID == project.ProjectID).ToList();
            var existingProjectTreatmentBMPModelingAttributes = existingProjectTreatmentBMPs.Where(x => x.TreatmentBMPModelingAttributeTreatmentBMP != null).Select(x => x.TreatmentBMPModelingAttributeTreatmentBMP).ToList();

            var updatedTreatmentBMPs = treatmentBMPUpsertDtos.Select(x => TreatmentBMPs.TreatmentBMPFromUpsertDtoAndProject(DbContext, x, project)).ToList();

            await DbContext.TreatmentBMPs.AddRangeAsync(updatedTreatmentBMPs.Where(x => x.TreatmentBMPID == 0));
            await DbContext.SaveChangesAsync();

            // update upsert dtos with new TreatmentBMPIDs
            foreach (var treatmentBMPUpsertDto in treatmentBMPUpsertDtos.Where(x => x.TreatmentBMPID == 0))
            {
                treatmentBMPUpsertDto.TreatmentBMPID = existingProjectTreatmentBMPs
                    .Single(x => x.TreatmentBMPName == treatmentBMPUpsertDto.TreatmentBMPName).TreatmentBMPID;
            }

            // update TreatmentBMP and TreatmentBMPModelingAttribute records
            existingProjectTreatmentBMPs.MergeUpdate(updatedTreatmentBMPs,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID,
                (x, y) =>
                {
                    x.TreatmentBMPName = y.TreatmentBMPName;
                    x.LocationPoint4326 = y.LocationPoint4326;
                    x.LocationPoint = y.LocationPoint;
                    x.WatershedID = y.WatershedID;
                    x.ModelBasinID = y.ModelBasinID;
                    x.PrecipitationZoneID = y.PrecipitationZoneID;
                    x.RegionalSubbasinID = y.RegionalSubbasinID;
                    x.Notes = y.Notes;
                });

            await DbContext.SaveChangesAsync();

            // merge TreatmentBMPModelingAttributeIDs
            var updatedTreatmentBMPModelingAttributes = updatedTreatmentBMPs.Select(x => x.TreatmentBMPModelingAttributeTreatmentBMP).ToList();
            existingProjectTreatmentBMPModelingAttributes.MergeUpdate(updatedTreatmentBMPModelingAttributes,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID,
                (x, y) =>
                {
                    x.AverageDivertedFlowrate = y.AverageDivertedFlowrate;
                    x.AverageTreatmentFlowrate = y.AverageTreatmentFlowrate;
                    x.DesignDryWeatherTreatmentCapacity = y.DesignDryWeatherTreatmentCapacity;
                    x.DesignLowFlowDiversionCapacity = y.DesignLowFlowDiversionCapacity;
                    x.DesignMediaFiltrationRate = y.DesignMediaFiltrationRate;
                    x.DiversionRate = y.DiversionRate;
                    x.DrawdownTimeForWQDetentionVolume = y.DrawdownTimeForWQDetentionVolume;
                    x.EffectiveFootprint = y.EffectiveFootprint;
                    x.EffectiveRetentionDepth = y.EffectiveRetentionDepth;
                    x.InfiltrationDischargeRate = y.InfiltrationDischargeRate;
                    x.InfiltrationSurfaceArea = y.InfiltrationSurfaceArea;
                    x.MediaBedFootprint = y.MediaBedFootprint;
                    x.PermanentPoolOrWetlandVolume = y.PermanentPoolOrWetlandVolume;
                    x.RoutingConfigurationID = y.RoutingConfigurationID;
                    x.StorageVolumeBelowLowestOutletElevation = y.StorageVolumeBelowLowestOutletElevation;
                    x.SummerHarvestedWaterDemand = y.SummerHarvestedWaterDemand;
                    x.TimeOfConcentrationID = y.TimeOfConcentrationID;
                    x.DrawdownTimeForDetentionVolume = y.DrawdownTimeForDetentionVolume;
                    x.TotalEffectiveBMPVolume = y.TotalEffectiveBMPVolume;
                    x.TotalEffectiveDrywellBMPVolume = y.TotalEffectiveDrywellBMPVolume;
                    x.TreatmentRate = y.TreatmentRate;
                    x.UnderlyingHydrologicSoilGroupID = y.UnderlyingHydrologicSoilGroupID;
                    x.UnderlyingInfiltrationRate = y.UnderlyingInfiltrationRate;
                    x.WaterQualityDetentionVolume = y.WaterQualityDetentionVolume;
                    x.WettedFootprint = y.WettedFootprint;
                    x.WinterHarvestedWaterDemand = y.WinterHarvestedWaterDemand;
                    x.MonthsOfOperationID = y.MonthsOfOperationID;
                    x.DryWeatherFlowOverrideID = y.DryWeatherFlowOverrideID;
                });
            await DbContext.SaveChangesAsync();

            await MergeDeleteTreatmentBMPs(existingProjectTreatmentBMPs, updatedTreatmentBMPs);

            return Ok();
        }

        private async Task MergeDeleteTreatmentBMPs(List<TreatmentBMP> existingProjectTreatmentBMPs, List<TreatmentBMP> updatedTreatmentBMPs)
        {
            var treatmentBMPIDsWhoAreBeingDeleted = existingProjectTreatmentBMPs.Select(x => x.TreatmentBMPID)
                .Where(x => updatedTreatmentBMPs.All(y => x != y.TreatmentBMPID)).ToList();
            // delete all the Delineation related entities
            await DbContext.ProjectHRUCharacteristics.Include(x => x.ProjectLoadGeneratingUnit)
                .ThenInclude(x => x.Delineation).Where(x =>
                    x.ProjectLoadGeneratingUnit.DelineationID.HasValue &&
                    treatmentBMPIDsWhoAreBeingDeleted.Contains(x.ProjectLoadGeneratingUnit.Delineation
                        .TreatmentBMPID))
                .ExecuteDeleteAsync();
            await DbContext.ProjectLoadGeneratingUnits.Include(x => x.Delineation)
                .Where(x => x.DelineationID.HasValue &&
                            treatmentBMPIDsWhoAreBeingDeleted.Contains(x.Delineation.TreatmentBMPID))
                .ExecuteDeleteAsync();
            await DbContext.DelineationOverlaps
                .Include(x => x.Delineation).Include(x => x.OverlappingDelineation).Where(x =>
                    treatmentBMPIDsWhoAreBeingDeleted.Contains(x.Delineation.TreatmentBMPID) ||
                    treatmentBMPIDsWhoAreBeingDeleted.Contains(x.OverlappingDelineation.TreatmentBMPID))
                .ExecuteDeleteAsync();
            await DbContext.Delineations.Where(x => treatmentBMPIDsWhoAreBeingDeleted.Contains(x.TreatmentBMPID))
                .ExecuteDeleteAsync();
            await DbContext.TreatmentBMPModelingAttributes
                .Where(x => treatmentBMPIDsWhoAreBeingDeleted.Contains(x.TreatmentBMPID))
                .ExecuteDeleteAsync();
            await DbContext.TreatmentBMPs.Where(x => treatmentBMPIDsWhoAreBeingDeleted.Contains(x.TreatmentBMPID))
                .ExecuteDeleteAsync();

            var treatmentBMPsWhoseLocationChanged = existingProjectTreatmentBMPs
                .Where(x => updatedTreatmentBMPs.Any(y =>
                    x.TreatmentBMPID == y.TreatmentBMPID && (!x.LocationPoint4326.Equals(y.LocationPoint4326))))
                .Select(x => x.TreatmentBMPID).ToList();

            if (treatmentBMPsWhoseLocationChanged.Any())
            {
                await DbContext.Delineations
                    .Where(x => x.DelineationTypeID == (int)DelineationTypeEnum.Centralized &&
                                treatmentBMPsWhoseLocationChanged.Contains(x.TreatmentBMPID)).ExecuteDeleteAsync();
            }
        }

        [HttpGet("treatmentBMPs/{treatmentBMPID}/upstreamRSBCatchmentGeoJSON")]
        [UserViewFeature]
        public ActionResult<GeometryGeoJSONAndAreaDto> GetUpstreamRSBCatchmentGeoJSONForTreatmentBMP([FromRoute] int treatmentBMPID)
        {
            var treatmentBMP = TreatmentBMPs.GetByTreatmentBMPID(DbContext, treatmentBMPID);
            var delineation = Delineations.GetByTreatmentBMPID(DbContext, treatmentBMPID);
            if (ThrowNotFound(treatmentBMP, "TreatmentBMP", treatmentBMPID, out var actionResult))
            {
                return actionResult;
            }

            var regionalSubbasin = RegionalSubbasins.GetFirstByContainsGeometry(DbContext, treatmentBMP.LocationPoint);

            return Ok(RegionalSubbasins.GetUpstreamCatchmentGeometry4326GeoJSONAndArea(DbContext, regionalSubbasin.RegionalSubbasinID, treatmentBMPID, delineation?.DelineationID));
        }
    }
}