//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeValue]
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
        public static CustomAttributeValue GetCustomAttributeValue(this IQueryable<CustomAttributeValue> customAttributeValues, int customAttributeValueID)
        {
            var customAttributeValue = customAttributeValues.SingleOrDefault(x => x.CustomAttributeValueID == customAttributeValueID);
            Check.RequireNotNullThrowNotFound(customAttributeValue, "CustomAttributeValue", customAttributeValueID);
            return customAttributeValue;
        }

        public static void DeleteCustomAttributeValue(this IQueryable<CustomAttributeValue> customAttributeValues, List<int> customAttributeValueIDList)
        {
            if(customAttributeValueIDList.Any())
            {
                customAttributeValues.Where(x => customAttributeValueIDList.Contains(x.CustomAttributeValueID)).Delete();
            }
        }

        public static void DeleteCustomAttributeValue(this IQueryable<CustomAttributeValue> customAttributeValues, ICollection<CustomAttributeValue> customAttributeValuesToDelete)
        {
            if(customAttributeValuesToDelete.Any())
            {
                var customAttributeValueIDList = customAttributeValuesToDelete.Select(x => x.CustomAttributeValueID).ToList();
                customAttributeValues.Where(x => customAttributeValueIDList.Contains(x.CustomAttributeValueID)).Delete();
            }
        }

        public static void DeleteCustomAttributeValue(this IQueryable<CustomAttributeValue> customAttributeValues, int customAttributeValueID)
        {
            DeleteCustomAttributeValue(customAttributeValues, new List<int> { customAttributeValueID });
        }

        public static void DeleteCustomAttributeValue(this IQueryable<CustomAttributeValue> customAttributeValues, CustomAttributeValue customAttributeValueToDelete)
        {
            DeleteCustomAttributeValue(customAttributeValues, new List<CustomAttributeValue> { customAttributeValueToDelete });
        }
    }
}