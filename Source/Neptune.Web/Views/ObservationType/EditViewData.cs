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
using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.ObservationType
{
    public class EditViewData : NeptuneViewData
    {
        public ViewDataForAngular ViewDataForAngular { get; } 
        public string ObservationTypeIndexUrl { get; }

        public EditViewData(Person currentPerson, List<MeasurementUnitType> measurementUnitTypes,
            List<ObservationTypeSpecification> observationTypeSpecifications,
            List<ObservationThresholdType> observationThresholdTypes, List<ObservationTargetType> observationTargetTypes,
            List<ObservationTypeCollectionMethod> observationTypeCollectionMethods) : base(currentPerson)
        {
            PageTitle = "New Observation Type";
            ViewDataForAngular = new ViewDataForAngular(observationTypeSpecifications, observationTypeCollectionMethods, observationThresholdTypes, observationTargetTypes, measurementUnitTypes);
            ObservationTypeIndexUrl = SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(x => x.Index());
        }
    }

    public class ViewDataForAngular
    {
       
        public List<ObservationTypeSpecificationSimple> ObservationTypeSpecificationSimples { get; }
        public List<SelectItemSimple> ObservationTypeCollectionMethods { get; }
        public List<SelectItemSimple> ObservationThresholdTypes { get; }
        public List<SelectItemSimple> ObservationTargetTypes { get; }
        public List<SelectItemSimple> MeasurementUnitTypes { get; }

        public ViewDataForAngular(List<ObservationTypeSpecification> observationTypeSpecifications,
            List<ObservationTypeCollectionMethod> observationTypeCollectionMethods,
            List<ObservationThresholdType> observationThresholdTypes,
            List<ObservationTargetType> observationTargetTypes, List<MeasurementUnitType> measurementUnitTypes)
        {
            ObservationTypeSpecificationSimples = observationTypeSpecifications.Select(x => new ObservationTypeSpecificationSimple(x)).ToList();
            ObservationTypeCollectionMethods = observationTypeCollectionMethods.Select(x => new SelectItemSimple(x.ObservationTypeCollectionMethodID, x.ObservationTypeCollectionMethodDisplayName)).ToList();
            ObservationThresholdTypes = observationThresholdTypes.Select(x => new SelectItemSimple(x.ObservationThresholdTypeID, x.ObservationThresholdTypeDisplayName)).ToList();
            ObservationTargetTypes = observationTargetTypes.Select(x => new SelectItemSimple(x.ObservationTargetTypeID, x.ObservationTargetTypeDisplayName)).ToList();
            MeasurementUnitTypes = measurementUnitTypes.Select(x => new SelectItemSimple(x.MeasurementUnitTypeID, x.MeasurementUnitTypeDisplayName)).ToList();
        }
    }

    public class SelectItemSimple
    {
        public int SelectItemSimpleID { get; }
        public string SelectItemSimpleDisplayName { get; }

        public SelectItemSimple(int id, string displayName)
        {
            SelectItemSimpleID = id;
            SelectItemSimpleDisplayName = displayName;
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
