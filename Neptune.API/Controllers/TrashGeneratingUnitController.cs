using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
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
                LandUseType = x.LandUseType,
                CurrentLoadingRate = x.CurrentLoadingRate,
                OnlandVisualTrashAssessmentAreaID = x.OnlandVisualTrashAssessmentAreaID,
                OnlandVisualTrashAssessmentAreaName = x.OnlandVisualTrashAssessmentAreaName,
                OnlandVisualTrashAssessmentAreaBaselineScore = x.OnlandVisualTrashAssessmentAreaBaselineScore,
                WaterQualityManagementPlanID = x.WaterQualityManagementPlanID,
                WaterQualityManagementPlanName = x.WaterQualityManagementPlanName,
                TrashCaptureStatusWQMP = x.TrashCaptureStatusWQMP,
                TrashCaptureEffectivenessWQMP = x.TrashCaptureEffectivenessWQMP,
                LastUpdateDate = x.LastUpdateDate,
                Area = x.Area,
                AssessmentScore = x.AssessmentScore != "NotProvided" ? x.AssessmentScore : string.Empty,
                AssessmentDate = x.MostRecentAssessmentDate,
                CompletedAssessmentCount = x.CompletedAssessmentCount
            }).FirstOrDefault();
        
        return Ok(trashGeneratingUnitDto);
    }
}
