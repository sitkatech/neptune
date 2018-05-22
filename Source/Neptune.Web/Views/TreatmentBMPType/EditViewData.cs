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
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.TreatmentBMPType
{
    public class EditViewData : NeptuneViewData
    {
        public ViewDataForAngular ViewDataForAngular { get; } 
        public string TreatmentBMPTypeIndexUrl { get; }
        public string SubmitUrl { get; }
        public ViewPageContentViewData ViewInstructionsNeptunePage { get; }

        public EditViewData(Person currentPerson, IEnumerable<TreatmentBMPTypeAssessmentObservationType> observationTypes, string submitUrl,
            Models.NeptunePage instructionsNeptunePage, Models.TreatmentBMPType treatmentBMPType,
            IEnumerable<TreatmentBMPTypeCustomAttributeType> customAttributeTypes) : base(currentPerson)
        {
            EntityName = Models.FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabelPluralized();
            EntityUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(c => c.Manage());
            if (treatmentBMPType != null)
            {
                SubEntityName = treatmentBMPType.TreatmentBMPTypeName;
                SubEntityUrl = treatmentBMPType.GetDetailUrl();
            }
            PageTitle = $"{(treatmentBMPType != null ? "Edit" : "New")} {Models.FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabel()}";

            ViewDataForAngular = new ViewDataForAngular(observationTypes, customAttributeTypes);
            TreatmentBMPTypeIndexUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(x => x.Manage());
            SubmitUrl = submitUrl;
            ViewInstructionsNeptunePage = new ViewPageContentViewData(instructionsNeptunePage, currentPerson);
        }
    }

    public class ViewDataForAngular
    {
        public List<TreatmentBMPTypeAssessmentObservationTypeSimple> TreatmentBMPAssessmentObservationTypes { get; }
        public List<CustomAttributeTypeSimple> CustomAttributeTypes { get; }


        public ViewDataForAngular(IEnumerable<Models.TreatmentBMPTypeAssessmentObservationType> observationTypes,
            IEnumerable<Models.TreatmentBMPTypeCustomAttributeType> customAttributeTypes)
        {
            TreatmentBMPAssessmentObservationTypes = observationTypes.Select(x => new TreatmentBMPTypeAssessmentObservationTypeSimple(x.TreatmentBMPAssessmentObservationType){TreatmentBMPAssessmentObservationTypeSortOrder = x.SortOrder}).ToList();
            CustomAttributeTypes = customAttributeTypes.Select(x => new CustomAttributeTypeSimple(x.CustomAttributeType){ CustomAttributeTypeSortOrder = x.SortOrder}).ToList();
        }
    }
}
