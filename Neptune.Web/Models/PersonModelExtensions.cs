﻿/*-----------------------------------------------------------------------
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

using Neptune.EFModels.Entities;

namespace Neptune.Web.Models
{
    /// <summary>
    /// These have been implemented as extension methods on <see cref="Person"/> so we can handle the anonymous user as a null person object
    /// </summary>
    public static class PersonModelExtensions
    {
        //public static HtmlString GetFullNameFirstLastAsUrl(this Person person)
        //{
        //    return UrlTemplate.MakeHrefString(person.GetDetailUrl(), person.GetFullNameFirstLast());
        //}

        //public static HtmlString GetFullNameFirstLastAndOrgAsUrl(this Person person)
        //{
        //    var userUrl = person.GetFullNameFirstLastAsUrl();
        //    var orgUrl = person.Organization.GetDisplayNameAsUrl();
        //    return new HtmlString($"{userUrl} - {orgUrl}");
        //}

        //public static HtmlString GetFullNameFirstLastAndOrgShortNameAsUrl(this Person person)
        //{
        //    var userUrl = person.GetFullNameFirstLastAsUrl();
        //    var orgUrl = person.Organization.GetShortNameAsUrl();
        //    return new HtmlString($"{userUrl} ({orgUrl})");
        //}

        //public static HtmlString GetFullNameFirstLastAsStringAndOrgAsUrl(this Person person)
        //{
        //    var userString = person.GetFullNameFirstLast();
        //    var orgUrl = person.Organization.GetShortNameAsUrl();
        //    return new HtmlString($"{userString} - {orgUrl}");
        //}

        public static bool ShouldReceiveNotifications(this Person person)
        {
            return person.ReceiveSupportEmails;
        }

        public static string GetKeystoneEditLink(this Person person)
        {
            return "";//todo:$"{NeptuneWebConfiguration.KeystoneUserProfileUrl}{person.PersonGuid}";
        }

        /// <summary>
        /// Needed for Keystone; basically <see cref="Person" /> is set to this fake
        /// "Anonymous" person when we are not authenticated to not have to handle the null Person case.
        /// Seems like MR and all the other RPs do this so following the pattern
        /// </summary>
        /// <returns></returns>
        public static Person GetAnonymousSitkaUser()
        {
            // as we add new areas, we need to make sure we assign the anonymous user with the unassigned roles for each area
            var anonymousSitkaUser = new Person()
            {
                PersonID = Person.AnonymousPersonID,
                PersonGuid = new Guid(),
                FirstName = "Anonymous",
                LastName = "User",
                RoleID = Role.Unassigned.RoleID,
                CreateDate = DateTime.Now,
                LastActivityDate = DateTime.Now,
                IsActive = true,
                OrganizationID = -1,
                ReceiveSupportEmails = false, ReceiveRSBRevisionRequestEmails = false,
                WebServiceAccessToken = Guid.NewGuid(),
                IsOCTAGrantReviewer = false
            };
            return anonymousSitkaUser;
        }

        public static bool CanManageStormwaterJurisdiction(this Person person, int stormwaterJurisdictionID)
        {
            if (!person.IsManagerOrAdmin())
            {
                return false;
            }

            return person.IsAssignedToStormwaterJurisdiction(stormwaterJurisdictionID);
        }

        /// <summary>
        /// List of Organizations for which this Person is the primary contact
        /// </summary>
        /// <param name="person"></param>
        public static List<Organization> GetPrimaryContactOrganizations(this Person person)
        {
            return person.Organizations.OrderBy(x => x.OrganizationName).ToList();
        }

        public static List<TreatmentBMP> GetTreatmentBmpsPersonCanView(this Person person, NeptuneDbContext dbContext, IEnumerable<int> stormwaterJurisdictionIDsPersonCanView)
        {
            var treatmentBmps = TreatmentBMPs.GetNonPlanningModuleBMPs(dbContext)
                .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();
            return person.IsAnonymousOrUnassigned() ? treatmentBmps.Where(x => x.InventoryIsVerified).ToList() : treatmentBmps;
        }

        public static string GetStormwaterJurisdictionCqlFilter(this Person currentPerson, NeptuneDbContext dbContext)
        {
            return GetStormwaterJurisdictionCqlFilter(currentPerson,
                StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(dbContext, currentPerson));
        }

        public static string GetStormwaterJurisdictionCqlFilter(this Person currentPerson,
            IEnumerable<int> stormwaterJurisdictionIDs)
        {
            return currentPerson.IsAdministrator()
                ? string.Empty
                : $"StormwaterJurisdictionID IN ({string.Join(",", stormwaterJurisdictionIDs)})";
        }

        public static string GetNegativeStormwaterJurisdictionCqlFilter(this Person currentPerson, NeptuneDbContext dbContext)
        {
            return GetNegativeStormwaterJurisdictionCqlFilter(currentPerson,
                StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(dbContext, currentPerson));
        }

        public static string GetNegativeStormwaterJurisdictionCqlFilter(this Person currentPerson,
            IEnumerable<int> stormwaterJurisdictionIDs)
        {
            return currentPerson.IsAdministrator()
                ? string.Empty
                : $"StormwaterJurisdictionID NOT IN ({string.Join(",", stormwaterJurisdictionIDs)})";
        }

        public static List<TreatmentBMP> GetInventoriedBMPsForWQMP(this Person person,
            WaterQualityManagementPlan waterQualityManagementPlan, List<TreatmentBMP> treatmentBMPs)
        {
            if (person.IsAnonymousOrUnassigned())
            {
                switch (waterQualityManagementPlan.StormwaterJurisdiction
                            .StormwaterJurisdictionPublicBMPVisibilityTypeID)
                {
                    case (int)StormwaterJurisdictionPublicBMPVisibilityTypeEnum.VerifiedOnly:
                        return treatmentBMPs.Where(x => x.InventoryIsVerified)
                            .OrderBy(x => x.TreatmentBMPName).ToList();
                    default:
                        return new List<TreatmentBMP>();
                }
            }

            return treatmentBMPs.OrderBy(x => x.TreatmentBMPName).ToList();
        }
    }
}