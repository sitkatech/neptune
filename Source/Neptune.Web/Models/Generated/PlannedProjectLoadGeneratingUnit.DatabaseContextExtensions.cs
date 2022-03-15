//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PlannedProjectLoadGeneratingUnit]
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
        public static PlannedProjectLoadGeneratingUnit GetPlannedProjectLoadGeneratingUnit(this IQueryable<PlannedProjectLoadGeneratingUnit> plannedProjectLoadGeneratingUnits, int plannedProjectLoadGeneratingUnitID)
        {
            var plannedProjectLoadGeneratingUnit = plannedProjectLoadGeneratingUnits.SingleOrDefault(x => x.PlannedProjectLoadGeneratingUnitID == plannedProjectLoadGeneratingUnitID);
            Check.RequireNotNullThrowNotFound(plannedProjectLoadGeneratingUnit, "PlannedProjectLoadGeneratingUnit", plannedProjectLoadGeneratingUnitID);
            return plannedProjectLoadGeneratingUnit;
        }

        // Delete using an IDList (Firma style)
        public static void DeletePlannedProjectLoadGeneratingUnit(this IQueryable<PlannedProjectLoadGeneratingUnit> plannedProjectLoadGeneratingUnits, List<int> plannedProjectLoadGeneratingUnitIDList)
        {
            if(plannedProjectLoadGeneratingUnitIDList.Any())
            {
                plannedProjectLoadGeneratingUnits.Where(x => plannedProjectLoadGeneratingUnitIDList.Contains(x.PlannedProjectLoadGeneratingUnitID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeletePlannedProjectLoadGeneratingUnit(this IQueryable<PlannedProjectLoadGeneratingUnit> plannedProjectLoadGeneratingUnits, ICollection<PlannedProjectLoadGeneratingUnit> plannedProjectLoadGeneratingUnitsToDelete)
        {
            if(plannedProjectLoadGeneratingUnitsToDelete.Any())
            {
                var plannedProjectLoadGeneratingUnitIDList = plannedProjectLoadGeneratingUnitsToDelete.Select(x => x.PlannedProjectLoadGeneratingUnitID).ToList();
                plannedProjectLoadGeneratingUnits.Where(x => plannedProjectLoadGeneratingUnitIDList.Contains(x.PlannedProjectLoadGeneratingUnitID)).Delete();
            }
        }

        public static void DeletePlannedProjectLoadGeneratingUnit(this IQueryable<PlannedProjectLoadGeneratingUnit> plannedProjectLoadGeneratingUnits, int plannedProjectLoadGeneratingUnitID)
        {
            DeletePlannedProjectLoadGeneratingUnit(plannedProjectLoadGeneratingUnits, new List<int> { plannedProjectLoadGeneratingUnitID });
        }

        public static void DeletePlannedProjectLoadGeneratingUnit(this IQueryable<PlannedProjectLoadGeneratingUnit> plannedProjectLoadGeneratingUnits, PlannedProjectLoadGeneratingUnit plannedProjectLoadGeneratingUnitToDelete)
        {
            DeletePlannedProjectLoadGeneratingUnit(plannedProjectLoadGeneratingUnits, new List<PlannedProjectLoadGeneratingUnit> { plannedProjectLoadGeneratingUnitToDelete });
        }
    }
}