using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.Common.Services.GDAL;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services;
using Neptune.WebMvc.Views.LandUseBlockDownload;

namespace Neptune.WebMvc.Controllers;

public class LandUseBlockDownloadController : NeptuneBaseController<LandUseBlockDownloadController>
{
    private readonly SitkaSmtpClientService _sitkaSmtpClientService;
    private readonly GDALAPIService _gdalApiService;
    private readonly AzureBlobStorageService _azureBlobStorageService;

    public LandUseBlockDownloadController(NeptuneDbContext dbContext, ILogger<LandUseBlockDownloadController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, SitkaSmtpClientService sitkaSmtpClientService, GDALAPIService gdalApiService, AzureBlobStorageService azureBlobStorageService) : base(dbContext, logger, linkGenerator, webConfiguration)
    {
        _sitkaSmtpClientService = sitkaSmtpClientService;
        _gdalApiService = gdalApiService;
        _azureBlobStorageService = azureBlobStorageService;
    }

    [HttpGet]
    [JurisdictionManageFeature]
    public ViewResult DownloadLandUseBlockGeometry()
    {
        var viewModel = new DownloadLandUseBlockGeometryViewModel { PersonID = CurrentPerson.PersonID };
        return ViewDownloadLandUseBlockGeometry(viewModel);
    }

    [HttpPost]
    [JurisdictionManageFeature]
    public async Task<ActionResult> DownloadLandUseBlockGeometry(DownloadLandUseBlockGeometryViewModel viewModel)
    {
        var file = viewModel.FileResourceData;
        var blobName = Guid.NewGuid().ToString();
        //await _azureBlobStorageService.DownloadToBlobStorage(await FileStreamHelpers.StreamToBytes(file), blobName, ".gdb");
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
            return ViewDownloadLandUseBlockGeometry(viewModel);
        }

        try
        {
            var columns = new List<string>
                {
                    $"{CurrentPerson.PersonID} as DownloadedByPersonID",
                    "PLU_Cat as PriorityLandUseType",
                    "LU_Descr as LandUseDescription",
                    "shape as LandUseBlockGeometry",
                    "TGR as TrashGenerationRate",
                    "LU_for_TGR as LandUseForTGR",
                    "MHI as MedianHouseHoldIncome",
                    "Jurisdic as StormwaterJurisdiction",
                    "Permit as PermitType",
                };

            var apiRequest = new GdbToGeoJsonRequestDto()
            {
                BlobContainer = AzureBlobStorageService.BlobContainerName,
                CanonicalName = blobName,
                GdbLayerOutputs = new()
                    {
                        new()
                        {
                            Columns = columns,
                            FeatureLayerName = featureClassNames.Single().LayerName,
                            NumberOfSignificantDigits = 4,
                            Filter = "",
                            CoordinateSystemID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID,
                        }
                    }
            };
            var geoJson = await _gdalApiService.Ogr2OgrGdbToGeoJson(apiRequest);
            var landUseBlockStagings = await GeoJsonSerializer.DeserializeFromFeatureCollectionWithCCWCheck<LandUseBlockStaging>(geoJson,
                GeoJsonSerializer.DefaultSerializerOptions, Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
            var validLandUseBlockStagings = landUseBlockStagings.Where(x => x.Geometry is { IsValid: true, Area: > 0 }).ToList();
            if (validLandUseBlockStagings.Any())
            {
                await _dbContext.Database.ExecuteSqlAsync($"dbo.pLandUseBlockStagingDeleteByPersonID @PersonID = {CurrentPerson.PersonID}");
                _dbContext.LandUseBlockStagings.AddRange(validLandUseBlockStagings);
                await _dbContext.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            if (e.Message.Contains("Unrecognized field name",
                    StringComparison.InvariantCultureIgnoreCase))
            {
                ModelState.AddModelError("",
                    "The columns in the uploaded file did not match the LandUseBlock schema. The file is invalid and cannot be uploaded.");
            }
            else
            {
                ModelState.AddModelError("",
                    $"There was a problem processing the Feature Class \"{featureClassNames[0]}\". The file may be corrupted or invalid.");
            }
            return ViewDownloadLandUseBlockGeometry(viewModel);
        }

        try
        {
            var count = await _dbContext.LandUseBlockStagings.CountAsync();

            //if (count > 0)
            //{
            //    BackgroundJob.Enqueue<LandUseBlockDownloadBackgroundJob>(x => x.RunJob(CurrentPerson.PersonID));
            //}
        }
        catch (Exception)
        {
            var body =
                "There was an unexpected system error during processing of your Parcel Download. The Orange County Stormwater Tools development team will investigate and be in touch when this issue is resolved.";

            var mailMessage = new MailMessage
            {
                Subject = "Land Use Block Download Error",
                Body = body,
                From = new MailAddress(_webConfiguration.DoNotReplyEmail, "Orange County Stormwater Tools")
            };

            mailMessage.To.Add(CurrentPerson.Email);
            await _sitkaSmtpClientService.Send(mailMessage);

            throw;
        }

        SetMessageForDisplay("The Land Use Blocks were successfully added to the staging area. The staged Land Use Blocks will be processed and added to the system. You will receive an email notification when this process completes or if errors in the upload are discovered during processing.");

        return Redirect(SitkaRoute<LandUseBlockController>.BuildUrlFromExpression(_linkGenerator, x => x.Index()));
    }

    private ViewResult ViewDownloadLandUseBlockGeometry(DownloadLandUseBlockGeometryViewModel viewModel)
    {
        var newGisDownloadUrl = SitkaRoute<LandUseBlockDownloadController>.BuildUrlFromExpression(_linkGenerator, x => x.DownloadLandUseBlockGeometry());

        var viewData = new DownloadLandUseBlockGeometryViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, newGisDownloadUrl);
        return RazorView<DownloadLandUseBlockGeometry, DownloadLandUseBlockGeometryViewData, DownloadLandUseBlockGeometryViewModel>(viewData, viewModel);
    }
}