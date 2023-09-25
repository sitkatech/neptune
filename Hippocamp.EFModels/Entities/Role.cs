using System.Collections.Generic;
using System.Linq;
using Neptune.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public partial class Role
    {
        public static IEnumerable<RoleDto> List(NeptuneDbContext dbContext)
        {
            var roles = dbContext.Roles
                .AsNoTracking()
                .Select(x => x.AsDto());

            return roles;
        }

        public static RoleDto GetByRoleID(NeptuneDbContext dbContext, int roleID)
        {
            var role = dbContext.Roles
                .AsNoTracking()
                .FirstOrDefault(x => x.RoleID == roleID);

            return role?.AsDto();
        }
    }

    public enum RoleEnum
    {
        Admin = 1,
        Unassigned = 3,
        SitkaAdmin = 4,
        JurisdictionManager = 5,
        JurisdictionEditor = 6,
    }
}
