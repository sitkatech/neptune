using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services.Filters;
using Neptune.WebMvc.Views.Delineation;
using Neptune.WebMvc.Views.HRUCharacteristic;
using Neptune.WebMvc.Views.LoadGeneratingUnit;
using Neptune.WebMvc.Views.Shared;
using Neptune.WebMvc.Views.Shared.HRUCharacteristics;
using Neptune.WebMvc.Views.Shared.ModeledPerformance;
using NetTopologySuite.Features;
using DetailViewData = Neptune.WebMvc.Views.LoadGeneratingUnit.DetailViewData;
using IndexViewData = Neptune.WebMvc.Views.LoadGeneratingUnit.IndexViewData;

namespace Neptune.WebMvc.Controllers
{
    public class LoadGeneratingUnitController : NeptuneBaseController<LoadGeneratingUnitController>
    {
        public LoadGeneratingUnitController(NeptuneDbContext dbContext, ILogger<LoadGeneratingUnitController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.HRUCharacteristics);

            var stormwaterJurisdictionIDs = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson).ToList();
            BoundingBoxDto boundingBoxDto;
            if (stormwaterJurisdictionIDs.Any())
            {
                var geometries = StormwaterJurisdictionGeometries
                    .ListByStormwaterJurisdictionIDList(_dbContext, stormwaterJurisdictionIDs)
                    .Select(x => x.Geometry4326);
                boundingBoxDto = new BoundingBoxDto(geometries);
            }
            else
            {
                boundingBoxDto = new BoundingBoxDto();
            }

            var treatmentBMPs = TreatmentBMPs.GetNonPlanningModuleBMPs(_dbContext).Include(x => x.Delineation).Where(x => stormwaterJurisdictionIDs.Contains(x.StormwaterJurisdictionID)).ToList();
            var delineationMapInitJson = new DelineationMapInitJson("delineationMap", treatmentBMPs, boundingBoxDto, MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList(), _linkGenerator);
            var stormwaterJurisdictionCqlFilter = CurrentPerson.GetStormwaterJurisdictionCqlFilter(_dbContext);

            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage, delineationMapInitJson, _webConfiguration.MapServiceUrl, stormwaterJurisdictionCqlFilter);
            return RazorView<Views.LoadGeneratingUnit.Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<vLoadGeneratingUnit> LoadGeneratingUnitGridJsonData()
        {
            var gridSpec = new LoadGeneratingUnitGridSpec(_linkGenerator);
            var loadGeneratingUnits = vLoadGeneratingUnits.List(_dbContext);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vLoadGeneratingUnit>(loadGeneratingUnits, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{loadGeneratingUnitPrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("loadGeneratingUnitPrimaryKey")]
        public ViewResult Detail([FromRoute] LoadGeneratingUnitPrimaryKey loadGeneratingUnitPrimaryKey)
        {
            var loadGeneratingUnit = LoadGeneratingUnits.GetByID(_dbContext, loadGeneratingUnitPrimaryKey);
            var loadGeneratingUnit4326 = LoadGeneratingUnit4326s.GetByID(_dbContext, loadGeneratingUnit.LoadGeneratingUnitID);
            var regionalSubbasin = loadGeneratingUnit.RegionalSubbasin;
            var treatmentBMP = loadGeneratingUnit.Delineation?.TreatmentBMP;
            var hruLog = loadGeneratingUnit.HRULog;
            var wqmp = loadGeneratingUnit.WaterQualityManagementPlan;
            
            var boundingBoxGeometry = loadGeneratingUnit4326.LoadGeneratingUnit4326Geometry;
            var feature =
                new Feature(loadGeneratingUnit4326.LoadGeneratingUnit4326Geometry, new AttributesTable());
            var loadGeneratingUnitFeatureCollection = new FeatureCollection
            {
                feature
            };


            var layerGeoJsons = new List<LayerGeoJson>
            {
                new("loadGeneratingUnits", loadGeneratingUnitFeatureCollection, "#fb00be",
                    1,
                    LayerInitialVisibility.Show),
            };

            var boundingBoxDto = new BoundingBoxDto(boundingBoxGeometry);
            var mapInitJson = new MapInitJson("loadGeneratingUnitMap", 0, layerGeoJsons,
                boundingBoxDto);

            var hruCharacteristics = vHRUCharacteristics.ListByLoadGeneratingUnitID(_dbContext, loadGeneratingUnit.LoadGeneratingUnitID);
            var hruCharacteristicsViewData = new HRUCharacteristicsViewData(hruCharacteristics);
            var mapServiceUrl = _webConfiguration.MapServiceUrl;
            var viewData = new DetailViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, loadGeneratingUnit, regionalSubbasin, treatmentBMP, wqmp, hruLog, mapInitJson, hruCharacteristicsViewData, mapServiceUrl);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet("{loadGeneratingUnitPrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("loadGeneratingUnitPrimaryKey")]
        public GridJsonNetJObjectResult<vHRUCharacteristic> HRUCharacteristicGridJsonData([FromRoute] LoadGeneratingUnitPrimaryKey loadGeneratingUnitPrimaryKey)
        {
            var loadGeneratingUnit = LoadGeneratingUnits.GetByID(_dbContext, loadGeneratingUnitPrimaryKey);
            var gridSpec = new HRUCharacteristicGridSpec(_linkGenerator);
            var hruCharacteristics = vHRUCharacteristics.ListByLoadGeneratingUnitID(_dbContext, loadGeneratingUnit.LoadGeneratingUnitID);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vHRUCharacteristic>(hruCharacteristics, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}