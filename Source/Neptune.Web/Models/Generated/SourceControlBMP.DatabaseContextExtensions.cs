//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMP]
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
        public static SourceControlBMP GetSourceControlBMP(this IQueryable<SourceControlBMP> sourceControlBMPs, int sourceControlBMPID)
        {
            var sourceControlBMP = sourceControlBMPs.SingleOrDefault(x => x.SourceControlBMPID == sourceControlBMPID);
            Check.RequireNotNullThrowNotFound(sourceControlBMP, "SourceControlBMP", sourceControlBMPID);
            return sourceControlBMP;
        }

        public static void DeleteSourceControlBMP(this IQueryable<SourceControlBMP> sourceControlBMPs, List<int> sourceControlBMPIDList)
        {
            if(sourceControlBMPIDList.Any())
            {
                sourceControlBMPs.Where(x => sourceControlBMPIDList.Contains(x.SourceControlBMPID)).Delete();
            }
        }

        public static void DeleteSourceControlBMP(this IQueryable<SourceControlBMP> sourceControlBMPs, ICollection<SourceControlBMP> sourceControlBMPsToDelete)
        {
            if(sourceControlBMPsToDelete.Any())
            {
                var sourceControlBMPIDList = sourceControlBMPsToDelete.Select(x => x.SourceControlBMPID).ToList();
                sourceControlBMPs.Where(x => sourceControlBMPIDList.Contains(x.SourceControlBMPID)).Delete();
            }
        }

        public static void DeleteSourceControlBMP(this IQueryable<SourceControlBMP> sourceControlBMPs, int sourceControlBMPID)
        {
            DeleteSourceControlBMP(sourceControlBMPs, new List<int> { sourceControlBMPID });
        }

        public static void DeleteSourceControlBMP(this IQueryable<SourceControlBMP> sourceControlBMPs, SourceControlBMP sourceControlBMPToDelete)
        {
            DeleteSourceControlBMP(sourceControlBMPs, new List<SourceControlBMP> { sourceControlBMPToDelete });
        }
    }
}