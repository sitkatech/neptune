using System.Collections.Generic;
using Neptune.Models.DataTransferObjects;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetTopologySuite.Features;

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
        [JurisdictionEditFeature]
        public ActionResult<List<TreatmentBMPDisplayDto>> ListByPersonID()
        {
            var treatmentBMPDisplayDtos = TreatmentBMPs.ListByPersonAsDisplayDto(DbContext, CallingUser);
            return Ok(treatmentBMPDisplayDtos);
        }

        [HttpGet("verified/feature-collection")]
        public ActionResult<FeatureCollection> ListInventoryVerifiedTreatmentBMPsAsFeatureCollection()
        {
            var featureCollection = TreatmentBMPs.ListInventoryIsVerifiedByPersonAsFeatureCollection(DbContext, CallingUser);
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

        [HttpGet("modeling-attribute-dropdown-items")]
        [UserViewFeature]
        public ActionResult<List<TreatmentBMPModelingAttributeDropdownItemDto>> GetModelingAttributeDropdownItems()
        {
            var treatmentBMPModelingAttributeDropdownItemDtos = TreatmentBMPs.GetModelingAttributeDropdownItemsAsDto(DbContext);
            return Ok(treatmentBMPModelingAttributeDropdownItemDtos);
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
    }
}