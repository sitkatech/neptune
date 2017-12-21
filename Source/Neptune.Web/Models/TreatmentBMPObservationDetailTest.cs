/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPObservationDetailTest.cs" company="Tahoe Regional Planning Agency">
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
using NUnit.Framework;

namespace Neptune.Web.Models
{
    [TestFixture]
    public class TreatmentBMPObservationDetailTest
    {
        [TestCase(0,5,10,100,50,0,600)]
        [TestCase(5, 0, 15, 2.5, 4, 1, 13.5)]
        public void TestOutputOCalculateInfiltrationRateFromReadings(double time1, double time2, double time3, double reading1, double reading2, double reading3, double expectedAverageRate)
        {
            //Arrange
            var testReading1 = new TreatmentBMPInfiltrationReadingSimple(null, null, reading1, time1);
            var testReading2 = new TreatmentBMPInfiltrationReadingSimple(null, null, reading2, time2);
            var testReading3 = new TreatmentBMPInfiltrationReadingSimple(null, null, reading3, time3);
            var testList = new List<TreatmentBMPInfiltrationReadingSimple>{testReading1, testReading2, testReading3};

            //Act
            var actualAverageRate = TreatmentBMPObservationDetail.CalculateInfiltrationRateFromReadings(testList);

            //Assert
            Assert.That(actualAverageRate, Is.EqualTo(expectedAverageRate).Within(0.001));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInputsOfCalculateInfiltrationRateFromReadings()
        {
            //Arrange
            var testReading1 = new TreatmentBMPInfiltrationReadingSimple(null, null, 1, 2);
            var testReading2 = new TreatmentBMPInfiltrationReadingSimple(null, null, 2, 4);
            var testList = new List<TreatmentBMPInfiltrationReadingSimple> { testReading1, testReading2 };
           
            TreatmentBMPObservationDetail.CalculateInfiltrationRateFromReadings(testList);            
        }

    }
}
