/*-----------------------------------------------------------------------
<copyright file="DhtmlxGridHtmlHelpersTest.cs" company="Sitka Technology Group">
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
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Common.ModalDialog;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Neptune.Tests
{
    [TestClass]
    public class DhtmlxGridHtmlHelpersTest
    {
        protected const string TestControllerName = "TestController";

        [TestMethod]
        [UseReporter(typeof(CustomDiffReporter))]
        public void BuildGridColumnsTest()
        {
            const string indent = "";
            var gridSpec = new TestGridSpec();
            var result = DhtmlxGridHtmlHelpers.BuildGridColumns(gridSpec, indent);
            Approvals.Verify(result);
        }

        [TestMethod]
        public void IsUsingSmartRenderingWithColumnsThatHaveTotalsTest()
        {
            var gridSpec = new TestGridSpec();
            Assert.IsFalse(DhtmlxGridHtmlHelpers.IsUsingSmartRendering(gridSpec), "Should not be using smart rendering because we have a grid spec that has a total column");
        }

        [TestMethod]
        public void IsUsingSmartRenderingWithColumnsThatHaveNoTotalsTest()
        {
            var gridSpec = new TestGridSpecWithNoTotalColumns();
            Assert.IsTrue(DhtmlxGridHtmlHelpers.IsUsingSmartRendering(gridSpec), "Should be using smart rendering because we have a grid spec that has a total column");
        }

        [TestMethod]
        [UseReporter(typeof(CustomDiffReporter))]
        public void VerifyJavascriptForDhtmlxGrid()
        {
            var gridSpec = new TestGridSpec();
            const string gridName = "testGridName";


            var testGridSpecClasses = new List<TestGridSpecClass>();
            testGridSpecClasses.Add(new TestGridSpecClass(1, "One", true, 1000m));
            testGridSpecClasses.Add(new TestGridSpecClass(2, "Two", true, 2000m));
            testGridSpecClasses.Add(new TestGridSpecClass(3, "Three", false, 3000m));
            testGridSpecClasses.Add(new TestGridSpecClass(4, "Four", true, 4000m));
            testGridSpecClasses.Add(new TestGridSpecClass(5, "Five", false, 5000m));
            testGridSpecClasses.Add(new TestGridSpecClass(6, "Six", true, 6000m));

            var result = DhtmlxGridHtmlHelpers.DhtmlxGrid(gridSpec,
                                                          gridName,
                                                          $"{TestControllerName}/ListGridDataXml",
                                                          "height:250px;");
            Approvals.Verify(result);
        }

        private class TestGridSpecClass
        {
            public readonly int PrimaryKey;
            public readonly string DisplayName;
            public readonly bool IsActive;
            public readonly decimal? Amount;

            public TestGridSpecClass(int primaryKey, string displayName, bool isActive, decimal? amount)
            {
                PrimaryKey = primaryKey;
                DisplayName = displayName;
                IsActive = isActive;
                Amount = amount;
            }
        }

        private class TestGridSpec : GridSpec<TestGridSpecClass>
        {
            public TestGridSpec()
            {
                ObjectNameSingular = "SOY";
                ObjectNamePlural = "SOYs";
                GridInstructionsWhenEmpty = "I am empty";

                // Edit SOY
                Add(string.Empty,
                    m =>
                    {
                        // Edit button
                        // -----------
                        var contentUrl = $"{TestControllerName}/EditAction/{m.PrimaryKey}";
                        var dialogTitle = $"Edit this {m.DisplayName}";
                        var dialogForm = new ModalDialogForm(contentUrl, 350, dialogTitle);
                        return DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(dialogForm);

                    },
                    35);

                // Delete SOY
                Add(string.Empty,
                    m =>
                    {
                        var contentUrl = $"{TestControllerName}/DeleteAction/{m.PrimaryKey}";
                        var deleteLink = DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(contentUrl, true);
                        return deleteLink;
                    },
                    35);

                Add("Display Name", m => m.DisplayName, 200);
                Add("Is Active", m => m.IsActive.ToYesNo(), 60, DhtmlxGridColumnFilterType.SelectFilterStrict);
                Add("Amount", m => m.Amount, 100, DhtmlxGridColumnFormatType.Currency, DhtmlxGridColumnAggregationType.Total);
            }
        }

        private class TestGridSpecWithNoTotalColumns : GridSpec<TestGridSpecClass>
        {
            public TestGridSpecWithNoTotalColumns()
            {
                ObjectNameSingular = "SOY";
                ObjectNamePlural = "SOYs";
                GridInstructionsWhenEmpty = "I am empty";

                // Edit SOY
                Add(string.Empty,
                    m =>
                    {
                        // Edit button
                        // -----------
                        var contentUrl = $"{TestControllerName}/EditAction/{m.PrimaryKey}";
                        var dialogTitle = $"Edit this {m.DisplayName}";
                        var dialogForm = new ModalDialogForm(contentUrl, 350, dialogTitle);
                        return DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(dialogForm);

                    },
                    35);

                // Delete SOY
                Add(string.Empty,
                    m =>
                    {
                        var contentUrl = $"{TestControllerName}/DeleteAction/{m.PrimaryKey}";
                        var deleteLink = DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(contentUrl, true);
                        return deleteLink;
                    },
                    35);

                Add("Display Name", m => m.DisplayName, 200);
                Add("Is Active", m => m.IsActive.ToYesNo(), 60, DhtmlxGridColumnFilterType.SelectFilterStrict);
                Add("Amount", m => m.Amount, 100, DhtmlxGridColumnFormatType.Currency);
            }
        }
    }
}
