//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OCTAPrioritizationStaging]
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
        public static OCTAPrioritizationStaging GetOCTAPrioritizationStaging(this IQueryable<OCTAPrioritizationStaging> oCTAPrioritizationStagings, int oCTAPrioritizationStagingID)
        {
            var oCTAPrioritizationStaging = oCTAPrioritizationStagings.SingleOrDefault(x => x.OCTAPrioritizationStagingID == oCTAPrioritizationStagingID);
            Check.RequireNotNullThrowNotFound(oCTAPrioritizationStaging, "OCTAPrioritizationStaging", oCTAPrioritizationStagingID);
            return oCTAPrioritizationStaging;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteOCTAPrioritizationStaging(this IQueryable<OCTAPrioritizationStaging> oCTAPrioritizationStagings, List<int> oCTAPrioritizationStagingIDList)
        {
            if(oCTAPrioritizationStagingIDList.Any())
            {
                oCTAPrioritizationStagings.Where(x => oCTAPrioritizationStagingIDList.Contains(x.OCTAPrioritizationStagingID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteOCTAPrioritizationStaging(this IQueryable<OCTAPrioritizationStaging> oCTAPrioritizationStagings, ICollection<OCTAPrioritizationStaging> oCTAPrioritizationStagingsToDelete)
        {
            if(oCTAPrioritizationStagingsToDelete.Any())
            {
                var oCTAPrioritizationStagingIDList = oCTAPrioritizationStagingsToDelete.Select(x => x.OCTAPrioritizationStagingID).ToList();
                oCTAPrioritizationStagings.Where(x => oCTAPrioritizationStagingIDList.Contains(x.OCTAPrioritizationStagingID)).Delete();
            }
        }

        public static void DeleteOCTAPrioritizationStaging(this IQueryable<OCTAPrioritizationStaging> oCTAPrioritizationStagings, int oCTAPrioritizationStagingID)
        {
            DeleteOCTAPrioritizationStaging(oCTAPrioritizationStagings, new List<int> { oCTAPrioritizationStagingID });
        }

        public static void DeleteOCTAPrioritizationStaging(this IQueryable<OCTAPrioritizationStaging> oCTAPrioritizationStagings, OCTAPrioritizationStaging oCTAPrioritizationStagingToDelete)
        {
            DeleteOCTAPrioritizationStaging(oCTAPrioritizationStagings, new List<OCTAPrioritizationStaging> { oCTAPrioritizationStagingToDelete });
        }
    }
}