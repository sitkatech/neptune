//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HydrologicSubarea]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteHydrologicSubarea(this List<int> hydrologicSubareaIDList)
        {
            if(hydrologicSubareaIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllHydrologicSubareas.RemoveRange(HttpRequestStorage.DatabaseEntities.HydrologicSubareas.Where(x => hydrologicSubareaIDList.Contains(x.HydrologicSubareaID)));
            }
        }

        public static void DeleteHydrologicSubarea(this ICollection<HydrologicSubarea> hydrologicSubareasToDelete)
        {
            if(hydrologicSubareasToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllHydrologicSubareas.RemoveRange(hydrologicSubareasToDelete);
            }
        }

        public static void DeleteHydrologicSubarea(this int hydrologicSubareaID)
        {
            DeleteHydrologicSubarea(new List<int> { hydrologicSubareaID });
        }

        public static void DeleteHydrologicSubarea(this HydrologicSubarea hydrologicSubareaToDelete)
        {
            DeleteHydrologicSubarea(new List<HydrologicSubarea> { hydrologicSubareaToDelete });
        }
    }
}