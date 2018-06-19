/*-----------------------------------------------------------------------
<copyright file="EditViewData.cs" company="Tahoe Regional Planning Agency">
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
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Shared.UserJurisdictions
{
    public class EditUserJurisdictionsViewData : NeptuneViewData
    {
        
        public readonly EditViewDataForAngular ViewDataForAngular;

        public EditUserJurisdictionsViewData(Person currentPerson, List<StormwaterJurisdiction> allStormwaterJurisdictions, List<StormwaterJurisdiction> stormwaterJurisdictionsCurrentPersonCanManage)
            : base(currentPerson, StormwaterBreadCrumbEntity.Users)
        {
            ViewDataForAngular = new EditViewDataForAngular(allStormwaterJurisdictions, stormwaterJurisdictionsCurrentPersonCanManage);
        }

        public class EditViewDataForAngular
        {
            public readonly List<StormwaterJurisdictionSimple> AllStormwaterJurisdictions;
            public readonly List<StormwaterJurisdictionSimple> StormwaterJurisdictionsCurrentPersonCanManage;

            public EditViewDataForAngular(List<StormwaterJurisdiction> allStormwaterJurisdictions, List<StormwaterJurisdiction> stormwaterJurisdictions)
            {
                AllStormwaterJurisdictions = allStormwaterJurisdictions.OrderBy(x => x.Organization.DisplayName).Select(x => new StormwaterJurisdictionSimple(x)).ToList();
                StormwaterJurisdictionsCurrentPersonCanManage = stormwaterJurisdictions.OrderBy(x => x.Organization.DisplayName).Select(x => new StormwaterJurisdictionSimple(x)).ToList();
            }
        }
    }

}
