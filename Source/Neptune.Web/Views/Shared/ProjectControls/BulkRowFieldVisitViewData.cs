﻿/*-----------------------------------------------------------------------
<copyright file="BulkRowFieldVisitViewData.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
using System;
using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Controllers;


namespace Neptune.Web.Views.Shared.ProjectControls
{
    public class BulkRowFieldVisitViewData
    {
        public List<Models.FieldVisit> FieldVisits { get; }
        public string BulkRowPostUrl { get; }
        public string EntityLabel { get; }
        public string EntityModalDescription { get; }

        public BulkRowFieldVisitViewData(List<Models.FieldVisit> fieldVisits, string bulkRowPostUrl, string entityLabel, string entityModalDescription)
        {
            FieldVisits = fieldVisits;
            BulkRowPostUrl = bulkRowPostUrl;

            EntityLabel = entityLabel + (fieldVisits.Count > 1 ? "s" : String.Empty);
            EntityModalDescription = entityModalDescription;
        }
    }
}
