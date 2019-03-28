//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentObservationType]
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
        public static TreatmentBMPAssessmentObservationType GetTreatmentBMPAssessmentObservationType(this IQueryable<TreatmentBMPAssessmentObservationType> treatmentBMPAssessmentObservationTypes, int treatmentBMPAssessmentObservationTypeID)
        {
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypes.SingleOrDefault(x => x.TreatmentBMPAssessmentObservationTypeID == treatmentBMPAssessmentObservationTypeID);
            Check.RequireNotNullThrowNotFound(treatmentBMPAssessmentObservationType, "TreatmentBMPAssessmentObservationType", treatmentBMPAssessmentObservationTypeID);
            return treatmentBMPAssessmentObservationType;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteTreatmentBMPAssessmentObservationType(this IQueryable<TreatmentBMPAssessmentObservationType> treatmentBMPAssessmentObservationTypes, List<int> treatmentBMPAssessmentObservationTypeIDList)
        {
            if(treatmentBMPAssessmentObservationTypeIDList.Any())
            {
                treatmentBMPAssessmentObservationTypes.Where(x => treatmentBMPAssessmentObservationTypeIDList.Contains(x.TreatmentBMPAssessmentObservationTypeID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteTreatmentBMPAssessmentObservationType(this IQueryable<TreatmentBMPAssessmentObservationType> treatmentBMPAssessmentObservationTypes, ICollection<TreatmentBMPAssessmentObservationType> treatmentBMPAssessmentObservationTypesToDelete)
        {
            if(treatmentBMPAssessmentObservationTypesToDelete.Any())
            {
                var treatmentBMPAssessmentObservationTypeIDList = treatmentBMPAssessmentObservationTypesToDelete.Select(x => x.TreatmentBMPAssessmentObservationTypeID).ToList();
                treatmentBMPAssessmentObservationTypes.Where(x => treatmentBMPAssessmentObservationTypeIDList.Contains(x.TreatmentBMPAssessmentObservationTypeID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPAssessmentObservationType(this IQueryable<TreatmentBMPAssessmentObservationType> treatmentBMPAssessmentObservationTypes, int treatmentBMPAssessmentObservationTypeID)
        {
            DeleteTreatmentBMPAssessmentObservationType(treatmentBMPAssessmentObservationTypes, new List<int> { treatmentBMPAssessmentObservationTypeID });
        }

        public static void DeleteTreatmentBMPAssessmentObservationType(this IQueryable<TreatmentBMPAssessmentObservationType> treatmentBMPAssessmentObservationTypes, TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationTypeToDelete)
        {
            DeleteTreatmentBMPAssessmentObservationType(treatmentBMPAssessmentObservationTypes, new List<TreatmentBMPAssessmentObservationType> { treatmentBMPAssessmentObservationTypeToDelete });
        }
    }
}