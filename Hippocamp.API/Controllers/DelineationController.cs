using Hippocamp.API.Services;
using Hippocamp.API.Services.Authorization;
using Hippocamp.EFModels.Entities;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Hippocamp.API.Controllers
{
    [ApiController]
    public class DelineationController : SitkaController<DelineationController>
    {
        public DelineationController(HippocampDbContext dbContext, ILogger<DelineationController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }

        [HttpGet("delineations")]
        [JurisdictionEditFeature]
        public ActionResult<DelineationSimpleDto> ListByPersonID()
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var delineationsUpsertDtos = Delineations.ListByPersonIDAsSimpleDto(_dbContext, personDto.PersonID);
            return Ok(delineationsUpsertDtos);
        }

        [HttpGet("delineations/all")]
        [UserViewFeature]
        public ActionResult<DelineationSimpleDto> List()
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            if (!personDto.IsOCTAGrantReviewer)
            {
                return Forbid();
            }
            var delineationsSimpleDtos = Delineations.ListAsSimpleDto(_dbContext);
            return Ok(delineationsSimpleDtos);
        }
    }
}