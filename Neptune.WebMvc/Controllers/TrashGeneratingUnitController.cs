﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services.Filters;
using Neptune.WebMvc.Views.TrashGeneratingUnit;
using Index = Neptune.WebMvc.Views.TrashGeneratingUnit.Index;

namespace Neptune.WebMvc.Controllers
{
    //[Area("Trash")]
    //[Route("[area]/[controller]/[action]", Name = "[area]_[controller]_[action]")]
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
            return RazorView<Index, IndexViewData>(viewData);
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
            var trashGeneratingUnits = GetRelevantTrashGeneratingUnitsForCalculations(jurisdiction);

            var fullTrashCapture = trashGeneratingUnits.FullTrashCaptureAcreage();

            var equivalentArea = trashGeneratingUnits.EquivalentAreaAcreage();

            var totalAcresCaptured = fullTrashCapture + equivalentArea;

            var totalPLUAcres = _dbContext.LandUseBlocks.AsNoTracking()
                .Where(x => x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&  x.PriorityLandUseTypeID != (int) PriorityLandUseTypeEnum.ALU && x.PermitTypeID == (int) PermitTypeEnum.PhaseIMS4).Sum(x =>
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

        private List<TrashGeneratingUnit> GetRelevantTrashGeneratingUnitsForCalculations(StormwaterJurisdiction jurisdiction)
        {
            var trashGeneratingUnits = _dbContext.TrashGeneratingUnits
                .Include(x => x.LandUseBlock)
                .Include(x => x.OnlandVisualTrashAssessmentArea)
                .Include(x => x.Delineation)
                .ThenInclude(x => x.TreatmentBMP)
                .Include(x => x.WaterQualityManagementPlan)
                .AsNoTracking()
                .Where(x => x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID && x.LandUseBlock != null)
                .ToList();
            return trashGeneratingUnits;
        }

        [HttpGet("{jurisdictionPrimaryKey}")]
        [AnonymousUnclassifiedFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("jurisdictionPrimaryKey")]
        public JsonResult OVTABasedResultsCalculations([FromRoute] StormwaterJurisdictionPrimaryKey jurisdictionPrimaryKey)
        {
            var jurisdiction = jurisdictionPrimaryKey.EntityObject;
            var trashGeneratingUnits = GetRelevantTrashGeneratingUnitsForCalculations(jurisdiction);

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

