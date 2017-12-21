/*-----------------------------------------------------------------------
<copyright file="ConveyanceFunctionViewModel.cs" company="Tahoe Regional Planning Agency">
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

using System.Linq;
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class ConveyanceFunctionViewModel : ObservationViewModel
    {
        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public ConveyanceFunctionViewModel()
        {            
        }

        public ConveyanceFunctionViewModel(Models.TreatmentBMPAssessment treatmentBMPAssessment) : base(treatmentBMPAssessment, ObservationTypeEnum.ConveyanceFunction)
        {
            var observation = treatmentBMPAssessment.TreatmentBMPObservations.SingleOrDefault(x => x.ObservationType.ToEnum == ObservationTypeEnum.ConveyanceFunction);

            var inletCount = treatmentBMPAssessment.TreatmentBMP.InletCount;
            var observedInletCount = observation == null ? 0 : observation.TreatmentBMPObservationDetails.Count(x => x.TreatmentBMPObservationDetailType == TreatmentBMPObservationDetailType.Inlet);

            for (var i = 0; i < inletCount - observedInletCount; i++)
            {
                TreatmentBMPObservationDetailSimples.Add(new TreatmentBMPObservationDetailSimple(ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue(), TreatmentBMPObservationDetailType.Inlet.TreatmentBMPObservationDetailTypeID, null, string.Empty));
            }

            var outletCount = treatmentBMPAssessment.TreatmentBMP.OutletCount;
            var observedOutletCount = observation == null ? 0 : observation.TreatmentBMPObservationDetails.Count(x => x.TreatmentBMPObservationDetailType == TreatmentBMPObservationDetailType.Outlet);

            for (var i = 0; i < outletCount - observedOutletCount; i++)
            {
                TreatmentBMPObservationDetailSimples.Add(new TreatmentBMPObservationDetailSimple(ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue(), TreatmentBMPObservationDetailType.Outlet.TreatmentBMPObservationDetailTypeID, null, string.Empty));
            }
        }
    }
}
