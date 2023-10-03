using System.Collections.Generic;
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
    public class FieldDefinitionController : SitkaController<FieldDefinitionController>
    {
        public FieldDefinitionController(NeptuneDbContext dbContext, ILogger<FieldDefinitionController> logger, KeystoneService keystoneService, IOptions<NeptuneConfiguration> neptuneConfiguration) : base(dbContext, logger, keystoneService, neptuneConfiguration)
        {
        }

        [HttpGet("/fieldDefinitions")]
        public ActionResult<List<FieldDefinitionDto>> ListAllFieldDefinitions()
        {
            var fieldDefinitionDtos = FieldDefinitions.List(_dbContext);
            return fieldDefinitionDtos;
        }


        [HttpGet("fieldDefinitions/{fieldDefinitionTypeID}")]
        public ActionResult<FieldDefinitionDto> GetFieldDefinition([FromRoute] int fieldDefinitionTypeID)
        {
            var fieldDefinitionDto = FieldDefinitions.GetByFieldDefinitionTypeID(_dbContext, fieldDefinitionTypeID);
            return RequireNotNullThrowNotFound(fieldDefinitionDto, "FieldDefinition", fieldDefinitionTypeID);
        }

        [HttpPut("fieldDefinitions/{fieldDefinitionTypeID}")]
        [AdminFeature]
        public ActionResult<FieldDefinitionDto> UpdateFieldDefinition([FromRoute] int fieldDefinitionTypeID,
            [FromBody] FieldDefinitionDto fieldDefinitionUpdateDto)
        {
            var fieldDefinitionDto = FieldDefinitions.GetByFieldDefinitionTypeID(_dbContext, fieldDefinitionTypeID);
            if (ThrowNotFound(fieldDefinitionDto, "FieldDefinition", fieldDefinitionTypeID, out var actionResult))
            {
                return actionResult;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedFieldDefinitionDto =
                FieldDefinitions.UpdateFieldDefinition(_dbContext, fieldDefinitionTypeID, fieldDefinitionUpdateDto);
            return Ok(updatedFieldDefinitionDto);
        }
    }
}
