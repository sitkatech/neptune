//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeAssessmentObservationType]
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
        public static TreatmentBMPTypeAssessmentObservationType GetTreatmentBMPTypeAssessmentObservationType(this IQueryable<TreatmentBMPTypeAssessmentObservationType> treatmentBMPTypeAssessmentObservationTypes, int treatmentBMPTypeAssessmentObservationTypeID)
        {
            var treatmentBMPTypeAssessmentObservationType = treatmentBMPTypeAssessmentObservationTypes.SingleOrDefault(x => x.TreatmentBMPTypeAssessmentObservationTypeID == treatmentBMPTypeAssessmentObservationTypeID);
            Check.RequireNotNullThrowNotFound(treatmentBMPTypeAssessmentObservationType, "TreatmentBMPTypeAssessmentObservationType", treatmentBMPTypeAssessmentObservationTypeID);
            return treatmentBMPTypeAssessmentObservationType;
        }

        public static void DeleteTreatmentBMPTypeAssessmentObservationType(this IQueryable<TreatmentBMPTypeAssessmentObservationType> treatmentBMPTypeAssessmentObservationTypes, List<int> treatmentBMPTypeAssessmentObservationTypeIDList)
        {
            if(treatmentBMPTypeAssessmentObservationTypeIDList.Any())
            {
                treatmentBMPTypeAssessmentObservationTypes.Where(x => treatmentBMPTypeAssessmentObservationTypeIDList.Contains(x.TreatmentBMPTypeAssessmentObservationTypeID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPTypeAssessmentObservationType(this IQueryable<TreatmentBMPTypeAssessmentObservationType> treatmentBMPTypeAssessmentObservationTypes, ICollection<TreatmentBMPTypeAssessmentObservationType> treatmentBMPTypeAssessmentObservationTypesToDelete)
        {
            if(treatmentBMPTypeAssessmentObservationTypesToDelete.Any())
            {
                var treatmentBMPTypeAssessmentObservationTypeIDList = treatmentBMPTypeAssessmentObservationTypesToDelete.Select(x => x.TreatmentBMPTypeAssessmentObservationTypeID).ToList();
                treatmentBMPTypeAssessmentObservationTypes.Where(x => treatmentBMPTypeAssessmentObservationTypeIDList.Contains(x.TreatmentBMPTypeAssessmentObservationTypeID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPTypeAssessmentObservationType(this IQueryable<TreatmentBMPTypeAssessmentObservationType> treatmentBMPTypeAssessmentObservationTypes, int treatmentBMPTypeAssessmentObservationTypeID)
        {
            DeleteTreatmentBMPTypeAssessmentObservationType(treatmentBMPTypeAssessmentObservationTypes, new List<int> { treatmentBMPTypeAssessmentObservationTypeID });
        }

        public static void DeleteTreatmentBMPTypeAssessmentObservationType(this IQueryable<TreatmentBMPTypeAssessmentObservationType> treatmentBMPTypeAssessmentObservationTypes, TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationTypeToDelete)
        {
            DeleteTreatmentBMPTypeAssessmentObservationType(treatmentBMPTypeAssessmentObservationTypes, new List<TreatmentBMPTypeAssessmentObservationType> { treatmentBMPTypeAssessmentObservationTypeToDelete });
        }
    }
}