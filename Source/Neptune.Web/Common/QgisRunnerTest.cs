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
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using NUnit.Framework;

namespace Neptune.Web.Common
{
    [TestFixture]
    public class QgisRunnerTest
    {
        [Test]
        public void TestCanExecutePythonScript()
        {
            var pathToPythonExecutable = NeptuneWebConfiguration.PathToPythonExecutable;

            var commandLineArguments = new List<string> { NeptuneWebConfiguration.PathToPythonTestScript };

            var processUtilityResult = ProcessUtility.ShellAndWaitImpl(NeptuneWebConfiguration.PythonWorkingDirectory, pathToPythonExecutable, commandLineArguments, true, Convert.ToInt32(5000));

            Assert.That(processUtilityResult.ReturnCode == 0);
            Assert.That(processUtilityResult.StdOut.Contains("Success!"));
        }
        [Test]
        public void TestCanExecuteBatchFile()
        {
            var paramToEcho = "Echo Me!";

            var commandLineArguments = new List<string> { "/q", "/c", NeptuneWebConfiguration.PathToTestBatchFile, paramToEcho };

            var processUtilityResult = ProcessUtility.ShellAndWaitImpl(@"C:\Temp", "cmd.exe", commandLineArguments, true, Convert.ToInt32(5000));

            Assert.That(processUtilityResult.ReturnCode == 0);
            Assert.That(processUtilityResult.StdOut.Contains(paramToEcho));
        }

        [Test]
        public void TestCanExecutePythonScriptViaBatchFile()
        {
            var commandLineArguments = new List<string>
            {
                "/q",
                "/c",
                NeptuneWebConfiguration.PathToTestPythonLauncher,
                NeptuneWebConfiguration.PythonWorkingDirectory,
                NeptuneWebConfiguration.PathToPythonTestScript
            };

            var processUtilityResult = ProcessUtility.ShellAndWaitImpl(@"C:\Temp", "cmd.exe", commandLineArguments, true, Convert.ToInt32(5000));

            Assert.That(processUtilityResult.ReturnCode == 0);
            Assert.That(processUtilityResult.StdOut.Contains("Success!"));
        }

        [Test]
        public void TestPyqgisLauncher()
        {
          var processUtilityResult = QgisRunner.ExecutePyqgisScript(NeptuneWebConfiguration.PathToPyqgisTestScript, NeptuneWebConfiguration.PyqgisTestWorkingDirectory);

            Assert.That(processUtilityResult.ReturnCode == 0 );
            Assert.That(processUtilityResult.StdOut.Contains("Aliso Creek"));
        }
    }
}
