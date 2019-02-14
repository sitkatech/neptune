//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttribute]
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
        public static CustomAttribute GetCustomAttribute(this IQueryable<CustomAttribute> customAttributes, int customAttributeID)
        {
            var customAttribute = customAttributes.SingleOrDefault(x => x.CustomAttributeID == customAttributeID);
            Check.RequireNotNullThrowNotFound(customAttribute, "CustomAttribute", customAttributeID);
            return customAttribute;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteCustomAttribute(this IQueryable<CustomAttribute> customAttributes, List<int> customAttributeIDList)
        {
            if(customAttributeIDList.Any())
            {
                customAttributes.Where(x => customAttributeIDList.Contains(x.CustomAttributeID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteCustomAttribute(this IQueryable<CustomAttribute> customAttributes, ICollection<CustomAttribute> customAttributesToDelete)
        {
            if(customAttributesToDelete.Any())
            {
                var customAttributeIDList = customAttributesToDelete.Select(x => x.CustomAttributeID).ToList();
                customAttributes.Where(x => customAttributeIDList.Contains(x.CustomAttributeID)).Delete();
            }
        }

        public static void DeleteCustomAttribute(this IQueryable<CustomAttribute> customAttributes, int customAttributeID)
        {
            DeleteCustomAttribute(customAttributes, new List<int> { customAttributeID });
        }

        public static void DeleteCustomAttribute(this IQueryable<CustomAttribute> customAttributes, CustomAttribute customAttributeToDelete)
        {
            DeleteCustomAttribute(customAttributes, new List<CustomAttribute> { customAttributeToDelete });
        }
    }
}