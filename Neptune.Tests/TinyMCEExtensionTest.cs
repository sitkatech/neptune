﻿/*-----------------------------------------------------------------------
<copyright file="TinyMCEEditorExtensionTest.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
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
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptune.WebMvc.Common;

namespace Neptune.Tests
{
    [TestClass]
    public class TinyMCEEditorExtensionTest
    {
        [TestMethod]
        [UseReporter(typeof(CustomDiffReporter))]
        public void GenerateJavascriptWithMinimalToolbarTest()
        {
            const string modelID = "ProgramPageContent";
            var result = TinyMCEExtension.GenerateJavascript(modelID,
                TinyMCEExtension.TinyMCEToolbarStyle.Minimal, null);
            Approvals.Verify(result.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(CustomDiffReporter))]
        public void GenerateJavascriptWithMinimalWithImagesToolbarTest()
        {
            const string modelID = "ProgramPageContent";
            var result = TinyMCEExtension.GenerateJavascript(modelID,
                TinyMCEExtension.TinyMCEToolbarStyle.MinimalWithImages, null);
            Approvals.Verify(result.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(CustomDiffReporter))]
        public void GenerateJavascriptWithNoToolbarTest()
        {
            const string modelID = "ProgramPageContent";
            var result = TinyMCEExtension.GenerateJavascript(modelID,
                TinyMCEExtension.TinyMCEToolbarStyle.None, null);
            Approvals.Verify(result.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(CustomDiffReporter))]
        public void GenerateJavascriptWithAllToolbarsTest()
        {
            const string modelID = "ProgramPageContent";
            var result = TinyMCEExtension.GenerateJavascript(modelID,
                TinyMCEExtension.TinyMCEToolbarStyle.All, null);
            Approvals.Verify(result.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(CustomDiffReporter))]
        public void GenerateJavascriptWithAllOnOneRowToolbarsTest()
        {
            const string modelID = "ProgramPageContent";
            var result = TinyMCEExtension.GenerateJavascript(modelID,
                TinyMCEExtension.TinyMCEToolbarStyle.AllOnOneRow, null);
            Approvals.Verify(result.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(CustomDiffReporter))]
        public void GenerateJavascriptWithAllOnOneRowNoMaximizeToolbarsTest()
        {
            const string modelID = "ProgramPageContent";
            var result = TinyMCEExtension.GenerateJavascript(modelID,
                TinyMCEExtension.TinyMCEToolbarStyle.AllOnOneRowNoMaximize, null);
            Approvals.Verify(result.ToString());
        }

    }
}