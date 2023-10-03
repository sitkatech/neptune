/*-----------------------------------------------------------------------
<copyright file="LabelForExtensionsTest.cs" company="Sitka Technology Group">
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

using ApprovalTests;
using ApprovalTests.Reporters;using DocumentFormat.OpenXml.Drawing;
using IdentityServer4.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptune.EFModels.Entities;
using Neptune.Web.Common.HtmlHelperExtensions;

namespace Neptune.Tests
{
    [TestClass]
    public class LabelWithSugarForExtensionsTest
    {
        [TestMethod]
        [UseReporter(typeof(CustomDiffReporter))]
        public void GenerateLabelWithSugarForNotYetDefinedFieldDefinitionTest()
        {
            var result = LabelWithSugarForExtensions.LabelWithFieldDefinitionForImpl("My Field",
                "myField",
                "/FieldDefinition/FieldDefinitionDetails/1",
                300,
                LabelWithSugarForExtensions.DisplayStyle.HelpIconWithLabel, false);
            Approvals.Verify(result.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(CustomDiffReporter))]
        public void GenerateLabelWithSugarForDefinedFieldDefinitionTest()
        {
            var fieldDefinitionData = new FieldDefinition(-1, "My field's definition");
            var result = LabelWithSugarForExtensions.LabelWithFieldDefinitionForImpl("My Field",
                "myField",
                "/FieldDefinition/FieldDefinitionDetails/1",
                300,
                LabelWithSugarForExtensions.DisplayStyle.HelpIconWithLabel, false);
            Approvals.Verify(result.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(CustomDiffReporter))]
        public void GenerateLabelWithSugarForDefinedFieldDefinitionForGridHeaderTest()
        {
            var fieldDefinitionData = new FieldDefinition(-1, "My field's definition");
            var result = LabelWithSugarForExtensions.LabelWithFieldDefinitionForImpl("My Field",
                "myField",
                "/FieldDefinition/FieldDefinitionDetails/1",
                300,
                LabelWithSugarForExtensions.DisplayStyle.AsGridHeader, false);
            Approvals.Verify(result.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(CustomDiffReporter))]
        public void GenerateLabelWithSugarForDefinedFieldDefinitionForHelpIconOnlyTest()
        {
            var fieldDefinitionData = new FieldDefinition(-1, "My field's definition");
            var result = LabelWithSugarForExtensions.LabelWithFieldDefinitionForImpl("My Field",
                "myField",
                "/FieldDefinition/FieldDefinitionDetails/1",
                300,
                LabelWithSugarForExtensions.DisplayStyle.HelpIconOnly, false);
            Approvals.Verify(result.ToString());
        }
    }
}


