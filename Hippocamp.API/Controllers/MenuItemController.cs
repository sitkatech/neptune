using System.Collections.Generic;
using Hippocamp.API.Services;
using Hippocamp.EFModels.Entities;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hippocamp.API.Controllers
{
    public class MenuItemController : SitkaController<MenuItemController>
    {
        [HttpGet("menuItems")]
        public ActionResult<IEnumerable<MenuItemDto>> GetMenuItems()
        {
            var menuItemsDto = MenuItem.List(_dbContext);
            return Ok(menuItemsDto);
        }

        public MenuItemController(HippocampDbContext dbContext, ILogger<MenuItemController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }
    }
}