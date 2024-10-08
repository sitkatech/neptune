﻿/*-----------------------------------------------------------------------
<copyright file="UrlTemplateTest.cs" company="Sitka Technology Group">
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptune.Common.DesignByContract;
using Neptune.WebMvc.Common;

namespace Neptune.Tests
{
    [TestClass]
    public class UrlTemplateTest
    {
        [TestMethod, System.ComponentModel.Description("The number of parameters for each type is the same")]
        public void HasConsistentNumberOfParameters()
        {
            Assert.AreEqual(UrlTemplate.StringParameters.Length, UrlTemplate.IntParameters.Length, "Should be the same number of parameters across each type");
        }

        [TestMethod]
        public void CanReplaceParameter1()
        {
            var urlTemplate = $"/SampleController/SampleAction/{UrlTemplate.Parameter1Int}";
            var template = new UrlTemplate<int>(urlTemplate);

            const int realParameter1 = 123;
            var result = template.ParameterReplace(realParameter1);

            Assert.AreEqual(urlTemplate.Replace(UrlTemplate.Parameter1Int.ToString(), realParameter1.ToString()), result, "Should be able to replace with 1 parameter");
        }

        [TestMethod]
        public void CanReplaceParameter2()
        {
            var urlTemplate =
                $"/SampleController/SampleAction/{UrlTemplate.Parameter1Int}/{UrlTemplate.Parameter2String}";
            var template = new UrlTemplate<int,string>(urlTemplate);

            const int realParameter1 = 123;
            const string realParameter2 = "hi";
            var result = template.ParameterReplace(realParameter1, realParameter2);

            var expected = urlTemplate.Replace(UrlTemplate.Parameter1Int.ToString(), realParameter1.ToString()).Replace(UrlTemplate.Parameter2String, realParameter2);
            Assert.AreEqual(expected, result, "Should be able to replace with 2 parameters");
        }

        [TestMethod]
        public void CanReplaceParameter3()
        {
            var urlTemplate =
                $"/SampleController/SampleAction/{UrlTemplate.Parameter1Int}/{UrlTemplate.Parameter2String}/{UrlTemplate.Parameter3Int}";
            var template = new UrlTemplate<int,string, int>(urlTemplate);

            const int realParameter1 = 123;
            const string realParameter2 = "hi";
            const int realParameter3 = 1234;

            var result = template.ParameterReplace(realParameter1, realParameter2, realParameter3);

            var expected = urlTemplate.Replace(UrlTemplate.Parameter1Int.ToString(), realParameter1.ToString()).Replace(UrlTemplate.Parameter2String, realParameter2).Replace(UrlTemplate.Parameter3Int.ToString(), realParameter3.ToString());
            Assert.AreEqual(expected, result, "Should be able to replace with 3 parameters");
        }

        [TestMethod]
        public void CanDetectBadUrlTemplate()
        {
            Assert.ThrowsException<PreconditionException>(() => new UrlTemplate<string>($"/SampleController/SampleAction/{UrlTemplate.Parameter1Int}"), "Types and parameter order must align");
            Assert.ThrowsException<PreconditionException>(() => new UrlTemplate<int>($"/SampleController/SampleAction/{UrlTemplate.Parameter1String}"), "Types and parameter order must align");

            Assert.ThrowsException<PreconditionException>(() => new UrlTemplate<int>($"/SampleController/SampleAction/{UrlTemplate.Parameter2Int}"), "Must do them in order");
            Assert.ThrowsException<PreconditionException>(() => new UrlTemplate<int, string>($"/SampleController/SampleAction/{UrlTemplate.Parameter1Int}/{UrlTemplate.Parameter1String}"), "Should error if there's two of the same ordinals");
        }

        //[Test]
        //public void JavascriptAndCSharpConstantsAreTheSame()
        //{
        //    var sitkaJsFile = FileUtility.FirstMatchingFileUpDirectoryTree(@"Neptune.WebMvc\ScriptsCustom\sitka.js");
        //    var sitkaJsFileContents = FileUtility.FileToString(sitkaJsFile);

        //    var allParameters = UrlTemplate.StringParameters.Union(UrlTemplate.IntParameters.Select(x => x.ToString())).ToList();
        //    var missingParameters = allParameters.Where(x => !sitkaJsFileContents.Contains(x)).ToList();

        //    Assert.That(missingParameters, Is.Empty, string.Format("Could not find some of the parameters from class {0} in file \"{1}\", the .js file should be in sync with the C#", typeof(UrlTemplate).Name, sitkaJsFile.FullName));
        //}

    }
}
