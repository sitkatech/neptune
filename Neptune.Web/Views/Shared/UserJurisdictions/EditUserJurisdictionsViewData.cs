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

using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.Web.Views.Shared.UserJurisdictions
{
    public class EditUserJurisdictionsViewData : NeptuneViewData
    {
        
        public EditViewDataForAngular ViewDataForAngular { get; }

        public bool Standalone { get; }

        public EditUserJurisdictionsViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, List<StormwaterJurisdiction> allStormwaterJurisdictions, List<StormwaterJurisdiction> stormwaterJurisdictionsCurrentPersonCanManage, bool standalone)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            ViewDataForAngular = new EditViewDataForAngular(allStormwaterJurisdictions, stormwaterJurisdictionsCurrentPersonCanManage);
            Standalone = standalone;
        }

        public class EditViewDataForAngular
        {
            public List<StormwaterJurisdictionDisplayDto> AllStormwaterJurisdictions { get; }
            public List<StormwaterJurisdictionDisplayDto> StormwaterJurisdictionsCurrentPersonCanManage { get; }

            public EditViewDataForAngular(List<StormwaterJurisdiction> allStormwaterJurisdictions, List<StormwaterJurisdiction> stormwaterJurisdictions)
            {
                AllStormwaterJurisdictions = allStormwaterJurisdictions.OrderBy(x => x.Organization.GetDisplayName()).Select(x => x.AsDisplayDto()).ToList();
                StormwaterJurisdictionsCurrentPersonCanManage = stormwaterJurisdictions.OrderBy(x => x.Organization.GetDisplayName()).Select(x => x.AsDisplayDto()).ToList();
            }
        }
    }

}
