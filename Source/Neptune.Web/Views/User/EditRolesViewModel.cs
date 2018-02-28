﻿/*-----------------------------------------------------------------------
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
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.Web.Models;
using LtInfo.Common.Models;

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

            ShouldReceiveSystemCommunications = person.ReceiveSupportEmails;
        }

        public void UpdateModel(Person person, Person currentPerson)
        {
            person.RoleID = RoleID.Value;
            person.ReceiveSupportEmails = ShouldReceiveSystemCommunications;

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
