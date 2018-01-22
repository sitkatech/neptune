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

using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.EMMA;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMP;

namespace Neptune.Web.Views.Jurisdiction
{
    public class DetailViewData : NeptuneViewData
    {
        public readonly StormwaterJurisdiction StormwaterJurisdiction;

        public readonly JurisdictionModeledCatchmentsGridSpec JurisdictionModeledCatchmentsGridSpec;
        public readonly string JurisdictionModeledCatchmentsGridName;
        public readonly string JurisdictionModeledCatchmentsGridDataUrl;

        public readonly TreatmentBMPGridSpec TreatmentBMPGridSpec;
        public readonly string TreatmentBMPGridName;
        public readonly string TreatmentBMPGridDataUrl;

        public readonly List<Person> UsersAssignedToJurisdiction;


        public DetailViewData(Person currentPerson, StormwaterJurisdiction stormwaterJurisdiction) : base(currentPerson, StormwaterBreadCrumbEntity.Jurisdiction) 
        {
            StormwaterJurisdiction = stormwaterJurisdiction;
            PageTitle = stormwaterJurisdiction.OrganizationDisplayName;
            EntityName = $"{Models.FieldDefinition.Jurisdiction.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(x => x.Index());
            
            JurisdictionModeledCatchmentsGridSpec = new JurisdictionModeledCatchmentsGridSpec(currentPerson)
            {
                ObjectNameSingular = "Modeled Catchment",
                ObjectNamePlural = $"Modeled Catchments associated with {stormwaterJurisdiction.OrganizationDisplayName}",
                SaveFiltersInCookie = true
            };
            JurisdictionModeledCatchmentsGridName = "jurisdictionModeledCatchmentGrid";
            JurisdictionModeledCatchmentsGridDataUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(x => x.JurisdictionModeledCatchmentGridJsonData(stormwaterJurisdiction));

            TreatmentBMPGridSpec = new TreatmentBMPGridSpec(currentPerson)
            {
                ObjectNameSingular = "Treatment BMP",
                ObjectNamePlural = $"Treatment BMPs for {stormwaterJurisdiction.OrganizationDisplayName}",
                SaveFiltersInCookie = true
            };
            TreatmentBMPGridName = "jurisdictionTreatmentBMPGrid";
            TreatmentBMPGridDataUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(x => x.JurisdictionTreatmentBMPGridJsonData(stormwaterJurisdiction));

            UsersAssignedToJurisdiction = StormwaterJurisdiction.PeopleWhoCanManageStormwaterJurisdictionExceptSitka().ToList();           
        }
    }
}
