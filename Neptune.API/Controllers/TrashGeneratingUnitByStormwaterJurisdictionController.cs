using Microsoft.AspNetCore.Authorization;
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
using System.Threading.Tasks;

namespace Neptune.API.Controllers;

[ApiController]
[Route("trash-results-by-jurisdiction/{jurisdictionID}")]
public class TrashGeneratingUnitByStormwaterJurisdictionController(
    NeptuneDbContext dbContext,
    ILogger<TrashGeneratingUnitController> logger,
    IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<TrashGeneratingUnitController>(dbContext, logger, neptuneConfiguration)
{

    [HttpGet("area-based-results-calculations")]
    [AllowAnonymous]
    [EntityNotFound(typeof(StormwaterJurisdiction), "jurisdictionID")]
    public ActionResult<AreaBasedAcreCalculationsDto> GetAreaBasedResultsCalculations([FromRoute] int jurisdictionID)
    {
        var trashGeneratingUnits = GetRelevantTrashGeneratingUnitsForCalculations(jurisdictionID);

        var fullTrashCapturePLU = trashGeneratingUnits.FullTrashCaptureAcreage(true);
        var partialTrashCapturePLU = trashGeneratingUnits.PartialTrashCaptureAcreage(true);

        var totalAcresCapturedPLU = fullTrashCapturePLU + partialTrashCapturePLU;

        var totalPLUAcres = DbContext.LandUseBlocks.AsNoTracking()
            .Where(x => x.StormwaterJurisdictionID == jurisdictionID && x.PriorityLandUseTypeID != (int)PriorityLandUseTypeEnum.ALU && x.PermitTypeID == (int)PermitTypeEnum.PhaseIMS4).Sum(x =>
                x.LandUseBlockGeometry.Area * Constants.SquareMetersToAcres);

        var untreatedPLU = totalPLUAcres != 0 ?  totalPLUAcres - totalAcresCapturedPLU : 0;



        var fullTrashCaptureALU = trashGeneratingUnits.FullTrashCaptureAcreage(false);
        var partialTrashCaptureALU = trashGeneratingUnits.PartialTrashCaptureAcreage(false);

        var totalAcresCapturedALU = fullTrashCaptureALU + partialTrashCaptureALU;

        var totalALUAcres = DbContext.LandUseBlocks.AsNoTracking()
            .Where(x => x.StormwaterJurisdictionID == jurisdictionID && x.PriorityLandUseTypeID == (int)PriorityLandUseTypeEnum.ALU && x.PermitTypeID == (int)PermitTypeEnum.PhaseIMS4).Sum(x =>
                x.LandUseBlockGeometry.Area * Constants.SquareMetersToAcres);
        var untreatedALU = totalALUAcres != 0 ? totalALUAcres - totalAcresCapturedALU : 0;

        var areaBasedAcreCalculationsDto = new AreaBasedAcreCalculationsDto
        {
            FullTrashCaptureAcreagePLU = fullTrashCapturePLU,
            PartialTrashCaptureAcreagePLU = partialTrashCapturePLU,
            UntreatedAcreagePLU = untreatedPLU,
            FullTrashCaptureAcreageALU = fullTrashCaptureALU,
            PartialTrashCaptureAcreageALU = partialTrashCaptureALU,
            UntreatedAcreageALU = untreatedALU
        };
        return Ok(areaBasedAcreCalculationsDto);
    }

    private List<TrashGeneratingUnit> GetRelevantTrashGeneratingUnitsForCalculations(int stormwaterJurisdictionID)
    {
        var trashGeneratingUnits = DbContext.TrashGeneratingUnits
            .Include(x => x.LandUseBlock)
            .Include(x => x.OnlandVisualTrashAssessmentArea).ThenInclude(x => x.OnlandVisualTrashAssessments)
            .Include(x => x.Delineation)
            .ThenInclude(x => x.TreatmentBMP)
            .Include(x => x.WaterQualityManagementPlan)
            .AsNoTracking()
            .Where(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID && x.LandUseBlock != null)
            .ToList();
        return trashGeneratingUnits;
    }


    [HttpGet("ovta-based-results-calculations")]
    [AllowAnonymous]
    [EntityNotFound(typeof(StormwaterJurisdiction), "jurisdictionID")]
    public ActionResult<OVTAResultsDto> GetOVTABasedResultsCalculations([FromRoute] int jurisdictionID)
    {
        var trashGeneratingUnits = GetRelevantTrashGeneratingUnitsForCalculations(jurisdictionID);

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
            ALUSumAcresWhereOVTAIsD = sumALUAcresWhereOVTAIsD
        };
        return Ok(ovtaResultsDto);
    }


    [HttpGet("load-based-results-calculations")]
    [AllowAnonymous]
    [EntityNotFound(typeof(StormwaterJurisdiction), "jurisdictionID")]
    public async Task<ActionResult<LoadResultsDto>> GetLoadBasedResultsCalculations([FromRoute] int jurisdictionID)
    {
        var vTrashGeneratingUnitLoadStatistics =
            DbContext.vTrashGeneratingUnitLoadStatistics.Where(x => x.StormwaterJurisdictionID == jurisdictionID);

        // Single DB round-trip: compute all three conditional sums in one aggregate
        var aggregates = await vTrashGeneratingUnitLoadStatistics
            .GroupBy(x => 1)
            .Select(g => new
            {
                Full = g.Where(x => x.IsFullTrashCapture && x.BaselineLoadingRate.HasValue)
                    .Sum(x => x.Area * (double)(x.BaselineLoadingRate - TrashGeneratingUnitHelper.FullTrashCaptureLoading) * Constants.SquareMetersToAcres),
                Partial = g.Where(x => x.IsPartialTrashCapture && x.BaselineLoadingRate.HasValue)
                    .Sum(x => x.Area * (double)(x.BaselineLoadingRate - x.CurrentLoadingRate) * Constants.SquareMetersToAcres),
                Ovta = g.Where(x => x.HasBaselineScore == true && x.HasProgressScore == true && x.BaselineLoadingRate.HasValue)
                    .Sum(x => x.Area * (double)(x.BaselineLoadingRate - x.ProgressLoadingRate) * Constants.SquareMetersToAcres)
            })
            .SingleOrDefaultAsync();

        var viaFullCapture = aggregates?.Full ?? 0;
        var viaPartialCapture = aggregates?.Partial ?? 0;
        var viaOVTAs = aggregates?.Ovta ?? 0;

        var totalAchieved = viaFullCapture + viaPartialCapture + viaOVTAs;
        var targetLoadReduction = TrashGeneratingUnitHelper.TargetLoadReduction(DbContext, jurisdictionID, vTrashGeneratingUnitLoadStatistics);

        var loadResultsDto = new LoadResultsDto
        {
            LoadFullCapture = viaFullCapture,
            LoadPartialCapture = viaPartialCapture,
            LoadOVTA = viaOVTAs,
            TotalAchieved = totalAchieved,
            TargetLoadReduction = targetLoadReduction
        };
        return Ok(loadResultsDto);
    }
}
