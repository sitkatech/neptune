//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterRole]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static StormwaterRole GetStormwaterRole(this IQueryable<StormwaterRole> stormwaterRoles, int stormwaterRoleID)
        {
            var stormwaterRole = stormwaterRoles.SingleOrDefault(x => x.StormwaterRoleID == stormwaterRoleID);
            Check.RequireNotNullThrowNotFound(stormwaterRole, "StormwaterRole", stormwaterRoleID);
            return stormwaterRole;
        }

        public static void DeleteStormwaterRole(this List<int> stormwaterRoleIDList)
        {
            if(stormwaterRoleIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllStormwaterRoles.RemoveRange(HttpRequestStorage.DatabaseEntities.StormwaterRoles.Where(x => stormwaterRoleIDList.Contains(x.StormwaterRoleID)));
            }
        }

        public static void DeleteStormwaterRole(this ICollection<StormwaterRole> stormwaterRolesToDelete)
        {
            if(stormwaterRolesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllStormwaterRoles.RemoveRange(stormwaterRolesToDelete);
            }
        }

        public static void DeleteStormwaterRole(this int stormwaterRoleID)
        {
            DeleteStormwaterRole(new List<int> { stormwaterRoleID });
        }

        public static void DeleteStormwaterRole(this StormwaterRole stormwaterRoleToDelete)
        {
            DeleteStormwaterRole(new List<StormwaterRole> { stormwaterRoleToDelete });
        }
    }
}