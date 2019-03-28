//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordObservation]
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
        public static MaintenanceRecordObservation GetMaintenanceRecordObservation(this IQueryable<MaintenanceRecordObservation> maintenanceRecordObservations, int maintenanceRecordObservationID)
        {
            var maintenanceRecordObservation = maintenanceRecordObservations.SingleOrDefault(x => x.MaintenanceRecordObservationID == maintenanceRecordObservationID);
            Check.RequireNotNullThrowNotFound(maintenanceRecordObservation, "MaintenanceRecordObservation", maintenanceRecordObservationID);
            return maintenanceRecordObservation;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteMaintenanceRecordObservation(this IQueryable<MaintenanceRecordObservation> maintenanceRecordObservations, List<int> maintenanceRecordObservationIDList)
        {
            if(maintenanceRecordObservationIDList.Any())
            {
                maintenanceRecordObservations.Where(x => maintenanceRecordObservationIDList.Contains(x.MaintenanceRecordObservationID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteMaintenanceRecordObservation(this IQueryable<MaintenanceRecordObservation> maintenanceRecordObservations, ICollection<MaintenanceRecordObservation> maintenanceRecordObservationsToDelete)
        {
            if(maintenanceRecordObservationsToDelete.Any())
            {
                var maintenanceRecordObservationIDList = maintenanceRecordObservationsToDelete.Select(x => x.MaintenanceRecordObservationID).ToList();
                maintenanceRecordObservations.Where(x => maintenanceRecordObservationIDList.Contains(x.MaintenanceRecordObservationID)).Delete();
            }
        }

        public static void DeleteMaintenanceRecordObservation(this IQueryable<MaintenanceRecordObservation> maintenanceRecordObservations, int maintenanceRecordObservationID)
        {
            DeleteMaintenanceRecordObservation(maintenanceRecordObservations, new List<int> { maintenanceRecordObservationID });
        }

        public static void DeleteMaintenanceRecordObservation(this IQueryable<MaintenanceRecordObservation> maintenanceRecordObservations, MaintenanceRecordObservation maintenanceRecordObservationToDelete)
        {
            DeleteMaintenanceRecordObservation(maintenanceRecordObservations, new List<MaintenanceRecordObservation> { maintenanceRecordObservationToDelete });
        }
    }
}