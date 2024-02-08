using Neptune.WebMvc.Common;
using Neptune.WebMvc.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Views.ParcelLayerUpload;
using Neptune.Common.Services.GDAL;
using Neptune.WebMvc.Services;
using Neptune.Common;
using Hangfire;
using Neptune.Jobs.Hangfire;

namespace Neptune.WebMvc.Controllers
{
    public class ParcelLayerUploadController : NeptuneBaseController<ParcelLayerUploadController>
    {
        private readonly GDALAPIService _gdalApiService;
        private readonly AzureBlobStorageService _azureBlobStorageService;

        public ParcelLayerUploadController(NeptuneDbContext dbContext, ILogger<ParcelLayerUploadController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, GDALAPIService gdalApiService, AzureBlobStorageService azureBlobStorageService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _gdalApiService = gdalApiService;
            _azureBlobStorageService = azureBlobStorageService;
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult UpdateParcelLayerGeometry()
        {
            var viewModel = new UploadParcelLayerViewModel();
            return ViewUpdateParcelLayerGeometry(viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [RequestSizeLimit(100_000_000_000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 100_000_000_000)]
        public async Task<IActionResult> UpdateParcelLayerGeometry(UploadParcelLayerViewModel viewModel)
        {
            var file = viewModel.FileResourceData;
            var blobName = Guid.NewGuid().ToString();
            await _azureBlobStorageService.UploadToBlobStorage(await FileStreamHelpers.StreamToBytes(file), blobName, ".gdb");
            var featureClassNames = await _gdalApiService.OgrInfoGdbToFeatureClassInfo(file);

            if (featureClassNames.Count == 0)
            {
                ModelState.AddModelError("FileResourceData",
                    "The file geodatabase contained no feature class. Please upload a file geodatabase containing exactly one feature class.");
            }
            else if (featureClassNames.Count > 1)
            {
                ModelState.AddModelError("FileResourceData",
                    "The file geodatabase contained more than one feature class. Please upload a file geodatabase containing exactly one feature class.");
            }
            if (!ModelState.IsValid)
            {
                return ViewUpdateParcelLayerGeometry(viewModel);
            }

            BackgroundJob.Enqueue<ParcelUploadJob>(x => x.RunJob(blobName, featureClassNames.Single().LayerName, CurrentPerson.PersonID, CurrentPerson.Email));

            SetMessageForDisplay("Parcels are currently being added to the staging area. The staged Parcels will be processed and added to the system. You will receive an email notification when this process completes or if errors in the upload are discovered during processing.");

            return Redirect(SitkaRoute<ParcelController>.BuildUrlFromExpression(_linkGenerator, c => c.Index()));
        }

        private ViewResult ViewUpdateParcelLayerGeometry(UploadParcelLayerViewModel viewModel)
        {
            var viewData = new UploadParcelLayerViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson);
            return RazorView<UploadParcelLayer, UploadParcelLayerViewData, UploadParcelLayerViewModel>(viewData, viewModel);
        }
    }


}
