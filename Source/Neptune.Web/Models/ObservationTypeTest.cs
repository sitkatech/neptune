/*-----------------------------------------------------------------------
<copyright file="ObservationTypeTest.cs" company="Tahoe Regional Planning Agency">
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
using Neptune.Web.UnitTestCommon;
using NUnit.Framework;

namespace Neptune.Web.Models
{
    [TestFixture]
    public class ObservationTypeTest
    {
       [TestCase(10, 10, 0, 5)]
        [TestCase(0, 10, 0, 2)]
        [TestCase(7.4, 13.5, 6.75, 2.2888889)]
        [TestCase(1.75, 2.5, 2, 0.5)]
        public void TestPositiveLinearInterpolation(double observation, double benchmark, double threshold, double expectedScore)
        {
            //Act
            var actualScore = ObservationTypeHelper.PositiveLinearInterpolation(observation, benchmark, threshold);

            //Assert
            Assert.That(actualScore, Is.EqualTo(Math.Round(expectedScore, 1)).Within(0.00001));
        }

        [TestCase(10, 0, 10, 2)]
        [TestCase(0, 0, 10, 5)]
        [TestCase(74.5, 0, 120, 3.1375)]
        [TestCase(90, 63, 85, 1.31818)]
        public void TestNegativeLinearInterpolation(double observation, double benchmark, double threshold, double expectedScore)
        {
            //Act
            var actualScore = ObservationTypeHelper.NegativeLinearInterpolation(observation, benchmark, threshold);

            //Assert
            Assert.That(actualScore, Is.EqualTo(Math.Round(expectedScore, 1)).Within(0.00001));
        }

        [TestCase(25, 50, 25, 2)]
        [TestCase(75, 50, 75, 2)]
        [TestCase(50, 50, 10, 5)]
        public void TestWetBasinBipolarLinearInterpolation(double observation, double benchmark, double threshold, double expectedScore)
        {
            //Act
            var actualScore = ObservationTypeHelper.WetBasinBipolarLinearInterpolation(observation, benchmark, threshold);

            //Assert
            Assert.That(actualScore, Is.EqualTo(expectedScore).Within(0.00001));
        }

        [TestCase(100, 25, 75)]
        [TestCase(20, 50, 10)]
        public void TestThresholdValueFromThresholdPercentDecline(double benchmarkValue, double thresholdPercent, double expectedThresholdValue)
        {
            //Act
            var actualThresholdValue = ObservationTypeHelper.ThresholdValueFromThresholdPercentDecline(benchmarkValue, thresholdPercent);

            //Assert
            Assert.That(actualThresholdValue, Is.EqualTo(expectedThresholdValue).Within(0.00001));
        }

      
    }
}
