//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Delineation]
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
        public static Delineation GetDelineation(this IQueryable<Delineation> delineations, int delineationID)
        {
            var delineation = delineations.SingleOrDefault(x => x.DelineationID == delineationID);
            Check.RequireNotNullThrowNotFound(delineation, "Delineation", delineationID);
            return delineation;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteDelineation(this IQueryable<Delineation> delineations, List<int> delineationIDList)
        {
            if(delineationIDList.Any())
            {
                delineations.Where(x => delineationIDList.Contains(x.DelineationID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteDelineation(this IQueryable<Delineation> delineations, ICollection<Delineation> delineationsToDelete)
        {
            if(delineationsToDelete.Any())
            {
                var delineationIDList = delineationsToDelete.Select(x => x.DelineationID).ToList();
                delineations.Where(x => delineationIDList.Contains(x.DelineationID)).Delete();
            }
        }

        public static void DeleteDelineation(this IQueryable<Delineation> delineations, int delineationID)
        {
            DeleteDelineation(delineations, new List<int> { delineationID });
        }

        public static void DeleteDelineation(this IQueryable<Delineation> delineations, Delineation delineationToDelete)
        {
            DeleteDelineation(delineations, new List<Delineation> { delineationToDelete });
        }
    }
}