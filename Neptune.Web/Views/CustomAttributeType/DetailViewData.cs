/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TreatmentBMPType;

namespace Neptune.Web.Views.CustomAttributeType
{
    public class DetailViewData : NeptuneViewData
    {
        public EFModels.Entities.CustomAttributeType CustomAttributeType { get; }
        public bool UserHasCustomAttributeTypeManagePermissions { get; }
        public TreatmentBMPTypeGridSpec TreatmentBMPTypeGridSpec { get; }
        public string TreatmentBMPTypeGridName { get; }
        public string TreatmentBMPTypeGridDataUrl { get; }
        public string EditUrl { get; }

        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.CustomAttributeType customAttributeType, Dictionary<int, int> countByTreatmentBMPType) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            CustomAttributeType = customAttributeType;
            EntityName = FieldDefinitionType.CustomAttributeType.GetFieldDefinitionLabelPluralized();
            PageTitle = customAttributeType.CustomAttributeTypeName;

            UserHasCustomAttributeTypeManagePermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);

            if (UserHasCustomAttributeTypeManagePermissions)
            {
                EntityUrl = SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Manage());
            }

            TreatmentBMPTypeGridSpec = new TreatmentBMPTypeGridSpec(linkGenerator, currentPerson, countByTreatmentBMPType)
            {
                ObjectNameSingular = $"{FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true
            };

            TreatmentBMPTypeGridName = "treatmentBMPTypeGridForAttribute";
            TreatmentBMPTypeGridDataUrl = SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(linkGenerator, x => x.TreatmentBMPTypeGridJsonData(customAttributeType));
            EditUrl = SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Edit(customAttributeType));
        }
    }
}
