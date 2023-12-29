/*-----------------------------------------------------------------------
<copyright file="StringFormatsTest.cs" company="Sitka Technology Group">
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
using Neptune.WebMvc.Common;

namespace Neptune.Tests
{
    [TestClass]
    public class StringFormatsTest
    {
        [TestMethod]
        public void ParseNullableDecimalFromCurrencyString()
        {
            Assert.AreEqual(100m,StringFormats.ParseNullableDecimalFromCurrencyString("$100"));
            Assert.AreEqual(200000m,StringFormats.ParseNullableDecimalFromCurrencyString("$200,000"));
            Assert.AreEqual(100.10m,StringFormats.ParseNullableDecimalFromCurrencyString("$100.10"));
            Assert.AreEqual(-100.10m, StringFormats.ParseNullableDecimalFromCurrencyString("-$100.10"));
            Assert.AreEqual(-100.10m,StringFormats.ParseNullableDecimalFromCurrencyString("($100.10)"));
        }

        [TestMethod]
        public void FormatFileSizeHumanReadableTest()
        {
            Assert.AreEqual("0 B", StringFormats.ToHumanReadableByteSize(0));
            Assert.AreEqual("1 KB", StringFormats.ToHumanReadableByteSize(1024));
            Assert.AreEqual("1 MB", StringFormats.ToHumanReadableByteSize(1024 * 1024));
            Assert.AreEqual("1.5 MB", StringFormats.ToHumanReadableByteSize(1024 * 1024 + 512 * 1024));
            Assert.AreEqual("1.23 MB", StringFormats.ToHumanReadableByteSize(1024 * 1024 + 231 * 1024));
            Assert.AreEqual("1 GB", StringFormats.ToHumanReadableByteSize(1024 * 1024 * 1024));
            Assert.AreEqual("1 TB", StringFormats.ToHumanReadableByteSize(1024L * 1024L * 1024L * 1024L));
            Assert.AreEqual("1 PB", StringFormats.ToHumanReadableByteSize(1024L * 1024L * 1024L * 1024L * 1024L));
            Assert.AreEqual("1 EB", StringFormats.ToHumanReadableByteSize(1024L * 1024L * 1024L * 1024L * 1024L * 1024L));
        }

        //[Test]
        //public void MakeAbsoluteLinksToApplicationDomainRelativeNullTest()
        //{
        //    var result = StringFormats.MakeAbsoluteLinksToApplicationDomainRelative(null);

        //    Assert.That(result, Is.Null);
        //}

        //[Test]
        //public void MakeAbsoluteLinksToApplicationDomainRelativeInnerNullTest()
        //{
        //    var htmlString = new HtmlString(null);
        //    var result = htmlString.MakeAbsoluteLinksToApplicationDomainRelative();

        //    Assert.That(result, Is.EqualTo(htmlString));
        //}

        //[Test]
        //public void MakeAbsoluteLinksToApplicationDomainRelativeStringEmptyTest()
        //{
        //    var result = new HtmlString(string.Empty).MakeAbsoluteLinksToApplicationDomainRelative();

        //    Assert.That(result.ToString(), Is.EqualTo(string.Empty));
        //}

        //[Test]
        //public void MakeAbsoluteLinksToApplicationDomainRelativeActualAppDomainTest()
        //{
        //    const string relativeUrl = "/awesome/awesomepage.cshtml";
        //    var absoluteUrl = string.Format("http://{0}{1}", SitkaWebConfiguration.ApplicationDomain, relativeUrl);
        //    var result = new HtmlString(absoluteUrl).MakeAbsoluteLinksToApplicationDomainRelative();

        //    Assert.That(result.ToString(), Is.EqualTo(relativeUrl));
        //}

        //[Test]
        //public void MakeAbsoluteLinksToApplicationDomainRelativeOutsideDomainTest()
        //{
        //    const string relativeUrl = "/awesome/awesomepage.cshtml";
        //    var absoluteUrl = string.Format("https://{0}{1}", "example.org", relativeUrl);
        //    var result = new HtmlString(absoluteUrl).MakeAbsoluteLinksToApplicationDomainRelative();

        //    Assert.That(result.ToString(), Is.EqualTo(absoluteUrl));
        //}

    }
}
