using System.Collections.Generic;
using System.Linq;
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
    public class CustomPageController : SitkaController<CustomPageController>
    {
        public CustomPageController(HippocampDbContext dbContext, ILogger<CustomPageController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }

        [HttpGet("customPages")]
        public ActionResult<IEnumerable<CustomPageDto>> GetAllCustomPages()
        {
            var customPagesDto = CustomPage.List(_dbContext);
            return Ok(customPagesDto);
        }

        [HttpGet("customPages/withRoles")]
        public ActionResult<List<CustomPageWithRolesDto>> GetAllCustomPagesWithRoles()
        {
            var customPagesWithRolesDto = CustomPage.ListWithRoles(_dbContext);
            return Ok(customPagesWithRolesDto);
        }

        [HttpGet("customPages/getByID/{customPageID}")]
        public ActionResult<CustomPageDto> GetCustomPageByID([FromRoute] int customPageID)
        {
            var customPageDto = CustomPage.GetByCustomPageID(_dbContext, customPageID);
            return RequireNotNullThrowNotFound(customPageDto, "CustomPage", customPageID);
        }

        [HttpGet("customPages/getByURL/{customPageVanityURL}")]
        public ActionResult<CustomPageDto> GetCustomPageByVanityURL([FromRoute] string customPageVanityURL)
        {
            var customPageDto = CustomPage.GetByCustomPageVanityURL(_dbContext, customPageVanityURL);
            return RequireNotNullThrowNotFound(customPageDto, "CustomPage", customPageVanityURL);
        }

        [HttpGet("customPages/roles")]
        public ActionResult<List<CustomPageRoleSimpleDto>> GetAllCustomPageRoles()
        {
            var customPageRoleSimpleDtos = CustomPageRole.List(_dbContext);
            return Ok(customPageRoleSimpleDtos);
        }

        [HttpGet("customPages/getByURL/{customPageVanityURL}/roles")]
        public ActionResult<List<CustomPageRoleSimpleDto>> GetCustomPageRolesByVanityURL([FromRoute] string customPageVanityURL)
        {
            var customPageRoleSimpleDtos = CustomPageRole.GetByCustomPageVanityURL(_dbContext, customPageVanityURL);
            return Ok(customPageRoleSimpleDtos);
        }

        [HttpGet("customPages/getByID/{customPageID}/roles")]
        public ActionResult<List<CustomPageRoleSimpleDto>> GetCustomPageRolesByID([FromRoute] int customPageID)
        {
            var customPageRoleSimpleDtos = CustomPageRole.GetByCustomPageID(_dbContext, customPageID);
            return Ok(customPageRoleSimpleDtos);
        }

        [HttpPut("customPages/{customPageID}")]
        [AdminFeature]
        public ActionResult<CustomPageDto> UpdateCustomPage([FromRoute] int customPageID, [FromBody] CustomPageUpsertDto customPageUpsertDto)
        {
            var customPage = _dbContext.CustomPages.SingleOrDefault(x => x.CustomPageID == customPageID);

            if (ThrowNotFound(customPage, "CustomPage", customPageID, out var actionResult))
            {
                return actionResult;
            }

            // trim space before checking for uniqueness
            customPageUpsertDto.CustomPageDisplayName = customPageUpsertDto.CustomPageDisplayName.Trim();
            RunExtraCustomPageValidation(customPageUpsertDto.CustomPageDisplayName, customPageUpsertDto.CustomPageVanityUrl, customPage.CustomPageID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedCustomPageDto = CustomPage.UpdateCustomPage(_dbContext, customPage, customPageUpsertDto);

            return Ok(updatedCustomPageDto);
        }

        [HttpPost("customPages")]
        [AdminFeature]
        public ActionResult<CustomPageDto> CreateCustomPage([FromBody] CustomPageUpsertDto customPageUpsertDto)
        {
            // trim space before checking for uniqueness
            customPageUpsertDto.CustomPageDisplayName = customPageUpsertDto.CustomPageDisplayName.Trim();
            RunExtraCustomPageValidation(customPageUpsertDto.CustomPageDisplayName, customPageUpsertDto.CustomPageVanityUrl, null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customPage = CustomPage.CreateNewCustomPage(_dbContext, customPageUpsertDto);

            return Ok(customPage);
        }

        [HttpDelete("customPages/{customPageID}")]
        [AdminFeature]
        public ActionResult DeleteCustomPageByID([FromRoute] int customPageID)
        {
            var customPageDto = CustomPage.GetByCustomPageID(_dbContext, customPageID);

            if (ThrowNotFound(customPageDto, "CustomPage", customPageID, out var actionResult))
            {
                return actionResult;
            }

            CustomPage.DeleteByCustomPageID(_dbContext, customPageID);

            return Ok();
        }

        private void RunExtraCustomPageValidation(string customPageDisplayName, string customPageVanityUrl, int? currentID)
        {
            if (CustomPage.IsDisplayNameUnique(_dbContext, customPageDisplayName, currentID))
            {
                ModelState.AddModelError("CustomPageDisplayName", "Display name must be unique");
            }

            if (CustomPage.IsVanityUrlUnique(_dbContext, customPageVanityUrl, currentID))
            {
                ModelState.AddModelError("CustomPageVanityUrl", "Vanity URL must be unique");
            }
        }
    }
}