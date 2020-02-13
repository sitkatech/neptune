/*-----------------------------------------------------------------------
<copyright file="EditRolesViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Web.Views.User
{
    public class EditRolesViewModel : FormViewModel
    {
        [Required]
        public int PersonID { get; set; }

        [Required]
        [DisplayName("Role")]
        public int? RoleID { get; set; }

        [Required]
        [DisplayName("Drool Tool Role")]
        public int? DroolToolRoleID { get; set; }

        [Required]
        [DisplayName("Should Receive RSB Revision Requests?")]
        public bool ShouldReceiveRSBRevisionRequests { get; set; }

        [Required]
        [DisplayName("Should Receive System Communications?")]
        public bool ShouldReceiveSystemCommunications { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditRolesViewModel()
        {
        }

        public EditRolesViewModel(Person person)
        {
            PersonID = person.PersonID;
            RoleID = person.RoleID;
            DroolToolRoleID = person.DroolToolRoleID;
            ShouldReceiveSystemCommunications = person.ReceiveSupportEmails;
            ShouldReceiveRSBRevisionRequests = person.ReceiveRSBRevisionRequestEmails;
        }

        public void UpdateModel(Person person, Person currentPerson)
        {
            person.RoleID = RoleID.GetValueOrDefault();  // will never default due to RequiredAttribute
            person.DroolToolRoleID = DroolToolRoleID.GetValueOrDefault();
            person.ReceiveSupportEmails = ShouldReceiveSystemCommunications;
            person.ReceiveRSBRevisionRequestEmails = ShouldReceiveRSBRevisionRequests;

            var assignedRole = Models.Role.AllLookupDictionary[RoleID.GetValueOrDefault()];
            if (assignedRole == Models.Role.Admin || assignedRole == Models.Role.SitkaAdmin)
            {
                HttpRequestStorage.DatabaseEntities.StormwaterJurisdictionPeople.DeleteStormwaterJurisdictionPerson(
                person.StormwaterJurisdictionPeople);
            }

            if (ModelObjectHelpers.IsRealPrimaryKeyValue(person.PersonID))
            {
                // Existing person
                person.UpdateDate = DateTime.Now;
            }
            else
            {
                // New person
                person.CreateDate = DateTime.Now;
            }
        }
    }
}
