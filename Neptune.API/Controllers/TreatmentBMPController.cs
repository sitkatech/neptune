using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Common;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using NetTopologySuite.Features;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neptune.API.Controllers
{
    [ApiController]
    [Route("treatment-bmps")]
    public class TreatmentBMPController(
        NeptuneDbContext dbContext,
        ILogger<TreatmentBMPController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration)
        : SitkaController<TreatmentBMPController>(dbContext, logger, keystoneService, neptuneConfiguration)
    {
        [HttpGet]
        [LoggedInUnclassifiedFeature]
        public async Task<ActionResult<List<TreatmentBMPGridDto>>> List()
        {
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonIDForBMPs(dbContext, CallingUser.PersonID);
            var entities = await dbContext.vTreatmentBMPDetaileds
                .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID))
                .ToListAsync();
            var treatmentBMPGridDtos = entities.Select(x => x.AsGridDto())
                .ToList();
            return Ok(treatmentBMPGridDtos);
        }

        [HttpGet("verified/feature-collection")]
        public ActionResult<FeatureCollection> ListInventoryVerifiedTreatmentBMPsAsFeatureCollection()
        {
            var featureCollection = TreatmentBMPs.ListInventoryIsVerifiedByPersonAsFeatureCollection(DbContext, CallingUser);
            return Ok(featureCollection);
        }

        [HttpGet("jurisdictions/{jurisdictionID}/verified/feature-collection")]
        public ActionResult<FeatureCollection> ListInventoryVerifiedTreatmentBMPsByJurisdictionIDAsFeatureCollection([FromRoute] int jurisdictionID)
        {
            var featureCollection = TreatmentBMPs.ListInventoryIsVerifiedByPersonAndJurisdictionIDAsFeatureCollection(DbContext, CallingUser, jurisdictionID);
            return Ok(featureCollection);
        }

        [HttpGet("planned-projects")]
        [JurisdictionEditFeature]
        public ActionResult<List<TreatmentBMPDisplayDto>> ListTreatmentBMPsWithProjectIDAsFeatureCollection()
        {
            var featureCollection = TreatmentBMPs.ListWithProjectByPerson(DbContext, CallingUser);
            return Ok(featureCollection);
        }

        [HttpGet("octa-m2-tier2-grant-program")]
        [JurisdictionEditFeature]
        public ActionResult<List<TreatmentBMPDisplayDto>> ListOCTAM2Tier2GrantProgramTreatmentBMPs()
        {
            var featureCollection = TreatmentBMPs.ListWithOCTAM2Tier2GrantProgramByPerson(DbContext, CallingUser);
            return Ok(featureCollection);
        }

        [HttpPut("{treatmentBMPID}/treatment-bmp-types/{treatmentBMPTypeID}")]
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

        [HttpGet("{treatmentBMPID}/upstreamRSBCatchmentGeoJSON")]
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

        [HttpGet("{treatmentBMPID}")]
        [EntityNotFound(typeof(TreatmentBMP), "treatmentBMPID")]
        [UserViewFeature]
        public ActionResult<TreatmentBMPDto> GetByID([FromRoute] int treatmentBMPID)
        {
            var treatmentBMP = dbContext.TreatmentBMPs
                .Include(x => x.TreatmentBMPType)
                .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
                .Include(x => x.OwnerOrganization)
                .Include(x => x.WaterQualityManagementPlan)
                .Include(x => x.Delineation)
                .AsNoTracking()
                .Single(x => x.TreatmentBMPID == treatmentBMPID);
            var dto = treatmentBMP.AsDto();
            return Ok(dto);
        }

        [HttpDelete("{treatmentBMPID}")]
        [JurisdictionEditFeature]
        public async Task<IActionResult> Delete([FromRoute] int treatmentBMPID)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDWithChangeTracking(dbContext, treatmentBMPID);

            var delineation = Delineations.GetByTreatmentBMPIDWithChangeTracking(dbContext, treatmentBMP.TreatmentBMPID);
            var delineationGeometry = delineation?.DelineationGeometry;
            var isDelineationDistributed = delineation != null && delineation.DelineationTypeID == (int)DelineationTypeEnum.Distributed;

            await EFModels.Nereid.NereidUtilities.MarkDownstreamNodeDirty(treatmentBMP, dbContext);

            // Remove upstream references
            foreach (var downstreamBMP in treatmentBMP.InverseUpstreamBMP)
            {
                downstreamBMP.UpstreamBMPID = null;
            }
            await dbContext.SaveChangesAsync();

            await treatmentBMP.DeleteFull(dbContext);

            // Queue LGU refresh if needed
            if (isDelineationDistributed && delineationGeometry != null)
            {
                await ModelingEngineUtilities.QueueLGURefreshForArea(delineationGeometry, null, dbContext);
            }

            return NoContent();
        }

        [HttpGet("{treatmentBMPID}/hru-characteristics")]
        [EntityNotFound(typeof(TreatmentBMP), "treatmentBMPID")]
        [SitkaAdminFeature]
        public ActionResult<List<HRUCharacteristicDto>> ListHRUCharacteristics([FromRoute] int treatmentBMPID)
        {
            var treatmentBMP = TreatmentBMPs.GetByID(dbContext, treatmentBMPID);
            var treatmentBMPTree = dbContext.vTreatmentBMPUpstreams.AsNoTracking()
                .Single(x => x.TreatmentBMPID == treatmentBMP.TreatmentBMPID);
            var upstreamestBMP = treatmentBMPTree.UpstreamBMPID.HasValue ? TreatmentBMPs.GetByID(dbContext, treatmentBMPTree.UpstreamBMPID) : null;
            var delineation = Delineations.GetByTreatmentBMPID(dbContext, upstreamestBMP?.TreatmentBMPID ?? treatmentBMP.TreatmentBMPID);
            var hruCharacteristics = vHRUCharacteristics.ListByTreatmentBMPAsDto(dbContext, upstreamestBMP ?? treatmentBMP, delineation);
            return Ok(hruCharacteristics);
        }

        [HttpGet("{treatmentBMPID}/custom-attributes")]
        [EntityNotFound(typeof(TreatmentBMP), "treatmentBMPID")]
        [SitkaAdminFeature]
        public ActionResult<List<CustomAttributeDto>> ListCustomAttributes([FromRoute] int treatmentBMPID)
        {
            var customAttributes = CustomAttributes.ListByTreatmentBMPIDAsDto(dbContext, treatmentBMPID);
            return Ok(customAttributes);
        }

        [HttpGet("{treatmentBMPID}/field-visits")]
        [EntityNotFound(typeof(TreatmentBMP), "treatmentBMPID")]
        [TreatmentBMPViewFeature]
        public ActionResult<List<FieldVisitDto>> FieldVisitGridJsonData([FromRoute] int treatmentBMPID)
        {
            var fieldVisits = vFieldVisitDetaileds.ListAsDtoByTreatmentBMPID(dbContext, treatmentBMPID);
            return Ok(fieldVisits);
        }

    }
}