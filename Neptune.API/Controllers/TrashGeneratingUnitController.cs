using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;
using Neptune.Common;

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

    [HttpGet("{trashGeneratingUnitID}")]
    public ActionResult<TrashGeneratingUnitDto> Get([FromRoute] int trashGeneratingUnitID)
    {
        var trashGeneratingUnitDto = DbContext.vTrashGeneratingUnitLoadStatistics
            .Where(x => x.TrashGeneratingUnitID == trashGeneratingUnitID).Select(x => new TrashGeneratingUnitDto()
            {
                TrashGeneratingUnitID = x.TrashGeneratingUnitID,
                TrashCaptureEffectivenessBMP = x.TrashCaptureEffectivenessBMP,
                TreatmentBMPName = x.TreatmentBMPName,
                TreatmentBMPTypeName = x.TreatmentBMPTypeName,
                TreatmentBMPID = x.TreatmentBMPID,
                TrashCaptureStatusBMP = x.TrashCaptureStatusBMP,
                StormwaterJurisdictionID = x.StormwaterJurisdictionID,
                StormwaterJurisdictionName = x.OrganizationName,
                BaselineLoadingRate = x.BaselineLoadingRate,
                ProgressLoadingRate = x.ProgressLoadingRate,
                LoadingRateDelta = x.LoadingRateDelta,
                LandUseType = x.LandUseType,
                PermitClass = x.PermitClass,
                CurrentLoadingRate = x.CurrentLoadingRate,
                OnlandVisualTrashAssessmentAreaID = x.OnlandVisualTrashAssessmentAreaID,
                OnlandVisualTrashAssessmentAreaName = x.OnlandVisualTrashAssessmentAreaName,
                OnlandVisualTrashAssessmentAreaBaselineScore = x.OnlandVisualTrashAssessmentAreaBaselineScore,
                OnlandVisualTrashAssessmentAreaProgressScore = x.OnlandVisualTrashAssessmentAreaProgressScore,
                WaterQualityManagementPlanID = x.WaterQualityManagementPlanID,
                WaterQualityManagementPlanName = x.WaterQualityManagementPlanName,
                TrashCaptureStatusWQMP = x.TrashCaptureStatusWQMP,
                TrashCaptureEffectivenessWQMP = x.TrashCaptureEffectivenessWQMP,
                LastUpdateDate = x.LastUpdateDate,
                Area = x.Area * Constants.SquareMetersToAcres,
                AssessmentDate = x.MostRecentAssessmentDate,
                CompletedBaselineAssessmentCount = x.CompletedBaselineAssessmentCount,
                CompletedProgressAssessmentCount = x.CompletedProgressAssessmentCount
            }).FirstOrDefault();
        
        return Ok(trashGeneratingUnitDto);
    }

    [HttpGet("last-update-date")]
    public ActionResult<DateTime> GetLastUpdateDate()
    {
        var lastUpdateDate = DbContext.vTrashGeneratingUnitLoadStatistics.FirstOrDefault()?.LastUpdateDate;
        return Ok(lastUpdateDate);
    }
}
