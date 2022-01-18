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
    public class CustomRichTextController : SitkaController<CustomRichTextController>
    {
        public CustomRichTextController(HippocampDbContext dbContext, ILogger<CustomRichTextController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }

        [HttpGet("customRichText/{customRichTextTypeID}")]
        public ActionResult<NeptunePageDto> GetCustomRichText([FromRoute] int customRichTextTypeID)
        {
            var customRichTextDto = NeptunePages.GetByNeptunePageTypeID(_dbContext, customRichTextTypeID);
            return RequireNotNullThrowNotFound(customRichTextDto, "CustomRichText", customRichTextTypeID);
        }

        [HttpPut("customRichText/{customRichTextTypeID}")]
        [AdminFeature]
        public ActionResult<NeptunePageDto> UpdateCustomRichText([FromRoute] int customRichTextTypeID, [FromBody] NeptunePageDto customRichTextUpdateDto)
        {
            var customRichTextDto = NeptunePages.GetByNeptunePageTypeID(_dbContext, customRichTextTypeID);
            if (ThrowNotFound(customRichTextDto, "CustomRichText", customRichTextTypeID, out var actionResult))
            {
                return actionResult;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedCustomRichTextDto =
                NeptunePages.UpdateNeptunePage(_dbContext, customRichTextTypeID, customRichTextUpdateDto);
            return Ok(updatedCustomRichTextDto);
        }
    }
}
