/*-----------------------------------------------------------------------
<copyright file="EditViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Microsoft.AspNetCore.Html;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.NeptunePage;

public class EditViewData : NeptuneUserControlViewData
{
    public TinyMCEExtension.TinyMCEToolbarStyle TinyMceToolbarStyle { get; }
    public HtmlString? NeptunePageContentOnLoad { get; }
    public string ViewPageContentDivID { get; }
    public string EditPageContentDivID { get; }
    public string TogglePageContentFunctionName { get; }
    public string PostUrl { get; }

    public EditViewData(LinkGenerator linkGenerator, TinyMCEExtension.TinyMCEToolbarStyle tinyMceToolbarStyle, EFModels.Entities.NeptunePage neptunePage)
    {
        TinyMceToolbarStyle = tinyMceToolbarStyle;
        NeptunePageContentOnLoad = new HtmlString(neptunePage.NeptunePageContent);
        EditPageContentDivID = $"editNeptunePageContentFor{neptunePage.NeptunePageID}";
        ViewPageContentDivID = $"viewNeptunePageContentFor{neptunePage.NeptunePageID}";
        TogglePageContentFunctionName = $"toggleIsEditingFor{neptunePage.NeptunePageID}";
        PostUrl = SitkaRoute<NeptunePageController>.BuildUrlFromExpression(linkGenerator, x => x.Edit(neptunePage));
    }
}