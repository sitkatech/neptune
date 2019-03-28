//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordObservationValue]
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
        public static MaintenanceRecordObservationValue GetMaintenanceRecordObservationValue(this IQueryable<MaintenanceRecordObservationValue> maintenanceRecordObservationValues, int maintenanceRecordObservationValueID)
        {
            var maintenanceRecordObservationValue = maintenanceRecordObservationValues.SingleOrDefault(x => x.MaintenanceRecordObservationValueID == maintenanceRecordObservationValueID);
            Check.RequireNotNullThrowNotFound(maintenanceRecordObservationValue, "MaintenanceRecordObservationValue", maintenanceRecordObservationValueID);
            return maintenanceRecordObservationValue;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteMaintenanceRecordObservationValue(this IQueryable<MaintenanceRecordObservationValue> maintenanceRecordObservationValues, List<int> maintenanceRecordObservationValueIDList)
        {
            if(maintenanceRecordObservationValueIDList.Any())
            {
                maintenanceRecordObservationValues.Where(x => maintenanceRecordObservationValueIDList.Contains(x.MaintenanceRecordObservationValueID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteMaintenanceRecordObservationValue(this IQueryable<MaintenanceRecordObservationValue> maintenanceRecordObservationValues, ICollection<MaintenanceRecordObservationValue> maintenanceRecordObservationValuesToDelete)
        {
            if(maintenanceRecordObservationValuesToDelete.Any())
            {
                var maintenanceRecordObservationValueIDList = maintenanceRecordObservationValuesToDelete.Select(x => x.MaintenanceRecordObservationValueID).ToList();
                maintenanceRecordObservationValues.Where(x => maintenanceRecordObservationValueIDList.Contains(x.MaintenanceRecordObservationValueID)).Delete();
            }
        }

        public static void DeleteMaintenanceRecordObservationValue(this IQueryable<MaintenanceRecordObservationValue> maintenanceRecordObservationValues, int maintenanceRecordObservationValueID)
        {
            DeleteMaintenanceRecordObservationValue(maintenanceRecordObservationValues, new List<int> { maintenanceRecordObservationValueID });
        }

        public static void DeleteMaintenanceRecordObservationValue(this IQueryable<MaintenanceRecordObservationValue> maintenanceRecordObservationValues, MaintenanceRecordObservationValue maintenanceRecordObservationValueToDelete)
        {
            DeleteMaintenanceRecordObservationValue(maintenanceRecordObservationValues, new List<MaintenanceRecordObservationValue> { maintenanceRecordObservationValueToDelete });
        }
    }
}