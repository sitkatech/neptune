//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Neighborhood]
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
        public static Neighborhood GetNeighborhood(this IQueryable<Neighborhood> neighborhoods, int neighborhoodID)
        {
            var neighborhood = neighborhoods.SingleOrDefault(x => x.NeighborhoodID == neighborhoodID);
            Check.RequireNotNullThrowNotFound(neighborhood, "Neighborhood", neighborhoodID);
            return neighborhood;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteNeighborhood(this IQueryable<Neighborhood> neighborhoods, List<int> neighborhoodIDList)
        {
            if(neighborhoodIDList.Any())
            {
                neighborhoods.Where(x => neighborhoodIDList.Contains(x.NeighborhoodID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteNeighborhood(this IQueryable<Neighborhood> neighborhoods, ICollection<Neighborhood> neighborhoodsToDelete)
        {
            if(neighborhoodsToDelete.Any())
            {
                var neighborhoodIDList = neighborhoodsToDelete.Select(x => x.NeighborhoodID).ToList();
                neighborhoods.Where(x => neighborhoodIDList.Contains(x.NeighborhoodID)).Delete();
            }
        }

        public static void DeleteNeighborhood(this IQueryable<Neighborhood> neighborhoods, int neighborhoodID)
        {
            DeleteNeighborhood(neighborhoods, new List<int> { neighborhoodID });
        }

        public static void DeleteNeighborhood(this IQueryable<Neighborhood> neighborhoods, Neighborhood neighborhoodToDelete)
        {
            DeleteNeighborhood(neighborhoods, new List<Neighborhood> { neighborhoodToDelete });
        }
    }
}