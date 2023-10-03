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
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.TreatmentBMPAssessmentObservationType
{
    public class EditViewData : NeptuneViewData
    {
        public ViewDataForAngular ViewDataForAngular { get; } 
        public string ObservationTypeCancelUrl { get; }
        public string SubmitUrl { get; }

        public int PassFailObservationThresholdTypeID { get; }
        public int PassFailObservationTargetTypeID { get; }

        public ViewPageContentViewData ViewInstructionsNeptunePage { get; }
        public ViewPageContentViewData ViewObservationInstructionsNeptunePage { get; }
        public ViewPageContentViewData ViewLabelsAndUnitsInstructionsNeptunePage { get; }

        public EditViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, List<MeasurementUnitType> measurementUnitTypes,
            List<ObservationTypeSpecification> observationTypeSpecifications,
            List<ObservationThresholdType> observationThresholdTypes,
            List<ObservationTargetType> observationTargetTypes,
            List<ObservationTypeCollectionMethod> observationTypeCollectionMethods, string submitUrl,
            EFModels.Entities.NeptunePage instructionsNeptunePage, EFModels.Entities.NeptunePage observationInstructionsNeptunePage,
            EFModels.Entities.NeptunePage labelAndUnitsInstructionsNeptunePage, EFModels.Entities.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = "Observation Type";
            EntityUrl = SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            PageTitle = $"{(treatmentBMPAssessmentObservationType != null ? "Edit" : "New")} Observation Type";

            if (treatmentBMPAssessmentObservationType != null)
            {
                SubEntityName = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName;
                SubEntityUrl = SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(treatmentBMPAssessmentObservationType));
            }

            var previewUrl = SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, x => x.PreviewObservationType());
            ViewDataForAngular = new ViewDataForAngular(observationTypeSpecifications, observationTypeCollectionMethods, observationThresholdTypes, observationTargetTypes, measurementUnitTypes, previewUrl);
            ObservationTypeCancelUrl = treatmentBMPAssessmentObservationType == null
                ? SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Index())
                : SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, x =>
                    x.Detail(treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID));
            SubmitUrl = submitUrl;

            PassFailObservationThresholdTypeID = ObservationThresholdType.None.ObservationThresholdTypeID;
            PassFailObservationTargetTypeID = ObservationTargetType.PassFail.ObservationTargetTypeID;

            ViewInstructionsNeptunePage = new ViewPageContentViewData(linkGenerator, instructionsNeptunePage, currentPerson);
            ViewObservationInstructionsNeptunePage = new ViewPageContentViewData(linkGenerator, observationInstructionsNeptunePage, currentPerson);
            ViewLabelsAndUnitsInstructionsNeptunePage = new ViewPageContentViewData(linkGenerator, labelAndUnitsInstructionsNeptunePage, currentPerson);
        }
    }

    public class ViewDataForAngular
    {
        public List<ObservationTypeSpecificationSimple> ObservationTypeSpecificationSimples { get; }
        public List<ObservationTypeCollectionMethodSimple> ObservationTypeCollectionMethods { get; }
        public List<SelectItemSimple> ObservationThresholdTypes { get; }
        public List<SelectItemSimple> ObservationTargetTypes { get; }
        public List<SelectItemSimple> MeasurementUnitTypes { get; }
        public int DiscreteObservationTypeCollectionMethodID { get; }
        public int PassFailObservationTypeCollectionMethodID { get; }
        public int PercentageObservationTypeCollectionMethodID { get; }
        public string PreviewUrl { get; set; }

        public ViewDataForAngular(List<ObservationTypeSpecification> observationTypeSpecifications,
            List<ObservationTypeCollectionMethod> observationTypeCollectionMethods,
            List<ObservationThresholdType> observationThresholdTypes,
            List<ObservationTargetType> observationTargetTypes,
            List<MeasurementUnitType> measurementUnitTypes, string previewUrl)
        {
            ObservationTypeSpecificationSimples = observationTypeSpecifications.Select(x => new ObservationTypeSpecificationSimple(x)).ToList();
            ObservationTypeCollectionMethods = observationTypeCollectionMethods.Select(x => new ObservationTypeCollectionMethodSimple(x)).ToList();
            ObservationThresholdTypes = observationThresholdTypes.Select(x => new SelectItemSimple(x.ObservationThresholdTypeID, x.ObservationThresholdTypeDisplayName)).ToList();
            ObservationTargetTypes = observationTargetTypes.Select(x => new SelectItemSimple(x.ObservationTargetTypeID, x.ObservationTargetTypeDisplayName)).ToList();
            MeasurementUnitTypes = measurementUnitTypes.Select(x => new SelectItemSimple(x.MeasurementUnitTypeID, x.MeasurementUnitTypeDisplayName)).ToList();

            DiscreteObservationTypeCollectionMethodID = ObservationTypeCollectionMethod.DiscreteValue.ObservationTypeCollectionMethodID;
            PassFailObservationTypeCollectionMethodID = ObservationTypeCollectionMethod.PassFail.ObservationTypeCollectionMethodID;
            PercentageObservationTypeCollectionMethodID = ObservationTypeCollectionMethod.Percentage.ObservationTypeCollectionMethodID;
            PreviewUrl = previewUrl;
        }
    }

    public class ObservationTypeCollectionMethodSimple
    {
        public int ID { get; }
        public string DisplayName { get; }
        public bool HasBenchmarkAndThresholds { get; }
        public string Definition { get; }

        public ObservationTypeCollectionMethodSimple(ObservationTypeCollectionMethod observationTypeCollectionMethod)
        {
            ID = observationTypeCollectionMethod.ObservationTypeCollectionMethodID;
            DisplayName = observationTypeCollectionMethod.ObservationTypeCollectionMethodDisplayName;
            HasBenchmarkAndThresholds = observationTypeCollectionMethod != ObservationTypeCollectionMethod.PassFail;
            Definition = observationTypeCollectionMethod.ObservationTypeCollectionMethodDescription;
        }
    }

    public class SelectItemSimple
    {
        public int ID { get; }
        public string DisplayName { get; }

        public SelectItemSimple(int id, string displayName)
        {
            ID = id;
            DisplayName = displayName;
        }
    }

    public class ObservationTypeSpecificationSimple
    {
        public int ObservationTypeCollectionMethodID { get; }
        public int ObservationTargetTypeID { get; }
        public int ObservationThresholdTypeID { get; }
        
        public ObservationTypeSpecificationSimple(ObservationTypeSpecification observationTypeSpecification)
        {
            ObservationTypeCollectionMethodID = observationTypeSpecification.ObservationTypeCollectionMethodID;
            ObservationTargetTypeID = observationTypeSpecification.ObservationTargetTypeID;
            ObservationThresholdTypeID = observationTypeSpecification.ObservationThresholdTypeID;
        }
    }
}
