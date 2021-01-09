﻿/*-----------------------------------------------------------------------
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

namespace Neptune.Web.Views.TreatmentBMPType
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.TreatmentBMPType TreatmentBMPType { get; }
        public bool UserHasTreatmentBMPTypeManagePermissions { get; }
        public string ObservationTypeSortOrderUrl { get; set; }
        public bool CurrentPersonIsAnonymousOrUnassigned { get; set; }

        public DetailViewData(Person currentPerson,
            Models.TreatmentBMPType treatmentBMPType) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            CurrentPersonIsAnonymousOrUnassigned = currentPerson.IsAnonymousOrUnassigned();
            TreatmentBMPType = treatmentBMPType;
            EntityName = Models.FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabelPluralized();
            PageTitle = treatmentBMPType.TreatmentBMPTypeName;

            UserHasTreatmentBMPTypeManagePermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);

            EntityUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(c => c.Index());

            ObservationTypeSortOrderUrl =
                SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(x =>
                    x.EditObservationTypesSortOrder(TreatmentBMPType));

        }

        public string AttributeTypeSortOrderUrl(int attributeTypePurposeID)
        {
            return SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(x =>
                x.EditAttributeTypesSortOrder(TreatmentBMPType, attributeTypePurposeID));
        }
    }
}