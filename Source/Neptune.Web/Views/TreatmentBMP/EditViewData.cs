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
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class EditViewData : NeptuneViewData
    {
        public IEnumerable<SelectListItem> StormwaterJurisdictionSelectListItems { get; }
        public IEnumerable<SelectListItem> TreatmentBMPTypeSelectListItems { get; }
        public Models.TreatmentBMP TreatmentBMP { get; }
        public string TreatmentBMPIndexUrl { get; }
        public IEnumerable<SelectListItem> OwnerOrganizationSelectListItems { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanSelectListItems { get; }
        public IEnumerable<SelectListItem> TreatmentBMPLifespanTypes { get; }

        public EditViewData(Person currentPerson,
            Models.TreatmentBMP treatmentBMP,
            IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions,
            IEnumerable<Models.TreatmentBMPType> treatmentBMPTypes,
            IEnumerable<Models.Organization> organizations,
            IEnumerable<Models.WaterQualityManagementPlan> waterQualityManagementPlans, IEnumerable<TreatmentBMPLifespanType> treatmentBMPLifespanTypes)
            : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP)
        {
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var treatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.FindABMP());
            EntityUrl = treatmentBMPIndexUrl;

            SubEntityName = treatmentBMP?.TreatmentBMPName;
            SubEntityUrl = treatmentBMP?.GetDetailUrl();
            TreatmentBMP = treatmentBMP;

            PageTitle = $"Edit {Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabel()}";

            StormwaterJurisdictionSelectListItems = stormwaterJurisdictions.OrderBy(x => x.OrganizationDisplayName)
                .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture),
                    y => y.OrganizationDisplayName);
            TreatmentBMPTypeSelectListItems = treatmentBMPTypes.OrderBy(x => x.TreatmentBMPTypeName)
                .ToSelectListWithEmptyFirstRow(x => x.TreatmentBMPTypeID.ToString(CultureInfo.InvariantCulture),
                    y => y.TreatmentBMPTypeName);
            OwnerOrganizationSelectListItems = organizations.OrderBy(x => x.GetDisplayName())
                .ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture),
                    y => y.GetDisplayName(), "Same as the BMP Jurisdiction");
            TreatmentBMPIndexUrl = treatmentBMPIndexUrl;
            WaterQualityManagementPlanSelectListItems = BuildWaterQualityPlanSelectList(waterQualityManagementPlans);

            TreatmentBMPLifespanTypes = treatmentBMPLifespanTypes.ToSelectListWithEmptyFirstRow(
                x => x.TreatmentBMPLifespanTypeID.ToString(CultureInfo.InvariantCulture), x => x.TreatmentBMPLifespanTypeDisplayName.ToString(CultureInfo.InvariantCulture), "Unknown");
        }

        private IEnumerable<SelectListItem> BuildWaterQualityPlanSelectList(
            IEnumerable<Models.WaterQualityManagementPlan> waterQualityManagementPlans)
        {
            var selectListItems = waterQualityManagementPlans
                .ToSelectList(x => x.WaterQualityManagementPlanID.ToString(), x => x.WaterQualityManagementPlanName)
                .ToList();
            selectListItems.Insert(0,
                new SelectListItem {Text = "No Associated WQMP", Value = string.Empty});
            return selectListItems;
        }
    }
}
