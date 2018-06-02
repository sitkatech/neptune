/*-----------------------------------------------------------------------
<copyright file="StormwaterJurisdictionPersonSimple.cs" company="Tahoe Regional Planning Agency">
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
namespace Neptune.Web.Models
{
    public class StormwaterJurisdictionPersonSimple
    {
        public int? StormwaterJurisdictionPersonID { get; set; }
        public int PersonID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public bool CurrentPersonCanRemove { get; set; }

           /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public StormwaterJurisdictionPersonSimple()
        {
        }

        public StormwaterJurisdictionPersonSimple(StormwaterJurisdictionPerson stormwaterJurisdictionPerson, Person currentPerson)
        {
            StormwaterJurisdictionPersonID = stormwaterJurisdictionPerson.StormwaterJurisdictionPersonID;
            PersonID = stormwaterJurisdictionPerson.PersonID;
            StormwaterJurisdictionID = stormwaterJurisdictionPerson.StormwaterJurisdictionID;
            CurrentPersonCanRemove =
                currentPerson.IsAssignedToStormwaterJurisdiction(stormwaterJurisdictionPerson.StormwaterJurisdiction);
        }
    }
}
