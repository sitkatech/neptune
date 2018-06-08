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

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Security;
using Neptune.Web.Models;
using Neptune.Web.Common;
using Neptune.Web.Views.Shared;
using Neptune.Web.Security.Shared;
using Neptune.Web.Views.Home;
using Neptune.Web.Views.Map;
using Neptune.Web.Views.Shared.JurisdictionControls;

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

            // map stuff

            var layerGeoJsons = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.GetBoundaryLayerGeoJson(true)
                .Where(x => x.LayerInitialVisibility == LayerInitialVisibility.Show)
                .ToList();

            var projectLocationsMapInitJson = new JurisdictionsMapInitJson("JurisdictionsMap")
            {
                AllowFullScreen = false,
                Layers = layerGeoJsons
            };
            var projectLocationsMapViewData = new JurisdictionsMapViewData(projectLocationsMapInitJson.MapDivID);

            var launchPadNeptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.LaunchPad);
            var numberOfBmpTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Count();
            var launchPadViewData = new LaunchPadViewData(CurrentPerson, launchPadNeptunePage, numberOfBmpTypes);

            var viewData = new IndexViewData(CurrentPerson, neptunePageByPageTypeHomePage,
                neptunePageByPageTypeHomePageAdditionalInfo, neptunePageByPageTypeHomePageMapInfo,
                neptuneHomePageImages, projectLocationsMapViewData, projectLocationsMapInitJson,
                launchPadViewData);

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
        [Route("Legal")]
        [AnonymousUnclassifiedFeature]
        public ViewResult Legal()
        {
            var con = new HomeController {ControllerContext = ControllerContext};
            return con.ViewPageContent(NeptunePageTypeEnum.Legal);
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
                true,
                x => x.CaptionOnFullView,
                "Photo");
            return imageGalleryViewData;
        }
    }
}
