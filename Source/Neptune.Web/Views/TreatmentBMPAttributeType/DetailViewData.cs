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

using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TreatmentBMPType;

namespace Neptune.Web.Views.TreatmentBMPAttributeType
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.TreatmentBMPAttributeType TreatmentBMPAttributeType { get; }
        public bool UserHasTreatmentBMPAttributeTypeManagePermissions { get; }
        public TreatmentBMPTypeGridSpec TreatmentBMPTypeGridSpec { get; }
        public string TreatmentBMPTypeGridName { get; }
        public string TreatmentBMPTypeGridDataUrl { get; }

        public DetailViewData(Person currentPerson,
            Models.TreatmentBMPAttributeType treatmentBMPAttributeType) : base(currentPerson)
        {
            TreatmentBMPAttributeType = treatmentBMPAttributeType;
            EntityName = Models.FieldDefinition.TreatmentBMPAttributeType.GetFieldDefinitionLabelPluralized();
            PageTitle = treatmentBMPAttributeType.TreatmentBMPAttributeTypeName;

            UserHasTreatmentBMPAttributeTypeManagePermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);

            if (UserHasTreatmentBMPAttributeTypeManagePermissions)
            {
                EntityUrl = SitkaRoute<TreatmentBMPAttributeTypeController>.BuildUrlFromExpression(c => c.Manage());
            }

            TreatmentBMPTypeGridSpec = new TreatmentBMPTypeGridSpec(currentPerson)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{Models.FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true
            };

            TreatmentBMPTypeGridName = "treatmentBMPTypeGridForAttribute";
            TreatmentBMPTypeGridDataUrl = SitkaRoute<TreatmentBMPAttributeTypeController>.BuildUrlFromExpression(tc => tc.TreatmentBMPTypeGridJsonData(treatmentBMPAttributeType));
        }
    }
}
