using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Controllers
{
    [ApiController]
    public class FieldDefinitionController(
        NeptuneDbContext dbContext,
        ILogger<FieldDefinitionController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration,
        Person callingUser)
        : SitkaController<FieldDefinitionController>(dbContext, logger, keystoneService, neptuneConfiguration,
            callingUser)
    {
        [HttpGet("/fieldDefinitions")]
        public ActionResult<List<FieldDefinitionDto>> List()
        {
            var fieldDefinitionDtos = FieldDefinitions.List(DbContext);
            return fieldDefinitionDtos;
        }


        [HttpGet("fieldDefinitions/{fieldDefinitionTypeID}")]
        public ActionResult<FieldDefinitionDto> Get([FromRoute] int fieldDefinitionTypeID)
        {
            var fieldDefinitionDto = FieldDefinitions.GetByFieldDefinitionTypeID(DbContext, fieldDefinitionTypeID);
            return RequireNotNullThrowNotFound(fieldDefinitionDto, "FieldDefinition", fieldDefinitionTypeID);
        }

        [HttpPut("fieldDefinitions/{fieldDefinitionTypeID}")]
        [AdminFeature]
        public async Task<ActionResult<FieldDefinitionDto>> Update([FromRoute] int fieldDefinitionTypeID,
            [FromBody] FieldDefinitionDto fieldDefinitionUpdateDto)
        {
            var fieldDefinitionDto = EFModels.Entities.FieldDefinitions.GetByFieldDefinitionTypeID(DbContext, fieldDefinitionTypeID);
            if (ThrowNotFound(fieldDefinitionDto, "FieldDefinition", fieldDefinitionTypeID, out var actionResult))
            {
                return actionResult;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedFieldDefinitionDto = await FieldDefinitions.Update(DbContext, fieldDefinitionTypeID, fieldDefinitionUpdateDto);
            return Ok(updatedFieldDefinitionDto);
        }
    }
}
