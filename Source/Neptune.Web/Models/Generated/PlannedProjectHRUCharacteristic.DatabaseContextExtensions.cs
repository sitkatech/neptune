//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PlannedProjectHRUCharacteristic]
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
        public static PlannedProjectHRUCharacteristic GetPlannedProjectHRUCharacteristic(this IQueryable<PlannedProjectHRUCharacteristic> plannedProjectHRUCharacteristics, int plannedProjectHRUCharacteristicID)
        {
            var plannedProjectHRUCharacteristic = plannedProjectHRUCharacteristics.SingleOrDefault(x => x.PlannedProjectHRUCharacteristicID == plannedProjectHRUCharacteristicID);
            Check.RequireNotNullThrowNotFound(plannedProjectHRUCharacteristic, "PlannedProjectHRUCharacteristic", plannedProjectHRUCharacteristicID);
            return plannedProjectHRUCharacteristic;
        }

        // Delete using an IDList (Firma style)
        public static void DeletePlannedProjectHRUCharacteristic(this IQueryable<PlannedProjectHRUCharacteristic> plannedProjectHRUCharacteristics, List<int> plannedProjectHRUCharacteristicIDList)
        {
            if(plannedProjectHRUCharacteristicIDList.Any())
            {
                plannedProjectHRUCharacteristics.Where(x => plannedProjectHRUCharacteristicIDList.Contains(x.PlannedProjectHRUCharacteristicID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeletePlannedProjectHRUCharacteristic(this IQueryable<PlannedProjectHRUCharacteristic> plannedProjectHRUCharacteristics, ICollection<PlannedProjectHRUCharacteristic> plannedProjectHRUCharacteristicsToDelete)
        {
            if(plannedProjectHRUCharacteristicsToDelete.Any())
            {
                var plannedProjectHRUCharacteristicIDList = plannedProjectHRUCharacteristicsToDelete.Select(x => x.PlannedProjectHRUCharacteristicID).ToList();
                plannedProjectHRUCharacteristics.Where(x => plannedProjectHRUCharacteristicIDList.Contains(x.PlannedProjectHRUCharacteristicID)).Delete();
            }
        }

        public static void DeletePlannedProjectHRUCharacteristic(this IQueryable<PlannedProjectHRUCharacteristic> plannedProjectHRUCharacteristics, int plannedProjectHRUCharacteristicID)
        {
            DeletePlannedProjectHRUCharacteristic(plannedProjectHRUCharacteristics, new List<int> { plannedProjectHRUCharacteristicID });
        }

        public static void DeletePlannedProjectHRUCharacteristic(this IQueryable<PlannedProjectHRUCharacteristic> plannedProjectHRUCharacteristics, PlannedProjectHRUCharacteristic plannedProjectHRUCharacteristicToDelete)
        {
            DeletePlannedProjectHRUCharacteristic(plannedProjectHRUCharacteristics, new List<PlannedProjectHRUCharacteristic> { plannedProjectHRUCharacteristicToDelete });
        }
    }
}