using System.Collections.Generic;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Neptune.API.Controllers
{
    public class OrganizationController : SitkaController<OrganizationController>
    {
        public OrganizationController(NeptuneDbContext dbContext, ILogger<OrganizationController> logger, KeystoneService keystoneService, IOptions<NeptuneConfiguration> neptuneConfiguration) : base(dbContext, logger, keystoneService, neptuneConfiguration)
        {
        }

        [HttpGet("organizations")]
        public ActionResult<List<OrganizationSimpleDto>> List()
        {
            var organizationSimpleDtos = Organizations.ListAsSimpleDtos(_dbContext);
            return Ok(organizationSimpleDtos);
        }
    }
}