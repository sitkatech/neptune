//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentObservationType]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteTreatmentBMPAssessmentObservationType(this List<int> treatmentBMPAssessmentObservationTypeIDList)
        {
            if(treatmentBMPAssessmentObservationTypeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessmentObservationTypes.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes.Where(x => treatmentBMPAssessmentObservationTypeIDList.Contains(x.TreatmentBMPAssessmentObservationTypeID)));
            }
        }

        public static void DeleteTreatmentBMPAssessmentObservationType(this ICollection<TreatmentBMPAssessmentObservationType> treatmentBMPAssessmentObservationTypesToDelete)
        {
            if(treatmentBMPAssessmentObservationTypesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessmentObservationTypes.RemoveRange(treatmentBMPAssessmentObservationTypesToDelete);
            }
        }

        public static void DeleteTreatmentBMPAssessmentObservationType(this int treatmentBMPAssessmentObservationTypeID)
        {
            DeleteTreatmentBMPAssessmentObservationType(new List<int> { treatmentBMPAssessmentObservationTypeID });
        }

        public static void DeleteTreatmentBMPAssessmentObservationType(this TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationTypeToDelete)
        {
            DeleteTreatmentBMPAssessmentObservationType(new List<TreatmentBMPAssessmentObservationType> { treatmentBMPAssessmentObservationTypeToDelete });
        }
    }
}