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
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
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

            TreatmentBMPAttributeDataTypes = treatmentBMPAttributeDataTypes.ToSelectListWithEmptyFirstRow(x => x.TreatmentBMPAttributeDataTypeID.ToString(), x => x.TreatmentBMPAttributeDataTypeDisplayName);
            MeasurementUnitTypes = measurementUnitTypes.ToSelectListWithEmptyFirstRow(x => x.MeasurementUnitTypeID.ToString(), x => x.MeasurementUnitTypeDisplayName, "None");

            TreatmentBMPAttributeTypeIndexUrl = SitkaRoute<TreatmentBMPAttributeTypeController>.BuildUrlFromExpression(x => x.Manage());
            SubmitUrl = submitUrl;

            ViewInstructionsNeptunePage = new ViewPageContentViewData(instructionsNeptunePage, currentPerson);
            ViewTreatmentBMPAttributeInstructionsNeptunePage = new ViewPageContentViewData(treatmentBMPAttributeInstructionsNeptunePage, currentPerson);
        }
    }
}
