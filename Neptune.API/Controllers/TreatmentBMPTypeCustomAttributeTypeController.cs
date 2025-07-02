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


    [HttpGet("{TreatmentBMPTypeCustomAttributeTypeID}")]
    public ActionResult<TreatmentBMPTypeCustomAttributeTypeDto> Get([FromRoute] int TreatmentBMPTypeCustomAttributeTypeID)
    {
        var TreatmentBMPTypeCustomAttributeTypeDto = TreatmentBMPTypeCustomAttributeTypes.GetByIDAsDto(DbContext, TreatmentBMPTypeCustomAttributeTypeID);
        return RequireNotNullThrowNotFound(TreatmentBMPTypeCustomAttributeTypeDto, "TreatmentBMPTypeCustomAttributeType", TreatmentBMPTypeCustomAttributeTypeID);
    }

    [HttpGet("purpose/{TreatmentBMPTypeCustomAttributeTypePurposeID}")]
    public ActionResult<List<TreatmentBMPTypeCustomAttributeTypeDto>> GetByTreatmentBMPTypeCustomAttributeTypePurposeID(
        [FromRoute] int TreatmentBMPTypeCustomAttributeTypePurposeID)
    {
        var TreatmentBMPTypeCustomAttributeTypeDtos =
            TreatmentBMPTypeCustomAttributeTypes.GetByCustomAttributeTypePurposeAsDto(DbContext, TreatmentBMPTypeCustomAttributeTypePurposeID);

        return TreatmentBMPTypeCustomAttributeTypeDtos;
    }
}