using System.Collections.Generic;
using System.Linq;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public partial class CustomPageRole
    {
        public static List<CustomPageRoleSimpleDto> List(HippocampDbContext dbContext)
        {
            return dbContext.CustomPageRoles
                .Include(x => x.CustomPage)
                .Include(x => x.Role)
                .AsNoTracking()
                .Select(x => x.AsSimpleDto()).ToList();
        }
        public static List<CustomPageRoleSimpleDto> GetByCustomPageVanityURL(HippocampDbContext dbContext, string customPageVanityUrl)
        {
            return dbContext.CustomPageRoles
                .Include(x => x.CustomPage)
                .Include(x => x.Role)
                .AsNoTracking()
                .Where(x => x.CustomPage.CustomPageVanityUrl == customPageVanityUrl)
                .Select(x => x.AsSimpleDto()).ToList();
        }

        public static List<CustomPageRoleSimpleDto> GetByCustomPageID(HippocampDbContext dbContext, int customPageID)
        {
            return dbContext.CustomPageRoles
                .Include(x => x.CustomPage)
                .Include(x => x.Role)
                .AsNoTracking()
                .Where(x => x.CustomPage.CustomPageID == customPageID)
                .Select(x => x.AsSimpleDto()).ToList();
        }

    }
}