//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMPAttributeCategory]
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
        public static SourceControlBMPAttributeCategory GetSourceControlBMPAttributeCategory(this IQueryable<SourceControlBMPAttributeCategory> sourceControlBMPAttributeCategories, int sourceControlBMPAttributeCategoryID)
        {
            var sourceControlBMPAttributeCategory = sourceControlBMPAttributeCategories.SingleOrDefault(x => x.SourceControlBMPAttributeCategoryID == sourceControlBMPAttributeCategoryID);
            Check.RequireNotNullThrowNotFound(sourceControlBMPAttributeCategory, "SourceControlBMPAttributeCategory", sourceControlBMPAttributeCategoryID);
            return sourceControlBMPAttributeCategory;
        }

        public static void DeleteSourceControlBMPAttributeCategory(this IQueryable<SourceControlBMPAttributeCategory> sourceControlBMPAttributeCategories, List<int> sourceControlBMPAttributeCategoryIDList)
        {
            if(sourceControlBMPAttributeCategoryIDList.Any())
            {
                sourceControlBMPAttributeCategories.Where(x => sourceControlBMPAttributeCategoryIDList.Contains(x.SourceControlBMPAttributeCategoryID)).Delete();
            }
        }

        public static void DeleteSourceControlBMPAttributeCategory(this IQueryable<SourceControlBMPAttributeCategory> sourceControlBMPAttributeCategories, ICollection<SourceControlBMPAttributeCategory> sourceControlBMPAttributeCategoriesToDelete)
        {
            if(sourceControlBMPAttributeCategoriesToDelete.Any())
            {
                var sourceControlBMPAttributeCategoryIDList = sourceControlBMPAttributeCategoriesToDelete.Select(x => x.SourceControlBMPAttributeCategoryID).ToList();
                sourceControlBMPAttributeCategories.Where(x => sourceControlBMPAttributeCategoryIDList.Contains(x.SourceControlBMPAttributeCategoryID)).Delete();
            }
        }

        public static void DeleteSourceControlBMPAttributeCategory(this IQueryable<SourceControlBMPAttributeCategory> sourceControlBMPAttributeCategories, int sourceControlBMPAttributeCategoryID)
        {
            DeleteSourceControlBMPAttributeCategory(sourceControlBMPAttributeCategories, new List<int> { sourceControlBMPAttributeCategoryID });
        }

        public static void DeleteSourceControlBMPAttributeCategory(this IQueryable<SourceControlBMPAttributeCategory> sourceControlBMPAttributeCategories, SourceControlBMPAttributeCategory sourceControlBMPAttributeCategoryToDelete)
        {
            DeleteSourceControlBMPAttributeCategory(sourceControlBMPAttributeCategories, new List<SourceControlBMPAttributeCategory> { sourceControlBMPAttributeCategoryToDelete });
        }
    }
}