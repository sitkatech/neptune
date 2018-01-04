/*-----------------------------------------------------------------------
<copyright file="ObservationTypeHelper.cs" company="Tahoe Regional Planning Agency">
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

namespace Neptune.Web.Models
{
    public static class ObservationTypeHelper
    {
        public static double LinearInterpolation(double observation, double benchmark, double threshold)
        {
            var score = benchmark > threshold ? PositiveLinearInterpolation(observation, benchmark, threshold) : NegativeLinearInterpolation(observation, benchmark, threshold);
            return score < 0 ? 0 : (score > 5 ? 5 : score);
        }

        public static double PositiveLinearInterpolation(double observation, double benchmark, double threshold)
        {
            // y = y0 + (x - x0) * ((y1 - y0)/(x1-x0)) where y0 = 2 and y1 = 5
            var score = 2 + (observation - threshold)*((5 - 2)/(benchmark - threshold));
            return Math.Round(score, 1);
        }

        public static double NegativeLinearInterpolation(double observation, double benchmark, double threshold)
        {
            // y = y0 + (x - x0) * ((y1 - y0)/(x1-x0)) where y0 = 5 and y1 = 2
            var score = 5 + (observation - benchmark)*((2 - 5)/(threshold - benchmark));
            return Math.Round(score,1);
        }

        public static double WetBasinBipolarLinearInterpolation(double observation, double benchmark, double threshold)
        {
            var score = observation < benchmark ? PositiveLinearInterpolation(observation, benchmark, threshold) : NegativeLinearInterpolation(observation, benchmark, threshold);
            return score < 0 ? 0 : (score > 5 ? 5 : score);
        }

        public static double ThresholdValueFromThresholdPercentDecline(double benchmarkValue, double thresholdPercent)
        {
            return benchmarkValue - (thresholdPercent / 100) * benchmarkValue;
        }

        public static string FormattedDefaultBenchmarkValue(ObservationType observationType, double? benchmarkValue)
        {
            if (!benchmarkValue.HasValue)
            {
                return "-";
            }
            var optionalSpace = observationType.MeasurementUnitType == MeasurementUnitType.Percent ? "" : " ";
            return $"{benchmarkValue}{optionalSpace}{observationType.MeasurementUnitType.LegendDisplayName}";
        }

        public static string ApplyThresholdFormatting(ObservationType observationType, double? thresholdValue)
        {
            if (thresholdValue == null)
            {
                return "-";
            }

            var optionalSpace = observationType.MeasurementUnitType == MeasurementUnitType.Percent || observationType.ThresholdPercentDecline
                ? String.Empty
                : " ";

            var unit = observationType.ThresholdPercentDecline ? "% decline" : observationType.MeasurementUnitType.LegendDisplayName;

            return $"{thresholdValue}{optionalSpace}{unit}";
        }
    }
}
