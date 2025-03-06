using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.DataHub;
using Index = Neptune.WebMvc.Views.DataHub.Index;
using IndexViewData = Neptune.WebMvc.Views.DataHub.IndexViewData;


namespace Neptune.WebMvc.Controllers;

public class DataHubController : NeptuneBaseController<DataHubController>
{
    public DataHubController(NeptuneDbContext dbContext, ILogger<DataHubController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
    {
    }

    [JurisdictionEditFeature]
    [HttpGet]
    public ViewResult Index()
    {
        var treatmentBMPPage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.BMPDataHub);
        var delineationPage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.DelineationDataHub);
        var fieldTripPage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.FieldVisitDataHub);
        var wqmpPage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.WQMPDataHub);
        var simplifiedBMPPage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.SimplifiedBMPsDataHub);
        var wqmpLocationPage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.WQMPLocationsDataHub);
        var assessmentAreaPage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.AssessmentAreasDataHub);
        var ovtasPage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.OVTADataHub);
        var landUseBlocksPage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.LandUseBlockDataHub);
        var parcelPage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ParcelUploadDataHub);
        var regionalSubbasinsPage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.RegionalSubbasinsDataHub);
        var landUseStatisticsPage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.LandUseStatisticsDataHub);
        var modelBasinsPage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ModelBasinsDataHub);
        var precipitationZonesPage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.PrecipitationZonesDataHub);
        
        
        var lastUpdatedDateParcels = _dbContext.Parcels.MaxBy(x => x.LastUpdate)?.LastUpdate;
        var lastUpdatedDateRegionalSubbasin = _dbContext.RegionalSubbasins.MaxBy(x => x.LastUpdate)?.LastUpdate;
        var lastUpdatedDateHRUCharacteristics = _dbContext.HRUCharacteristics.MaxBy(x => x.LastUpdated)?.LastUpdated;
        var lastUpdatedDateModelBasins = _dbContext.ModelBasins.MaxBy(x => x.LastUpdate)?.LastUpdate;
        var lastUpdatedDatePrecipitationZones = _dbContext.PrecipitationZones.MaxBy(x => x.LastUpdate)?.LastUpdate;
        var allMethods = FindAttributedMethods(typeof(PowerBIController), typeof(WebServiceNameAndDescriptionAttribute));
        var serviceDocumentationList = allMethods.Select(c => new WebServiceDocumentation(c, _dbContext, _linkGenerator)).OrderBy(x => x.Name).ToList();
        var webServiceAccessToken = new WebServiceToken(_dbContext, CurrentPerson.WebServiceAccessToken.ToString());
        var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson,
            webServiceAccessToken, serviceDocumentationList, lastUpdatedDateParcels, lastUpdatedDateRegionalSubbasin,
            lastUpdatedDateModelBasins, lastUpdatedDatePrecipitationZones, lastUpdatedDateHRUCharacteristics,
            treatmentBMPPage, delineationPage, fieldTripPage, wqmpPage, simplifiedBMPPage, wqmpLocationPage, assessmentAreaPage,
            ovtasPage, landUseBlocksPage, parcelPage, regionalSubbasinsPage, landUseStatisticsPage, modelBasinsPage, precipitationZonesPage);
        return RazorView<Index, IndexViewData>(viewData);
    }

}