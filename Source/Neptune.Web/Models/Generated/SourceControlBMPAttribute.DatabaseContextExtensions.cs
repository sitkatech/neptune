//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMPAttribute]
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
        public static SourceControlBMPAttribute GetSourceControlBMPAttribute(this IQueryable<SourceControlBMPAttribute> sourceControlBMPAttributes, int sourceControlBMPAttributeID)
        {
            var sourceControlBMPAttribute = sourceControlBMPAttributes.SingleOrDefault(x => x.SourceControlBMPAttributeID == sourceControlBMPAttributeID);
            Check.RequireNotNullThrowNotFound(sourceControlBMPAttribute, "SourceControlBMPAttribute", sourceControlBMPAttributeID);
            return sourceControlBMPAttribute;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteSourceControlBMPAttribute(this IQueryable<SourceControlBMPAttribute> sourceControlBMPAttributes, List<int> sourceControlBMPAttributeIDList)
        {
            if(sourceControlBMPAttributeIDList.Any())
            {
                sourceControlBMPAttributes.Where(x => sourceControlBMPAttributeIDList.Contains(x.SourceControlBMPAttributeID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteSourceControlBMPAttribute(this IQueryable<SourceControlBMPAttribute> sourceControlBMPAttributes, ICollection<SourceControlBMPAttribute> sourceControlBMPAttributesToDelete)
        {
            if(sourceControlBMPAttributesToDelete.Any())
            {
                var sourceControlBMPAttributeIDList = sourceControlBMPAttributesToDelete.Select(x => x.SourceControlBMPAttributeID).ToList();
                sourceControlBMPAttributes.Where(x => sourceControlBMPAttributeIDList.Contains(x.SourceControlBMPAttributeID)).Delete();
            }
        }

        public static void DeleteSourceControlBMPAttribute(this IQueryable<SourceControlBMPAttribute> sourceControlBMPAttributes, int sourceControlBMPAttributeID)
        {
            DeleteSourceControlBMPAttribute(sourceControlBMPAttributes, new List<int> { sourceControlBMPAttributeID });
        }

        public static void DeleteSourceControlBMPAttribute(this IQueryable<SourceControlBMPAttribute> sourceControlBMPAttributes, SourceControlBMPAttribute sourceControlBMPAttributeToDelete)
        {
            DeleteSourceControlBMPAttribute(sourceControlBMPAttributes, new List<SourceControlBMPAttribute> { sourceControlBMPAttributeToDelete });
        }
    }
}