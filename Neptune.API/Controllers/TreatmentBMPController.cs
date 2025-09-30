using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using NetTopologySuite.Features;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neptune.API.Common;

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
        public ActionResult<List<TreatmentBMPIndexGridDto>> List()
        {
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonIDForBMPs(dbContext, CallingUser.PersonID);
            var treatmentBMPs = dbContext.vTreatmentBMPDetaileds
                .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID))
                .ToList()
                .Select(x => x.AsDto())
                .ToList();
            return Ok(treatmentBMPs);
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
    }
}