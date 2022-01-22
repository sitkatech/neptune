using System.Collections.Generic;
using System.Linq;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public static class Organizations
    {
        public const int OrganizationIDUnassigned = 2;

        public static Organization GetByName(HippocampDbContext dbContext, string organizationName)
        {
            return dbContext.Organizations
                .AsNoTracking().SingleOrDefault(x => x.OrganizationName == organizationName);
        }

        public static List<OrganizationSimpleDto> ListAsSimpleDtos(HippocampDbContext dbContext)
        {
            var organizations = dbContext.Organizations
                .AsNoTracking()
                .Select(x => x.AsSimpleDto())
                .ToList();
            return organizations;
        }
    }
}