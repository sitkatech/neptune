/*-----------------------------------------------------------------------
<copyright file="HomeController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Security;
using Neptune.Web.Models;
using Neptune.Web.Common;
using Neptune.Web.Views.Shared;
using Neptune.Web.Security.Shared;
using Neptune.Web.Views.Home;

namespace Neptune.Web.Controllers
{
    public class HomeController : NeptuneBaseController
    {
        [AnonymousUnclassifiedFeature]
        public FileResult ExportGridToExcel(string gridName, bool printFooter)
        {
            return ExportGridToExcelImpl(gridName, printFooter);
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public ViewResult Index()
        {
            var neptunePageTypeHomePage = NeptunePageType.ToType(NeptunePageTypeEnum.HomePage);
            var neptunePageByPageTypeHomePage = NeptunePage.GetNeptunePageByPageType(neptunePageTypeHomePage);

            var neptunePageTypeHomePageAdditionalInfo = NeptunePageType.ToType(NeptunePageTypeEnum.HomeAdditionalInfo);
            var neptunePageByPageTypeHomePageAdditionalInfo = NeptunePage.GetNeptunePageByPageType(neptunePageTypeHomePageAdditionalInfo);

            var neptunePageTypeHomePageMapInfo = NeptunePageType.ToType(NeptunePageTypeEnum.HomeMapInfo);
            var neptunePageByPageTypeHomePageMapInfo = NeptunePage.GetNeptunePageByPageType(neptunePageTypeHomePageMapInfo);

            var neptuneHomePageImages = HttpRequestStorage.DatabaseEntities.NeptuneHomePageImages.ToList().OrderBy(x => x.SortOrder).ToList();

            var viewData = new IndexViewData(CurrentPerson, neptunePageByPageTypeHomePage, neptunePageByPageTypeHomePageAdditionalInfo, neptunePageByPageTypeHomePageMapInfo, neptuneHomePageImages);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [AnonymousUnclassifiedFeature]
        public ViewResult Error()
        {
            var viewData = new ErrorViewData(CurrentPerson);
            return RazorView<Error, ErrorViewData>(viewData);
        }

        [AnonymousUnclassifiedFeature]
        public ViewResult NotFound()
        {
            var viewData = new NotFoundViewData(CurrentPerson);
            return RazorView<NotFound, NotFoundViewData>(viewData);
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public ViewResult ViewPageContent(NeptunePageTypeEnum neptunePageTypeEnum)
        {
            var neptunePageType = NeptunePageType.ToType(neptunePageTypeEnum);
            var viewData = new DisplayPageContentViewData(CurrentPerson, neptunePageType);
            return RazorView<DisplayPageContent, DisplayPageContentViewData>(viewData);
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public ViewResult About()
        {
            var con = new HomeController { ControllerContext = ControllerContext };
            return con.ViewPageContent(NeptunePageTypeEnum.About);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult ManageHomePageImages()
        {
            var viewData = new ManageHomePageImagesViewData(CurrentPerson, BuildImageGalleryViewData(CurrentPerson));
            return RazorView<ManageHomePageImages, ManageHomePageImagesViewData>(viewData);
        }

        private static ImageGalleryViewData BuildImageGalleryViewData(Person currentPerson)
        {
            var userCanAddPhotosToHomePage = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            var newPhotoForProjectUrl = SitkaRoute<NeptuneHomePageImageController>.BuildUrlFromExpression(x => x.New());
            var galleryName = "HomePageImagesGallery";
            var neptuneHomePageImages = HttpRequestStorage.DatabaseEntities.NeptuneHomePageImages.ToList(); 
            var imageGalleryViewData = new ImageGalleryViewData(currentPerson,
                galleryName,
                neptuneHomePageImages,
                userCanAddPhotosToHomePage,
                newPhotoForProjectUrl,
                null,
                true,
                x => x.CaptionOnFullView,
                "Photo");
            return imageGalleryViewData;
        }
    }
}
