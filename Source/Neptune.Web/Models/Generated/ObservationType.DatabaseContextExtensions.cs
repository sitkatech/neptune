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
        public static TreatmentBMPAssessmentObservationType GetObservationType(this IQueryable<TreatmentBMPAssessmentObservationType> observationTypes, int observationTypeID)
        {
            var TreatmentBMPAssessmentObservationType = observationTypes.SingleOrDefault(x => x.ObservationTypeID == observationTypeID);
            Check.RequireNotNullThrowNotFound(TreatmentBMPAssessmentObservationType, "TreatmentBMPAssessmentObservationType", observationTypeID);
            return TreatmentBMPAssessmentObservationType;
        }

        public static void DeleteObservationType(this List<int> observationTypeIDList)
        {
            if(observationTypeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllObservationTypes.RemoveRange(HttpRequestStorage.DatabaseEntities.ObservationTypes.Where(x => observationTypeIDList.Contains(x.ObservationTypeID)));
            }
        }

        public static void DeleteObservationType(this ICollection<TreatmentBMPAssessmentObservationType> observationTypesToDelete)
        {
            if(observationTypesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllObservationTypes.RemoveRange(observationTypesToDelete);
            }
        }

        public static void DeleteObservationType(this int observationTypeID)
        {
            DeleteObservationType(new List<int> { observationTypeID });
        }

        public static void DeleteObservationType(this TreatmentBMPAssessmentObservationType observationTypeToDelete)
        {
            DeleteObservationType(new List<TreatmentBMPAssessmentObservationType> { observationTypeToDelete });
        }
    }
}