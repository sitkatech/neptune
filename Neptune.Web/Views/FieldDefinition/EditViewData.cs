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

using Neptune.EFModels.Entities;
using Neptune.Web.Controllers;
using Neptune.Web.Common;

namespace Neptune.Web.Views.FieldDefinition
{
    public class EditViewData : NeptuneViewData
    {
        public readonly string FileBrowserImageUploadUrl;
        public readonly FieldDefinitionType FieldDefinitionType;
        public readonly string CancelUrl;

        public EditViewData(Person currentPerson, FieldDefinitionType fieldDefinitionType, LinkGenerator linkGenerator, HttpContext httpContext) : base(currentPerson, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            EntityName = "Field Definitions";
            EntityUrl = SitkaRoute<FieldDefinitionController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            SubEntityName = fieldDefinitionType.FieldDefinitionTypeDisplayName;
            PageTitle = "Manage";

            FieldDefinitionType = fieldDefinitionType;
            FileBrowserImageUploadUrl = ""; //todo: SitkaRoute<FileResourceController>.BuildUrlFromExpression(linkGenerator, x => x.CkEditorUploadFileResourceForFieldDefinition(FieldDefinitionType, null));
            CancelUrl = SitkaRoute<FieldDefinitionController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
        }
    }
}
