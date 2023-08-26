/*-----------------------------------------------------------------------
<copyright file="ImageGalleryViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace Neptune.Web.Views.Shared
{
    public class ImageGalleryViewData
    {
        public Person CurrentPerson { get; }
        public string GalleryName { get; }
        public IEnumerable<NeptuneHomePageImage> GalleryImages { get; }
        public string AddNewPhotoUrl { get; }
        public bool UserCanAddPhotos { get; }
        public bool IsGalleryMode { get; }
        public Func<NeptuneHomePageImage, object> SortFunction { get; }
        public string ImageEntityName { get; }

        public ImageGalleryViewData(Person currentPerson, string galleryName, IEnumerable<NeptuneHomePageImage> galleryImages, bool canAddPhotos, string addNewPhotoUrl, bool isGalleryMode, Func<NeptuneHomePageImage, object> sortFunction, string imageEntityName)
        {
            CurrentPerson = currentPerson;
            GalleryImages = galleryImages.ToList();
            UserCanAddPhotos = canAddPhotos;
            AddNewPhotoUrl = addNewPhotoUrl;
            GalleryName = galleryName;
            IsGalleryMode = isGalleryMode;
            SortFunction = sortFunction;
            ImageEntityName = imageEntityName;
        }
    }
}
