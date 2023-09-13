/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Common;
using Neptune.Web.Common.DhtmlWrappers;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.CustomAttributeType
{
    public class CustomAttributeTypeGridSpec : GridSpec<EFModels.Entities.CustomAttributeType>
    {
        public CustomAttributeTypeGridSpec(LinkGenerator linkGenerator)
        {
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var editUrlTemplate = new UrlTemplate<int>(SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Edit(UrlTemplate.Parameter1Int)));
            var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(linkGenerator, x => x.DeleteCustomAttributeType(UrlTemplate.Parameter1Int)));

            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.CustomAttributeTypeID), true), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(editUrlTemplate.ParameterReplace(x.CustomAttributeTypeID), true), 30, DhtmlxGridColumnFilterType.None);
            Add(FieldDefinitionType.CustomAttributeType.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.CustomAttributeTypeID), x.CustomAttributeTypeName), 200, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.CustomAttributeDataType.ToGridHeaderString(), a => a.CustomAttributeDataType.CustomAttributeDataTypeDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.MeasurementUnit.ToGridHeaderString(), a => a.GetMeasurementUnitDisplayName(), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.AttributeTypePurpose.ToGridHeaderString(),
                a => a.CustomAttributeTypePurpose.CustomAttributeTypePurposeDisplayName, 200,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Required?", a => a.IsRequired.ToYesNo(), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
