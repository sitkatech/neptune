using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Hippocamp.API.Services;
using Hippocamp.API.Services.Authorization;
using Hippocamp.EFModels.Entities;
using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.API.Controllers
{
    [ApiController]
    public class FieldDefinitionController : SitkaController<FieldDefinitionController>
    {
        public FieldDefinitionController(HippocampDbContext dbContext, ILogger<FieldDefinitionController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }

        [HttpGet("/fieldDefinitions")]
        public ActionResult<List<FieldDefinitionDto>> ListAllFieldDefinitions()
        {
            var fieldDefinitionDtos = FieldDefinition.List(_dbContext);
            return fieldDefinitionDtos;
        }


        [HttpGet("fieldDefinitions/{fieldDefinitionTypeID}")]
        public ActionResult<FieldDefinitionDto> GetFieldDefinition([FromRoute] int fieldDefinitionTypeID)
        {
            var fieldDefinitionDto = FieldDefinition.GetByFieldDefinitionTypeID(_dbContext, fieldDefinitionTypeID);
            return RequireNotNullThrowNotFound(fieldDefinitionDto, "FieldDefinition", fieldDefinitionTypeID);
        }

        [HttpPut("fieldDefinitions/{fieldDefinitionTypeID}")]
        [AdminFeature]
        public ActionResult<FieldDefinitionDto> UpdateFieldDefinition([FromRoute] int fieldDefinitionTypeID,
            [FromBody] FieldDefinitionDto fieldDefinitionUpdateDto)
        {
            var fieldDefinitionDto = FieldDefinition.GetByFieldDefinitionTypeID(_dbContext, fieldDefinitionTypeID);
            if (ThrowNotFound(fieldDefinitionDto, "FieldDefinition", fieldDefinitionTypeID, out var actionResult))
            {
                return actionResult;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedFieldDefinitionDto =
                FieldDefinition.UpdateFieldDefinition(_dbContext, fieldDefinitionTypeID, fieldDefinitionUpdateDto);
            return Ok(updatedFieldDefinitionDto);
        }
    }
}
