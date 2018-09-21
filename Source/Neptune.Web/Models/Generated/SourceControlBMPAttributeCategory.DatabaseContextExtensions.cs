//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMPAttributeCategory]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static SourceControlBMPAttributeCategory GetSourceControlBMPAttributeCategory(this IQueryable<SourceControlBMPAttributeCategory> sourceControlBMPAttributeCategories, int sourceControlBMPAttributeCategoryID)
        {
            var sourceControlBMPAttributeCategory = sourceControlBMPAttributeCategories.SingleOrDefault(x => x.SourceControlBMPAttributeCategoryID == sourceControlBMPAttributeCategoryID);
            Check.RequireNotNullThrowNotFound(sourceControlBMPAttributeCategory, "SourceControlBMPAttributeCategory", sourceControlBMPAttributeCategoryID);
            return sourceControlBMPAttributeCategory;
        }

        public static void DeleteSourceControlBMPAttributeCategory(this List<int> sourceControlBMPAttributeCategoryIDList)
        {
            if(sourceControlBMPAttributeCategoryIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllSourceControlBMPAttributeCategories.RemoveRange(HttpRequestStorage.DatabaseEntities.SourceControlBMPAttributeCategories.Where(x => sourceControlBMPAttributeCategoryIDList.Contains(x.SourceControlBMPAttributeCategoryID)));
            }
        }

        public static void DeleteSourceControlBMPAttributeCategory(this ICollection<SourceControlBMPAttributeCategory> sourceControlBMPAttributeCategoriesToDelete)
        {
            if(sourceControlBMPAttributeCategoriesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllSourceControlBMPAttributeCategories.RemoveRange(sourceControlBMPAttributeCategoriesToDelete);
            }
        }

        public static void DeleteSourceControlBMPAttributeCategory(this int sourceControlBMPAttributeCategoryID)
        {
            DeleteSourceControlBMPAttributeCategory(new List<int> { sourceControlBMPAttributeCategoryID });
        }

        public static void DeleteSourceControlBMPAttributeCategory(this SourceControlBMPAttributeCategory sourceControlBMPAttributeCategoryToDelete)
        {
            DeleteSourceControlBMPAttributeCategory(new List<SourceControlBMPAttributeCategory> { sourceControlBMPAttributeCategoryToDelete });
        }
    }
}