//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeType]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteCustomAttributeType(this List<int> customAttributeTypeIDList)
        {
            if(customAttributeTypeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllCustomAttributeTypes.RemoveRange(HttpRequestStorage.DatabaseEntities.CustomAttributeTypes.Where(x => customAttributeTypeIDList.Contains(x.CustomAttributeTypeID)));
            }
        }

        public static void DeleteCustomAttributeType(this ICollection<CustomAttributeType> customAttributeTypesToDelete)
        {
            if(customAttributeTypesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllCustomAttributeTypes.RemoveRange(customAttributeTypesToDelete);
            }
        }

        public static void DeleteCustomAttributeType(this int customAttributeTypeID)
        {
            DeleteCustomAttributeType(new List<int> { customAttributeTypeID });
        }

        public static void DeleteCustomAttributeType(this CustomAttributeType customAttributeTypeToDelete)
        {
            DeleteCustomAttributeType(new List<CustomAttributeType> { customAttributeTypeToDelete });
        }
    }
}