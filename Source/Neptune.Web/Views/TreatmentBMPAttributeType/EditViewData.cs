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
using Neptune.Web.Views.ObservationType;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.TreatmentBMPAttributeType
{
    public class EditViewData : NeptuneViewData
    {
        public string TreatmentBMPAttributeTypeIndexUrl { get; }
        public string SubmitUrl { get; }

        public ViewPageContentViewData ViewInstructionsNeptunePage { get; }
        public ViewPageContentViewData ViewTreatmentBMPAttributeInstructionsNeptunePage { get; }

        public IEnumerable<SelectListItem> TreatmentBMPAttributeDataTypes { get; }
        public IEnumerable<SelectListItem> MeasurementUnitTypes { get; }
        public IEnumerable<SelectListItem> TreatmentBMPAttributeTypePurposes { get; }
        public IEnumerable<SelectListItem> YesNos { get; }
        public ViewDataForAngular ViewDataForAngular { get; }

        public EditViewData(Person currentPerson, List<MeasurementUnitType> measurementUnitTypes,
            List<TreatmentBMPAttributeDataType> treatmentBMPAttributeDataTypes, string submitUrl,
            Models.NeptunePage instructionsNeptunePage, Models.NeptunePage treatmentBMPAttributeInstructionsNeptunePage,
            Models.TreatmentBMPAttributeType treatmentBMPAttributeType) : base(currentPerson)
        {
            EntityName = Models.FieldDefinition.TreatmentBMPAttributeType.GetFieldDefinitionLabelPluralized();
            EntityUrl = SitkaRoute<TreatmentBMPAttributeTypeController>.BuildUrlFromExpression(x => x.Manage());
            PageTitle =
                $"{(treatmentBMPAttributeType != null ? "Edit" : "New")} {Models.FieldDefinition.TreatmentBMPAttributeType.GetFieldDefinitionLabel()}";

            if (treatmentBMPAttributeType != null)
            {
                SubEntityName = treatmentBMPAttributeType.TreatmentBMPAttributeTypeName;
                SubEntityUrl = treatmentBMPAttributeType.GetDetailUrl();
            }

            YesNos = BooleanFormats.GetYesNoSelectList();
            TreatmentBMPAttributeDataTypes = treatmentBMPAttributeDataTypes.ToSelectListWithDisabledEmptyFirstRow(
                x => x.TreatmentBMPAttributeDataTypeID.ToString(), x => x.TreatmentBMPAttributeDataTypeDisplayName);
            MeasurementUnitTypes = measurementUnitTypes.OrderBy(x => x.MeasurementUnitTypeDisplayName).ToSelectListWithEmptyFirstRow(
                x => x.MeasurementUnitTypeID.ToString(), x => x.MeasurementUnitTypeDisplayName, ViewUtilities.NoneString);
            TreatmentBMPAttributeTypePurposes =
                TreatmentBMPAttributeTypePurpose.All.ToSelectListWithDisabledEmptyFirstRow(
                    x => x.TreatmentBMPAttributeTypePurposeID.ToString(CultureInfo.InvariantCulture),
                    x => x.TreatmentBMPAttributeTypePurposeDisplayName);

            TreatmentBMPAttributeTypeIndexUrl =
                SitkaRoute<TreatmentBMPAttributeTypeController>.BuildUrlFromExpression(x => x.Manage());
            SubmitUrl = submitUrl;

            ViewInstructionsNeptunePage = new ViewPageContentViewData(instructionsNeptunePage, currentPerson);
            ViewTreatmentBMPAttributeInstructionsNeptunePage =
                new ViewPageContentViewData(treatmentBMPAttributeInstructionsNeptunePage, currentPerson);

            ViewDataForAngular = new ViewDataForAngular(treatmentBMPAttributeDataTypes);
        }
    }

    public class ViewDataForAngular
    {
        public List<TreatmentBMPAttributeDataTypeSimple> TreatmentBMPAttributeDataTypes { get; }
       
        public ViewDataForAngular(List<TreatmentBMPAttributeDataType> treatmentBMPAttributeDataTypes)           
        {
            TreatmentBMPAttributeDataTypes = treatmentBMPAttributeDataTypes.Select(x => new TreatmentBMPAttributeDataTypeSimple(x)).ToList();
        }
    }

    public class TreatmentBMPAttributeDataTypeSimple
    {
        public int ID { get; }
        public string DisplayName { get; }
        public bool HasOptions { get; }
        public bool HasMeasurementUnit { get; }

        public TreatmentBMPAttributeDataTypeSimple(TreatmentBMPAttributeDataType treatmentBMPAttributeDataType)
        {
            ID = treatmentBMPAttributeDataType.TreatmentBMPAttributeDataTypeID;
            DisplayName = treatmentBMPAttributeDataType.TreatmentBMPAttributeDataTypeDisplayName;
            HasOptions = treatmentBMPAttributeDataType.HasOptions();
            HasMeasurementUnit = treatmentBMPAttributeDataType.HasMeasurementUnit();
        }
    }
}
