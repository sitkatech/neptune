//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SystemAttribute]
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
        public static SystemAttribute GetSystemAttribute(this IQueryable<SystemAttribute> systemAttributes, int systemAttributeID)
        {
            var systemAttribute = systemAttributes.SingleOrDefault(x => x.SystemAttributeID == systemAttributeID);
            Check.RequireNotNullThrowNotFound(systemAttribute, "SystemAttribute", systemAttributeID);
            return systemAttribute;
        }

        public static void DeleteSystemAttribute(this IQueryable<SystemAttribute> systemAttributes, List<int> systemAttributeIDList)
        {
            if(systemAttributeIDList.Any())
            {
                systemAttributes.Where(x => systemAttributeIDList.Contains(x.SystemAttributeID)).Delete();
            }
        }

        public static void DeleteSystemAttribute(this IQueryable<SystemAttribute> systemAttributes, ICollection<SystemAttribute> systemAttributesToDelete)
        {
            if(systemAttributesToDelete.Any())
            {
                var systemAttributeIDList = systemAttributesToDelete.Select(x => x.SystemAttributeID).ToList();
                systemAttributes.Where(x => systemAttributeIDList.Contains(x.SystemAttributeID)).Delete();
            }
        }

        public static void DeleteSystemAttribute(this IQueryable<SystemAttribute> systemAttributes, int systemAttributeID)
        {
            DeleteSystemAttribute(systemAttributes, new List<int> { systemAttributeID });
        }

        public static void DeleteSystemAttribute(this IQueryable<SystemAttribute> systemAttributes, SystemAttribute systemAttributeToDelete)
        {
            DeleteSystemAttribute(systemAttributes, new List<SystemAttribute> { systemAttributeToDelete });
        }
    }
}