using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.Common.Services.GDAL;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services;
using Neptune.WebMvc.Views.LandUseBlockDownload;
using NetTopologySuite.Features;

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
        var stormwaterJurisdiction = _dbContext.StormwaterJurisdictions.Include(x => x.Organization)
            .Single(x => x.StormwaterJurisdictionID == viewModel.StormwaterJurisdictionID)
            .GetOrganizationDisplayName();
        await using var stream = new MemoryStream();

        var featureCollection = new FeatureCollection();
        var landUseBlocks = _dbContext.LandUseBlocks
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization)
            .Where(x =>
                x.StormwaterJurisdictionID == viewModel.StormwaterJurisdictionID).ToList();

        foreach (var landUseBlock in landUseBlocks)
        {
            var attributesTable = new AttributesTable
            {
                { "LandUseBlockID", landUseBlock.LandUseBlockID },
                { "PriorityLandUseType", PriorityLandUseType.AllLookupDictionary[(int)landUseBlock.PriorityLandUseTypeID].PriorityLandUseTypeDisplayName},
                { "LandUseDescription", landUseBlock.LandUseDescription },
                { "TrashGenerationRate", landUseBlock.TrashGenerationRate },
                { "LandUseForTGR", landUseBlock.LandUseForTGR },
                { "MedianHouseHoldIncome", landUseBlock.MedianHouseholdIncomeRetail },
                { "StormwaterJurisdiction", landUseBlock.StormwaterJurisdiction.GetOrganizationDisplayName() },
                { "PermitType", PermitType.AllLookupDictionary[landUseBlock.PermitTypeID].PermitTypeDisplayName }
            };
            var feature = new Feature(landUseBlock.LandUseBlockGeometry, attributesTable);
            featureCollection.Add(feature);
        }

        await GeoJsonSerializer.SerializeAsGeoJsonToStream(featureCollection,
            GeoJsonSerializer.DefaultSerializerOptions, stream);

        var jurisdictionName = stormwaterJurisdiction.Replace(' ', '-');

        var gdbInput = new GdbInput()
        {
            FileContents = stream.ToArray(),
            LayerName = "test",
            CoordinateSystemID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID,
            GeometryTypeName = "POLYGON",
        };
        var bytes = await _gdalApiService.Ogr2OgrInputToGdbAsZip(new GdbInputsToGdbRequestDto()
        {
            GdbInputs = new List<GdbInput> { gdbInput },
            GdbName = $"{jurisdictionName}"
        });

        return File(bytes, "application/zip", $"{jurisdictionName}-land-use-block.gdb.zip");
    }

    private ViewResult ViewDownloadLandUseBlockGeometry(DownloadLandUseBlockGeometryViewModel viewModel)
    {
        var newGisDownloadUrl = SitkaRoute<LandUseBlockDownloadController>.BuildUrlFromExpression(_linkGenerator, x => x.DownloadLandUseBlockGeometry());

        var viewData = new DownloadLandUseBlockGeometryViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, newGisDownloadUrl, StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson));
        return RazorView<DownloadLandUseBlockGeometry, DownloadLandUseBlockGeometryViewData, DownloadLandUseBlockGeometryViewModel>(viewData, viewModel);
    }
}