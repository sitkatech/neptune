//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordObservation]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteMaintenanceRecordObservation(this List<int> maintenanceRecordObservationIDList)
        {
            if(maintenanceRecordObservationIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllMaintenanceRecordObservations.RemoveRange(HttpRequestStorage.DatabaseEntities.MaintenanceRecordObservations.Where(x => maintenanceRecordObservationIDList.Contains(x.MaintenanceRecordObservationID)));
            }
        }

        public static void DeleteMaintenanceRecordObservation(this ICollection<MaintenanceRecordObservation> maintenanceRecordObservationsToDelete)
        {
            if(maintenanceRecordObservationsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllMaintenanceRecordObservations.RemoveRange(maintenanceRecordObservationsToDelete);
            }
        }

        public static void DeleteMaintenanceRecordObservation(this int maintenanceRecordObservationID)
        {
            DeleteMaintenanceRecordObservation(new List<int> { maintenanceRecordObservationID });
        }

        public static void DeleteMaintenanceRecordObservation(this MaintenanceRecordObservation maintenanceRecordObservationToDelete)
        {
            DeleteMaintenanceRecordObservation(new List<MaintenanceRecordObservation> { maintenanceRecordObservationToDelete });
        }
    }
}