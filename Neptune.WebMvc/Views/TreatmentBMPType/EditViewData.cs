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

using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Views.Shared;

namespace Neptune.WebMvc.Views.TreatmentBMPType
{
    public class EditViewData : NeptuneViewData
    {
        public ViewDataForAngular ViewDataForAngular { get; } 
        public string TreatmentBMPTypeIndexUrl { get; }
        public string SubmitUrl { get; }
        public ViewPageContentViewData ViewInstructionsNeptunePage { get; }

        public EditViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            IEnumerable<TreatmentBMPTypeAssessmentObservationType> observationTypes, string submitUrl,
            EFModels.Entities.NeptunePage instructionsNeptunePage, EFModels.Entities.TreatmentBMPType treatmentBMPType,
            IEnumerable<TreatmentBMPTypeCustomAttributeType> customAttributeTypes,
            IEnumerable<EFModels.Entities.TreatmentBMPAssessmentObservationType> allObservationTypes,
            IEnumerable<EFModels.Entities.CustomAttributeType> allCustomAttributeTypes) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabelPluralized();
            EntityUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Manage());
            if (treatmentBMPType != null)
            {
                SubEntityName = treatmentBMPType.TreatmentBMPTypeName;
                SubEntityUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMPType));
            }
            PageTitle = $"{(treatmentBMPType != null ? "Edit" : "New")} {FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabel()}";

            ViewDataForAngular = new ViewDataForAngular(observationTypes, customAttributeTypes, allObservationTypes, allCustomAttributeTypes);
            TreatmentBMPTypeIndexUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Manage());
            SubmitUrl = submitUrl;
            ViewInstructionsNeptunePage = new ViewPageContentViewData(linkGenerator, instructionsNeptunePage, currentPerson);
        }
    }

    public class ViewDataForAngular
    {
        public List<TreatmentBMPAssessmentObservationTypeDetailedDto> TreatmentBMPAssessmentObservationTypes { get; }
        public List<CustomAttributeTypeSimpleDto> CustomAttributeTypes { get; }


        public ViewDataForAngular(IEnumerable<TreatmentBMPTypeAssessmentObservationType> observationTypes,
            IEnumerable<TreatmentBMPTypeCustomAttributeType> customAttributeTypes, IEnumerable<EFModels.Entities.TreatmentBMPAssessmentObservationType> allObservationTypes, IEnumerable<EFModels.Entities.CustomAttributeType> allCustomAttributeTypes)
        {
            TreatmentBMPAssessmentObservationTypes = allObservationTypes.Select(x =>
            {
                var treatmentBMPAssessmentObservationTypeDetailedDto = x.AsDetailedDto();
                treatmentBMPAssessmentObservationTypeDetailedDto.TreatmentBMPAssessmentObservationTypeSortOrder = observationTypes.SingleOrDefault(y =>
                        y.TreatmentBMPAssessmentObservationTypeID == x.TreatmentBMPAssessmentObservationTypeID)
                    ?.SortOrder;
                return treatmentBMPAssessmentObservationTypeDetailedDto;
            }).ToList();

            CustomAttributeTypes = allCustomAttributeTypes.Select(x =>
                {
                    var customAttributeTypeSimpleDto = x.AsSimpleDto();
                    customAttributeTypeSimpleDto.CustomAttributeTypeSortOrder = customAttributeTypes
                        .SingleOrDefault(y => y.CustomAttributeTypeID == x.CustomAttributeTypeID)?.SortOrder;
                    return customAttributeTypeSimpleDto;
                }
            ).ToList();
        }
    }
}
