/*-----------------------------------------------------------------------
<copyright file="InfiltrationRateViewModel.cs" company="Tahoe Regional Planning Agency">
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class InfiltrationRateViewModel : FormViewModel, IValidatableObject
    {
        public int TreatmentBMPAssessmentID { get; set; }
        public List<TreatmentBMPInfiltrationObservationDetailSimple> TreatmentBMPInfiltrationObservationDetailSimples { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public InfiltrationRateViewModel()
        {            
        }

        public InfiltrationRateViewModel(Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            TreatmentBMPAssessmentID = treatmentBMPAssessment.TreatmentBMPAssessmentID;
            var treatmentBMPObservationDetails = treatmentBMPAssessment.TreatmentBMPObservations.Where(x => x.ObservationType.ToEnum == ObservationTypeEnum.InfiltrationRate).SelectMany(x => x.TreatmentBMPObservationDetails).ToList();
            TreatmentBMPInfiltrationObservationDetailSimples = treatmentBMPObservationDetails.Select(x => new TreatmentBMPInfiltrationObservationDetailSimple(x)).ToList();
        }

        public void UpdateModel(TreatmentBMPObservation treatmentBMPObservation, IList<TreatmentBMPObservationDetail> allDetails, IList<TreatmentBMPInfiltrationReading> allReadings )
        {
            if (TreatmentBMPInfiltrationObservationDetailSimples == null)
            {
                TreatmentBMPInfiltrationObservationDetailSimples = new List<TreatmentBMPInfiltrationObservationDetailSimple>();
            }

            var readings = treatmentBMPObservation.TreatmentBMPObservationDetails.SelectMany(x => x.TreatmentBMPInfiltrationReadings).ToList();

            var details = TreatmentBMPInfiltrationObservationDetailSimples.Select(detailSimple =>
            {
                var treatmentBMPObservationValue = detailSimple.HasReadings
                    ? TreatmentBMPObservationDetail.CalculateInfiltrationRateFromReadings(detailSimple.TreatmentBMPInfiltrationReadingSimples)
                    : detailSimple.TreatmentBMPObservationValue.Value;

                var detail = new TreatmentBMPObservationDetail(detailSimple.TreatmentBMPObservationDetailID ?? ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue(),
                    treatmentBMPObservation.TreatmentBMPObservationID,
                    detailSimple.TreatmentBMPObservationDetailTypeID,
                    treatmentBMPObservationValue,
                    detailSimple.Notes);

                if (detailSimple.TreatmentBMPInfiltrationReadingSimples != null)
                {
                    detail.TreatmentBMPInfiltrationReadings =
                        detailSimple.TreatmentBMPInfiltrationReadingSimples.Select(
                            reading =>
                                new TreatmentBMPInfiltrationReading(reading.TreatmentBMPInfiltrationReadingID ?? ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue(),
                                    detail.TreatmentBMPObservationDetailID,
                                    reading.ReadingValue.Value,
                                    reading.ReadingTime.Value)).ToList();
                }

                return detail;
            }).ToList();

            treatmentBMPObservation.TreatmentBMPObservationDetails.Merge(details,
                allDetails,
                (x, y) => x.TreatmentBMPObservationDetailID == y.TreatmentBMPObservationDetailID,
                (x, y) =>
                {
                    x.TreatmentBMPObservationValue = y.TreatmentBMPObservationValue;
                    x.Notes = y.Notes;
                });

            readings.Merge(details.SelectMany(x => x.TreatmentBMPInfiltrationReadings).ToList(),
                allReadings,
                (x, y) => x.TreatmentBMPInfiltrationReadingID == y.TreatmentBMPInfiltrationReadingID,
                (x, y) =>
                {
                    x.ReadingValue = y.ReadingValue;
                    x.ReadingTime = y.ReadingTime;
                });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            return validationResults;
        }
    }
}
