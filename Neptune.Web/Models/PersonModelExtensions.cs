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

using Neptune.Web.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;
using Neptune.Web.Controllers;

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
            return "";//todo:SitkaRoute<UserController>.BuildUrlFromExpression(t => t.EditRoles(person));
        }

        //public static readonly UrlTemplate<int> DeleteUrlTemplate =
        //    new UrlTemplate<int>(
        //        SitkaRoute<UserController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));

        public static string GetDeleteUrl(this Person person)
        {
            return "";//todo:DeleteUrlTemplate.ParameterReplace(person.PersonID);
        }

        //public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(
        //    SitkaRoute<UserController>.BuildAbsoluteUrlHttpsFromExpression(t => t.Detail(UrlTemplate.Parameter1Int),
        //        NeptuneWebConfiguration.CanonicalHostNameRoot));

        public static string GetDetailUrl(this Person person)
        {
            return "";//todo:DetailUrlTemplate.ParameterReplace(person.PersonID);
        }

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

        public static List<StormwaterJurisdiction> GetStormwaterJurisdictionsPersonCanViewWithContext(
            this Person person, NeptuneDbContext dbContext)
        {
            if (person.IsAdministrator() || person.IsAnonymousOrUnassigned())
            {
                return StormwaterJurisdictions.List(dbContext);
            }
            //todo: anonymous user can see more jurisdictions?
            var stormwaterJurisdictionIDsForPerson = person.StormwaterJurisdictionPeople.Select(x => x.StormwaterJurisdictionID).ToList();
            return StormwaterJurisdictions.List(dbContext).Where(x =>
                stormwaterJurisdictionIDsForPerson.Contains(x.StormwaterJurisdictionID)).ToList();
        }

        public static IEnumerable<int> GetStormwaterJurisdictionIDsPersonCanViewWithContext(this Person person,
            NeptuneDbContext dbContext)
        {
            if (person.IsAdministrator() || person.IsAnonymousOrUnassigned())
            {
                return dbContext.StormwaterJurisdictions.Select(x => x.StormwaterJurisdictionID);
            }

            return person.StormwaterJurisdictionPeople.Select(x => x.StormwaterJurisdictionID);
        }

        /// <summary>
        /// List of Organizations for which this Person is the primary contact
        /// </summary>
        /// <param name="person"></param>
        public static List<Organization> GetPrimaryContactOrganizations(this Person person)
        {
            return person.Organizations.OrderBy(x => x.OrganizationName).ToList();
        }

        public static List<TreatmentBMP> GetTreatmentBmpsPersonCanView(this Person person,
            IEnumerable<StormwaterJurisdiction> stormwaterJurisdictionsPersonCanView, NeptuneDbContext dbContext)
        {
            //These users can technically see all Jurisdictions, just potentially not the BMPs inside them
            if (person.IsAnonymousOrUnassigned())
            {
                var stormwaterJurisdictionIDsAnonymousPersonCanView = stormwaterJurisdictionsPersonCanView
                    .Where(x => x.StormwaterJurisdictionPublicBMPVisibilityTypeID !=
                                (int)StormwaterJurisdictionPublicBMPVisibilityTypeEnum.None)
                    .Select(x => x.StormwaterJurisdictionID);

                var treatmentBMPs = TreatmentBMPs.GetNonPlanningModuleBMPs(dbContext)
                    .Where(x => stormwaterJurisdictionIDsAnonymousPersonCanView.Contains(x.StormwaterJurisdictionID) &&
                                x.InventoryIsVerified).ToList();
                return treatmentBMPs;
            }

            var stormwaterJurisdictionIDsPersonCanView =
                stormwaterJurisdictionsPersonCanView.Select(x => x.StormwaterJurisdictionID);
            var treatmentBmps = TreatmentBMPs.GetNonPlanningModuleBMPs(dbContext)
                .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();
            return treatmentBmps;
        }

        public static List<WaterQualityManagementPlan> GetWQMPsPersonCanView(this Person person, NeptuneDbContext dbContext)
        {
            //These users can technically see all Jurisdictions, just potentially not the WQMPs inside them
            if (person.IsAnonymousOrUnassigned())
            {
                var stormwaterJurisdictionIDsAnonymousPersonCanView = person.GetStormwaterJurisdictionsPersonCanViewWithContext(dbContext)
                    .Where(x => x.StormwaterJurisdictionPublicWQMPVisibilityTypeID !=
                                (int)StormwaterJurisdictionPublicWQMPVisibilityTypeEnum.None)
                    .Select(x => x.StormwaterJurisdictionID);

                var publicWaterQualityManagementPlans = WaterQualityManagementPlans.GetImpl(dbContext).AsNoTracking()
                    .Where(x => stormwaterJurisdictionIDsAnonymousPersonCanView.Contains(x.StormwaterJurisdictionID) &&
                                (x.WaterQualityManagementPlanStatusID ==
                                 (int)WaterQualityManagementPlanStatusEnum.Active ||
                                 x.WaterQualityManagementPlanStatusID ==
                                 (int)WaterQualityManagementPlanStatusEnum.Inactive &&
                                 x.StormwaterJurisdiction.StormwaterJurisdictionPublicWQMPVisibilityTypeID ==
                                 (int)StormwaterJurisdictionPublicWQMPVisibilityTypeEnum.ActiveAndInactive)).ToList();
                return publicWaterQualityManagementPlans;
            }

            var stormwaterJurisdictionIDsPersonCanView = person.GetStormwaterJurisdictionIDsPersonCanViewWithContext(dbContext);
            var waterQualityManagementPlans = WaterQualityManagementPlans.GetImpl(dbContext).AsNoTracking()
                .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();
            return waterQualityManagementPlans;
        }

        public static string GetStormwaterJurisdictionCqlFilter(this Person currentPerson, NeptuneDbContext dbContext)
        {
            return GetStormwaterJurisdictionCqlFilter(currentPerson,
                currentPerson.GetStormwaterJurisdictionIDsPersonCanViewWithContext(dbContext));
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
                currentPerson.GetStormwaterJurisdictionIDsPersonCanViewWithContext(dbContext));
        }

        public static string GetNegativeStormwaterJurisdictionCqlFilter(this Person currentPerson,
            IEnumerable<int> stormwaterJurisdictionIDs)
        {
            return currentPerson.IsAdministrator()
                ? string.Empty
                : $"StormwaterJurisdictionID NOT IN ({string.Join(",", stormwaterJurisdictionIDs)})";
        }

        public static List<TreatmentBMP> GetInventoriedBMPsForWQMP(this Person person,
            WaterQualityManagementPlan waterQualityManagementPlan)
        {
            if (person.IsAnonymousOrUnassigned())
            {
                switch (waterQualityManagementPlan.StormwaterJurisdiction
                            .StormwaterJurisdictionPublicBMPVisibilityTypeID)
                {
                    case (int)StormwaterJurisdictionPublicBMPVisibilityTypeEnum.VerifiedOnly:
                        return waterQualityManagementPlan.TreatmentBMPs.Where(x => x.InventoryIsVerified)
                            .OrderBy(x => x.TreatmentBMPName).ToList();
                    default:
                        return new List<TreatmentBMP>();
                }
            }

            return waterQualityManagementPlan.TreatmentBMPs.OrderBy(x => x.TreatmentBMPName).ToList();
        }
    }
}