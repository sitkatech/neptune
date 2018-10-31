//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMP]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteSourceControlBMP(this List<int> sourceControlBMPIDList)
        {
            if(sourceControlBMPIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllSourceControlBMPs.RemoveRange(HttpRequestStorage.DatabaseEntities.SourceControlBMPs.Where(x => sourceControlBMPIDList.Contains(x.SourceControlBMPID)));
            }
        }

        public static void DeleteSourceControlBMP(this ICollection<SourceControlBMP> sourceControlBMPsToDelete)
        {
            if(sourceControlBMPsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllSourceControlBMPs.RemoveRange(sourceControlBMPsToDelete);
            }
        }

        public static void DeleteSourceControlBMP(this int sourceControlBMPID)
        {
            DeleteSourceControlBMP(new List<int> { sourceControlBMPID });
        }

        public static void DeleteSourceControlBMP(this SourceControlBMP sourceControlBMPToDelete)
        {
            DeleteSourceControlBMP(new List<SourceControlBMP> { sourceControlBMPToDelete });
        }
    }
}