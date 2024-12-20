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

namespace Neptune.API.Controllers
{
    [ApiController]
    public class TreatmentBMPController : SitkaController<TreatmentBMPController>
    {
        public TreatmentBMPController(NeptuneDbContext dbContext, ILogger<TreatmentBMPController> logger, KeystoneService keystoneService, IOptions<NeptuneConfiguration> neptuneConfiguration) : base(dbContext, logger, keystoneService, neptuneConfiguration)
        {
        }

        [HttpGet("treatmentBMPs/{projectID}/getByProjectID")]
        [UserViewFeature]
        public ActionResult<List<TreatmentBMPUpsertDto>> GetByProjectID([FromRoute] int projectID)
        {
            var treatmentBMPUpsertDtos = TreatmentBMPs.ListByProjectIDAsUpsertDto(_dbContext, projectID);
            return Ok(treatmentBMPUpsertDtos);
        }

        [HttpGet("treatmentBMPs")]
        [JurisdictionEditFeature]
        public ActionResult<List<TreatmentBMPDisplayDto>> ListByPersonID()
        {
            var person = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var treatmentBMPDisplayDtos = TreatmentBMPs.ListByPersonIDAsDisplayDto(_dbContext, person);
            return Ok(treatmentBMPDisplayDtos);
        }

        [HttpGet("treatmentBMPTypes")]
        [UserViewFeature]
        public ActionResult<List<TreatmentBMPTypeWithModelingAttributesDto>> ListTypes()
        {
            var treatmentBMPTypeWithModelingAttributesDtos = TreatmentBMPs.ListWithModelingAttributesAsDto(_dbContext);
            return Ok(treatmentBMPTypeWithModelingAttributesDtos);
        }

        [HttpPut("treatmentBMPs/{treatmentBMPID}/treatmentBMPType/{treatmentBMPTypeID}")]
        [UserViewFeature]
        public ActionResult<int> ChangeTreatmentBMPType([FromRoute] int treatmentBMPID, int treatmentBMPTypeID)
        {
            var updatedTreatmentBMPModelingTypeID = TreatmentBMPs.ChangeTreatmentBMPType(_dbContext, treatmentBMPID, treatmentBMPTypeID);
            var personID = UserContext.GetUserFromHttpContext(_dbContext, HttpContext).PersonID;
            var projectID = TreatmentBMPs.GetByTreatmentBMPID(_dbContext, treatmentBMPID).ProjectID;
            if (projectID != null)
            {
                Projects.SetUpdatePersonAndDate(_dbContext, (int)projectID, personID);
            }
            
            return Ok(updatedTreatmentBMPModelingTypeID);
        }

        [HttpGet("treatmentBMPModelingAttributeDropdownItems")]
        [UserViewFeature]
        public ActionResult<List<TreatmentBMPModelingAttributeDropdownItemDto>> GetModelingAttributeDropdownItems()
        {
            var treatmentBMPModelingAttributeDropdownItemDtos = TreatmentBMPs.GetModelingAttributeDropdownItemsAsDto(_dbContext);
            return Ok(treatmentBMPModelingAttributeDropdownItemDtos);
        }

        [HttpPut("treatmentBMPs/{projectID}/updateLocations")]
        [JurisdictionEditFeature]
        public async Task<ActionResult> MergeTreatmentBMPLocations([FromRoute] int projectID, List<TreatmentBMPDisplayDto> treatmentBMPDisplayDtos)
        {
            var project = _dbContext.Projects.SingleOrDefault(x => x.ProjectID == projectID);
            if (project == null)
            {
                return BadRequest();
            }

            var existingProjectTreatmentBMPs = _dbContext.TreatmentBMPs.Where(x => x.ProjectID == project.ProjectID).ToList();
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
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("treatmentBMPs/{projectID}")]
        [JurisdictionEditFeature]
        public async Task<ActionResult> MergeTreatmentBMPs([FromRoute] int projectID, List<TreatmentBMPUpsertDto> treatmentBMPUpsertDtos)
        {
            var project = _dbContext.Projects.SingleOrDefault(x => x.ProjectID == projectID);
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
                var existingTreatmentBMPs = _dbContext.TreatmentBMPs.Where(x => x.ProjectID == projectID).AsNoTracking().ToList();
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

            await Projects.DeleteProjectNereidResultsAndGrantScores(_dbContext, projectID);
            var existingProjectTreatmentBMPs = _dbContext.TreatmentBMPs.Include(x => x.TreatmentBMPModelingAttributeTreatmentBMP).Where(x => x.ProjectID == project.ProjectID).ToList();
            var existingProjectTreatmentBMPModelingAttributes = existingProjectTreatmentBMPs.Where(x => x.TreatmentBMPModelingAttributeTreatmentBMP != null).Select(x => x.TreatmentBMPModelingAttributeTreatmentBMP).ToList();

            var updatedTreatmentBMPs = treatmentBMPUpsertDtos.Select(x => TreatmentBMPs.TreatmentBMPFromUpsertDtoAndProject(_dbContext, x, project)).ToList();

            await _dbContext.TreatmentBMPs.AddRangeAsync(updatedTreatmentBMPs.Where(x => x.TreatmentBMPID == 0));
            await _dbContext.SaveChangesAsync();

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

            await _dbContext.SaveChangesAsync();

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
            await _dbContext.SaveChangesAsync();

            await MergeDeleteTreatmentBMPs(existingProjectTreatmentBMPs, updatedTreatmentBMPs);

            return Ok();
        }

