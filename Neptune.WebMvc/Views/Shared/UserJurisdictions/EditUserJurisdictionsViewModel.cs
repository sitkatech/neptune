/*-----------------------------------------------------------------------
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

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Models;

namespace Neptune.WebMvc.Views.Shared.UserJurisdictions
{
    public class EditUserJurisdictionsViewModel : FormViewModel
    {

        [Required]
        public int PersonID { get; set; }

        public List<StormwaterJurisdictionPersonUpsertDto> StormwaterJurisdictionPersonSimples { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditUserJurisdictionsViewModel()
        {
        }

        public EditUserJurisdictionsViewModel(Person person, Person currentPerson)
        {
            PersonID = person.PersonID;
            StormwaterJurisdictionPersonSimples = person.StormwaterJurisdictionPeople.OrderBy(x => x.StormwaterJurisdiction.GetOrganizationDisplayName()).Select(x => x.AsUpsertDto(currentPerson)).ToList();
        }

        public void UpdateModel(Person person, DbSet<StormwaterJurisdictionPerson> allStormwaterJurisdictionPeople)
        {
            if (StormwaterJurisdictionPersonSimples == null)
            {
                StormwaterJurisdictionPersonSimples = new List<StormwaterJurisdictionPersonUpsertDto>();
            }

            var stormwaterJurisdictionPersonUpdated = StormwaterJurisdictionPersonSimples.Select(x =>
            {
                var stormwaterJurisdictionPerson = new StormwaterJurisdictionPerson
                {
                    StormwaterJurisdictionID = x.StormwaterJurisdictionID,
                    PersonID = x.PersonID
                };
                return stormwaterJurisdictionPerson;
            }).ToList();

            person.StormwaterJurisdictionPeople.Merge(stormwaterJurisdictionPersonUpdated,
                allStormwaterJurisdictionPeople,
                (x, y) => x.PersonID == y.PersonID &&
                          x.StormwaterJurisdictionID == y.StormwaterJurisdictionID);
        }
    }
}
