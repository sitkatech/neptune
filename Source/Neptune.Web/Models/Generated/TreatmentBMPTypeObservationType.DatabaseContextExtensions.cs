//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeObservationType]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPTypeObservationType GetTreatmentBMPTypeObservationType(this IQueryable<TreatmentBMPTypeObservationType> treatmentBMPTypeObservationTypes, int treatmentBMPTypeObservationTypeID)
        {
            var treatmentBMPTypeObservationType = treatmentBMPTypeObservationTypes.SingleOrDefault(x => x.TreatmentBMPTypeObservationTypeID == treatmentBMPTypeObservationTypeID);
            Check.RequireNotNullThrowNotFound(treatmentBMPTypeObservationType, "TreatmentBMPTypeObservationType", treatmentBMPTypeObservationTypeID);
            return treatmentBMPTypeObservationType;
        }

        public static void DeleteTreatmentBMPTypeObservationType(this List<int> treatmentBMPTypeObservationTypeIDList)
        {
            if(treatmentBMPTypeObservationTypeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeObservationTypes.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPTypeObservationTypes.Where(x => treatmentBMPTypeObservationTypeIDList.Contains(x.TreatmentBMPTypeObservationTypeID)));
            }
        }

        public static void DeleteTreatmentBMPTypeObservationType(this ICollection<TreatmentBMPTypeObservationType> treatmentBMPTypeObservationTypesToDelete)
        {
            if(treatmentBMPTypeObservationTypesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeObservationTypes.RemoveRange(treatmentBMPTypeObservationTypesToDelete);
            }
        }

        public static void DeleteTreatmentBMPTypeObservationType(this int treatmentBMPTypeObservationTypeID)
        {
            DeleteTreatmentBMPTypeObservationType(new List<int> { treatmentBMPTypeObservationTypeID });
        }

        public static void DeleteTreatmentBMPTypeObservationType(this TreatmentBMPTypeObservationType treatmentBMPTypeObservationTypeToDelete)
        {
            DeleteTreatmentBMPTypeObservationType(new List<TreatmentBMPTypeObservationType> { treatmentBMPTypeObservationTypeToDelete });
        }
    }
}