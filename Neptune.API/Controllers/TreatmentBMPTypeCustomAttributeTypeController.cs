using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

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
    public ActionResult<List<TreatmentBMPTypeCustomAttributeTypeDto>> List()
    {
        var TreatmentBMPTypeCustomAttributeTypeDtos = TreatmentBMPTypeCustomAttributeTypes.ListAsDto(DbContext);
        return TreatmentBMPTypeCustomAttributeTypeDtos;
    }


    [HttpGet("{treatmentBMPTypeCustomAttributeTypeID}")]
    public ActionResult<TreatmentBMPTypeCustomAttributeTypeDto> Get([FromRoute] int treatmentBMPTypeCustomAttributeTypeID)
    {
        var TreatmentBMPTypeCustomAttributeTypeDto = TreatmentBMPTypeCustomAttributeTypes.GetByIDAsDto(DbContext, treatmentBMPTypeCustomAttributeTypeID);
        return RequireNotNullThrowNotFound(TreatmentBMPTypeCustomAttributeTypeDto, "TreatmentBMPTypeCustomAttributeType", treatmentBMPTypeCustomAttributeTypeID);
    }

    [HttpGet("purpose/{customAttributeTypePurposeID}")]
    public ActionResult<List<TreatmentBMPTypeCustomAttributeTypeDto>> GetTreatmentBMPTypeCustomAttributeTypeByCustomAttributePurposeID(
        [FromRoute] int customAttributeTypePurposeID)
    {
        var TreatmentBMPTypeCustomAttributeTypeDtos =
            TreatmentBMPTypeCustomAttributeTypes.GetByCustomAttributeTypePurposeAsDto(DbContext, customAttributeTypePurposeID);

        return TreatmentBMPTypeCustomAttributeTypeDtos;
    }
}