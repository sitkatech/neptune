using System;
using System.Collections.Generic;
using System.Linq;
using Hippocamp.API.Services;
using Hippocamp.API.Services.Authorization;
using Hippocamp.EFModels.Entities;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjNet.CoordinateSystems;

namespace Hippocamp.API.Controllers
{
    [ApiController]
    public class TreatmentBMPController : SitkaController<TreatmentBMPController>
    {
        public TreatmentBMPController(HippocampDbContext dbContext, ILogger<TreatmentBMPController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }

        [HttpGet("treatmentBMPs/{projectID}/getByProjectID")]
        [UserViewFeature]
        public ActionResult<TreatmentBMPUpsertDto> GetByProjectID([FromRoute] int projectID)
        {
            var treatmentBMPUpsertDtos = TreatmentBMPs.ListByProjectIDAsUpsertDto(_dbContext, projectID);
            return Ok(treatmentBMPUpsertDtos);
        }

        [HttpGet("treatmentBMPs")]
        [JurisdictionEditFeature]
        public ActionResult<TreatmentBMPDisplayDto> ListByPersonID()
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var treatmentBMPDisplayDtos = TreatmentBMPs.ListByPersonIDAsDisplayDto(_dbContext, personDto.PersonID);
            return Ok(treatmentBMPDisplayDtos);
        }

        [HttpGet("treatmentBMPs/types")]
        [UserViewFeature]
        public ActionResult<TreatmentBMPTypeSimpleDto> ListTypes()
        {
            var treatmentBMPTypeSimpleDtos = TreatmentBMPs.ListTypesAsSimpleDto(_dbContext);
            return Ok(treatmentBMPTypeSimpleDtos);
        }

        [HttpPut("treatmentBMPs/{treatmentBMPID}/treatmentBMPType/{treatmentBMPTypeID}")]
        [UserViewFeature]
        public ActionResult<int> ChangeTreatmentBMPType([FromRoute] int treatmentBMPID, int treatmentBMPTypeID, [FromBody] TreatmentBMPUpsertDto treatmentBMP)
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

        [HttpGet("treatmentBMPs/modelingAttributeDropdownItems")]
        [JurisdictionEditFeature]
        public ActionResult<TreatmentBMPModelingAttributeDropdownItemDto> GetModelingAttributeDropdownItems()
        {
            var treatmentBMPModelingAttributeDropdownItemDtos = TreatmentBMPs.GetModelingAttributeDropdownItemsAsDto(_dbContext);
            return Ok(treatmentBMPModelingAttributeDropdownItemDtos);
        }

        [HttpPut("treatmentBMPs/{projectID}")]
        [JurisdictionEditFeature]
        public ActionResult MergeTreatmentBMPs(List<TreatmentBMPUpsertDto> treatmentBMPUpsertDtos, [FromRoute] int projectID)
        {
            var project = _dbContext.Projects.SingleOrDefault(x => x.ProjectID == projectID);
            if (project == null)
            {
                return BadRequest();
            }

            var existingTreatmentBMPs = _dbContext.TreatmentBMPs.ToList();
            foreach (var treatmentBMPUpsertDto in treatmentBMPUpsertDtos)
            {
                var namingConflicts = existingTreatmentBMPs
                    .Where(x => x.TreatmentBMPName == treatmentBMPUpsertDto.TreatmentBMPName && x.TreatmentBMPID != treatmentBMPUpsertDto.TreatmentBMPID)
                    .ToList();

                if (namingConflicts.Count > 0)
                {
                    ModelState.AddModelError("TreatmentBMPName", 
                        $"A Treatment BMP with the name {treatmentBMPUpsertDto.TreatmentBMPName} already exists. Treatment BMP names must be unique.");
                }
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Projects.DeleteProjectNereidResultsAndGrantScores(_dbContext, projectID);
            TreatmentBMPs.MergeProjectTreatmentBMPs(_dbContext, treatmentBMPUpsertDtos, existingTreatmentBMPs, project);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet("treatmentBMPs/{treatmentBMPID}/upstreamRSBCatchmentGeoJSON")]
        [UserViewFeature]
        public ActionResult<GeometryGeoJSONAndAreaDto> GetUpstreamRSBCatchmentGeoJSONForTreatmentBMP([FromRoute] int treatmentBMPID)
        {
            var treatmentBMP = TreatmentBMPs.GetByTreatmentBMPID(_dbContext, treatmentBMPID);
            if (ThrowNotFound(treatmentBMP, "TreatmentBMP", treatmentBMPID, out var actionResult))
            {
                return actionResult;
            }

            var regionalSubbasin = RegionalSubbasins.GetFirstByContainsGeometry(_dbContext, treatmentBMP.LocationPoint);

            return Ok(RegionalSubbasins.GetUpstreamCatchmentGeometry4326GeoJSONAndArea(_dbContext, regionalSubbasin.RegionalSubbasinID));
        }

        [HttpGet("treatmentBMPs/verified")]
        [UserViewFeature]
        public ActionResult<TreatmentBMPDisplayDto> ListVerifiedTreatmentBMPs()
        {
            var treatmentBMPDisplayDtos = TreatmentBMPs.ListVerifiedTreatmentBMPs(_dbContext);
            return Ok(treatmentBMPDisplayDtos);
        }
    }
}