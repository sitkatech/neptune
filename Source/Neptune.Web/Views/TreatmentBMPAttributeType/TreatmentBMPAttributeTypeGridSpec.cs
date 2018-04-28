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

using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAttributeType
{
    public class TreatmentBMPAttributeTypeGridSpec : GridSpec<Models.TreatmentBMPAttributeType>
    {
        public TreatmentBMPAttributeTypeGridSpec()
        {
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(x.GetEditUrl(), true), 30, DhtmlxGridColumnFilterType.None);
            Add(Models.FieldDefinition.TreatmentBMPAttributeType.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(a.GetDetailUrl(), a.TreatmentBMPAttributeTypeName), 200, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.TreatmentBMPAttributeDataType.ToGridHeaderString(), a => a.TreatmentBMPAttributeDataType.TreatmentBMPAttributeDataTypeDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.MeasurementUnit.ToGridHeaderString(), a => a.GetMeasurementUnitDisplayName(), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.AttributeTypePurpose.ToGridHeaderString(),
                a => a.TreatmentBMPAttributeTypePurpose.TreatmentBMPAttributeTypePurposeDisplayName, 200,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Required?", a => a.IsRequired.ToYesNo(), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