        private async Task MergeDeleteTreatmentBMPs(List<TreatmentBMP> existingProjectTreatmentBMPs, List<TreatmentBMP> updatedTreatmentBMPs)
        {
            var treatmentBMPIDsWhoAreBeingDeleted = existingProjectTreatmentBMPs.Select(x => x.TreatmentBMPID)
                .Where(x => updatedTreatmentBMPs.All(y => x != y.TreatmentBMPID)).ToList();
            // delete all the Delineation related entities
            await _dbContext.ProjectHRUCharacteristics.Include(x => x.ProjectLoadGeneratingUnit)
                .ThenInclude(x => x.Delineation).Where(x =>
                    x.ProjectLoadGeneratingUnit.DelineationID.HasValue &&
                    treatmentBMPIDsWhoAreBeingDeleted.Contains(x.ProjectLoadGeneratingUnit.Delineation
                        .TreatmentBMPID))
                .ExecuteDeleteAsync();
            await _dbContext.ProjectLoadGeneratingUnits.Include(x => x.Delineation)
                .Where(x => x.DelineationID.HasValue &&
                            treatmentBMPIDsWhoAreBeingDeleted.Contains(x.Delineation.TreatmentBMPID))
                .ExecuteDeleteAsync();
            await _dbContext.DelineationOverlaps
                .Include(x => x.Delineation).Include(x => x.OverlappingDelineation).Where(x =>
                    treatmentBMPIDsWhoAreBeingDeleted.Contains(x.Delineation.TreatmentBMPID) ||
                    treatmentBMPIDsWhoAreBeingDeleted.Contains(x.OverlappingDelineation.TreatmentBMPID))
                .ExecuteDeleteAsync();
            await _dbContext.Delineations.Where(x => treatmentBMPIDsWhoAreBeingDeleted.Contains(x.TreatmentBMPID))
                .ExecuteDeleteAsync();
            await _dbContext.TreatmentBMPModelingAttributes
                .Where(x => treatmentBMPIDsWhoAreBeingDeleted.Contains(x.TreatmentBMPID))
                .ExecuteDeleteAsync();
            await _dbContext.TreatmentBMPs.Where(x => treatmentBMPIDsWhoAreBeingDeleted.Contains(x.TreatmentBMPID))
                .ExecuteDeleteAsync();

            var treatmentBMPsWhoseLocationChanged = existingProjectTreatmentBMPs
                .Where(x => updatedTreatmentBMPs.Any(y =>
                    x.TreatmentBMPID == y.TreatmentBMPID && (!x.LocationPoint4326.Equals(y.LocationPoint4326))))
                .Select(x => x.TreatmentBMPID).ToList();

            if (treatmentBMPsWhoseLocationChanged.Any())
            {
                await _dbContext.Delineations
                    .Where(x => x.DelineationTypeID == (int)DelineationTypeEnum.Centralized &&
                                treatmentBMPsWhoseLocationChanged.Contains(x.TreatmentBMPID)).ExecuteDeleteAsync();
            }
        }

        [HttpGet("treatmentBMPs/{treatmentBMPID}/upstreamRSBCatchmentGeoJSON")]
        [UserViewFeature]
        public ActionResult<GeometryGeoJSONAndAreaDto> GetUpstreamRSBCatchmentGeoJSONForTreatmentBMP([FromRoute] int treatmentBMPID)
        {
            var treatmentBMP = TreatmentBMPs.GetByTreatmentBMPID(_dbContext, treatmentBMPID);
            var delineation = Delineations.GetByTreatmentBMPID(_dbContext, treatmentBMPID);
            if (ThrowNotFound(treatmentBMP, "TreatmentBMP", treatmentBMPID, out var actionResult))
            {
                return actionResult;
            }

            var regionalSubbasin = RegionalSubbasins.GetFirstByContainsGeometry(_dbContext, treatmentBMP.LocationPoint);

            return Ok(RegionalSubbasins.GetUpstreamCatchmentGeometry4326GeoJSONAndArea(_dbContext, regionalSubbasin.RegionalSubbasinID, treatmentBMPID, delineation?.DelineationID));
        }
    }
}