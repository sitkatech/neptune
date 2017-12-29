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

using LtInfo.Common.ModalDialog;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.ObservationType
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly IndexGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage)
            : base(currentPerson, neptunePage)
        {
            PageTitle = $"{Models.FieldDefinition.ObservationType.GetFieldDefinitionLabelPluralized()}";

            var contentUrl = SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(t => t.New());
            GridSpec = new IndexGridSpec(currentPerson)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.ObservationType.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{Models.FieldDefinition.ObservationType.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true,
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, $"Create a new {Models.FieldDefinition.ObservationType.GetFieldDefinitionLabel()}")
            };

            GridName = "observationTypeGrid";
            GridDataUrl = SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData());
        }
    }
}
