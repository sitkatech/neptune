//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePage]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static NeptunePage GetNeptunePage(this IQueryable<NeptunePage> neptunePages, int neptunePageID)
        {
            var neptunePage = neptunePages.SingleOrDefault(x => x.NeptunePageID == neptunePageID);
            Check.RequireNotNullThrowNotFound(neptunePage, "NeptunePage", neptunePageID);
            return neptunePage;
        }

        public static void DeleteNeptunePage(this List<int> neptunePageIDList)
        {
            if(neptunePageIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllNeptunePages.RemoveRange(HttpRequestStorage.DatabaseEntities.NeptunePages.Where(x => neptunePageIDList.Contains(x.NeptunePageID)));
            }
        }

        public static void DeleteNeptunePage(this ICollection<NeptunePage> neptunePagesToDelete)
        {
            if(neptunePagesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllNeptunePages.RemoveRange(neptunePagesToDelete);
            }
        }

        public static void DeleteNeptunePage(this int neptunePageID)
        {
            DeleteNeptunePage(new List<int> { neptunePageID });
        }

        public static void DeleteNeptunePage(this NeptunePage neptunePageToDelete)
        {
            DeleteNeptunePage(new List<NeptunePage> { neptunePageToDelete });
        }
    }
}