//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdiction]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static StormwaterJurisdiction GetStormwaterJurisdiction(this IQueryable<StormwaterJurisdiction> stormwaterJurisdictions, int stormwaterJurisdictionID)
        {
            var stormwaterJurisdiction = stormwaterJurisdictions.SingleOrDefault(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID);
            Check.RequireNotNullThrowNotFound(stormwaterJurisdiction, "StormwaterJurisdiction", stormwaterJurisdictionID);
            return stormwaterJurisdiction;
        }

        public static void DeleteStormwaterJurisdiction(this List<int> stormwaterJurisdictionIDList)
        {
            if(stormwaterJurisdictionIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllStormwaterJurisdictions.RemoveRange(HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.Where(x => stormwaterJurisdictionIDList.Contains(x.StormwaterJurisdictionID)));
            }
        }

        public static void DeleteStormwaterJurisdiction(this ICollection<StormwaterJurisdiction> stormwaterJurisdictionsToDelete)
        {
            if(stormwaterJurisdictionsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllStormwaterJurisdictions.RemoveRange(stormwaterJurisdictionsToDelete);
            }
        }

        public static void DeleteStormwaterJurisdiction(this int stormwaterJurisdictionID)
        {
            DeleteStormwaterJurisdiction(new List<int> { stormwaterJurisdictionID });
        }

        public static void DeleteStormwaterJurisdiction(this StormwaterJurisdiction stormwaterJurisdictionToDelete)
        {
            DeleteStormwaterJurisdiction(new List<StormwaterJurisdiction> { stormwaterJurisdictionToDelete });
        }
    }
}