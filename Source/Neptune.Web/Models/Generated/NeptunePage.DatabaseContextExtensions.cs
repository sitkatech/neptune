//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePage]
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
        public static NeptunePage GetNeptunePage(this IQueryable<NeptunePage> neptunePages, int neptunePageID)
        {
            var neptunePage = neptunePages.SingleOrDefault(x => x.NeptunePageID == neptunePageID);
            Check.RequireNotNullThrowNotFound(neptunePage, "NeptunePage", neptunePageID);
            return neptunePage;
        }

        public static void DeleteNeptunePage(this IQueryable<NeptunePage> neptunePages, List<int> neptunePageIDList)
        {
            if(neptunePageIDList.Any())
            {
                neptunePages.Where(x => neptunePageIDList.Contains(x.NeptunePageID)).Delete();
            }
        }

        public static void DeleteNeptunePage(this IQueryable<NeptunePage> neptunePages, ICollection<NeptunePage> neptunePagesToDelete)
        {
            if(neptunePagesToDelete.Any())
            {
                var neptunePageIDList = neptunePagesToDelete.Select(x => x.NeptunePageID).ToList();
                neptunePages.Where(x => neptunePageIDList.Contains(x.NeptunePageID)).Delete();
            }
        }

        public static void DeleteNeptunePage(this IQueryable<NeptunePage> neptunePages, int neptunePageID)
        {
            DeleteNeptunePage(neptunePages, new List<int> { neptunePageID });
        }

        public static void DeleteNeptunePage(this IQueryable<NeptunePage> neptunePages, NeptunePage neptunePageToDelete)
        {
            DeleteNeptunePage(neptunePages, new List<NeptunePage> { neptunePageToDelete });
        }
    }
}