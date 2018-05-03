//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeAssessmentObservationType]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPTypeAssessmentObservationType GetTreatmentBMPTypeAssessmentObservationType(this IQueryable<TreatmentBMPTypeAssessmentObservationType> treatmentBMPTypeAssessmentObservationTypes, int treatmentBMPTypeAssessmentObservationTypeID)
        {
            var treatmentBMPTypeAssessmentObservationType = treatmentBMPTypeAssessmentObservationTypes.SingleOrDefault(x => x.TreatmentBMPTypeAssessmentObservationTypeID == treatmentBMPTypeAssessmentObservationTypeID);
            Check.RequireNotNullThrowNotFound(treatmentBMPTypeAssessmentObservationType, "TreatmentBMPTypeAssessmentObservationType", treatmentBMPTypeAssessmentObservationTypeID);
            return treatmentBMPTypeAssessmentObservationType;
        }

        public static void DeleteTreatmentBMPTypeAssessmentObservationType(this List<int> treatmentBMPTypeAssessmentObservationTypeIDList)
        {
            if(treatmentBMPTypeAssessmentObservationTypeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeAssessmentObservationTypes.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPTypeAssessmentObservationTypes.Where(x => treatmentBMPTypeAssessmentObservationTypeIDList.Contains(x.TreatmentBMPTypeAssessmentObservationTypeID)));
            }
        }

        public static void DeleteTreatmentBMPTypeAssessmentObservationType(this ICollection<TreatmentBMPTypeAssessmentObservationType> treatmentBMPTypeAssessmentObservationTypesToDelete)
        {
            if(treatmentBMPTypeAssessmentObservationTypesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeAssessmentObservationTypes.RemoveRange(treatmentBMPTypeAssessmentObservationTypesToDelete);
            }
        }

        public static void DeleteTreatmentBMPTypeAssessmentObservationType(this int treatmentBMPTypeAssessmentObservationTypeID)
        {
            DeleteTreatmentBMPTypeAssessmentObservationType(new List<int> { treatmentBMPTypeAssessmentObservationTypeID });
        }

        public static void DeleteTreatmentBMPTypeAssessmentObservationType(this TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationTypeToDelete)
        {
            DeleteTreatmentBMPTypeAssessmentObservationType(new List<TreatmentBMPTypeAssessmentObservationType> { treatmentBMPTypeAssessmentObservationTypeToDelete });
        }
    }
}