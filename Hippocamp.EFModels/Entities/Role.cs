using System.Collections.Generic;
using System.Linq;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public partial class Role
    {
        public static IEnumerable<RoleDto> List(HippocampDbContext dbContext)
        {
            var roles = dbContext.Roles
                .AsNoTracking()
                .Select(x => x.AsDto());

            return roles;
        }

        public static RoleDto GetByRoleID(HippocampDbContext dbContext, int roleID)
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
        LandOwner = 2,
        Unassigned = 3,
        Disabled = 4
    }
}
