//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttribute]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static CustomAttribute GetCustomAttribute(this IQueryable<CustomAttribute> customAttributes, int customAttributeID)
        {
            var customAttribute = customAttributes.SingleOrDefault(x => x.CustomAttributeID == customAttributeID);
            Check.RequireNotNullThrowNotFound(customAttribute, "CustomAttribute", customAttributeID);
            return customAttribute;
        }

        public static void DeleteCustomAttribute(this List<int> customAttributeIDList)
        {
            if(customAttributeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllCustomAttributes.RemoveRange(HttpRequestStorage.DatabaseEntities.CustomAttributes.Where(x => customAttributeIDList.Contains(x.CustomAttributeID)));
            }
        }

        public static void DeleteCustomAttribute(this ICollection<CustomAttribute> customAttributesToDelete)
        {
            if(customAttributesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllCustomAttributes.RemoveRange(customAttributesToDelete);
            }
        }

        public static void DeleteCustomAttribute(this int customAttributeID)
        {
            DeleteCustomAttribute(new List<int> { customAttributeID });
        }

        public static void DeleteCustomAttribute(this CustomAttribute customAttributeToDelete)
        {
            DeleteCustomAttribute(new List<CustomAttribute> { customAttributeToDelete });
        }
    }
}