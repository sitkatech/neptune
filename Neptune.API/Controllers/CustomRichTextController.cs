﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Controllers
{
    [ApiController]
    public class CustomRichTextController(
        NeptuneDbContext dbContext,
        ILogger<CustomRichTextController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration,
        Person callingUser)
        : SitkaController<CustomRichTextController>(dbContext, logger, keystoneService, neptuneConfiguration,
            callingUser)
    {
        [HttpGet("customRichText/{customRichTextTypeID}")]
        public ActionResult<NeptunePageDto> GetCustomRichText([FromRoute] int customRichTextTypeID)
        {
            var customRichTextDto = NeptunePages.GetByNeptunePageTypeID(DbContext, customRichTextTypeID);
            return RequireNotNullThrowNotFound(customRichTextDto, "CustomRichText", customRichTextTypeID);
        }

        [HttpPut("customRichText/{customRichTextTypeID}")]
        [AdminFeature]
        public ActionResult<NeptunePageDto> UpdateCustomRichText([FromRoute] int customRichTextTypeID, [FromBody] NeptunePageDto customRichTextUpdateDto)
        {
            var customRichTextDto = NeptunePages.GetByNeptunePageTypeID(DbContext, customRichTextTypeID);
            if (ThrowNotFound(customRichTextDto, "CustomRichText", customRichTextTypeID, out var actionResult))
            {
                return actionResult;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedCustomRichTextDto =
                NeptunePages.UpdateNeptunePage(DbContext, customRichTextTypeID, customRichTextUpdateDto);
            return Ok(updatedCustomRichTextDto);
        }
    }
}
