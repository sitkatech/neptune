/*-----------------------------------------------------------------------
<copyright file="EditViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Common;
using Neptune.Common.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.CustomAttributeType
{
    public class EditViewData : NeptuneViewData
    {
        public string CustomAttributeTypeIndexUrl { get; }
        public string SubmitUrl { get; }

        public ViewPageContentViewData ViewInstructionsNeptunePage { get; }
        public ViewPageContentViewData ViewCustomAttributeInstructionsNeptunePage { get; }

        public IEnumerable<SelectListItem> CustomAttributeDataTypes { get; }
        public IEnumerable<SelectListItem> MeasurementUnitTypes { get; }
        public IEnumerable<SelectListItem> CustomAttributeTypePurposes { get; }
        public IEnumerable<SelectListItem> YesNos { get; }
        public ViewDataForAngular ViewDataForAngular { get; }

        public EditViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, List<MeasurementUnitType> measurementUnitTypes,
            List<CustomAttributeDataType> customAttributeDataTypes, string submitUrl,
            EFModels.Entities.NeptunePage instructionsNeptunePage, EFModels.Entities.NeptunePage customAttributeInstructionsNeptunePage,
            EFModels.Entities.CustomAttributeType customAttributeType) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = "Attribute Type";
            var manageUrl = SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Manage());
            EntityUrl = manageUrl;
            PageTitle = $"{(customAttributeType != null ? "Edit" : "New")} Attribute Type";

            if (customAttributeType != null)
            {
                SubEntityName = customAttributeType.CustomAttributeTypeName;
                SubEntityUrl = SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(customAttributeType));
            }

            YesNos = BooleanFormats.GetYesNoSelectList();
            CustomAttributeDataTypes = customAttributeDataTypes.ToSelectListWithDisabledEmptyFirstRow(
                x => x.CustomAttributeDataTypeID.ToString(), x => x.CustomAttributeDataTypeDisplayName);
            MeasurementUnitTypes = measurementUnitTypes.OrderBy(x => x.MeasurementUnitTypeDisplayName).ToSelectListWithEmptyFirstRow(
                x => x.MeasurementUnitTypeID.ToString(), x => x.MeasurementUnitTypeDisplayName, ViewUtilities.NoneString);
            CustomAttributeTypePurposes =
                CustomAttributeTypePurpose.All.ToSelectListWithDisabledEmptyFirstRow(
                    x => x.CustomAttributeTypePurposeID.ToString(CultureInfo.InvariantCulture),
                    x => x.CustomAttributeTypePurposeDisplayName);

            CustomAttributeTypeIndexUrl =
                manageUrl;
            SubmitUrl = submitUrl;

            ViewInstructionsNeptunePage = new ViewPageContentViewData(linkGenerator, instructionsNeptunePage, currentPerson);
            ViewCustomAttributeInstructionsNeptunePage = new ViewPageContentViewData(linkGenerator, customAttributeInstructionsNeptunePage, currentPerson);

            ViewDataForAngular = new ViewDataForAngular(customAttributeDataTypes);
        }
    }

    public class ViewDataForAngular
    {
        public List<CustomAttributeDataTypeSimpleDto> CustomAttributeDataTypes { get; }
       
        public ViewDataForAngular(IEnumerable<CustomAttributeDataType> customAttributeDataTypes)           
        {
            CustomAttributeDataTypes = customAttributeDataTypes.Select(x => x.AsSimpleDto()).ToList();
        }
    }
}
