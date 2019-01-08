//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeType]
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
        public static CustomAttributeType GetCustomAttributeType(this IQueryable<CustomAttributeType> customAttributeTypes, int customAttributeTypeID)
        {
            var customAttributeType = customAttributeTypes.SingleOrDefault(x => x.CustomAttributeTypeID == customAttributeTypeID);
            Check.RequireNotNullThrowNotFound(customAttributeType, "CustomAttributeType", customAttributeTypeID);
            return customAttributeType;
        }

        public static void DeleteCustomAttributeType(this IQueryable<CustomAttributeType> customAttributeTypes, List<int> customAttributeTypeIDList)
        {
            if(customAttributeTypeIDList.Any())
            {
                customAttributeTypes.Where(x => customAttributeTypeIDList.Contains(x.CustomAttributeTypeID)).Delete();
            }
        }

        public static void DeleteCustomAttributeType(this IQueryable<CustomAttributeType> customAttributeTypes, ICollection<CustomAttributeType> customAttributeTypesToDelete)
        {
            if(customAttributeTypesToDelete.Any())
            {
                var customAttributeTypeIDList = customAttributeTypesToDelete.Select(x => x.CustomAttributeTypeID).ToList();
                customAttributeTypes.Where(x => customAttributeTypeIDList.Contains(x.CustomAttributeTypeID)).Delete();
            }
        }

        public static void DeleteCustomAttributeType(this IQueryable<CustomAttributeType> customAttributeTypes, int customAttributeTypeID)
        {
            DeleteCustomAttributeType(customAttributeTypes, new List<int> { customAttributeTypeID });
        }

        public static void DeleteCustomAttributeType(this IQueryable<CustomAttributeType> customAttributeTypes, CustomAttributeType customAttributeTypeToDelete)
        {
            DeleteCustomAttributeType(customAttributeTypes, new List<CustomAttributeType> { customAttributeTypeToDelete });
        }
    }
}