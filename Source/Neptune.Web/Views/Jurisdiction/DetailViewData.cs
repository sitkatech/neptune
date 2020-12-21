/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency">
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

using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.TreatmentBMP;
using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Security;

namespace Neptune.Web.Views.Jurisdiction
{
    public class DetailViewData : NeptuneViewData
    {
        public readonly StormwaterJurisdiction StormwaterJurisdiction;

        public readonly TreatmentBMPGridSpec TreatmentBMPGridSpec;
        public readonly string TreatmentBMPGridName;
        public readonly string TreatmentBMPGridDataUrl;
        public readonly string EditStormwaterJurisdictionUrl;
        public readonly bool UserHasJurisdictionManagePermissions;

        public readonly List<Person> UsersAssignedToJurisdiction;


        public DetailViewData(Person currentPerson, StormwaterJurisdiction stormwaterJurisdiction) : base(currentPerson, NeptuneArea.OCStormwaterTools) 
        {
            StormwaterJurisdiction = stormwaterJurisdiction;
            PageTitle = stormwaterJurisdiction.GetOrganizationDisplayName();
            EntityName = $"{Models.FieldDefinition.Jurisdiction.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(x => x.Index());
            EditStormwaterJurisdictionUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(c => c.Edit(stormwaterJurisdiction));


            TreatmentBMPGridSpec = new TreatmentBMPGridSpec(currentPerson, false, false)
            {
                ObjectNameSingular = "Treatment BMP",
                ObjectNamePlural = $"Treatment BMPs for {stormwaterJurisdiction.GetOrganizationDisplayName()}",
                SaveFiltersInCookie = true
            };
            TreatmentBMPGridName = "jurisdictionTreatmentBMPGrid";
            TreatmentBMPGridDataUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(x => x.JurisdictionTreatmentBMPGridJsonData(stormwaterJurisdiction));

            UsersAssignedToJurisdiction = StormwaterJurisdiction.PeopleWhoCanManageStormwaterJurisdictionExceptSitka().ToList();   
            UserHasJurisdictionManagePermissions = new JurisdictionManageFeature().HasPermissionByPerson(CurrentPerson);
        }

        
    }
}
