//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceActivity]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static MaintenanceActivity GetMaintenanceActivity(this IQueryable<MaintenanceActivity> maintenanceActivities, int maintenanceActivityID)
        {
            var maintenanceActivity = maintenanceActivities.SingleOrDefault(x => x.MaintenanceActivityID == maintenanceActivityID);
            Check.RequireNotNullThrowNotFound(maintenanceActivity, "MaintenanceActivity", maintenanceActivityID);
            return maintenanceActivity;
        }

        public static void DeleteMaintenanceActivity(this List<int> maintenanceActivityIDList)
        {
            if(maintenanceActivityIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllMaintenanceActivities.RemoveRange(HttpRequestStorage.DatabaseEntities.MaintenanceActivities.Where(x => maintenanceActivityIDList.Contains(x.MaintenanceActivityID)));
            }
        }

        public static void DeleteMaintenanceActivity(this ICollection<MaintenanceActivity> maintenanceActivitiesToDelete)
        {
            if(maintenanceActivitiesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllMaintenanceActivities.RemoveRange(maintenanceActivitiesToDelete);
            }
        }

        public static void DeleteMaintenanceActivity(this int maintenanceActivityID)
        {
            DeleteMaintenanceActivity(new List<int> { maintenanceActivityID });
        }

        public static void DeleteMaintenanceActivity(this MaintenanceActivity maintenanceActivityToDelete)
        {
            DeleteMaintenanceActivity(new List<MaintenanceActivity> { maintenanceActivityToDelete });
        }
    }
}