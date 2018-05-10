//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeValue]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteCustomAttributeValue(this List<int> customAttributeValueIDList)
        {
            if(customAttributeValueIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllCustomAttributeValues.RemoveRange(HttpRequestStorage.DatabaseEntities.CustomAttributeValues.Where(x => customAttributeValueIDList.Contains(x.CustomAttributeValueID)));
            }
        }

        public static void DeleteCustomAttributeValue(this ICollection<CustomAttributeValue> customAttributeValuesToDelete)
        {
            if(customAttributeValuesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllCustomAttributeValues.RemoveRange(customAttributeValuesToDelete);
            }
        }

        public static void DeleteCustomAttributeValue(this int customAttributeValueID)
        {
            DeleteCustomAttributeValue(new List<int> { customAttributeValueID });
        }

        public static void DeleteCustomAttributeValue(this CustomAttributeValue customAttributeValueToDelete)
        {
            DeleteCustomAttributeValue(new List<CustomAttributeValue> { customAttributeValueToDelete });
        }
    }
}