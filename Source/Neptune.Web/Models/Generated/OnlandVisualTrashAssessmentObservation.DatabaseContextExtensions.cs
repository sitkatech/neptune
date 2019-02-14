//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservation]
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
        public static OnlandVisualTrashAssessmentObservation GetOnlandVisualTrashAssessmentObservation(this IQueryable<OnlandVisualTrashAssessmentObservation> onlandVisualTrashAssessmentObservations, int onlandVisualTrashAssessmentObservationID)
        {
            var onlandVisualTrashAssessmentObservation = onlandVisualTrashAssessmentObservations.SingleOrDefault(x => x.OnlandVisualTrashAssessmentObservationID == onlandVisualTrashAssessmentObservationID);
            Check.RequireNotNullThrowNotFound(onlandVisualTrashAssessmentObservation, "OnlandVisualTrashAssessmentObservation", onlandVisualTrashAssessmentObservationID);
            return onlandVisualTrashAssessmentObservation;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteOnlandVisualTrashAssessmentObservation(this IQueryable<OnlandVisualTrashAssessmentObservation> onlandVisualTrashAssessmentObservations, List<int> onlandVisualTrashAssessmentObservationIDList)
        {
            if(onlandVisualTrashAssessmentObservationIDList.Any())
            {
                onlandVisualTrashAssessmentObservations.Where(x => onlandVisualTrashAssessmentObservationIDList.Contains(x.OnlandVisualTrashAssessmentObservationID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteOnlandVisualTrashAssessmentObservation(this IQueryable<OnlandVisualTrashAssessmentObservation> onlandVisualTrashAssessmentObservations, ICollection<OnlandVisualTrashAssessmentObservation> onlandVisualTrashAssessmentObservationsToDelete)
        {
            if(onlandVisualTrashAssessmentObservationsToDelete.Any())
            {
                var onlandVisualTrashAssessmentObservationIDList = onlandVisualTrashAssessmentObservationsToDelete.Select(x => x.OnlandVisualTrashAssessmentObservationID).ToList();
                onlandVisualTrashAssessmentObservations.Where(x => onlandVisualTrashAssessmentObservationIDList.Contains(x.OnlandVisualTrashAssessmentObservationID)).Delete();
            }
        }

        public static void DeleteOnlandVisualTrashAssessmentObservation(this IQueryable<OnlandVisualTrashAssessmentObservation> onlandVisualTrashAssessmentObservations, int onlandVisualTrashAssessmentObservationID)
        {
            DeleteOnlandVisualTrashAssessmentObservation(onlandVisualTrashAssessmentObservations, new List<int> { onlandVisualTrashAssessmentObservationID });
        }

        public static void DeleteOnlandVisualTrashAssessmentObservation(this IQueryable<OnlandVisualTrashAssessmentObservation> onlandVisualTrashAssessmentObservations, OnlandVisualTrashAssessmentObservation onlandVisualTrashAssessmentObservationToDelete)
        {
            DeleteOnlandVisualTrashAssessmentObservation(onlandVisualTrashAssessmentObservations, new List<OnlandVisualTrashAssessmentObservation> { onlandVisualTrashAssessmentObservationToDelete });
        }
    }
}