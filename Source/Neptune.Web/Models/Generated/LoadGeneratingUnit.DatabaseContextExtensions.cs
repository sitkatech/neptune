//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LoadGeneratingUnit]
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
        public static LoadGeneratingUnit GetLoadGeneratingUnit(this IQueryable<LoadGeneratingUnit> loadGeneratingUnits, int loadGeneratingUnitID)
        {
            var loadGeneratingUnit = loadGeneratingUnits.SingleOrDefault(x => x.LoadGeneratingUnitID == loadGeneratingUnitID);
            Check.RequireNotNullThrowNotFound(loadGeneratingUnit, "LoadGeneratingUnit", loadGeneratingUnitID);
            return loadGeneratingUnit;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteLoadGeneratingUnit(this IQueryable<LoadGeneratingUnit> loadGeneratingUnits, List<int> loadGeneratingUnitIDList)
        {
            if(loadGeneratingUnitIDList.Any())
            {
                loadGeneratingUnits.Where(x => loadGeneratingUnitIDList.Contains(x.LoadGeneratingUnitID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteLoadGeneratingUnit(this IQueryable<LoadGeneratingUnit> loadGeneratingUnits, ICollection<LoadGeneratingUnit> loadGeneratingUnitsToDelete)
        {
            if(loadGeneratingUnitsToDelete.Any())
            {
                var loadGeneratingUnitIDList = loadGeneratingUnitsToDelete.Select(x => x.LoadGeneratingUnitID).ToList();
                loadGeneratingUnits.Where(x => loadGeneratingUnitIDList.Contains(x.LoadGeneratingUnitID)).Delete();
            }
        }

        public static void DeleteLoadGeneratingUnit(this IQueryable<LoadGeneratingUnit> loadGeneratingUnits, int loadGeneratingUnitID)
        {
            DeleteLoadGeneratingUnit(loadGeneratingUnits, new List<int> { loadGeneratingUnitID });
        }

        public static void DeleteLoadGeneratingUnit(this IQueryable<LoadGeneratingUnit> loadGeneratingUnits, LoadGeneratingUnit loadGeneratingUnitToDelete)
        {
            DeleteLoadGeneratingUnit(loadGeneratingUnits, new List<LoadGeneratingUnit> { loadGeneratingUnitToDelete });
        }
    }
}