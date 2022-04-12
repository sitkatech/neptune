//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OCTAPrioritization]
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
        public static OCTAPrioritization GetOCTAPrioritization(this IQueryable<OCTAPrioritization> oCTAPrioritizations, int oCTAPrioritizationID)
        {
            var oCTAPrioritization = oCTAPrioritizations.SingleOrDefault(x => x.OCTAPrioritizationID == oCTAPrioritizationID);
            Check.RequireNotNullThrowNotFound(oCTAPrioritization, "OCTAPrioritization", oCTAPrioritizationID);
            return oCTAPrioritization;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteOCTAPrioritization(this IQueryable<OCTAPrioritization> oCTAPrioritizations, List<int> oCTAPrioritizationIDList)
        {
            if(oCTAPrioritizationIDList.Any())
            {
                oCTAPrioritizations.Where(x => oCTAPrioritizationIDList.Contains(x.OCTAPrioritizationID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteOCTAPrioritization(this IQueryable<OCTAPrioritization> oCTAPrioritizations, ICollection<OCTAPrioritization> oCTAPrioritizationsToDelete)
        {
            if(oCTAPrioritizationsToDelete.Any())
            {
                var oCTAPrioritizationIDList = oCTAPrioritizationsToDelete.Select(x => x.OCTAPrioritizationID).ToList();
                oCTAPrioritizations.Where(x => oCTAPrioritizationIDList.Contains(x.OCTAPrioritizationID)).Delete();
            }
        }

        public static void DeleteOCTAPrioritization(this IQueryable<OCTAPrioritization> oCTAPrioritizations, int oCTAPrioritizationID)
        {
            DeleteOCTAPrioritization(oCTAPrioritizations, new List<int> { oCTAPrioritizationID });
        }

        public static void DeleteOCTAPrioritization(this IQueryable<OCTAPrioritization> oCTAPrioritizations, OCTAPrioritization oCTAPrioritizationToDelete)
        {
            DeleteOCTAPrioritization(oCTAPrioritizations, new List<OCTAPrioritization> { oCTAPrioritizationToDelete });
        }
    }
}