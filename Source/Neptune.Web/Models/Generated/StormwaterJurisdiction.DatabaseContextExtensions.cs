//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdiction]
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
        public static StormwaterJurisdiction GetStormwaterJurisdiction(this IQueryable<StormwaterJurisdiction> stormwaterJurisdictions, int stormwaterJurisdictionID)
        {
            var stormwaterJurisdiction = stormwaterJurisdictions.SingleOrDefault(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID);
            Check.RequireNotNullThrowNotFound(stormwaterJurisdiction, "StormwaterJurisdiction", stormwaterJurisdictionID);
            return stormwaterJurisdiction;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteStormwaterJurisdiction(this IQueryable<StormwaterJurisdiction> stormwaterJurisdictions, List<int> stormwaterJurisdictionIDList)
        {
            if(stormwaterJurisdictionIDList.Any())
            {
                stormwaterJurisdictions.Where(x => stormwaterJurisdictionIDList.Contains(x.StormwaterJurisdictionID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteStormwaterJurisdiction(this IQueryable<StormwaterJurisdiction> stormwaterJurisdictions, ICollection<StormwaterJurisdiction> stormwaterJurisdictionsToDelete)
        {
            if(stormwaterJurisdictionsToDelete.Any())
            {
                var stormwaterJurisdictionIDList = stormwaterJurisdictionsToDelete.Select(x => x.StormwaterJurisdictionID).ToList();
                stormwaterJurisdictions.Where(x => stormwaterJurisdictionIDList.Contains(x.StormwaterJurisdictionID)).Delete();
            }
        }

        public static void DeleteStormwaterJurisdiction(this IQueryable<StormwaterJurisdiction> stormwaterJurisdictions, int stormwaterJurisdictionID)
        {
            DeleteStormwaterJurisdiction(stormwaterJurisdictions, new List<int> { stormwaterJurisdictionID });
        }

        public static void DeleteStormwaterJurisdiction(this IQueryable<StormwaterJurisdiction> stormwaterJurisdictions, StormwaterJurisdiction stormwaterJurisdictionToDelete)
        {
            DeleteStormwaterJurisdiction(stormwaterJurisdictions, new List<StormwaterJurisdiction> { stormwaterJurisdictionToDelete });
        }
    }
}