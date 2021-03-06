﻿/*-----------------------------------------------------------------------
<copyright file="EditViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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
using System.ComponentModel;
using System.Web;
using LtInfo.Common.Models;

namespace Neptune.Web.Views.NeptunePage
{
    public class EditViewModel : FormViewModel
    {
        [DisplayName("Page Content")]
        public HtmlString NeptunePageContentHtmlString { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditViewModel()
        {
        }
        
        public EditViewModel(Models.NeptunePage neptunePage)
        {
            NeptunePageContentHtmlString = neptunePage != null ? neptunePage.NeptunePageContentHtmlString : null;
        }

        public void UpdateModel(Models.NeptunePage neptunePage)
        {
            neptunePage.NeptunePageContentHtmlString = NeptunePageContentHtmlString == null || string.IsNullOrWhiteSpace(NeptunePageContentHtmlString.ToString()) ? null : NeptunePageContentHtmlString;
        }
    }
}
