//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HydrologicSubarea]
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
        public static HydrologicSubarea GetHydrologicSubarea(this IQueryable<HydrologicSubarea> hydrologicSubareas, int hydrologicSubareaID)
        {
            var hydrologicSubarea = hydrologicSubareas.SingleOrDefault(x => x.HydrologicSubareaID == hydrologicSubareaID);
            Check.RequireNotNullThrowNotFound(hydrologicSubarea, "HydrologicSubarea", hydrologicSubareaID);
            return hydrologicSubarea;
        }

        public static void DeleteHydrologicSubarea(this IQueryable<HydrologicSubarea> hydrologicSubareas, List<int> hydrologicSubareaIDList)
        {
            if(hydrologicSubareaIDList.Any())
            {
                hydrologicSubareas.Where(x => hydrologicSubareaIDList.Contains(x.HydrologicSubareaID)).Delete();
            }
        }

        public static void DeleteHydrologicSubarea(this IQueryable<HydrologicSubarea> hydrologicSubareas, ICollection<HydrologicSubarea> hydrologicSubareasToDelete)
        {
            if(hydrologicSubareasToDelete.Any())
            {
                var hydrologicSubareaIDList = hydrologicSubareasToDelete.Select(x => x.HydrologicSubareaID).ToList();
                hydrologicSubareas.Where(x => hydrologicSubareaIDList.Contains(x.HydrologicSubareaID)).Delete();
            }
        }

        public static void DeleteHydrologicSubarea(this IQueryable<HydrologicSubarea> hydrologicSubareas, int hydrologicSubareaID)
        {
            DeleteHydrologicSubarea(hydrologicSubareas, new List<int> { hydrologicSubareaID });
        }

        public static void DeleteHydrologicSubarea(this IQueryable<HydrologicSubarea> hydrologicSubareas, HydrologicSubarea hydrologicSubareaToDelete)
        {
            DeleteHydrologicSubarea(hydrologicSubareas, new List<HydrologicSubarea> { hydrologicSubareaToDelete });
        }
    }
}