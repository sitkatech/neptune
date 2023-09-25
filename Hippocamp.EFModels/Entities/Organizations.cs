using System.Collections.Generic;
using System.Linq;
using Neptune.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public static class Organizations
    {
        public const int OrganizationIDUnassigned = 2;

        public static Organization GetByName(NeptuneDbContext dbContext, string organizationName)
        {
            return dbContext.Organizations
                .AsNoTracking().SingleOrDefault(x => x.OrganizationName == organizationName);
        }

        public static List<OrganizationSimpleDto> ListAsSimpleDtos(NeptuneDbContext dbContext)
        {
            var organizations = dbContext.Organizations
                .AsNoTracking()
                .OrderBy(x => x.OrganizationName)
                .Select(x => x.AsSimpleDto())
                .ToList();
            return organizations;
        }
    }
}