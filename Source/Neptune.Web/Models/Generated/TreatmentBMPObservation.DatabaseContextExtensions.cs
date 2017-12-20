//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPObservation]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPObservation GetTreatmentBMPObservation(this IQueryable<TreatmentBMPObservation> treatmentBMPObservations, int treatmentBMPObservationID)
        {
            var treatmentBMPObservation = treatmentBMPObservations.SingleOrDefault(x => x.TreatmentBMPObservationID == treatmentBMPObservationID);
            Check.RequireNotNullThrowNotFound(treatmentBMPObservation, "TreatmentBMPObservation", treatmentBMPObservationID);
            return treatmentBMPObservation;
        }

        public static void DeleteTreatmentBMPObservation(this List<int> treatmentBMPObservationIDList)
        {
            if(treatmentBMPObservationIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservations.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPObservations.Where(x => treatmentBMPObservationIDList.Contains(x.TreatmentBMPObservationID)));
            }
        }

        public static void DeleteTreatmentBMPObservation(this ICollection<TreatmentBMPObservation> treatmentBMPObservationsToDelete)
        {
            if(treatmentBMPObservationsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservations.RemoveRange(treatmentBMPObservationsToDelete);
            }
        }

        public static void DeleteTreatmentBMPObservation(this int treatmentBMPObservationID)
        {
            DeleteTreatmentBMPObservation(new List<int> { treatmentBMPObservationID });
        }

        public static void DeleteTreatmentBMPObservation(this TreatmentBMPObservation treatmentBMPObservationToDelete)
        {
            DeleteTreatmentBMPObservation(new List<TreatmentBMPObservation> { treatmentBMPObservationToDelete });
        }
    }
}