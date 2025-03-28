﻿using System.Collections.Generic;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Neptune.API.Controllers
{
    [ApiController]
    [Route("organizations")]
    public class OrganizationController(
        NeptuneDbContext dbContext,
        ILogger<OrganizationController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration)
        : SitkaController<OrganizationController>(dbContext, logger, keystoneService, neptuneConfiguration)
    {
        [HttpGet]
        public ActionResult<List<OrganizationSimpleDto>> List()
        {
            var organizationSimpleDtos = Organizations.ListAsSimpleDtos(DbContext);
            return Ok(organizationSimpleDtos);
        }
    }
}