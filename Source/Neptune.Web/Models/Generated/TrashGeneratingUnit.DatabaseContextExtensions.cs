//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnit]
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
        public static TrashGeneratingUnit GetTrashGeneratingUnit(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits, int trashGeneratingUnitID)
        {
            var trashGeneratingUnit = trashGeneratingUnits.SingleOrDefault(x => x.TrashGeneratingUnitID == trashGeneratingUnitID);
            Check.RequireNotNullThrowNotFound(trashGeneratingUnit, "TrashGeneratingUnit", trashGeneratingUnitID);
            return trashGeneratingUnit;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteTrashGeneratingUnit(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits, List<int> trashGeneratingUnitIDList)
        {
            if(trashGeneratingUnitIDList.Any())
            {
                trashGeneratingUnits.Where(x => trashGeneratingUnitIDList.Contains(x.TrashGeneratingUnitID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteTrashGeneratingUnit(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits, ICollection<TrashGeneratingUnit> trashGeneratingUnitsToDelete)
        {
            if(trashGeneratingUnitsToDelete.Any())
            {
                var trashGeneratingUnitIDList = trashGeneratingUnitsToDelete.Select(x => x.TrashGeneratingUnitID).ToList();
                trashGeneratingUnits.Where(x => trashGeneratingUnitIDList.Contains(x.TrashGeneratingUnitID)).Delete();
            }
        }

        public static void DeleteTrashGeneratingUnit(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits, int trashGeneratingUnitID)
        {
            DeleteTrashGeneratingUnit(trashGeneratingUnits, new List<int> { trashGeneratingUnitID });
        }

        public static void DeleteTrashGeneratingUnit(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits, TrashGeneratingUnit trashGeneratingUnitToDelete)
        {
            DeleteTrashGeneratingUnit(trashGeneratingUnits, new List<TrashGeneratingUnit> { trashGeneratingUnitToDelete });
        }
    }
}