﻿/*-----------------------------------------------------------------------
<copyright file="Person.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public partial class Person //, IKeystoneUser
    {
        public const int AnonymousPersonID = -999;

        public bool IsAnonymousUser()
        {
            return PersonID == AnonymousPersonID;
        }

        public string GetFullNameFirstLast()
        {
            return $"{FirstName} {LastName}";
        }

        public string GetFullNameFirstLastAndOrg(NeptuneDbContext dbContext)
        {
            var organizationDisplayName =
                dbContext.Organizations.Single(x => x.OrganizationID == OrganizationID).GetDisplayName();
            return $"{FirstName} {LastName} - {organizationDisplayName}";
        }

        public string GetFullNameFirstLastAndOrgShortName()
        {
            return $"{FirstName} {LastName} ({Organization.GetOrganizationShortNameIfAvailable()})";
        }

        public string GetFullNameLastFirst()
        {
            return $"{LastName}, {FirstName}";
        }

        public bool IsSitkaAdministrator()
        {
            return RoleID == Role.SitkaAdmin.RoleID;
        }

        public bool IsAdministrator()
        {
            return RoleID == Role.SitkaAdmin.RoleID || RoleID == Role.Admin.RoleID;
        }

        public bool IsAssignedToStormwaterJurisdiction(int stormwaterJurisdictionID)
        {
            return IsAdministrator() ||
                   StormwaterJurisdictionPeople.Any(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID);
        }

        public bool IsManagerOrAdmin()
        {
            return RoleID == Role.Admin.RoleID || RoleID == Role.JurisdictionManager.RoleID ||
                   RoleID == Role.SitkaAdmin.RoleID;
        }

        public bool IsJurisdictionEditorOrManagerOrAdmin()
        {
            return RoleID == Role.Admin.RoleID || RoleID == Role.JurisdictionManager.RoleID ||
                   RoleID == Role.SitkaAdmin.RoleID || RoleID == Role.JurisdictionEditor.RoleID;
        }



        /// <summary>
        /// All role names of BOTH types used by Keystone not for user display 
        /// </summary>
        public IEnumerable<string> RoleNames
        {
            get
            {
                if (IsAnonymousUser())
                {
                    // the presence of roles switches you from being IsAuthenticated or not
                    return new List<string>();
                }
                var roleNames = new List<string> {Role.RoleName};
                return roleNames;
            }
        }

        //TODO:
        //public void SetKeystoneUserClaims(IKeystoneUserClaims keystoneUserClaims)
        //{
        //    //Organization = _dbContext.Organizations.Where(x => x.OrganizationGuid.HasValue).SingleOrDefault(x => x.OrganizationGuid == keystoneUserClaims.OrganizationGuid);
        //    Phone = keystoneUserClaims.PrimaryPhone.ToPhoneNumberString();
        //    Email = keystoneUserClaims.Email;
        //}

        public bool IsAnonymousOrUnassigned()
        {
            return IsAnonymousUser() || Role == Role.Unassigned;
        }

        public bool CanDelete(Person currentPerson)
        {
            return currentPerson.RoleID is (int)RoleEnum.Admin or (int)RoleEnum.SitkaAdmin;
        }


        public string GetFullNameFirstLastAndOrgAbbreviation()
        {
            var abbreviationIfAvailable = Organization.GetAbbreviationIfAvailable();
            return $"{FirstName} {LastName} ({abbreviationIfAvailable})";
        }

        public async Task DeleteFull(NeptuneDbContext dbContext)
        {
            foreach (var delineation in dbContext.Delineations.Where(x => x.VerifiedByPersonID == PersonID).ToList())
            {
                delineation.VerifiedByPersonID = AnonymousPersonID;
            }

            foreach (var fieldVisit in dbContext.FieldVisits.Where(x => x.PerformedByPersonID == PersonID).ToList())
            {
                fieldVisit.PerformedByPersonID = AnonymousPersonID;
            }

            foreach (var fileResource in dbContext.FileResources.Where(x => x.CreatePersonID == PersonID).ToList())
            {
                fileResource.CreatePersonID = AnonymousPersonID;
            }

            await dbContext.Notifications.Where(x => x.PersonID == PersonID).ExecuteDeleteAsync();

            foreach (var onlandVisualTrashAssessment in dbContext.OnlandVisualTrashAssessments.Where(x => x.CreatedByPersonID == PersonID).ToList())
            {
                onlandVisualTrashAssessment.CreatedByPersonID = AnonymousPersonID;
            }
            
            foreach (var parcelStaging in dbContext.ParcelStagings.Where(x => x.UploadedByPersonID == PersonID).ToList())
            {
                parcelStaging.UploadedByPersonID = AnonymousPersonID;
            }
            foreach (var project in dbContext.Projects.Where(x => x.CreatePersonID == PersonID 
                                                                  || x.PrimaryContactPersonID == PersonID 
                                                                  || x.UpdatePersonID == PersonID).ToList())
            {
                if (project.CreatePersonID == PersonID)
                {
                    project.CreatePersonID = AnonymousPersonID;
                }

                if (project.PrimaryContactPersonID == PersonID)
                {
                    project.PrimaryContactPersonID = AnonymousPersonID;
                }

                if (project.UpdatePersonID == PersonID)
                {
                    project.UpdatePersonID = AnonymousPersonID;
                }
            }


            foreach (var projectNetworkSolveHistory in dbContext.ProjectNetworkSolveHistories.Where(x => x.RequestedByPersonID == PersonID).ToList())
            {
                projectNetworkSolveHistory.RequestedByPersonID = AnonymousPersonID;
            }

            foreach (var regionalSubbasinRevisionRequest in dbContext.RegionalSubbasinRevisionRequests
                         .Where(x => x.ClosedByPersonID == PersonID || x.RequestPersonID == PersonID).ToList())
            {
                if (regionalSubbasinRevisionRequest.ClosedByPersonID == PersonID)
                {
                    regionalSubbasinRevisionRequest.ClosedByPersonID = AnonymousPersonID;
                }

                if (regionalSubbasinRevisionRequest.RequestPersonID == PersonID)
                {
                    regionalSubbasinRevisionRequest.RequestPersonID = AnonymousPersonID;
                }
            }

            await dbContext.StormwaterJurisdictionPeople.Where(x => x.PersonID == PersonID).ExecuteDeleteAsync();

            foreach (var supportRequestLog in dbContext.SupportRequestLogs.Where(x => x.RequestPersonID == PersonID).ToList())
            {
                supportRequestLog.RequestPersonID = AnonymousPersonID;
            }

            foreach (var trashGeneratingUnitAdjustment in dbContext.TrashGeneratingUnitAdjustments.Where(x => x.AdjustedByPersonID == PersonID).ToList())
            {
                trashGeneratingUnitAdjustment.AdjustedByPersonID = AnonymousPersonID;
            }
            foreach (var treatmentBMP in dbContext.TreatmentBMPs.Where(x => x.InventoryVerifiedByPersonID == PersonID).ToList())
            {
                treatmentBMP.InventoryVerifiedByPersonID = AnonymousPersonID;
            }

            foreach (var waterQualityManagementPlanVerify in dbContext.WaterQualityManagementPlanVerifies.Where(x => x.LastEditedByPersonID == PersonID).ToList())
            {
                waterQualityManagementPlanVerify.LastEditedByPersonID = AnonymousPersonID;
            }
            await dbContext.People.Where(x => x.PersonID == PersonID).ExecuteDeleteAsync();
        }
    }
}
