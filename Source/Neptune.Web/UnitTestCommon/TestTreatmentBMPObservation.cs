/*-----------------------------------------------------------------------
<copyright file="TestTreatmentBMPObservation.cs" company="Tahoe Regional Planning Agency">
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
using Neptune.Web.Models;

namespace Neptune.Web.UnitTestCommon
{
    public static partial class TestFramework
    {
        public static class TestTreatmentBMPObservation
        {
            public static TreatmentBMPObservation Create(TreatmentBMPType treatmentBMPType, ObservationType observationType)
            {
                var treatmentBMPAssessment = TestTreatmentBMPAssessment.Create(TestTreatmentBMP.Create(treatmentBMPType));
                return TreatmentBMPObservation.CreateNewBlank(treatmentBMPAssessment, observationType, ObservationValueType.floatType);
            }

            public static TreatmentBMPObservation Create(ObservationType observationType, TreatmentBMPType treatmentBMPType, double benchmark, double threshold, double observation, double? designDepth)
            {
                var treatmentBMPObservation = Create(treatmentBMPType, observationType);

                var treatmentBMP = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP;
                treatmentBMP.DesignDepth = designDepth;

                var treatmentBMPObservationDetail = TreatmentBMPObservationDetail.CreateNewBlank(treatmentBMPObservation, GetDefaultTreatmentType(observationType));
                treatmentBMPObservationDetail.TreatmentBMPObservationValue = observation;

                treatmentBMPObservation.TreatmentBMPObservationDetails.Add(treatmentBMPObservationDetail);

                var treatmentBMPBenchmarkAndThreshold = TestTreatmentBMPBenchmarkAndThreshold.Create(treatmentBMP, observationType);
                treatmentBMPBenchmarkAndThreshold.BenchmarkValue = benchmark;
                treatmentBMPBenchmarkAndThreshold.ThresholdValue = threshold;

                return treatmentBMPObservation;
            }

            private static TreatmentBMPObservationDetailType GetDefaultTreatmentType(ObservationType observationType)
            {
                switch (observationType.ToEnum)
                {
                    case ObservationTypeEnum.InfiltrationRate:
                        return TreatmentBMPObservationDetailType.ConstantHeadPermeameter;
                    case ObservationTypeEnum.VegetativeCover:
                        return TreatmentBMPObservationDetailType.VegetativeCoverWetlandAndRiparianSpecies;
                    case ObservationTypeEnum.MaterialAccumulation:
                        return TreatmentBMPObservationDetailType.StaffPlate;
                    case ObservationTypeEnum.VaultCapacity:
                        return TreatmentBMPObservationDetailType.VaultCapacityStadiaRod;
                    case ObservationTypeEnum.StandingWater:
                        return TreatmentBMPObservationDetailType.StandingWater;
                    case ObservationTypeEnum.Runoff:
                        return TreatmentBMPObservationDetailType.DurationOfInfiltration;
                    case ObservationTypeEnum.SedimentTrapCapacity:
                        return TreatmentBMPObservationDetailType.SedimentTrapCapacityStadiaRod;
                    case ObservationTypeEnum.WetBasinVegetativeCover:
                        return TreatmentBMPObservationDetailType.WetBasinVegetativeCoverWetlandAndRiparianSpecies;
                    case ObservationTypeEnum.ConveyanceFunction:
                        return TreatmentBMPObservationDetailType.Inlet;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

        }
    }
}
