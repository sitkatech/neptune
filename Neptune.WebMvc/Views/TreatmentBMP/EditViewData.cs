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

using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Common.Mvc;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.TreatmentBMP
{
    public class EditViewData : NeptuneViewData
    {
        public EFModels.Entities.TreatmentBMP TreatmentBMP { get; }
        public string TreatmentBMPIndexUrl { get; }
        public IEnumerable<SelectListItem> StormwaterJurisdictionSelectListItems { get; }
        public IEnumerable<SelectListItem> TreatmentBMPTypeSelectListItems { get; }
        public IEnumerable<SelectListItem> OwnerOrganizationSelectListItems { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanSelectListItems { get; }
        public IEnumerable<SelectListItem> TreatmentBMPLifespanTypes { get; }
        public IEnumerable<SelectListItem>  TrashCaptureStatusTypes { get; }
        public IEnumerable<SelectListItem> SizingBasisTypes { get; }

        public EditViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.TreatmentBMP treatmentBMP,
            IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions,
            IEnumerable<EFModels.Entities.TreatmentBMPType> treatmentBMPTypes,
            IEnumerable<EFModels.Entities.Organization> organizations,
            IEnumerable<EFModels.Entities.WaterQualityManagementPlan> waterQualityManagementPlans,
            IEnumerable<TreatmentBMPLifespanType> treatmentBMPLifespanTypes,
            IEnumerable<TrashCaptureStatusType> trashCaptureStatusTypes, IEnumerable<SizingBasisType> sizingBasisTypes)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var treatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.FindABMP());
            EntityUrl = treatmentBMPIndexUrl;

            SubEntityName = treatmentBMP.TreatmentBMPName;
            SubEntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP));
            TreatmentBMP = treatmentBMP;
            TrashCaptureStatusTypes = trashCaptureStatusTypes.ToSelectListWithDisabledEmptyFirstRow(
                x => x.TrashCaptureStatusTypeID.ToString(CultureInfo.InvariantCulture),
                x => x.TrashCaptureStatusTypeDisplayName.ToString(CultureInfo.InvariantCulture));
            SizingBasisTypes = sizingBasisTypes.ToSelectListWithDisabledEmptyFirstRow(
                x => x.SizingBasisTypeID.ToString(CultureInfo.InvariantCulture),
                x => x.SizingBasisTypeDisplayName.ToString(CultureInfo.InvariantCulture));

            PageTitle = $"Edit {FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabel()}";

            StormwaterJurisdictionSelectListItems = stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture),
                    y => y.GetOrganizationDisplayName());
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

        private static IEnumerable<SelectListItem> BuildWaterQualityPlanSelectList(
            IEnumerable<EFModels.Entities.WaterQualityManagementPlan> waterQualityManagementPlans)
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
