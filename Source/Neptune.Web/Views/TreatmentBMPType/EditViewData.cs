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

        public EditViewData(Person currentPerson, List<Models.ObservationType> observationTypes, string submitUrl,
            Models.NeptunePage instructionsNeptunePage, Models.TreatmentBMPType treatmentBMPType,
            List<Models.TreatmentBMPAttributeType> treatmentBMPAttributeTypes) : base(currentPerson)
        {
            EntityName = Models.FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabelPluralized();
            EntityUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(c => c.Manage());
            IEnumerable<int> treatmentBMPAttributeTypeIDsWithData = new List<int>();
            IEnumerable<int> observationTypeIDsWithData = new List<int>();
            if (treatmentBMPType != null)
            {
                SubEntityName = treatmentBMPType.TreatmentBMPTypeName;
                SubEntityUrl = treatmentBMPType.GetDetailUrl();
                treatmentBMPAttributeTypeIDsWithData = treatmentBMPType.TreatmentBMPAttributes.Select(x => x.TreatmentBMPAttributeTypeID).Distinct();
                observationTypeIDsWithData = treatmentBMPType.TreatmentBMPObservations.Select(x => x.ObservationTypeID).Distinct();
            }
            PageTitle = $"{(treatmentBMPType != null ? "Edit" : "New")} {Models.FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabel()}";

            ViewDataForAngular = new ViewDataForAngular(observationTypes, treatmentBMPAttributeTypes, observationTypeIDsWithData, treatmentBMPAttributeTypeIDsWithData);
            TreatmentBMPTypeIndexUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(x => x.Manage());
            SubmitUrl = submitUrl;
            ViewInstructionsNeptunePage = new ViewPageContentViewData(instructionsNeptunePage, currentPerson);
        }
    }

    public class ViewDataForAngular
    {
        public IEnumerable<int> ObservationTypeIDsWithData { get; }
        public IEnumerable<int> TreatmentBMPAttributeTypeIDsWithData { get; }

        public List<ObservationTypeSimple> ObservationTypes { get; }
        public List<TreatmentBMPAttributeTypeSimple> TreatmentBMPAttributeTypes { get; }


        public ViewDataForAngular(IEnumerable<Models.ObservationType> observationTypes,
            IEnumerable<Models.TreatmentBMPAttributeType> treatmentBMPAttributeTypes,
            IEnumerable<int> observationTypeIDsWithData,
            IEnumerable<int> treatmentBMPAttributeTypeIDsWithData)
        {
            ObservationTypeIDsWithData = observationTypeIDsWithData;
            TreatmentBMPAttributeTypeIDsWithData = treatmentBMPAttributeTypeIDsWithData;
            ObservationTypes = observationTypes.Select(x => new ObservationTypeSimple(x)).ToList();
            TreatmentBMPAttributeTypes = treatmentBMPAttributeTypes.Select(x => new TreatmentBMPAttributeTypeSimple(x)).ToList();
        }
    }
}
