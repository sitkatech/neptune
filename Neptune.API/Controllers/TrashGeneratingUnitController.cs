using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Helpers;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.API.Services.Authorization;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.API.Controllers;

[ApiController]
[Route("trash-generating-units")]
public class TrashGeneratingUnitController(
    NeptuneDbContext dbContext,
    ILogger<TrashGeneratingUnitController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<TrashGeneratingUnitController>(dbContext, logger, keystoneService, neptuneConfiguration)
{
    [HttpGet]
    [JurisdictionEditFeature]
    public ActionResult<List<TrashGeneratingUnitGridDto>> ListForCallingUser()
    {
        var trashGeneratingUnitGridDtos = TrashGeneratingUnits.List(DbContext, CallingUser);
        return Ok(trashGeneratingUnitGridDtos);
    }

    [HttpGet("area-based-results-calculations/{jurisdictionID}")]
    [EntityNotFound(typeof(StormwaterJurisdiction), "jurisdictionID")]
    public ActionResult<AreaBasedAcreCalculationsDto> GetAreaBasedResultsCalculations([FromRoute] int jurisdictionID)
    {
        var jurisdiction = StormwaterJurisdictions.GetByID(DbContext, jurisdictionID);
        var trashGeneratingUnits = GetRelevantTrashGeneratingUnitsForCalculations(jurisdiction);

        var fullTrashCapture = trashGeneratingUnits.FullTrashCaptureAcreage();

        var equivalentArea = trashGeneratingUnits.EquivalentAreaAcreage();

        var totalAcresCaptured = fullTrashCapture + equivalentArea;

        var totalPLUAcres = DbContext.LandUseBlocks.AsNoTracking()
            .Where(x => x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID && x.PriorityLandUseTypeID != (int)PriorityLandUseTypeEnum.ALU && x.PermitTypeID == (int)PermitTypeEnum.PhaseIMS4).Sum(x =>
                x.LandUseBlockGeometry.Area * Constants.SquareMetersToAcres);

        var percentTreated = totalPLUAcres != 0 ? totalAcresCaptured / totalPLUAcres : 0;

        return Ok(new AreaBasedAcreCalculationsDto
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
        var trashGeneratingUnits = DbContext.TrashGeneratingUnits
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


    [HttpGet("ovta-based-results-calculations/{jurisdictionID}")]
    [EntityNotFound(typeof(StormwaterJurisdiction), "jurisdictionID")]
    public ActionResult<OVTAResultsDto> GetOVTABasedResultsCalculations([FromRoute] int jurisdictionID)
    {
        var jurisdiction = StormwaterJurisdictions.GetByID(DbContext, jurisdictionID);
        var trashGeneratingUnits = GetRelevantTrashGeneratingUnitsForCalculations(jurisdiction);

        var sumPLUAcresWhereOVTAIsA = trashGeneratingUnits.PriorityOVTAScoreAAcreage();

        var sumPLUAcrexsWhereOVTAIsB = trashGeneratingUnits.PriorityOVTAScoreBAcreage();

        var sumPLUAcrexsWhereOVTAIsC = trashGeneratingUnits.PriorityOVTAScoreCAcreage();

        var sumPLUAcrexsWhereOVTAIsD = trashGeneratingUnits.PriorityOVTAScoreDAcreage();


        var sumALUAcresWhereOVTAIsA = trashGeneratingUnits.AlternateOVTAScoreAAcreage();

        var sumALUAcresWhereOVTAIsB = trashGeneratingUnits.AlternateOVTAScoreBAcreage();

        var sumALUAcresWhereOVTAIsC = trashGeneratingUnits.AlternateOVTAScoreCAcreage();

        var sumALUAcresWhereOVTAIsD = trashGeneratingUnits.AlternateOVTAScoreDAcreage();

        return Ok(new OVTAResultsDto
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


    [HttpGet("load-based-results-calculations/{jurisdictionID}")]
    [EntityNotFound(typeof(StormwaterJurisdiction), "jurisdictionID")]
    public ActionResult<LoadResultsDto> GetLoadBasedResultsCalculations([FromRoute] int jurisdictionID)
    {
        var jurisdiction = StormwaterJurisdictions.GetByID(DbContext, jurisdictionID);

        var viaFullCapture = TrashGeneratingUnitHelper.LoadBasedFullCapture(DbContext, jurisdiction);
        var viaPartialCapture = TrashGeneratingUnitHelper.LoadBasedPartialCapture(DbContext, jurisdiction);
        var viaOVTAs = TrashGeneratingUnitHelper.LoadBasedOVTAProgressScores(DbContext, jurisdiction);
        var totalAchieved = viaFullCapture + viaPartialCapture + viaOVTAs;
        var targetLoadReduction = TrashGeneratingUnitHelper.TargetLoadReduction(DbContext, jurisdiction);

        return Ok(new LoadResultsDto
        {
            LoadFullCapture = viaFullCapture,
            LoadPartialCapture = viaPartialCapture,
            LoadOVTA = viaOVTAs,
            TotalAchieved = totalAchieved,
            TargetLoadReduction = targetLoadReduction

        });
    }
}
