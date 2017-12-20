//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionPerson]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteStormwaterJurisdictionPerson(this List<int> stormwaterJurisdictionPersonIDList)
        {
            if(stormwaterJurisdictionPersonIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllStormwaterJurisdictionPeople.RemoveRange(HttpRequestStorage.DatabaseEntities.StormwaterJurisdictionPeople.Where(x => stormwaterJurisdictionPersonIDList.Contains(x.StormwaterJurisdictionPersonID)));
            }
        }

        public static void DeleteStormwaterJurisdictionPerson(this ICollection<StormwaterJurisdictionPerson> stormwaterJurisdictionPeopleToDelete)
        {
            if(stormwaterJurisdictionPeopleToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllStormwaterJurisdictionPeople.RemoveRange(stormwaterJurisdictionPeopleToDelete);
            }
        }

        public static void DeleteStormwaterJurisdictionPerson(this int stormwaterJurisdictionPersonID)
        {
            DeleteStormwaterJurisdictionPerson(new List<int> { stormwaterJurisdictionPersonID });
        }

        public static void DeleteStormwaterJurisdictionPerson(this StormwaterJurisdictionPerson stormwaterJurisdictionPersonToDelete)
        {
            DeleteStormwaterJurisdictionPerson(new List<StormwaterJurisdictionPerson> { stormwaterJurisdictionPersonToDelete });
        }
    }
}