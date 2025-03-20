using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Helpers;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.API.Controllers;

[ApiController]
[Route("trash-results-by-jurisdiction/{jurisdictionID}")]
public class TrashGeneratingUnitByStormwaterJurisdictionController(
    NeptuneDbContext dbContext,
    ILogger<TrashGeneratingUnitController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<TrashGeneratingUnitController>(dbContext, logger, keystoneService, neptuneConfiguration)
{

    [HttpGet("area-based-results-calculations")]
    [EntityNotFound(typeof(StormwaterJurisdiction), "jurisdictionID")]
    public ActionResult<AreaBasedAcreCalculationsDto> GetAreaBasedResultsCalculations([FromRoute] int jurisdictionID)
    {
        var trashGeneratingUnits = GetRelevantTrashGeneratingUnitsForCalculations(jurisdictionID);
        var lastUpdateDate = DbContext.vTrashGeneratingUnitLoadStatistics.FirstOrDefault()?.LastUpdateDate;

        var fullTrashCapture = trashGeneratingUnits.FullTrashCaptureAcreage();

        var equivalentArea = trashGeneratingUnits.EquivalentAreaAcreage();

        var totalAcresCaptured = fullTrashCapture + equivalentArea;

        var totalPLUAcres = DbContext.LandUseBlocks.AsNoTracking()
            .Where(x => x.StormwaterJurisdictionID == jurisdictionID && x.PriorityLandUseTypeID != (int)PriorityLandUseTypeEnum.ALU && x.PermitTypeID == (int)PermitTypeEnum.PhaseIMS4).Sum(x =>
                x.LandUseBlockGeometry.Area * Constants.SquareMetersToAcres);

        var percentTreated = totalPLUAcres != 0 ? totalAcresCaptured / totalPLUAcres : 0;

        var areaBasedAcreCalculationsDto = new AreaBasedAcreCalculationsDto
        {
            FullTrashCaptureAcreage = fullTrashCapture,
            EquivalentAreaAcreage = equivalentArea,
            TotalAcresCaptured = totalAcresCaptured,
            TotalPLUAcres = totalPLUAcres,
            PercentTreated = percentTreated,
            LastUpdateDate = lastUpdateDate
        };
        return Ok(areaBasedAcreCalculationsDto);
    }

    private List<TrashGeneratingUnit> GetRelevantTrashGeneratingUnitsForCalculations(int stormwaterJurisdictionID)
    {
        var trashGeneratingUnits = DbContext.TrashGeneratingUnits
            .Include(x => x.LandUseBlock)
            .Include(x => x.OnlandVisualTrashAssessmentArea)
            .Include(x => x.Delineation)
            .ThenInclude(x => x.TreatmentBMP)
            .Include(x => x.WaterQualityManagementPlan)
            .AsNoTracking()
            .Where(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID && x.LandUseBlock != null)
            .ToList();
        return trashGeneratingUnits;
    }


    [HttpGet("ovta-based-results-calculations")]
    [EntityNotFound(typeof(StormwaterJurisdiction), "jurisdictionID")]
    public ActionResult<OVTAResultsDto> GetOVTABasedResultsCalculations([FromRoute] int jurisdictionID)
    {
        var trashGeneratingUnits = GetRelevantTrashGeneratingUnitsForCalculations(jurisdictionID);
        var lastUpdateDate = DbContext.vTrashGeneratingUnitLoadStatistics.FirstOrDefault()?.LastUpdateDate;

        var sumPLUAcresWhereOVTAIsA = trashGeneratingUnits.PriorityOVTAScoreAAcreage();

        var sumPLUAcrexsWhereOVTAIsB = trashGeneratingUnits.PriorityOVTAScoreBAcreage();

        var sumPLUAcrexsWhereOVTAIsC = trashGeneratingUnits.PriorityOVTAScoreCAcreage();

        var sumPLUAcrexsWhereOVTAIsD = trashGeneratingUnits.PriorityOVTAScoreDAcreage();


        var sumALUAcresWhereOVTAIsA = trashGeneratingUnits.AlternateOVTAScoreAAcreage();

        var sumALUAcresWhereOVTAIsB = trashGeneratingUnits.AlternateOVTAScoreBAcreage();

        var sumALUAcresWhereOVTAIsC = trashGeneratingUnits.AlternateOVTAScoreCAcreage();

        var sumALUAcresWhereOVTAIsD = trashGeneratingUnits.AlternateOVTAScoreDAcreage();

        var ovtaResultsDto = new OVTAResultsDto
        {
            PLUSumAcresWhereOVTAIsA = sumPLUAcresWhereOVTAIsA,
            PLUSumAcresWhereOVTAIsB = sumPLUAcrexsWhereOVTAIsB,
            PLUSumAcresWhereOVTAIsC = sumPLUAcrexsWhereOVTAIsC,
            PLUSumAcresWhereOVTAIsD = sumPLUAcrexsWhereOVTAIsD,
            ALUSumAcresWhereOVTAIsA = sumALUAcresWhereOVTAIsA,
            ALUSumAcresWhereOVTAIsB = sumALUAcresWhereOVTAIsB,
            ALUSumAcresWhereOVTAIsC = sumALUAcresWhereOVTAIsC,
            ALUSumAcresWhereOVTAIsD = sumALUAcresWhereOVTAIsD,
            LastUpdateDate = lastUpdateDate
        };
        return Ok(ovtaResultsDto);
    }


    [HttpGet("load-based-results-calculations")]
    [EntityNotFound(typeof(StormwaterJurisdiction), "jurisdictionID")]
    public ActionResult<LoadResultsDto> GetLoadBasedResultsCalculations([FromRoute] int jurisdictionID)
    {
        var vTrashGeneratingUnitLoadStatistics =
            DbContext.vTrashGeneratingUnitLoadStatistics.Where(x => x.StormwaterJurisdictionID == jurisdictionID);
        var lastUpdateDate = DbContext.vTrashGeneratingUnitLoadStatistics.FirstOrDefault()?.LastUpdateDate;

        var viaFullCapture = vTrashGeneratingUnitLoadStatistics.Where(x => x.IsFullTrashCapture && x.BaselineLoadingRate.HasValue).Sum(x =>
            x.Area * (double)(x.BaselineLoadingRate - TrashGeneratingUnitHelper.FullTrashCaptureLoading) * Constants.SquareMetersToAcres);
        var viaPartialCapture = vTrashGeneratingUnitLoadStatistics.Where(x => x.IsPartialTrashCapture && x.BaselineLoadingRate.HasValue).Sum(x => 
            x.Area * (double)(x.BaselineLoadingRate - x.CurrentLoadingRate) * Constants.SquareMetersToAcres);
        var viaOVTAs = vTrashGeneratingUnitLoadStatistics.Where(x => x.HasBaselineScore == true && x.HasProgressScore == true && x.BaselineLoadingRate.HasValue).Sum(x =>
            x.Area * (double)(x.BaselineLoadingRate - x.ProgressLoadingRate) * Constants.SquareMetersToAcres);

        var totalAchieved = viaFullCapture + viaPartialCapture + viaOVTAs;
        var targetLoadReduction = TrashGeneratingUnitHelper.TargetLoadReduction(DbContext, jurisdictionID);

        var loadResultsDto = new LoadResultsDto
        {
            LoadFullCapture = viaFullCapture,
            LoadPartialCapture = viaPartialCapture,
            LoadOVTA = viaOVTAs,
            TotalAchieved = totalAchieved,
            TargetLoadReduction = targetLoadReduction,
            LastUpdateDate = lastUpdateDate
        };
        return Ok(loadResultsDto);
    }
}
