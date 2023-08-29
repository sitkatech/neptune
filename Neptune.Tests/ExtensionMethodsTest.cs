/*-----------------------------------------------------------------------
<copyright file="ExtensionMethodsTest.cs" company="Sitka Technology Group">
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

using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptune.Web.Common;

namespace Neptune.Tests
{
    [TestClass]
    public class ExtensionMethodsTest
    {
        [TestMethod]
        public void IsRegexToFindAbsoluteUrlsWorking()
        {
            var regExToUse = StringFormats.ConstructContainAbsoluteUrlWithApplicationDomainReferenceRegExForApplicationDomain("someapp.somedomain.org");
            var regEx = new Regex(regExToUse, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Trace.WriteLine($"Non-server root relative Regex string: {regExToUse}");
            Assert.IsFalse(((string) null).DoesHtmlStringContainAbsoluteUrlWithApplicationDomainReference(regEx), "Null string - can't be bad");
            Assert.IsFalse("".DoesHtmlStringContainAbsoluteUrlWithApplicationDomainReference(regEx), "Empty string - can't be bad");
            Assert.IsFalse("ABC".DoesHtmlStringContainAbsoluteUrlWithApplicationDomainReference(regEx), "Simple string - can't be bad");
            Assert.IsFalse("\"../../SomeAction".DoesHtmlStringContainAbsoluteUrlWithApplicationDomainReference(regEx), "not server root relative but we don't care for this case");
            Assert.IsTrue("http://qa.someapp.somedomain.org/".DoesHtmlStringContainAbsoluteUrlWithApplicationDomainReference(regEx), "should be bad - not server root relative");
            Assert.IsTrue("http://someapp.somedomain.org/".DoesHtmlStringContainAbsoluteUrlWithApplicationDomainReference(regEx), "should be bad - not server root relative");
            Assert.IsTrue("https://someapp.somedomain.org/".DoesHtmlStringContainAbsoluteUrlWithApplicationDomainReference(regEx), "should be bad - not server root relative");
            Assert.IsTrue("https://qa.someapp.somedomain.org/".DoesHtmlStringContainAbsoluteUrlWithApplicationDomainReference(regEx), "should be bad - not server root relative");
            Assert.IsTrue("http://someapp.somedomain.org/Project/Index".DoesHtmlStringContainAbsoluteUrlWithApplicationDomainReference(regEx), "should be bad - not server root relative");
            Assert.IsTrue("http://qa.someapp.somedomain.org/Project/Index".DoesHtmlStringContainAbsoluteUrlWithApplicationDomainReference(regEx), "should be bad - not server root relative");
            Assert.IsTrue("https://someapp.somedomain.org/Project/Index".DoesHtmlStringContainAbsoluteUrlWithApplicationDomainReference(regEx), "should be bad - not server root relative");
            Assert.IsTrue("https://qa.someapp.somedomain.org/Project/Index".DoesHtmlStringContainAbsoluteUrlWithApplicationDomainReference(regEx), "should be bad - not server root relative");
            Assert.IsFalse("/Project/Index".DoesHtmlStringContainAbsoluteUrlWithApplicationDomainReference(regEx), "Should be fine -- is server root relative");
        }

        [TestMethod]
        public void CanSumNull()
        {
            Assert.AreEqual(null, new List<decimal?>().SumNullWhenEmptyOrAllNull(x => x), "Empty returns null not zero as with Sum()");
            Assert.AreEqual(null, new List<decimal?>{null}.SumNullWhenEmptyOrAllNull(x => x), "Empty returns null not zero as with Sum()");
            Assert.AreEqual(null, new List<decimal?>{null, null}.SumNullWhenEmptyOrAllNull(x => x), "Empty returns null not zero as with Sum()");
            Assert.AreEqual(234, new List<decimal?> { 234 }.SumNullWhenEmptyOrAllNull(x => x), "Element returns with expected sum()");
            Assert.AreEqual(239, new List<decimal?> { 234, 5 }.SumNullWhenEmptyOrAllNull(x => x), "Element returns with expected sum()");
            Assert.AreEqual(239, new List<decimal?> { null, 234, 5 }.SumNullWhenEmptyOrAllNull(x => x), "Empty returns null not zero as with Sum()");
        }
    }
}
