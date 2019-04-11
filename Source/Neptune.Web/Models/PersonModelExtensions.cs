/*-----------------------------------------------------------------------
<copyright file="PersonModelExtensions.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using LtInfo.Common;
using Neptune.Web.Security;

namespace Neptune.Web.Models
{
    /// <summary>
    /// These have been implemented as extension methods on <see cref="Person"/> so we can handle the anonymous user as a null person object
    /// </summary>
    public static class PersonModelExtensions
    {
        public static HtmlString GetFullNameFirstLastAsUrl(this Person person)
        {
            return UrlTemplate.MakeHrefString(person.GetDetailUrl(), person.GetFullNameFirstLast());
        }

        public static HtmlString GetFullNameFirstLastAndOrgAsUrl(this Person person)
        {
            var userUrl = person.GetFullNameFirstLastAsUrl();
            var orgUrl = person.Organization.GetDisplayNameAsUrl();
            return new HtmlString($"{userUrl} - {orgUrl}");
        }

        public static HtmlString GetFullNameFirstLastAndOrgShortNameAsUrl(this Person person)
        {
            var userUrl = person.GetFullNameFirstLastAsUrl();
            var orgUrl = person.Organization.GetShortNameAsUrl();
            return new HtmlString($"{userUrl} ({orgUrl})");
        }

        public static HtmlString GetFullNameFirstLastAsStringAndOrgAsUrl(this Person person)
        {
            var userString = person.GetFullNameFirstLast();
            var orgUrl = person.Organization.GetShortNameAsUrl();
            return new HtmlString($"{userString} - {orgUrl}");
        }

        public static string GetEditUrl(this Person person)
        {
            return SitkaRoute<UserController>.BuildUrlFromExpression(t => t.EditRoles(person));
        }

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<UserController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this Person person)
        {
            return DeleteUrlTemplate.ParameterReplace(person.PersonID);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<UserController>.BuildAbsoluteUrlHttpsFromExpression(t => t.Detail(UrlTemplate.Parameter1Int), NeptuneWebConfiguration.CanonicalHostNameRoot));
        public static string GetDetailUrl(this Person person)
        {
            return DetailUrlTemplate.ParameterReplace(person.PersonID);
        }

        public static bool IsSitkaAdministrator(this Person person)
        {
            return person != null && person.Role == Role.SitkaAdmin;
        }

        public static bool IsAdministrator(this Person person)
        {
            return person != null && new NeptuneAdminFeature().HasPermissionByPerson(person);
        }

        public static bool ShouldReceiveNotifications(this Person person)
        {
            return person.ReceiveSupportEmails;
        }

        public static string GetKeystoneEditLink(this Person person)
        {
            return $"{NeptuneWebConfiguration.KeystoneUserProfileUrl}{person.PersonGuid}";
        }

        public static bool IsAssignedToStormwaterJurisdiction(this Person person, StormwaterJurisdiction stormwaterJurisdiction)
        {
            return person.IsAdministrator() || person.StormwaterJurisdictionPeople.Any(x => x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID);
        }

        public static bool IsManagerOrAdmin(this Person person)
        {
            return person.Role == Role.Admin || person.Role == Role.JurisdictionManager || person.Role == Role.SitkaAdmin;
        }

        public static bool CanManageStormwaterJurisdiction(this Person person, StormwaterJurisdiction stormwaterJurisdiction)
        {
            var isAdministrator = person.IsAdministrator();
            var isStormwaterJurisdictionManager = person.Role == Role.JurisdictionManager;
            var isPartOfStormwaterJurisdiction = person.StormwaterJurisdictionPeople.Any(x =>
                x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID);
            return isAdministrator || (isStormwaterJurisdictionManager && isPartOfStormwaterJurisdiction);
        }

        public static bool CanEditStormwaterJurisdiction(this Person person,
            StormwaterJurisdiction stormwaterJurisdiction)
        {
            return person.CanManageStormwaterJurisdiction(stormwaterJurisdiction) ||
                   (person.IsAssignedToStormwaterJurisdiction(stormwaterJurisdiction) &&
                    person.Role == Role.JurisdictionEditor);
        }

        /// <summary>
        /// List of Organizations for which this Person is the primary contact
        /// </summary>
        /// <param name="person"></param>
        public static List<Organization> GetPrimaryContactOrganizations(this Person person)
        {
            return person.OrganizationsWhereYouAreThePrimaryContactPerson.OrderBy(x => x.OrganizationName).ToList();
        }

        public static IEnumerable<StormwaterJurisdiction> GetStormwaterJurisdictionsPersonCanEdit(this Person person)
        {
            if (person.Role == Role.SitkaAdmin || person.Role == Role.Admin)
            {
                return HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions;
            }

            return person.StormwaterJurisdictionPeople.Select(x => x.StormwaterJurisdiction);
        }

        public static BoundingBox GetBoundingBox(this Person person)
        {
            var stormwaterJurisdictionsPersonCanEdit = person.GetStormwaterJurisdictionsPersonCanEdit().ToList();
            var boundingBox = stormwaterJurisdictionsPersonCanEdit.Any()
                ? new BoundingBox(stormwaterJurisdictionsPersonCanEdit
                    .Select(x => x.StormwaterJurisdictionGeometry))
                : BoundingBox.MakeNewDefaultBoundingBox();
            return boundingBox;
        }

        public static List<TreatmentBMP> GetTreatmentBmpsPersonCanManage(this Person person)
        {
            var treatmentBmps = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ToList()
                .Where(x => x.CanView(person)).ToList();
            return treatmentBmps;
        }

        public static string GetStormwaterJurisdictionCqlFilter(this Person currentPerson)
        {
            return currentPerson.IsAdministrator()
                ? ""
                : $"StormwaterJurisdictionID IN ({String.Join(",", currentPerson.GetStormwaterJurisdictionsPersonCanEdit().Select(x => x.StormwaterJurisdictionID))})";
        }
    }
}
