/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPObservationDetail.cs" company="Tahoe Regional Planning Agency">
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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPObservationDetail : IAuditableEntity
    {
        public string AuditDescriptionString => $"Observation Detail {TreatmentBMPObservationDetailID}";
        public string ObservedValueFormatted
        {
            get
            {
                switch (TreatmentBMPObservation.ObservationValueType.ToEnum)
                {
                    case ObservationValueTypeEnum.integerType:
                    case ObservationValueTypeEnum.stringType:
                    case ObservationValueTypeEnum.floatType:
                        return TreatmentBMPObservationValue.ToString(CultureInfo.InvariantCulture) +" "+ TreatmentBMPObservation.ObservationType.MeasurementUnitType.LegendDisplayName;
                    case ObservationValueTypeEnum.booleanType:
                        return TreatmentBMPObservationValue == 1 ? "Yes" : "No";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public static double CalculateInfiltrationRateFromReadings(List<TreatmentBMPInfiltrationReadingSimple> detailSimpleTreatmentBMPInfiltrationReadingSimples)
        {
            if (detailSimpleTreatmentBMPInfiltrationReadingSimples.Any(x => x.ReadingTime == null || x.ReadingValue == null))
                throw new ArgumentException("All infiltration rate times and readings must have values");
            if (detailSimpleTreatmentBMPInfiltrationReadingSimples.Count < 3 || detailSimpleTreatmentBMPInfiltrationReadingSimples.Count > 3)
                throw new ArgumentException("Must have exactly 3 readings");

            var orderedReadings = detailSimpleTreatmentBMPInfiltrationReadingSimples.OrderBy(x => x.ReadingTime).ToList();

            var rate1 = (orderedReadings[1].ReadingValue - orderedReadings[0].ReadingValue)/
                        (orderedReadings[1].ReadingTime - orderedReadings[0].ReadingTime);

            var rate2 = (orderedReadings[2].ReadingValue - orderedReadings[1].ReadingValue) /
                        (orderedReadings[2].ReadingTime - orderedReadings[1].ReadingTime);

            return Math.Abs((rate1.Value + rate2.Value)/2)*60;
        }
    }
}
