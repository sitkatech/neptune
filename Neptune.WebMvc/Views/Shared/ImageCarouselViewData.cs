/*-----------------------------------------------------------------------
<copyright file="ImageGalleryViewData.cs" company="Tahoe Regional Planning Agency">
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

using Neptune.EFModels;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.Shared
{
    public class ImageCarouselViewData
    {
        public List<IFileResourcePhoto> CarouselImages { get; }
        public int Height { get; }
        public UrlTemplate<string> DisplayFileResourceUrl { get; }

        public ImageCarouselViewData(IEnumerable<IFileResourcePhoto> carouselImages, int height, LinkGenerator linkGenerator)
        {
            CarouselImages = carouselImages.ToList();
            Height = height;
            DisplayFileResourceUrl = new UrlTemplate<string>(SitkaRoute<FileResourceController>.BuildUrlFromExpression(linkGenerator, t => t.DisplayResource(UrlTemplate.Parameter1String)));
        }
    }
}
