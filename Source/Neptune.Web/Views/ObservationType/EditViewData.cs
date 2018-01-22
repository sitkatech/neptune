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

namespace Neptune.Web.Views.ObservationType
{
    public class EditViewData : NeptuneViewData
    {
        public ViewDataForAngular ViewDataForAngular { get; } 
        public string ObservationTypeIndexUrl { get; }
        public string SubmitUrl { get; }

        public int PassFailObservationThresholdTypeID { get; }
        public int PassFailObservationTargetTypeID { get; }

        public ViewPageContentViewData ViewInstructionsNeptunePage { get; }
        public ViewPageContentViewData ViewObservationInstructionsNeptunePage { get; }
        public ViewPageContentViewData ViewLabelsAndUnitsInstructionsNeptunePage { get; }

        public EditViewData(Person currentPerson, List<MeasurementUnitType> measurementUnitTypes,
            List<ObservationTypeSpecification> observationTypeSpecifications,
            List<ObservationThresholdType> observationThresholdTypes,
            List<ObservationTargetType> observationTargetTypes,
            List<ObservationTypeCollectionMethod> observationTypeCollectionMethods, string submitUrl,
            Models.NeptunePage instructionsNeptunePage, Models.NeptunePage observationInstructionsNeptunePage,
            Models.NeptunePage labelAndUnitsInstructionsNeptunePage, Models.ObservationType observationType) : base(currentPerson)
        {
            EntityName = Models.FieldDefinition.ObservationType.GetFieldDefinitionLabelPluralized();
            EntityUrl = SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(x => x.Manage());
            PageTitle = $"{(observationType != null ? "Edit" : "New")} {Models.FieldDefinition.ObservationType.GetFieldDefinitionLabel()}";

            if (observationType != null)
            {
                SubEntityName = observationType.ObservationTypeName;
                SubEntityUrl = observationType.GetDetailUrl();
            }

            ViewDataForAngular = new ViewDataForAngular(observationTypeSpecifications, observationTypeCollectionMethods, observationThresholdTypes, observationTargetTypes, measurementUnitTypes);
            ObservationTypeIndexUrl = SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(x => x.Manage());
            SubmitUrl = submitUrl;

            PassFailObservationThresholdTypeID = ObservationThresholdType.None.ObservationThresholdTypeID;
            PassFailObservationTargetTypeID = ObservationTargetType.PassFail.ObservationTargetTypeID;

            ViewInstructionsNeptunePage = new ViewPageContentViewData(instructionsNeptunePage, currentPerson);
            ViewObservationInstructionsNeptunePage = new ViewPageContentViewData(observationInstructionsNeptunePage, currentPerson);
            ViewLabelsAndUnitsInstructionsNeptunePage = new ViewPageContentViewData(labelAndUnitsInstructionsNeptunePage, currentPerson);
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
        public int RateObservationTypeCollectionMethodID { get; }
        public int PassFailObservationTypeCollectionMethodID { get; }
        public int PercentageObservationTypeCollectionMethodID { get; }

        public ViewDataForAngular(List<ObservationTypeSpecification> observationTypeSpecifications,
            List<ObservationTypeCollectionMethod> observationTypeCollectionMethods,
            List<ObservationThresholdType> observationThresholdTypes,
            List<ObservationTargetType> observationTargetTypes, List<MeasurementUnitType> measurementUnitTypes)
        {
            ObservationTypeSpecificationSimples = observationTypeSpecifications.Select(x => new ObservationTypeSpecificationSimple(x)).ToList();
            ObservationTypeCollectionMethods = observationTypeCollectionMethods.Select(x => new ObservationTypeCollectionMethodSimple(x)).ToList();
            ObservationThresholdTypes = observationThresholdTypes.Select(x => new SelectItemSimple(x.ObservationThresholdTypeID, x.ObservationThresholdTypeDisplayName)).ToList();
            ObservationTargetTypes = observationTargetTypes.Select(x => new SelectItemSimple(x.ObservationTargetTypeID, x.ObservationTargetTypeDisplayName)).ToList();
            MeasurementUnitTypes = measurementUnitTypes.Select(x => new SelectItemSimple(x.MeasurementUnitTypeID, x.MeasurementUnitTypeDisplayName)).ToList();

            DiscreteObservationTypeCollectionMethodID = ObservationTypeCollectionMethod.DiscreteValue.ObservationTypeCollectionMethodID;
            RateObservationTypeCollectionMethodID = ObservationTypeCollectionMethod.Rate.ObservationTypeCollectionMethodID;
            PassFailObservationTypeCollectionMethodID = ObservationTypeCollectionMethod.PassFail.ObservationTypeCollectionMethodID;
            PercentageObservationTypeCollectionMethodID = ObservationTypeCollectionMethod.Percentage.ObservationTypeCollectionMethodID;
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
