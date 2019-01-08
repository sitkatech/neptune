//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPObservation]
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
        public static TreatmentBMPObservation GetTreatmentBMPObservation(this IQueryable<TreatmentBMPObservation> treatmentBMPObservations, int treatmentBMPObservationID)
        {
            var treatmentBMPObservation = treatmentBMPObservations.SingleOrDefault(x => x.TreatmentBMPObservationID == treatmentBMPObservationID);
            Check.RequireNotNullThrowNotFound(treatmentBMPObservation, "TreatmentBMPObservation", treatmentBMPObservationID);
            return treatmentBMPObservation;
        }

        public static void DeleteTreatmentBMPObservation(this IQueryable<TreatmentBMPObservation> treatmentBMPObservations, List<int> treatmentBMPObservationIDList)
        {
            if(treatmentBMPObservationIDList.Any())
            {
                treatmentBMPObservations.Where(x => treatmentBMPObservationIDList.Contains(x.TreatmentBMPObservationID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPObservation(this IQueryable<TreatmentBMPObservation> treatmentBMPObservations, ICollection<TreatmentBMPObservation> treatmentBMPObservationsToDelete)
        {
            if(treatmentBMPObservationsToDelete.Any())
            {
                var treatmentBMPObservationIDList = treatmentBMPObservationsToDelete.Select(x => x.TreatmentBMPObservationID).ToList();
                treatmentBMPObservations.Where(x => treatmentBMPObservationIDList.Contains(x.TreatmentBMPObservationID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPObservation(this IQueryable<TreatmentBMPObservation> treatmentBMPObservations, int treatmentBMPObservationID)
        {
            DeleteTreatmentBMPObservation(treatmentBMPObservations, new List<int> { treatmentBMPObservationID });
        }

        public static void DeleteTreatmentBMPObservation(this IQueryable<TreatmentBMPObservation> treatmentBMPObservations, TreatmentBMPObservation treatmentBMPObservationToDelete)
        {
            DeleteTreatmentBMPObservation(treatmentBMPObservations, new List<TreatmentBMPObservation> { treatmentBMPObservationToDelete });
        }
    }
}