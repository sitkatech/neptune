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

using Neptune.Web.Common;
using Neptune.Web.Security;
using Neptune.Web.Views.Home;
using Neptune.Web.Views.Shared;
using Microsoft.AspNetCore.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.JurisdictionControls;

namespace Neptune.Web.Controllers
{
    public class HomeController : NeptuneBaseController<HomeController>
    {
        public HomeController(NeptuneDbContext dbContext, ILogger<HomeController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

        //[AnonymousUnclassifiedFeature]
        //public FileResult ExportGridToExcel(string gridName, bool printFooter)
        //{
        //    return ExportGridToExcelImpl(gridName, printFooter);
        //}

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public ViewResult Index()
        {
            var neptunePageTypeHomePage = NeptunePageType.HomePage;
            var neptunePageByPageTypeHomePage = NeptunePages.GetNeptunePageByPageType(_dbContext, neptunePageTypeHomePage);

            var neptunePageTypeHomePageAdditionalInfo = NeptunePageType.HomeAdditionalInfo;
            var neptunePageByPageTypeHomePageAdditionalInfo = NeptunePages.GetNeptunePageByPageType(_dbContext, neptunePageTypeHomePageAdditionalInfo);

            var neptunePageTypeHomePageMapInfo = NeptunePageType.HomeMapInfo;
            var neptunePageByPageTypeHomePageMapInfo = NeptunePages.GetNeptunePageByPageType(_dbContext, neptunePageTypeHomePageMapInfo);

            var neptuneHomePageImages = NeptuneHomePageImages.List(_dbContext);

            // map stuff

            var layerGeoJsons = StormwaterJurisdictionModelExtensions.GetBoundaryLayerGeoJson(_dbContext, true)
                .Where(x => x.LayerInitialVisibility == LayerInitialVisibility.Show)
                .ToList();

            var projectLocationsMapInitJson = new JurisdictionsMapInitJson("JurisdictionsMap")
            {
                AllowFullScreen = false,
                Layers = layerGeoJsons
            };
            var projectLocationsMapViewData = new JurisdictionsMapViewData(projectLocationsMapInitJson.MapDivID);

            var launchPadNeptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.LaunchPad);
            var numberOfBmpTypes = _dbContext.TreatmentBMPTypes.Count();
            var managerDashboardDescription = GetManagerDashboardDescription();
            var launchPadViewData = new LaunchPadViewData(CurrentPerson, launchPadNeptunePage, numberOfBmpTypes, managerDashboardDescription);

            var viewData = new IndexViewData(CurrentPerson, neptunePageByPageTypeHomePage,
                neptunePageByPageTypeHomePageAdditionalInfo, neptunePageByPageTypeHomePageMapInfo,
                neptuneHomePageImages, projectLocationsMapViewData, projectLocationsMapInitJson,
                launchPadViewData, _linkGenerator, HttpContext);

            return RazorView<Views.Home.Index, IndexViewData>(viewData);
        }

        [AnonymousUnclassifiedFeature]
        public ViewResult Error()
        {
            var viewData = new ErrorViewData(CurrentPerson, _linkGenerator, HttpContext);
            return RazorView<Error, ErrorViewData>(viewData);
        }

        [AnonymousUnclassifiedFeature]
        public ViewResult NotFound()
        {
            var viewData = new NotFoundViewData(CurrentPerson, _linkGenerator, HttpContext);
            return RazorView<NotFound, NotFoundViewData>(viewData);
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public ViewResult ViewPageContent(NeptunePageTypeEnum neptunePageTypeEnum)
        {
            var neptunePageType = NeptunePageType.ToType(neptunePageTypeEnum);
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, neptunePageType);
            var viewData = new DisplayPageContentViewData(CurrentPerson, neptunePage, _linkGenerator, HttpContext);
            return RazorView<DisplayPageContent, DisplayPageContentViewData>(viewData);
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public ViewResult AboutModelingBMPPerformance()
        {
            var con = new HomeController(_dbContext, _logger, _linkGenerator) {ControllerContext = ControllerContext};
            return con.ViewPageContent(NeptunePageTypeEnum.AboutModelingBMPPerformance);
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public ViewResult About()
        {
            var con = new HomeController(_dbContext, _logger, _linkGenerator) { ControllerContext = ControllerContext };
            return con.ViewPageContent(NeptunePageTypeEnum.About);
        }

        [HttpGet]
        [Route("Legal")]
        [AnonymousUnclassifiedFeature]
        public ViewResult Legal()
        {
            var con = new HomeController(_dbContext, _logger, _linkGenerator) { ControllerContext = ControllerContext };
            return con.ViewPageContent(NeptunePageTypeEnum.Legal);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult ManageHomePageImages()
        {
            var imageGalleryViewData = BuildImageGalleryViewData(CurrentPerson, _dbContext);
            var viewData = new ManageHomePageImagesViewData(CurrentPerson, imageGalleryViewData, _linkGenerator, HttpContext);
            return RazorView<ManageHomePageImages, ManageHomePageImagesViewData>(viewData);
        }

        private static ImageGalleryViewData BuildImageGalleryViewData(Person currentPerson, NeptuneDbContext dbContext)
        {
            var userCanAddPhotosToHomePage = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            var newPhotoForProjectUrl = "";//todo: SitkaRoute<NeptuneHomePageImageController>.BuildUrlFromExpression(x => x.New());
            var galleryName = "HomePageImagesGallery";
            var neptuneHomePageImages = NeptuneHomePageImages.List(dbContext); 
            var imageGalleryViewData = new ImageGalleryViewData(currentPerson,
                galleryName,
                neptuneHomePageImages,
                userCanAddPhotosToHomePage,
                newPhotoForProjectUrl,
                true,
                x => x.GetCaptionOnFullView(),
                "Photo");
            return imageGalleryViewData;
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public ViewResult Training()
        {
            var neptunePageTypeTraining = NeptunePageType.Training;
            var neptunePageByPageTypeHomePage = NeptunePages.GetNeptunePageByPageType(_dbContext, neptunePageTypeTraining);
            var trainingVideos = _dbContext.TrainingVideos.ToList();

            var viewData = new TrainingViewData(CurrentPerson, neptunePageByPageTypeHomePage, trainingVideos, _linkGenerator, HttpContext);
            return RazorView<Training, TrainingViewData>(viewData);
        }


        private string GetManagerDashboardDescription()
        {
            var provisionalBMPRecordCount = TreatmentBMPs.GetProvisionalTreatmentBMPs(_dbContext, CurrentPerson).Count;
            var stormwaterJurisdictionIDs = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanViewWithContext(_dbContext);
            var provisionalFieldVisitCount = vFieldVisitDetaileds.GetProvisionalFieldVisits(_dbContext, stormwaterJurisdictionIDs).Count;
            string managerDashboardDescription = null;
            if (provisionalBMPRecordCount > 0)
            {
                managerDashboardDescription = $"There are <strong>{provisionalBMPRecordCount} Provisional BMP Records</strong>";
                if (provisionalFieldVisitCount > 0)
                {
                    managerDashboardDescription +=
                        $" and <strong>{provisionalFieldVisitCount} Provisional Assessment and Maintenance Records</strong>";
                }

                managerDashboardDescription += " waiting for your verification.";
            }
            else if (provisionalFieldVisitCount > 0)
            {
                managerDashboardDescription = $"There are {provisionalFieldVisitCount} Assessment and Maintenance Records Added during a Field Visit waiting for your verification.";

            }

            return managerDashboardDescription;
        }
    }
}
