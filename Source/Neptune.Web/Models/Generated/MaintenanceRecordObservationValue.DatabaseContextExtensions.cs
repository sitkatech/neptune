//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordObservationValue]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteMaintenanceRecordObservationValue(this List<int> maintenanceRecordObservationValueIDList)
        {
            if(maintenanceRecordObservationValueIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllMaintenanceRecordObservationValues.RemoveRange(HttpRequestStorage.DatabaseEntities.MaintenanceRecordObservationValues.Where(x => maintenanceRecordObservationValueIDList.Contains(x.MaintenanceRecordObservationValueID)));
            }
        }

        public static void DeleteMaintenanceRecordObservationValue(this ICollection<MaintenanceRecordObservationValue> maintenanceRecordObservationValuesToDelete)
        {
            if(maintenanceRecordObservationValuesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllMaintenanceRecordObservationValues.RemoveRange(maintenanceRecordObservationValuesToDelete);
            }
        }

        public static void DeleteMaintenanceRecordObservationValue(this int maintenanceRecordObservationValueID)
        {
            DeleteMaintenanceRecordObservationValue(new List<int> { maintenanceRecordObservationValueID });
        }

        public static void DeleteMaintenanceRecordObservationValue(this MaintenanceRecordObservationValue maintenanceRecordObservationValueToDelete)
        {
            DeleteMaintenanceRecordObservationValue(new List<MaintenanceRecordObservationValue> { maintenanceRecordObservationValueToDelete });
        }
    }
}