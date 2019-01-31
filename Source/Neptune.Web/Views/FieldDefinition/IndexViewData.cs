﻿/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Common;

namespace Neptune.Web.Views.FieldDefinition
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly FieldDefinitionGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;

        public IndexViewData(Person currentPerson) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = "Field Definitions";
            PageTitle = "Manage Field Definitions";

            GridSpec = new FieldDefinitionGridSpec(new FieldDefinitionViewListFeature().HasPermissionByPerson(currentPerson))
            {
                ObjectNameSingular = "Field Definition",
                ObjectNamePlural = "Field Definitions",
                SaveFiltersInCookie = true
            };
            GridName = "fieldDefinitionsGrid";
            GridDataUrl = SitkaRoute<FieldDefinitionController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData());
        }
    }
}
