//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPBenchmarkAndThreshold]
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
        public static TreatmentBMPBenchmarkAndThreshold GetTreatmentBMPBenchmarkAndThreshold(this IQueryable<TreatmentBMPBenchmarkAndThreshold> treatmentBMPBenchmarkAndThresholds, int treatmentBMPBenchmarkAndThresholdID)
        {
            var treatmentBMPBenchmarkAndThreshold = treatmentBMPBenchmarkAndThresholds.SingleOrDefault(x => x.TreatmentBMPBenchmarkAndThresholdID == treatmentBMPBenchmarkAndThresholdID);
            Check.RequireNotNullThrowNotFound(treatmentBMPBenchmarkAndThreshold, "TreatmentBMPBenchmarkAndThreshold", treatmentBMPBenchmarkAndThresholdID);
            return treatmentBMPBenchmarkAndThreshold;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteTreatmentBMPBenchmarkAndThreshold(this IQueryable<TreatmentBMPBenchmarkAndThreshold> treatmentBMPBenchmarkAndThresholds, List<int> treatmentBMPBenchmarkAndThresholdIDList)
        {
            if(treatmentBMPBenchmarkAndThresholdIDList.Any())
            {
                treatmentBMPBenchmarkAndThresholds.Where(x => treatmentBMPBenchmarkAndThresholdIDList.Contains(x.TreatmentBMPBenchmarkAndThresholdID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteTreatmentBMPBenchmarkAndThreshold(this IQueryable<TreatmentBMPBenchmarkAndThreshold> treatmentBMPBenchmarkAndThresholds, ICollection<TreatmentBMPBenchmarkAndThreshold> treatmentBMPBenchmarkAndThresholdsToDelete)
        {
            if(treatmentBMPBenchmarkAndThresholdsToDelete.Any())
            {
                var treatmentBMPBenchmarkAndThresholdIDList = treatmentBMPBenchmarkAndThresholdsToDelete.Select(x => x.TreatmentBMPBenchmarkAndThresholdID).ToList();
                treatmentBMPBenchmarkAndThresholds.Where(x => treatmentBMPBenchmarkAndThresholdIDList.Contains(x.TreatmentBMPBenchmarkAndThresholdID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPBenchmarkAndThreshold(this IQueryable<TreatmentBMPBenchmarkAndThreshold> treatmentBMPBenchmarkAndThresholds, int treatmentBMPBenchmarkAndThresholdID)
        {
            DeleteTreatmentBMPBenchmarkAndThreshold(treatmentBMPBenchmarkAndThresholds, new List<int> { treatmentBMPBenchmarkAndThresholdID });
        }

        public static void DeleteTreatmentBMPBenchmarkAndThreshold(this IQueryable<TreatmentBMPBenchmarkAndThreshold> treatmentBMPBenchmarkAndThresholds, TreatmentBMPBenchmarkAndThreshold treatmentBMPBenchmarkAndThresholdToDelete)
        {
            DeleteTreatmentBMPBenchmarkAndThreshold(treatmentBMPBenchmarkAndThresholds, new List<TreatmentBMPBenchmarkAndThreshold> { treatmentBMPBenchmarkAndThresholdToDelete });
        }
    }
}