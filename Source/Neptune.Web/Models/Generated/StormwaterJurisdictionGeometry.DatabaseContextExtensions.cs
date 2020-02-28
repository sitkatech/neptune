//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionGeometry]
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
        public static StormwaterJurisdictionGeometry GetStormwaterJurisdictionGeometry(this IQueryable<StormwaterJurisdictionGeometry> stormwaterJurisdictionGeometries, int stormwaterJurisdictionGeometryID)
        {
            var stormwaterJurisdictionGeometry = stormwaterJurisdictionGeometries.SingleOrDefault(x => x.StormwaterJurisdictionGeometryID == stormwaterJurisdictionGeometryID);
            Check.RequireNotNullThrowNotFound(stormwaterJurisdictionGeometry, "StormwaterJurisdictionGeometry", stormwaterJurisdictionGeometryID);
            return stormwaterJurisdictionGeometry;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteStormwaterJurisdictionGeometry(this IQueryable<StormwaterJurisdictionGeometry> stormwaterJurisdictionGeometries, List<int> stormwaterJurisdictionGeometryIDList)
        {
            if(stormwaterJurisdictionGeometryIDList.Any())
            {
                stormwaterJurisdictionGeometries.Where(x => stormwaterJurisdictionGeometryIDList.Contains(x.StormwaterJurisdictionGeometryID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteStormwaterJurisdictionGeometry(this IQueryable<StormwaterJurisdictionGeometry> stormwaterJurisdictionGeometries, ICollection<StormwaterJurisdictionGeometry> stormwaterJurisdictionGeometriesToDelete)
        {
            if(stormwaterJurisdictionGeometriesToDelete.Any())
            {
                var stormwaterJurisdictionGeometryIDList = stormwaterJurisdictionGeometriesToDelete.Select(x => x.StormwaterJurisdictionGeometryID).ToList();
                stormwaterJurisdictionGeometries.Where(x => stormwaterJurisdictionGeometryIDList.Contains(x.StormwaterJurisdictionGeometryID)).Delete();
            }
        }

        public static void DeleteStormwaterJurisdictionGeometry(this IQueryable<StormwaterJurisdictionGeometry> stormwaterJurisdictionGeometries, int stormwaterJurisdictionGeometryID)
        {
            DeleteStormwaterJurisdictionGeometry(stormwaterJurisdictionGeometries, new List<int> { stormwaterJurisdictionGeometryID });
        }

        public static void DeleteStormwaterJurisdictionGeometry(this IQueryable<StormwaterJurisdictionGeometry> stormwaterJurisdictionGeometries, StormwaterJurisdictionGeometry stormwaterJurisdictionGeometryToDelete)
        {
            DeleteStormwaterJurisdictionGeometry(stormwaterJurisdictionGeometries, new List<StormwaterJurisdictionGeometry> { stormwaterJurisdictionGeometryToDelete });
        }
    }
}