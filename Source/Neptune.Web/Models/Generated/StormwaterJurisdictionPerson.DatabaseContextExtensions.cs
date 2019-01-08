//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionPerson]
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
        public static StormwaterJurisdictionPerson GetStormwaterJurisdictionPerson(this IQueryable<StormwaterJurisdictionPerson> stormwaterJurisdictionPeople, int stormwaterJurisdictionPersonID)
        {
            var stormwaterJurisdictionPerson = stormwaterJurisdictionPeople.SingleOrDefault(x => x.StormwaterJurisdictionPersonID == stormwaterJurisdictionPersonID);
            Check.RequireNotNullThrowNotFound(stormwaterJurisdictionPerson, "StormwaterJurisdictionPerson", stormwaterJurisdictionPersonID);
            return stormwaterJurisdictionPerson;
        }

        public static void DeleteStormwaterJurisdictionPerson(this IQueryable<StormwaterJurisdictionPerson> stormwaterJurisdictionPeople, List<int> stormwaterJurisdictionPersonIDList)
        {
            if(stormwaterJurisdictionPersonIDList.Any())
            {
                stormwaterJurisdictionPeople.Where(x => stormwaterJurisdictionPersonIDList.Contains(x.StormwaterJurisdictionPersonID)).Delete();
            }
        }

        public static void DeleteStormwaterJurisdictionPerson(this IQueryable<StormwaterJurisdictionPerson> stormwaterJurisdictionPeople, ICollection<StormwaterJurisdictionPerson> stormwaterJurisdictionPeopleToDelete)
        {
            if(stormwaterJurisdictionPeopleToDelete.Any())
            {
                var stormwaterJurisdictionPersonIDList = stormwaterJurisdictionPeopleToDelete.Select(x => x.StormwaterJurisdictionPersonID).ToList();
                stormwaterJurisdictionPeople.Where(x => stormwaterJurisdictionPersonIDList.Contains(x.StormwaterJurisdictionPersonID)).Delete();
            }
        }

        public static void DeleteStormwaterJurisdictionPerson(this IQueryable<StormwaterJurisdictionPerson> stormwaterJurisdictionPeople, int stormwaterJurisdictionPersonID)
        {
            DeleteStormwaterJurisdictionPerson(stormwaterJurisdictionPeople, new List<int> { stormwaterJurisdictionPersonID });
        }

        public static void DeleteStormwaterJurisdictionPerson(this IQueryable<StormwaterJurisdictionPerson> stormwaterJurisdictionPeople, StormwaterJurisdictionPerson stormwaterJurisdictionPersonToDelete)
        {
            DeleteStormwaterJurisdictionPerson(stormwaterJurisdictionPeople, new List<StormwaterJurisdictionPerson> { stormwaterJurisdictionPersonToDelete });
        }
    }
}