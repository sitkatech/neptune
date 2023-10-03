/*-----------------------------------------------------------------------
<copyright file="MaterialAccumulationViewData.cs" company="Tahoe Regional Planning Agency">
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

using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.Shared.SortOrder;
using Neptune.Web.Views.TreatmentBMPAssessmentObservationType;

namespace Neptune.Web.Views.FieldVisit
{
    public class ObservationsViewData : FieldVisitSectionViewData
    {
        public ObservationsViewDataForAngular ViewDataForAngular { get; }
        public string SubmitUrl { get; }    
        public TreatmentBMPAssessmentTypeEnum TreatmentBMPAssessmentTypeEnum { get; set; }

        public ObservationsViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.FieldVisit fieldVisit,
            List<EFModels.Entities.TreatmentBMPAssessment> treatmentBMPAssessments,
            EFModels.Entities.MaintenanceRecord? maintenanceRecord,
            EFModels.Entities.TreatmentBMPType treatmentBMPType,
            TreatmentBMPAssessmentTypeEnum treatmentBMPAssessmentTypeEnum)
            : base(httpContext, linkGenerator, webConfiguration, currentPerson, fieldVisit, treatmentBMPAssessmentTypeEnum == TreatmentBMPAssessmentTypeEnum.Initial ? EFModels.Entities.FieldVisitSection.Assessment : EFModels.Entities.FieldVisitSection.PostMaintenanceAssessment, treatmentBMPType, maintenanceRecord, treatmentBMPAssessments)
        {
            var initialAssessmentObservations = InitialAssessment?.TreatmentBMPObservations.Select(x =>
                new CollectionMethodSectionViewModel(x, x.TreatmentBMPAssessmentObservationType)).ToList();
            SubsectionName = "Observations";
            SectionHeader = "Observations";
            TreatmentBMPAssessmentTypeEnum = treatmentBMPAssessmentTypeEnum;
            ViewDataForAngular = new ObservationsViewDataForAngular(treatmentBMPType, initialAssessmentObservations);
            SubmitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Observations(fieldVisit, treatmentBMPAssessmentTypeEnum));
        }

        public class ObservationsViewDataForAngular
        {
            public ObservationsViewDataForAngular()
            {
            }

            public ObservationsViewDataForAngular(EFModels.Entities.TreatmentBMPType treatmentBMPType, List<CollectionMethodSectionViewModel>? initialAssessmentObservations)
            {
                ObservationTypeSchemas = treatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes.SortByOrderThenName()
                    .Select(x => new ObservationTypeSchema(x.TreatmentBMPAssessmentObservationType, initialAssessmentObservations)).ToList();
            }

            public List<ObservationTypeSchema> ObservationTypeSchemas { get; }
        }

        public class ObservationTypeSchema
        {
            public int TreatmentBMPAssessmentObservationTypeID { get; }
            public string TreatmentBMPAssessmentObservationTypeName { get; }
            public ObservationTypeCollectionMethodEnum ObservationTypeCollectionMethod { get; }
            public string MeasurementUnitLabelAndUnit { get; }
            public string PassingScoreLabel { get; }
            public string FailingScoreLabel { get; }
            public string AssessmentDescription { get; }

            public List<SelectItemSimple> PropertiesToObserve { get; }
            public int MinimumNumberOfObservations { get; }
            public int MaximumNumberOfObservations { get; }
            public double MinimumValueOfObservations { get; }
            public double MaximumValueOfObservations { get; }

            public List<CollectionMethodSectionViewModel>? InitialAssessmentObservations { get; set; }

            public ObservationTypeSchema(EFModels.Entities.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, List<CollectionMethodSectionViewModel>? initialAssessmentObservations)
            {
                InitialAssessmentObservations = initialAssessmentObservations;
                TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID;
                TreatmentBMPAssessmentObservationTypeName = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName;
                ObservationTypeCollectionMethod = treatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethod.ToEnum;

                switch (ObservationTypeCollectionMethod)
                {
                    case ObservationTypeCollectionMethodEnum.DiscreteValue:
                        var discreteObservationTypeSchema = treatmentBMPAssessmentObservationType.GetDiscreteObservationTypeSchema();
                        PropertiesToObserve = discreteObservationTypeSchema.PropertiesToObserve
                            .Select((x, index) => new SelectItemSimple(index, x)).ToList();
                        AssessmentDescription = discreteObservationTypeSchema.AssessmentDescription;
                        MinimumNumberOfObservations = discreteObservationTypeSchema.MinimumNumberOfObservations;
                        MaximumNumberOfObservations = discreteObservationTypeSchema.MaximumNumberOfObservations ?? int.MaxValue;
                        MinimumValueOfObservations = discreteObservationTypeSchema.MinimumValueOfObservations;
                        MaximumValueOfObservations = discreteObservationTypeSchema.MaximumValueOfObservations ?? double.MaxValue;
                        MeasurementUnitLabelAndUnit = $"{discreteObservationTypeSchema.MeasurementUnitLabel} ({treatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitType().LegendDisplayName})";
                        break;

                    case ObservationTypeCollectionMethodEnum.PassFail:
                        var passFailObservationTypeSchema = treatmentBMPAssessmentObservationType.GetPassFailSchema();
                        PropertiesToObserve = passFailObservationTypeSchema.PropertiesToObserve
                            .Select((x, index) => new SelectItemSimple(index, x)).ToList();
                        AssessmentDescription = passFailObservationTypeSchema.AssessmentDescription;
                        PassingScoreLabel = passFailObservationTypeSchema.PassingScoreLabel;
                        FailingScoreLabel = passFailObservationTypeSchema.FailingScoreLabel;
                        break;
                    case ObservationTypeCollectionMethodEnum.Percentage:
                        var percentageObservationTypeSchema = treatmentBMPAssessmentObservationType.GetPercentageSchema();
                        PropertiesToObserve = percentageObservationTypeSchema.PropertiesToObserve
                            .Select((x, index) => new SelectItemSimple(index, x)).ToList();
                        AssessmentDescription = percentageObservationTypeSchema.AssessmentDescription;
                        MeasurementUnitLabelAndUnit = $"{treatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitLabel()} ({treatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitType().LegendDisplayName})";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
