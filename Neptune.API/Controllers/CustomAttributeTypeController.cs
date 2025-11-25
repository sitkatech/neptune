using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using System.Collections.Generic;

namespace Neptune.API.Controllers;

[ApiController]
[Route("custom-attribute-types")]
public class CustomAttributeTypeController(
    NeptuneDbContext dbContext,
    ILogger<CustomAttributeTypeController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<CustomAttributeTypeController>(dbContext, logger, keystoneService, neptuneConfiguration)
{
    [HttpGet]
    public ActionResult<List<CustomAttributeTypeDto>> List()
    {
        var customAttributeTypeDtos = CustomAttributeTypes.ListAsDto(DbContext);
        return customAttributeTypeDtos;
    }


    [HttpGet("{customAttributeTypeID}")]
    public ActionResult<CustomAttributeTypeDto> Get([FromRoute] int customAttributeTypeID)
    {
        var customAttributeTypeDto = CustomAttributeTypes.GetByIDAsDto(DbContext, customAttributeTypeID);
        return RequireNotNullThrowNotFound(customAttributeTypeDto, "CustomAttributeType", customAttributeTypeID);
    }

    [HttpGet("/purpose/{customAttributeTypePurposeID}")]
    public ActionResult<List<CustomAttributeTypeWithTreatmentBMPTypeIDsDto>> GetByCustomAttributeTypePurposeID(
        [FromRoute] int customAttributeTypePurposeID)
    {
        var customAttributeTypeDtos =
            CustomAttributeTypes.GetByCustomAttributeTypePurposeAsWithTreatmentBMPTypeIDsDto(DbContext, customAttributeTypePurposeID);

        return customAttributeTypeDtos;
    }
}