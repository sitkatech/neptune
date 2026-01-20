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
[Route("treatment-bmp-type-custom-attribute-types")]
public class TreatmentBMPTypeCustomAttributeTypeController(
    NeptuneDbContext dbContext,
    ILogger<TreatmentBMPTypeCustomAttributeTypeController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<TreatmentBMPTypeCustomAttributeTypeController>(dbContext, logger, keystoneService, neptuneConfiguration)
{
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<TreatmentBMPTypeCustomAttributeTypeDto>> List()
    {
        var treatmentBMPTypeCustomAttributeTypeDtos = TreatmentBMPTypeCustomAttributeTypes.ListAsDto(DbContext);
        return treatmentBMPTypeCustomAttributeTypeDtos;
    }


    [HttpGet("{treatmentBMPTypeCustomAttributeTypeID}")]
    [AllowAnonymous]
    public ActionResult<TreatmentBMPTypeCustomAttributeTypeDto> Get([FromRoute] int treatmentBMPTypeCustomAttributeTypeID)
    {
        var treatmentBMPTypeCustomAttributeTypeDto = TreatmentBMPTypeCustomAttributeTypes.GetByIDAsDto(DbContext, treatmentBMPTypeCustomAttributeTypeID);
        return RequireNotNullThrowNotFound(treatmentBMPTypeCustomAttributeTypeDto, "TreatmentBMPTypeCustomAttributeType", treatmentBMPTypeCustomAttributeTypeID);
    }

    [HttpGet("purpose/{customAttributeTypePurposeID}")]
    [AllowAnonymous]
    public ActionResult<List<TreatmentBMPTypeCustomAttributeTypeDto>> GetTreatmentBMPTypeCustomAttributeTypeByCustomAttributePurposeID(
        [FromRoute] int customAttributeTypePurposeID)
    {
        var treatmentBMPTypeCustomAttributeTypeDtos =
            TreatmentBMPTypeCustomAttributeTypes.GetByCustomAttributeTypePurposeAsDto(DbContext, customAttributeTypePurposeID);

        return treatmentBMPTypeCustomAttributeTypeDtos;
    }
}