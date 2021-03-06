﻿/*-----------------------------------------------------------------------
<copyright file="EditViewModel.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Shared.UserJurisdictions
{
    public class EditUserJurisdictionsViewModel : FormViewModel, IValidatableObject
    {

        [Required]
        public int PersonID { get; set; }

        public List<StormwaterJurisdictionPersonSimple> StormwaterJurisdictionPersonSimples { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditUserJurisdictionsViewModel()
        {
        }

        public EditUserJurisdictionsViewModel(Person person, Person currentPerson)
        {
            PersonID = person.PersonID;
            StormwaterJurisdictionPersonSimples = person.StormwaterJurisdictionPeople.OrderBy(x => x.StormwaterJurisdiction.GetOrganizationDisplayName()).Select(x => new StormwaterJurisdictionPersonSimple(x, currentPerson)).ToList();
        }

        public void UpdateModel(Person person, IList<StormwaterJurisdictionPerson> allStormwaterJurisdictionPeople)
        {
            if (StormwaterJurisdictionPersonSimples == null)
            {
                StormwaterJurisdictionPersonSimples = new List<StormwaterJurisdictionPersonSimple>();
            }

            var stormwaterJurisdictionPersonUpdated = StormwaterJurisdictionPersonSimples.Select(x =>
            {
                var stormwaterJurisdictionPerson = new StormwaterJurisdictionPerson(x.StormwaterJurisdictionPersonID ?? ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue(),
                    x.StormwaterJurisdictionID,
                    x.PersonID);
                return stormwaterJurisdictionPerson;
            }).ToList();

            person.StormwaterJurisdictionPeople.Merge(stormwaterJurisdictionPersonUpdated, 
                allStormwaterJurisdictionPeople, 
                (x, y) => x.StormwaterJurisdictionPersonID == y.StormwaterJurisdictionPersonID,
                (x, y) =>
                {
                    x.PersonID = y.PersonID;
                    x.StormwaterJurisdictionID = y.StormwaterJurisdictionID;
                });

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }

    }

}
