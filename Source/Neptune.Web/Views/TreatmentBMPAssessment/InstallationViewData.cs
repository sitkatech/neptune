/*-----------------------------------------------------------------------
<copyright file="StandingWaterViewData.cs" company="Tahoe Regional Planning Agency">
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

using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class InstallationViewData : AssessmentViewData
    {

        public readonly InstallationObservationViewDataForAngular ViewDataForAngular;


        public InstallationViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment, List<TreatmentBMPObservationDetailType> observationDetailTypes)
            : base(currentPerson, treatmentBMPAssessment, ObservationType.Installation.ObservationTypeDisplayName)
        {
            ViewDataForAngular = new InstallationObservationViewDataForAngular(observationDetailTypes);

        }

        public class InstallationObservationViewDataForAngular
        {
            public readonly List<TreatmentBMPObservationDetailTypeSimple> ObservationDetailTypeSimples;
            public readonly int TreatmentBMPObservationDetailTypeID = TreatmentBMPObservationDetailType.Installation.TreatmentBMPObservationDetailTypeID;

            public InstallationObservationViewDataForAngular(List<TreatmentBMPObservationDetailType> observationDetailTypes)
            {
                ObservationDetailTypeSimples = observationDetailTypes.Select(x => new TreatmentBMPObservationDetailTypeSimple(x)).ToList();
            }
        }
    }
}
