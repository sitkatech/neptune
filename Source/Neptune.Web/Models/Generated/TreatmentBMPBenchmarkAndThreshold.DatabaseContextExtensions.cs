//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPBenchmarkAndThreshold]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteTreatmentBMPBenchmarkAndThreshold(this List<int> treatmentBMPBenchmarkAndThresholdIDList)
        {
            if(treatmentBMPBenchmarkAndThresholdIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPBenchmarkAndThresholds.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPBenchmarkAndThresholds.Where(x => treatmentBMPBenchmarkAndThresholdIDList.Contains(x.TreatmentBMPBenchmarkAndThresholdID)));
            }
        }

        public static void DeleteTreatmentBMPBenchmarkAndThreshold(this ICollection<TreatmentBMPBenchmarkAndThreshold> treatmentBMPBenchmarkAndThresholdsToDelete)
        {
            if(treatmentBMPBenchmarkAndThresholdsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPBenchmarkAndThresholds.RemoveRange(treatmentBMPBenchmarkAndThresholdsToDelete);
            }
        }

        public static void DeleteTreatmentBMPBenchmarkAndThreshold(this int treatmentBMPBenchmarkAndThresholdID)
        {
            DeleteTreatmentBMPBenchmarkAndThreshold(new List<int> { treatmentBMPBenchmarkAndThresholdID });
        }

        public static void DeleteTreatmentBMPBenchmarkAndThreshold(this TreatmentBMPBenchmarkAndThreshold treatmentBMPBenchmarkAndThresholdToDelete)
        {
            DeleteTreatmentBMPBenchmarkAndThreshold(new List<TreatmentBMPBenchmarkAndThreshold> { treatmentBMPBenchmarkAndThresholdToDelete });
        }
    }
}