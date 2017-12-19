//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessment]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPAssessment GetTreatmentBMPAssessment(this IQueryable<TreatmentBMPAssessment> treatmentBMPAssessments, int treatmentBMPAssessmentID)
        {
            var treatmentBMPAssessment = treatmentBMPAssessments.SingleOrDefault(x => x.TreatmentBMPAssessmentID == treatmentBMPAssessmentID);
            Check.RequireNotNullThrowNotFound(treatmentBMPAssessment, "TreatmentBMPAssessment", treatmentBMPAssessmentID);
            return treatmentBMPAssessment;
        }

        public static void DeleteTreatmentBMPAssessment(this List<int> treatmentBMPAssessmentIDList)
        {
            if(treatmentBMPAssessmentIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessments.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessments.Where(x => treatmentBMPAssessmentIDList.Contains(x.TreatmentBMPAssessmentID)));
            }
        }

        public static void DeleteTreatmentBMPAssessment(this ICollection<TreatmentBMPAssessment> treatmentBMPAssessmentsToDelete)
        {
            if(treatmentBMPAssessmentsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessments.RemoveRange(treatmentBMPAssessmentsToDelete);
            }
        }

        public static void DeleteTreatmentBMPAssessment(this int treatmentBMPAssessmentID)
        {
            DeleteTreatmentBMPAssessment(new List<int> { treatmentBMPAssessmentID });
        }

        public static void DeleteTreatmentBMPAssessment(this TreatmentBMPAssessment treatmentBMPAssessmentToDelete)
        {
            DeleteTreatmentBMPAssessment(new List<TreatmentBMPAssessment> { treatmentBMPAssessmentToDelete });
        }
    }
}