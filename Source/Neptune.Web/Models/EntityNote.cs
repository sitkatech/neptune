﻿/*-----------------------------------------------------------------------
<copyright file="EntityNote.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.Models
{
    public class EntityNote : IEntityNote
    {
        public DateTime LastUpdated { get; }
        public string LastUpdatedBy { get; }
        public string DeleteUrl { get; }
        public string EditUrl { get; }
        public string Note { get; set; }
        public string DisplayCssClass { get; set; }

        public EntityNote(DateTime lastUpdated, string lastUpdatedBy, string deleteUrl, string editUrl, string note, string displayCssClass)
        {
            LastUpdated = lastUpdated;
            LastUpdatedBy = lastUpdatedBy;
            DeleteUrl = deleteUrl;
            EditUrl = editUrl;
            Note = note;
            DisplayCssClass = displayCssClass;
        }

        public static List<EntityNote> CreateFromEntityNote(List<IEntityNote> entityNotes)
        {
            return entityNotes.Select(x => new EntityNote(x.LastUpdated, x.LastUpdatedBy, x.DeleteUrl, x.EditUrl, x.Note, null)).ToList();
        }
    }
}
