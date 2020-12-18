using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security.Shared;

namespace Neptune.Web.Security
{
    public class RequiresJurisdictionsFeature
    {
        public RequiresJurisdictionsFeature()
        {
        }

        public static void CheckForJurisdictions(Person person)
        {
            if (person.IsAdministrator() || person.IsAnonymousOrUnassigned())
            {
                return;
            }

            if (person.StormwaterJurisdictionPeople.Any())
            {
                return;
            }

            throw new SitkaRecordNotAuthorizedException($"You are not assigned to any Jurisdictions. Log out and log in as a different user or request additional permissions.");
        }
    }
}