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

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common;
using LtInfo.Common.Mvc;
using LtInfo.Common.Views;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.TreatmentBMPAssessmentObservationType;
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

        public EditViewData(Person currentPerson, List<MeasurementUnitType> measurementUnitTypes,
            List<CustomAttributeDataType> customAttributeDataTypes, string submitUrl,
            Models.NeptunePage instructionsNeptunePage, Models.NeptunePage customAttributeInstructionsNeptunePage,
            Models.CustomAttributeType customAttributeType) : base(currentPerson)
        {
            EntityName = Models.FieldDefinition.CustomAttributeType.GetFieldDefinitionLabelPluralized();
            EntityUrl = SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(x => x.Manage());
            PageTitle =
                $"{(customAttributeType != null ? "Edit" : "New")} {Models.FieldDefinition.CustomAttributeType.GetFieldDefinitionLabel()}";

            if (customAttributeType != null)
            {
                SubEntityName = customAttributeType.CustomAttributeTypeName;
                SubEntityUrl = customAttributeType.GetDetailUrl();
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
                SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(x => x.Manage());
            SubmitUrl = submitUrl;

            ViewInstructionsNeptunePage = new ViewPageContentViewData(instructionsNeptunePage, currentPerson);
            ViewCustomAttributeInstructionsNeptunePage =
                new ViewPageContentViewData(customAttributeInstructionsNeptunePage, currentPerson);

            ViewDataForAngular = new ViewDataForAngular(customAttributeDataTypes);
        }
    }

    public class ViewDataForAngular
    {
        public List<CustomAttributeDataTypeSimple> CustomAttributeDataTypes { get; }
       
        public ViewDataForAngular(List<CustomAttributeDataType> customAttributeDataTypes)           
        {
            CustomAttributeDataTypes = customAttributeDataTypes.Select(x => new CustomAttributeDataTypeSimple(x)).ToList();
        }
    }

    public class CustomAttributeDataTypeSimple
    {
        public int ID { get; }
        public string DisplayName { get; }
        public bool HasOptions { get; }
        public bool HasMeasurementUnit { get; }

        public CustomAttributeDataTypeSimple(CustomAttributeDataType customAttributeDataType)
        {
            ID = customAttributeDataType.CustomAttributeDataTypeID;
            DisplayName = customAttributeDataType.CustomAttributeDataTypeDisplayName;
            HasOptions = customAttributeDataType.HasOptions();
            HasMeasurementUnit = customAttributeDataType.HasMeasurementUnit();
        }
    }
}
