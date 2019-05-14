//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PermitType]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static PermitType GetPermitType(this IQueryable<PermitType> permitTypes, int permitTypeID)
        {
            var permitType = permitTypes.SingleOrDefault(x => x.PermitTypeID == permitTypeID);
            Check.RequireNotNullThrowNotFound(permitType, "PermitType", permitTypeID);
            return permitType;
        }

        // Delete using an IDList (Firma style)
        public static void DeletePermitType(this IQueryable<PermitType> permitTypes, List<int> permitTypeIDList)
        {
            if(permitTypeIDList.Any())
            {
                permitTypes.Where(x => permitTypeIDList.Contains(x.PermitTypeID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeletePermitType(this IQueryable<PermitType> permitTypes, ICollection<PermitType> permitTypesToDelete)
        {
            if(permitTypesToDelete.Any())
            {
                var permitTypeIDList = permitTypesToDelete.Select(x => x.PermitTypeID).ToList();
                permitTypes.Where(x => permitTypeIDList.Contains(x.PermitTypeID)).Delete();
            }
        }

        public static void DeletePermitType(this IQueryable<PermitType> permitTypes, int permitTypeID)
        {
            DeletePermitType(permitTypes, new List<int> { permitTypeID });
        }

        public static void DeletePermitType(this IQueryable<PermitType> permitTypes, PermitType permitTypeToDelete)
        {
            DeletePermitType(permitTypes, new List<PermitType> { permitTypeToDelete });
        }
    }
}