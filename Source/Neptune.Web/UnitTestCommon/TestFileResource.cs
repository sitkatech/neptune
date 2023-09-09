﻿/*-----------------------------------------------------------------------
<copyright file="TestFileResource.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Models;

namespace Neptune.Web.UnitTestCommon
{
    public static partial class TestFramework
    {
        public static class TestFileResource
        {
            public static FileResource Create()
            {
                var fileResource = new FileResource(FileResourceMimeType.JPEG.FileResourceMimeTypeID,
                    MakeTestImagefileBaseName(),
                    ".jpg",
                    Guid.NewGuid(),
                    new byte[2000],
                    LoginConstants.PersonID,
                    DateTime.Now, false);
                return fileResource;
            }

            public static FileResource Create(DatabaseEntities dbContext)
            {
                var fileResource = new FileResource(FileResourceMimeType.JPEG.FileResourceMimeTypeID,
                    MakeTestImagefileBaseName(),
                    ".jpg",
                    Guid.NewGuid(),
                    new byte[2000],
                    LoginConstants.PersonID,
                    DateTime.Now, false);
                dbContext.FileResources.Add(fileResource);
                return fileResource;
            }

            private static string MakeTestImagefileBaseName()
            {
                return MakeTestName("SomeTestImageFile", FileResource.FieldLengths.OriginalBaseFilename);
            }
        }
    }
}
