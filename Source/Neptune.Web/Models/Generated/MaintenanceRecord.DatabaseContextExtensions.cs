//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecord]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static MaintenanceRecord GetMaintenanceRecord(this IQueryable<MaintenanceRecord> maintenanceRecords, int maintenanceRecordID)
        {
            var maintenanceRecord = maintenanceRecords.SingleOrDefault(x => x.MaintenanceRecordID == maintenanceRecordID);
            Check.RequireNotNullThrowNotFound(maintenanceRecord, "MaintenanceRecord", maintenanceRecordID);
            return maintenanceRecord;
        }

        public static void DeleteMaintenanceRecord(this List<int> maintenanceRecordIDList)
        {
            if(maintenanceRecordIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllMaintenanceRecords.RemoveRange(HttpRequestStorage.DatabaseEntities.MaintenanceRecords.Where(x => maintenanceRecordIDList.Contains(x.MaintenanceRecordID)));
            }
        }

        public static void DeleteMaintenanceRecord(this ICollection<MaintenanceRecord> maintenanceRecordsToDelete)
        {
            if(maintenanceRecordsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllMaintenanceRecords.RemoveRange(maintenanceRecordsToDelete);
            }
        }

        public static void DeleteMaintenanceRecord(this int maintenanceRecordID)
        {
            DeleteMaintenanceRecord(new List<int> { maintenanceRecordID });
        }

        public static void DeleteMaintenanceRecord(this MaintenanceRecord maintenanceRecordToDelete)
        {
            DeleteMaintenanceRecord(new List<MaintenanceRecord> { maintenanceRecordToDelete });
        }
    }
}