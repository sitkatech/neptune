/*-----------------------------------------------------------------------
<copyright file="RouteTableBuilderTest.cs" company="Sitka Technology Group">
Copyright (c) Sitka Technology Group. All rights reserved.
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
using System.IO;
using LtInfo.Common;
using NUnit.Framework;

namespace Neptune.Web.Common
{
    [TestFixture]
    public class QgisRunnerTest
    {
        [Test]
        public void TestPyqgisLauncher()
        {
            AssertCustom.IgnoreOnBuildServer();
            var processUtilityResult = QgisRunner.ExecutePyqgisScript(NeptuneWebConfiguration.PathToPyqgisTestScript,
                NeptuneWebConfiguration.PyqgisWorkingDirectory);

            Assert.That(processUtilityResult.ReturnCode == 0);
        }

        [Test]
        public void TestProcessing()
        {
            AssertCustom.IgnoreOnBuildServer();
            var processUtilityResult = QgisRunner.ExecutePyqgisScript(
                $"{NeptuneWebConfiguration.PyqgisWorkingDirectory}TestPyqgisProcessing.py",
                @"C:\Windows\System32\");

            Assert.That(processUtilityResult.ReturnCode == 0);
        }
    }
}