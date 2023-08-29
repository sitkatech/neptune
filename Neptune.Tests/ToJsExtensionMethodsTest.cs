/*-----------------------------------------------------------------------
<copyright file="ToJsExtensionMethodsTest.cs" company="Sitka Technology Group">
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
using Neptune.Web.Common;

namespace Neptune.Tests
{
    [TestClass]
    public class ToJsExtensionMethodsTest
    {
        [TestMethod]
        public void ShouldEncodeStringsProperly()
        {
            Assert.AreEqual("'Text'","Text".ToJS(), "Should add leading and trailing single quotes");
            Assert.AreEqual(@"'Text with \u0027 apostrophe'", "Text with ' apostrophe".ToJS(), "Should properly encode an apostrophe");
            Assert.AreEqual(@"'Text with \"" double quote'", "Text with \" double quote".ToJS(), "Should properly encode a double quote");
        }
    }
}
