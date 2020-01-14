//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPOperationMonth]
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
        public static TreatmentBMPOperationMonth GetTreatmentBMPOperationMonth(this IQueryable<TreatmentBMPOperationMonth> treatmentBMPOperationMonths, int treatmentBMPOperationMonthID)
        {
            var treatmentBMPOperationMonth = treatmentBMPOperationMonths.SingleOrDefault(x => x.TreatmentBMPOperationMonthID == treatmentBMPOperationMonthID);
            Check.RequireNotNullThrowNotFound(treatmentBMPOperationMonth, "TreatmentBMPOperationMonth", treatmentBMPOperationMonthID);
            return treatmentBMPOperationMonth;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteTreatmentBMPOperationMonth(this IQueryable<TreatmentBMPOperationMonth> treatmentBMPOperationMonths, List<int> treatmentBMPOperationMonthIDList)
        {
            if(treatmentBMPOperationMonthIDList.Any())
            {
                treatmentBMPOperationMonths.Where(x => treatmentBMPOperationMonthIDList.Contains(x.TreatmentBMPOperationMonthID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteTreatmentBMPOperationMonth(this IQueryable<TreatmentBMPOperationMonth> treatmentBMPOperationMonths, ICollection<TreatmentBMPOperationMonth> treatmentBMPOperationMonthsToDelete)
        {
            if(treatmentBMPOperationMonthsToDelete.Any())
            {
                var treatmentBMPOperationMonthIDList = treatmentBMPOperationMonthsToDelete.Select(x => x.TreatmentBMPOperationMonthID).ToList();
                treatmentBMPOperationMonths.Where(x => treatmentBMPOperationMonthIDList.Contains(x.TreatmentBMPOperationMonthID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPOperationMonth(this IQueryable<TreatmentBMPOperationMonth> treatmentBMPOperationMonths, int treatmentBMPOperationMonthID)
        {
            DeleteTreatmentBMPOperationMonth(treatmentBMPOperationMonths, new List<int> { treatmentBMPOperationMonthID });
        }

        public static void DeleteTreatmentBMPOperationMonth(this IQueryable<TreatmentBMPOperationMonth> treatmentBMPOperationMonths, TreatmentBMPOperationMonth treatmentBMPOperationMonthToDelete)
        {
            DeleteTreatmentBMPOperationMonth(treatmentBMPOperationMonths, new List<TreatmentBMPOperationMonth> { treatmentBMPOperationMonthToDelete });
        }
    }
}