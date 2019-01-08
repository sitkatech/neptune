//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecord]
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
        public static MaintenanceRecord GetMaintenanceRecord(this IQueryable<MaintenanceRecord> maintenanceRecords, int maintenanceRecordID)
        {
            var maintenanceRecord = maintenanceRecords.SingleOrDefault(x => x.MaintenanceRecordID == maintenanceRecordID);
            Check.RequireNotNullThrowNotFound(maintenanceRecord, "MaintenanceRecord", maintenanceRecordID);
            return maintenanceRecord;
        }

        public static void DeleteMaintenanceRecord(this IQueryable<MaintenanceRecord> maintenanceRecords, List<int> maintenanceRecordIDList)
        {
            if(maintenanceRecordIDList.Any())
            {
                maintenanceRecords.Where(x => maintenanceRecordIDList.Contains(x.MaintenanceRecordID)).Delete();
            }
        }

        public static void DeleteMaintenanceRecord(this IQueryable<MaintenanceRecord> maintenanceRecords, ICollection<MaintenanceRecord> maintenanceRecordsToDelete)
        {
            if(maintenanceRecordsToDelete.Any())
            {
                var maintenanceRecordIDList = maintenanceRecordsToDelete.Select(x => x.MaintenanceRecordID).ToList();
                maintenanceRecords.Where(x => maintenanceRecordIDList.Contains(x.MaintenanceRecordID)).Delete();
            }
        }

        public static void DeleteMaintenanceRecord(this IQueryable<MaintenanceRecord> maintenanceRecords, int maintenanceRecordID)
        {
            DeleteMaintenanceRecord(maintenanceRecords, new List<int> { maintenanceRecordID });
        }

        public static void DeleteMaintenanceRecord(this IQueryable<MaintenanceRecord> maintenanceRecords, MaintenanceRecord maintenanceRecordToDelete)
        {
            DeleteMaintenanceRecord(maintenanceRecords, new List<MaintenanceRecord> { maintenanceRecordToDelete });
        }
    }
}