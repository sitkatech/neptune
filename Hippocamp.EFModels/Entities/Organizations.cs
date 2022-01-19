using System.Linq;
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
    }
}