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
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.Shared
{
    public class ImageGalleryViewData
    {
        public Person CurrentPerson { get; }
        public string GalleryName { get; }
        public IEnumerable<EFModels.Entities.NeptuneHomePageImage> GalleryImages { get; }
        public string AddNewPhotoUrl { get; }
        public bool UserCanAddPhotos { get; }
        public bool IsGalleryMode { get; }
        public Func<EFModels.Entities.NeptuneHomePageImage, object> SortFunction { get; }
        public string ImageEntityName { get; }
        public UrlTemplate<int> DeleteUrlTemplate { get; }
        public UrlTemplate<int> EditUrlTemplate { get; }

        public ImageGalleryViewData(LinkGenerator linkGenerator, Person currentPerson, string galleryName, IEnumerable<EFModels.Entities.NeptuneHomePageImage> galleryImages, bool canAddPhotos, string addNewPhotoUrl, bool isGalleryMode, Func<EFModels.Entities.NeptuneHomePageImage, object> sortFunction, string imageEntityName)
        {
            CurrentPerson = currentPerson;
            GalleryImages = galleryImages.ToList();
            UserCanAddPhotos = canAddPhotos;
            AddNewPhotoUrl = addNewPhotoUrl;
            GalleryName = galleryName;
            IsGalleryMode = isGalleryMode;
            SortFunction = sortFunction;
            ImageEntityName = imageEntityName;
            EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<NeptuneHomePageImageController>.BuildUrlFromExpression(linkGenerator, x => x.Edit(UrlTemplate.Parameter1Int)));
            DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<NeptuneHomePageImageController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(UrlTemplate.Parameter1Int)));
        }
    }
}
