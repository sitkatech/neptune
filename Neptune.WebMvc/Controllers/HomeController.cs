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

using Microsoft.AspNetCore.Diagnostics;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.Home;
using Neptune.WebMvc.Views.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Views.Shared.JurisdictionControls;

namespace Neptune.WebMvc.Controllers
{
    public class HomeController : NeptuneBaseController<HomeController>
    {
        public HomeController(NeptuneDbContext dbContext, ILogger<HomeController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpPost("{gridName}/{printFooter}")]
        [DisableRequestSizeLimit]
        [RequestFormLimits(KeyLengthLimit = int.MaxValue, ValueLengthLimit = int.MaxValue)]
        public FileResult ExportGridToExcel([FromRoute] string gridName, [FromRoute] bool printFooter)
        {
            return ExportGridToExcelImpl(gridName, printFooter);
        }

        [Route("/")] // Default Route
        [HttpGet]
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

            var layerGeoJson = StormwaterJurisdictionModelExtensions.GetBoundaryLayerGeoJson(_dbContext, true, _linkGenerator);

            var projectLocationsMapInitJson = new JurisdictionsMapInitJson("JurisdictionsMap")
            {
                AllowFullScreen = false,
                Layers = new List<LayerGeoJson> { layerGeoJson }
            };
            var projectLocationsMapViewData = new JurisdictionsMapViewData(projectLocationsMapInitJson.MapDivID);

            var launchPadNeptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.LaunchPad);
            var numberOfBmpTypes = _dbContext.TreatmentBMPTypes.Count();
            var managerDashboardDescription = GetManagerDashboardDescription();
            var launchPadViewData = new LaunchPadViewData(_linkGenerator, CurrentPerson, launchPadNeptunePage, numberOfBmpTypes, managerDashboardDescription);

            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePageByPageTypeHomePage, neptunePageByPageTypeHomePageAdditionalInfo, neptunePageByPageTypeHomePageMapInfo, neptuneHomePageImages, projectLocationsMapViewData, projectLocationsMapInitJson, launchPadViewData);

            return RazorView<Views.Home.Index, IndexViewData>(viewData);
        }

        [HttpGet]
        public ViewResult Error()
        {
            var errorMessage = "Oops, an error has occurred!";
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exceptionFeature != null)
            {
                _logger.LogError(exceptionFeature.Error, exceptionFeature.Error.Message);
                var error = GetSitkaDisplayableExceptionIfAny(exceptionFeature.Error);
                if (error != null)
                {
                    errorMessage = error.Message;
                }
            }

            var viewData = new ErrorViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, errorMessage);
            return RazorView<Error, ErrorViewData>(viewData);
        }

        private static SitkaDisplayErrorException? GetSitkaDisplayableExceptionIfAny(Exception lastError)
        {
            while (true)
            {
                if (lastError is SitkaDisplayErrorException error)
                {
                    return error;
                }

                if (lastError.InnerException == null)
                {
                    return null;
                }

                lastError = lastError.InnerException;
            }
        }

        [HttpGet]
        public ViewResult ViewPageContent(NeptunePageTypeEnum neptunePageTypeEnum)
        {
            var neptunePageType = NeptunePageType.ToType(neptunePageTypeEnum);
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, neptunePageType);
            var viewData = new DisplayPageContentViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage);
            return RazorView<DisplayPageContent, DisplayPageContentViewData>(viewData);
        }

        [HttpGet]
        public ViewResult AboutModelingBMPPerformance()
        {
            var con = new HomeController(_dbContext, _logger, _webConfigurationOptions, _linkGenerator) {ControllerContext = ControllerContext};
            return con.ViewPageContent(NeptunePageTypeEnum.AboutModelingBMPPerformance);
        }

        [HttpGet]
        public ViewResult About()
        {
            var con = new HomeController(_dbContext, _logger, _webConfigurationOptions, _linkGenerator) { ControllerContext = ControllerContext };
            return con.ViewPageContent(NeptunePageTypeEnum.About);
        }

        [HttpGet]
        public ViewResult Legal()
        {
            var con = new HomeController(_dbContext, _logger, _webConfigurationOptions, _linkGenerator) { ControllerContext = ControllerContext };
            return con.ViewPageContent(NeptunePageTypeEnum.Legal);
        }

        [HttpGet]
        public ViewResult Modeling()
        {
            var con = new HomeController(_dbContext, _logger, _webConfigurationOptions, _linkGenerator) { ControllerContext = ControllerContext };
            return con.ViewPageContent(NeptunePageTypeEnum.ModelingHomePage);
        }


        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult ManageHomePageImages()
        {
            var imageGalleryViewData = BuildImageGalleryViewData(_linkGenerator, CurrentPerson, _dbContext);
            var viewData = new ManageHomePageImagesViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, imageGalleryViewData);
            return RazorView<ManageHomePageImages, ManageHomePageImagesViewData>(viewData);
        }

        private static ImageGalleryViewData BuildImageGalleryViewData(LinkGenerator linkGenerator, Person currentPerson, NeptuneDbContext dbContext)
        {
            var userCanAddPhotosToHomePage = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            var newPhotoForProjectUrl = SitkaRoute<NeptuneHomePageImageController>.BuildUrlFromExpression(linkGenerator, x => x.New());
            var galleryName = "HomePageImagesGallery";
            var neptuneHomePageImages = NeptuneHomePageImages.List(dbContext); 
            var imageGalleryViewData = new ImageGalleryViewData(linkGenerator, currentPerson, galleryName,
                neptuneHomePageImages,
                userCanAddPhotosToHomePage,
                newPhotoForProjectUrl,
                true,
                x => x.GetCaptionOnFullView(),
                "Photo");
            return imageGalleryViewData;
        }

        [HttpGet]
        public ViewResult Training()
        {
            var neptunePageTypeTraining = NeptunePageType.Training;
            var neptunePageByPageTypeHomePage = NeptunePages.GetNeptunePageByPageType(_dbContext, neptunePageTypeTraining);
            var trainingVideos = _dbContext.TrainingVideos.AsNoTracking().ToList();

            var viewData = new TrainingViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePageByPageTypeHomePage, trainingVideos);
            return RazorView<Training, TrainingViewData>(viewData);
        }


        private string GetManagerDashboardDescription()
        {
            var provisionalBMPRecordCount = TreatmentBMPs.GetProvisionalTreatmentBMPs(_dbContext, CurrentPerson).Count;
            var stormwaterJurisdictionIDs = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson);
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
