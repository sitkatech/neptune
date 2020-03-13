//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LoadGeneratingUnitRefreshArea]
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
        public static LoadGeneratingUnitRefreshArea GetLoadGeneratingUnitRefreshArea(this IQueryable<LoadGeneratingUnitRefreshArea> loadGeneratingUnitRefreshAreas, int loadGeneratingUnitRefreshAreaID)
        {
            var loadGeneratingUnitRefreshArea = loadGeneratingUnitRefreshAreas.SingleOrDefault(x => x.LoadGeneratingUnitRefreshAreaID == loadGeneratingUnitRefreshAreaID);
            Check.RequireNotNullThrowNotFound(loadGeneratingUnitRefreshArea, "LoadGeneratingUnitRefreshArea", loadGeneratingUnitRefreshAreaID);
            return loadGeneratingUnitRefreshArea;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteLoadGeneratingUnitRefreshArea(this IQueryable<LoadGeneratingUnitRefreshArea> loadGeneratingUnitRefreshAreas, List<int> loadGeneratingUnitRefreshAreaIDList)
        {
            if(loadGeneratingUnitRefreshAreaIDList.Any())
            {
                loadGeneratingUnitRefreshAreas.Where(x => loadGeneratingUnitRefreshAreaIDList.Contains(x.LoadGeneratingUnitRefreshAreaID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteLoadGeneratingUnitRefreshArea(this IQueryable<LoadGeneratingUnitRefreshArea> loadGeneratingUnitRefreshAreas, ICollection<LoadGeneratingUnitRefreshArea> loadGeneratingUnitRefreshAreasToDelete)
        {
            if(loadGeneratingUnitRefreshAreasToDelete.Any())
            {
                var loadGeneratingUnitRefreshAreaIDList = loadGeneratingUnitRefreshAreasToDelete.Select(x => x.LoadGeneratingUnitRefreshAreaID).ToList();
                loadGeneratingUnitRefreshAreas.Where(x => loadGeneratingUnitRefreshAreaIDList.Contains(x.LoadGeneratingUnitRefreshAreaID)).Delete();
            }
        }

        public static void DeleteLoadGeneratingUnitRefreshArea(this IQueryable<LoadGeneratingUnitRefreshArea> loadGeneratingUnitRefreshAreas, int loadGeneratingUnitRefreshAreaID)
        {
            DeleteLoadGeneratingUnitRefreshArea(loadGeneratingUnitRefreshAreas, new List<int> { loadGeneratingUnitRefreshAreaID });
        }

        public static void DeleteLoadGeneratingUnitRefreshArea(this IQueryable<LoadGeneratingUnitRefreshArea> loadGeneratingUnitRefreshAreas, LoadGeneratingUnitRefreshArea loadGeneratingUnitRefreshAreaToDelete)
        {
            DeleteLoadGeneratingUnitRefreshArea(loadGeneratingUnitRefreshAreas, new List<LoadGeneratingUnitRefreshArea> { loadGeneratingUnitRefreshAreaToDelete });
        }
    }
}