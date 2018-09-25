//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMPAttribute]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static SourceControlBMPAttribute GetSourceControlBMPAttribute(this IQueryable<SourceControlBMPAttribute> sourceControlBMPAttributes, int sourceControlBMPAttributeID)
        {
            var sourceControlBMPAttribute = sourceControlBMPAttributes.SingleOrDefault(x => x.SourceControlBMPAttributeID == sourceControlBMPAttributeID);
            Check.RequireNotNullThrowNotFound(sourceControlBMPAttribute, "SourceControlBMPAttribute", sourceControlBMPAttributeID);
            return sourceControlBMPAttribute;
        }

        public static void DeleteSourceControlBMPAttribute(this List<int> sourceControlBMPAttributeIDList)
        {
            if(sourceControlBMPAttributeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllSourceControlBMPAttributes.RemoveRange(HttpRequestStorage.DatabaseEntities.SourceControlBMPAttributes.Where(x => sourceControlBMPAttributeIDList.Contains(x.SourceControlBMPAttributeID)));
            }
        }

        public static void DeleteSourceControlBMPAttribute(this ICollection<SourceControlBMPAttribute> sourceControlBMPAttributesToDelete)
        {
            if(sourceControlBMPAttributesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllSourceControlBMPAttributes.RemoveRange(sourceControlBMPAttributesToDelete);
            }
        }

        public static void DeleteSourceControlBMPAttribute(this int sourceControlBMPAttributeID)
        {
            DeleteSourceControlBMPAttribute(new List<int> { sourceControlBMPAttributeID });
        }

        public static void DeleteSourceControlBMPAttribute(this SourceControlBMPAttribute sourceControlBMPAttributeToDelete)
        {
            DeleteSourceControlBMPAttribute(new List<SourceControlBMPAttribute> { sourceControlBMPAttributeToDelete });
        }
    }
}