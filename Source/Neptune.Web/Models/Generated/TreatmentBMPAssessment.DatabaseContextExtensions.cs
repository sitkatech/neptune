//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessment]
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
        public static TreatmentBMPAssessment GetTreatmentBMPAssessment(this IQueryable<TreatmentBMPAssessment> treatmentBMPAssessments, int treatmentBMPAssessmentID)
        {
            var treatmentBMPAssessment = treatmentBMPAssessments.SingleOrDefault(x => x.TreatmentBMPAssessmentID == treatmentBMPAssessmentID);
            Check.RequireNotNullThrowNotFound(treatmentBMPAssessment, "TreatmentBMPAssessment", treatmentBMPAssessmentID);
            return treatmentBMPAssessment;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteTreatmentBMPAssessment(this IQueryable<TreatmentBMPAssessment> treatmentBMPAssessments, List<int> treatmentBMPAssessmentIDList)
        {
            if(treatmentBMPAssessmentIDList.Any())
            {
                treatmentBMPAssessments.Where(x => treatmentBMPAssessmentIDList.Contains(x.TreatmentBMPAssessmentID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteTreatmentBMPAssessment(this IQueryable<TreatmentBMPAssessment> treatmentBMPAssessments, ICollection<TreatmentBMPAssessment> treatmentBMPAssessmentsToDelete)
        {
            if(treatmentBMPAssessmentsToDelete.Any())
            {
                var treatmentBMPAssessmentIDList = treatmentBMPAssessmentsToDelete.Select(x => x.TreatmentBMPAssessmentID).ToList();
                treatmentBMPAssessments.Where(x => treatmentBMPAssessmentIDList.Contains(x.TreatmentBMPAssessmentID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPAssessment(this IQueryable<TreatmentBMPAssessment> treatmentBMPAssessments, int treatmentBMPAssessmentID)
        {
            DeleteTreatmentBMPAssessment(treatmentBMPAssessments, new List<int> { treatmentBMPAssessmentID });
        }

        public static void DeleteTreatmentBMPAssessment(this IQueryable<TreatmentBMPAssessment> treatmentBMPAssessments, TreatmentBMPAssessment treatmentBMPAssessmentToDelete)
        {
            DeleteTreatmentBMPAssessment(treatmentBMPAssessments, new List<TreatmentBMPAssessment> { treatmentBMPAssessmentToDelete });
        }
    }
}