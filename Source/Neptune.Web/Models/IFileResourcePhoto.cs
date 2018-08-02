﻿/*-----------------------------------------------------------------------
<copyright file="IFileResourcePhoto.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace Neptune.Web.Models
{
    public interface IFileResourcePhoto
    {
        DateTime GetCreateDate();
        int PrimaryKey { get; }
        FileResource FileResource { get; set; }
        string GetDeleteUrl();
        string GetCaptionOnFullView();
        string GetCaptionOnGallery();
        string Caption { get; set; }
        string GetPhotoUrl();
        string PhotoUrlScaledThumbnail(int maxHeight);
        string GetEditUrl();
        List<string> AdditionalCssClasses { get; }
        object OrderBy { get; set; }
    }
}
