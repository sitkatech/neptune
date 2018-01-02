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
        public readonly IEnumerable<SelectListItem> MeasurementUnitTypes;
        public readonly List<ObservationTypeSpecificationSimple> ObservationTypeSpecificationSimples;
        public readonly IEnumerable<SelectListItem> ObservationThresholdTypes;
        public readonly IEnumerable<SelectListItem> ObservationTargetTypes;
        public readonly IEnumerable<SelectListItem> ObservationTypeCollectionMethods;
        public readonly string ObservationTypeIndexUrl;

        public EditViewData(Person currentPerson, IEnumerable<SelectListItem> measurementUnitTypes,
            List<ObservationTypeSpecification> observationTypeSpecifications,
            IEnumerable<SelectListItem> observationThresholdTypes, IEnumerable<SelectListItem> observationTargetTypes,
            IEnumerable<SelectListItem> observationTypeCollectionMethods) : base(currentPerson)
        {
            PageTitle = "New Observation Type";

            MeasurementUnitTypes = measurementUnitTypes;
            ObservationTypeSpecificationSimples = observationTypeSpecifications.Select(x => new ObservationTypeSpecificationSimple(x)).ToList();
            ObservationThresholdTypes = observationThresholdTypes;
            ObservationTargetTypes = observationTargetTypes;
            ObservationTypeCollectionMethods = observationTypeCollectionMethods;
            ObservationTypeIndexUrl = SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(x => x.Index());
        }
    }

    public class ObservationTypeSpecificationSimple
    {
        public readonly int ObservationTypeSpecificationID;
        public readonly int ObservationTypeCollectionMethodID;
        public readonly int ObservationTargetTypeID;
        public readonly int ObservationThresholdTypeID;

        public ObservationTypeSpecificationSimple(ObservationTypeSpecification observationTypeSpecification)
        {
            ObservationTypeSpecificationID = observationTypeSpecification.ObservationTypeSpecificationID;
            ObservationTypeCollectionMethodID = observationTypeSpecification.ObservationTypeCollectionMethodID;
            ObservationTargetTypeID = observationTypeSpecification.ObservationTargetTypeID;
            ObservationThresholdTypeID = observationTypeSpecification.ObservationThresholdTypeID;
        }
    }
}
