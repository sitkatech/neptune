using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.Delineation;
using Neptune.WebMvc.Views.LoadGeneratingUnit;

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
            var hruCharacteristics = vLoadGeneratingUnits.List(_dbContext);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vLoadGeneratingUnit>(hruCharacteristics, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}