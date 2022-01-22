using System.Collections.Generic;
using Hippocamp.API.Services;
using Hippocamp.EFModels.Entities;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hippocamp.API.Controllers
{
    public class OrganizationController : SitkaController<OrganizationController>
    {
        public OrganizationController(HippocampDbContext dbContext, ILogger<OrganizationController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }

        [HttpGet("organizations")]
        public ActionResult<List<OrganizationSimpleDto>> GetAllOrganizations()
        {
            var organizationSimpleDtos = Organizations.ListAsSimpleDtos(_dbContext);
            return Ok(organizationSimpleDtos);
        }
    }
}