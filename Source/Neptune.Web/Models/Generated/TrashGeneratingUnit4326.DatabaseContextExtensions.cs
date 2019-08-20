//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnit4326]
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
        public static TrashGeneratingUnit4326 GetTrashGeneratingUnit4326(this IQueryable<TrashGeneratingUnit4326> trashGeneratingUnit4326s, int trashGeneratingUnit4326ID)
        {
            var trashGeneratingUnit4326 = trashGeneratingUnit4326s.SingleOrDefault(x => x.TrashGeneratingUnit4326ID == trashGeneratingUnit4326ID);
            Check.RequireNotNullThrowNotFound(trashGeneratingUnit4326, "TrashGeneratingUnit4326", trashGeneratingUnit4326ID);
            return trashGeneratingUnit4326;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteTrashGeneratingUnit4326(this IQueryable<TrashGeneratingUnit4326> trashGeneratingUnit4326s, List<int> trashGeneratingUnit4326IDList)
        {
            if(trashGeneratingUnit4326IDList.Any())
            {
                trashGeneratingUnit4326s.Where(x => trashGeneratingUnit4326IDList.Contains(x.TrashGeneratingUnit4326ID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteTrashGeneratingUnit4326(this IQueryable<TrashGeneratingUnit4326> trashGeneratingUnit4326s, ICollection<TrashGeneratingUnit4326> trashGeneratingUnit4326sToDelete)
        {
            if(trashGeneratingUnit4326sToDelete.Any())
            {
                var trashGeneratingUnit4326IDList = trashGeneratingUnit4326sToDelete.Select(x => x.TrashGeneratingUnit4326ID).ToList();
                trashGeneratingUnit4326s.Where(x => trashGeneratingUnit4326IDList.Contains(x.TrashGeneratingUnit4326ID)).Delete();
            }
        }

        public static void DeleteTrashGeneratingUnit4326(this IQueryable<TrashGeneratingUnit4326> trashGeneratingUnit4326s, int trashGeneratingUnit4326ID)
        {
            DeleteTrashGeneratingUnit4326(trashGeneratingUnit4326s, new List<int> { trashGeneratingUnit4326ID });
        }

        public static void DeleteTrashGeneratingUnit4326(this IQueryable<TrashGeneratingUnit4326> trashGeneratingUnit4326s, TrashGeneratingUnit4326 trashGeneratingUnit4326ToDelete)
        {
            DeleteTrashGeneratingUnit4326(trashGeneratingUnit4326s, new List<TrashGeneratingUnit4326> { trashGeneratingUnit4326ToDelete });
        }
    }
}