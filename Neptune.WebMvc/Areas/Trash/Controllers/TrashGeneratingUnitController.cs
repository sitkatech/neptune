using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Areas.Trash.Views.TrashGeneratingUnit;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services.Filters;

namespace Neptune.WebMvc.Areas.Trash.Controllers
{
    public class TrashGeneratingUnitController : NeptuneBaseController<TrashGeneratingUnitController>
    {
        public TrashGeneratingUnitController(NeptuneDbContext dbContext, ILogger<TrashGeneratingUnitController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration);
            return RazorView<Neptune.WebMvc.Areas.Trash.Views.TrashGeneratingUnit.Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public GridJsonNetJObjectResult<vTrashGeneratingUnitLoadStatistic> TrashGeneratingUnitGridJsonData()
        {
            var gridSpec = new TrashGeneratingUnitGridSpec(_linkGenerator);
            var stormwaterJurisdictionIDsCurrentPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson);
            var treatmentBMPs = _dbContext.vTrashGeneratingUnitLoadStatistics
                .Where(x => stormwaterJurisdictionIDsCurrentPersonCanView.Contains(x.StormwaterJurisdictionID))
                .OrderByDescending(x => x.LastUpdateDate).ToList();
            return new GridJsonNetJObjectResult<vTrashGeneratingUnitLoadStatistic>(treatmentBMPs, gridSpec);
        }

        [HttpGet("{jurisdictionPrimaryKey}")]
        [AnonymousUnclassifiedFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("jurisdictionPrimaryKey")]
        public JsonResult AcreBasedCalculations([FromRoute] StormwaterJurisdictionPrimaryKey jurisdictionPrimaryKey)
        {
            var jurisdiction = jurisdictionPrimaryKey.EntityObject;
            var trashGeneratingUnits = _dbContext.TrashGeneratingUnits.Include(x => x.LandUseBlock).Where(x=>x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID && x.LandUseBlock != null).ToList();

            var fullTrashCapture = trashGeneratingUnits.FullTrashCaptureAcreage();

            var equivalentArea = trashGeneratingUnits.EquivalentAreaAcreage();

            var totalAcresCaptured = fullTrashCapture + equivalentArea;

            var totalPLUAcres = jurisdiction.LandUseBlocks
                .Where(x => x.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID && x.PermitTypeID == PermitType.PhaseIMS4.PermitTypeID).Sum(x =>
                    x.LandUseBlockGeometry.Area * Constants.SquareMetersToAcres);

            var percentTreated = totalPLUAcres != 0 ? totalAcresCaptured / totalPLUAcres : 0;

            return Json(new AreaBasedAcreCalculationsDto
            {
                FullTrashCaptureAcreage = fullTrashCapture,
                EquivalentAreaAcreage = equivalentArea,
                TotalAcresCaptured = totalAcresCaptured,
                TotalPLUAcres = totalPLUAcres,
                PercentTreated = percentTreated
            });
        }

        [HttpGet("{jurisdictionPrimaryKey}")]
        [AnonymousUnclassifiedFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("jurisdictionPrimaryKey")]
        public JsonResult OVTABasedResultsCalculations([FromRoute] StormwaterJurisdictionPrimaryKey jurisdictionPrimaryKey)
        {
            var jurisdiction = jurisdictionPrimaryKey.EntityObject;
            var trashGeneratingUnits = _dbContext.TrashGeneratingUnits.Include(x => x.LandUseBlock).Where(x=>x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID && x.LandUseBlock != null).ToList();

            var sumPLUAcresWhereOVTAIsA = trashGeneratingUnits.PriorityOVTAScoreAAcreage();

            var sumPLUAcrexsWhereOVTAIsB = trashGeneratingUnits.PriorityOVTAScoreBAcreage();

            var sumPLUAcrexsWhereOVTAIsC = trashGeneratingUnits.PriorityOVTAScoreCAcreage();

            var sumPLUAcrexsWhereOVTAIsD = trashGeneratingUnits.PriorityOVTAScoreDAcreage();


            var sumALUAcresWhereOVTAIsA = trashGeneratingUnits.AlternateOVTAScoreAAcreage();

            var sumALUAcresWhereOVTAIsB = trashGeneratingUnits.AlternateOVTAScoreBAcreage();

            var sumALUAcresWhereOVTAIsC = trashGeneratingUnits.AlternateOVTAScoreCAcreage();

            var sumALUAcresWhereOVTAIsD = trashGeneratingUnits.AlternateOVTAScoreDAcreage();

            return Json(new OVTAResultsDto
            {
                PLUSumAcresWhereOVTAIsA = sumPLUAcresWhereOVTAIsA,
                PLUSumAcresWhereOVTAIsB = sumPLUAcrexsWhereOVTAIsB,
                PLUSumAcresWhereOVTAIsC = sumPLUAcrexsWhereOVTAIsC,
                PLUSumAcresWhereOVTAIsD = sumPLUAcrexsWhereOVTAIsD,
                ALUSumAcresWhereOVTAIsA = sumALUAcresWhereOVTAIsA,
                ALUSumAcresWhereOVTAIsB = sumALUAcresWhereOVTAIsB,
                ALUSumAcresWhereOVTAIsC = sumALUAcresWhereOVTAIsC,
                ALUSumAcresWhereOVTAIsD = sumALUAcresWhereOVTAIsD
            });
        }


        [HttpGet("{jurisdictionPrimaryKey}")]
        [AnonymousUnclassifiedFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("jurisdictionPrimaryKey")]
        public JsonResult LoadBasedResultsCalculations([FromRoute] StormwaterJurisdictionPrimaryKey jurisdictionPrimaryKey)
        {
            var jurisdiction = jurisdictionPrimaryKey.EntityObject;

            var viaFullCapture = TrashGeneratingUnitHelper.LoadBasedFullCapture(_dbContext, jurisdiction);
            var viaPartialCapture = TrashGeneratingUnitHelper.LoadBasedPartialCapture(_dbContext, jurisdiction);
            var viaOVTAs = TrashGeneratingUnitHelper.LoadBasedOVTAProgressScores(_dbContext, jurisdiction);
            var totalAchieved = viaFullCapture + viaPartialCapture + viaOVTAs;
            var targetLoadReduction = TrashGeneratingUnitHelper.TargetLoadReduction(_dbContext, jurisdiction);

            return Json(new LoadResultsDto
            {
                LoadFullCapture = viaFullCapture,
                LoadPartialCapture = viaPartialCapture,
                LoadOVTA = viaOVTAs,
                TotalAchieved = totalAchieved,
                TargetLoadReduction = targetLoadReduction

            });
        }

    }
}

