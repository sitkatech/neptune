//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NereidResult]
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
        public static NereidResult GetNereidResult(this IQueryable<NereidResult> nereidResults, int nereidResultID)
        {
            var nereidResult = nereidResults.SingleOrDefault(x => x.NereidResultID == nereidResultID);
            Check.RequireNotNullThrowNotFound(nereidResult, "NereidResult", nereidResultID);
            return nereidResult;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteNereidResult(this IQueryable<NereidResult> nereidResults, List<int> nereidResultIDList)
        {
            if(nereidResultIDList.Any())
            {
                nereidResults.Where(x => nereidResultIDList.Contains(x.NereidResultID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteNereidResult(this IQueryable<NereidResult> nereidResults, ICollection<NereidResult> nereidResultsToDelete)
        {
            if(nereidResultsToDelete.Any())
            {
                var nereidResultIDList = nereidResultsToDelete.Select(x => x.NereidResultID).ToList();
                nereidResults.Where(x => nereidResultIDList.Contains(x.NereidResultID)).Delete();
            }
        }

        public static void DeleteNereidResult(this IQueryable<NereidResult> nereidResults, int nereidResultID)
        {
            DeleteNereidResult(nereidResults, new List<int> { nereidResultID });
        }

        public static void DeleteNereidResult(this IQueryable<NereidResult> nereidResults, NereidResult nereidResultToDelete)
        {
            DeleteNereidResult(nereidResults, new List<NereidResult> { nereidResultToDelete });
        }
    }
}