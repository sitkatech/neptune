
using System.Collections.Generic;
using System.Linq;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public partial class MenuItem
    {
        public static IEnumerable<MenuItemDto> List(HippocampDbContext dbContext)
        {
            IEnumerable<MenuItemDto> menuItems = dbContext.MenuItems
                .AsNoTracking()
                .Select(x => x.AsDto());

            return menuItems;
        }

    }
}
