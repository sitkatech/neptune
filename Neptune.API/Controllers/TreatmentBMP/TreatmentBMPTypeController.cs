using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using System.Collections.Generic;

namespace Neptune.API.Controllers;

[ApiController]
[Route("treatment-bmp-types")]
public class TreatmentBMPTypeController(
    NeptuneDbContext dbContext,
    ILogger<TreatmentBMPTypeController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<TreatmentBMPTypeController>(dbContext, logger, keystoneService, neptuneConfiguration)
{
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<TreatmentBMPTypeWithModelingAttributesDto>> List()
    {
        var treatmentBMPTypeWithModelingAttributesDtos = TreatmentBMPs.ListWithModelingAttributesAsDto(DbContext);
        return Ok(treatmentBMPTypeWithModelingAttributesDtos);
    }

    [HttpGet("{treatmentBMPTypeID}/custom-attribute-types")]
    [AllowAnonymous]
    public ActionResult<List<TreatmentBMPTypeCustomAttributeTypeDto>> ListCustomAttributeTypes([FromRoute] int treatmentBMPTypeID)
    {
        var treatmentBMPTypeCustomAttributeTypeDtos = TreatmentBMPTypeCustomAttributeTypes.ListByTreatmentBMPTypeAsDto(DbContext, treatmentBMPTypeID);
        return treatmentBMPTypeCustomAttributeTypeDtos;
    }
}