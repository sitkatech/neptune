using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.EFModels.Workflows;

namespace Neptune.API.Controllers;

[ApiController]
[Route("onland-visual-trash-assessments")]
public class OnlandVisualTrashAssessmentController(
    NeptuneDbContext dbContext,
    ILogger<OnlandVisualTrashAssessmentController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<OnlandVisualTrashAssessmentController>(dbContext, logger, keystoneService, neptuneConfiguration)
{
    [HttpGet]
    [JurisdictionEditFeature]
    public ActionResult<List<OnlandVisualTrashAssessmentGridDto>> ListForCallingUser()
    {
        var stormwaterJurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(DbContext, CallingUser.PersonID);
        var onlandVisualTrashAssessmentGridDtos = OnlandVisualTrashAssessments.ListByStormwaterJurisdictionIDAsGridDto(DbContext, stormwaterJurisdictionIDs);
        return Ok(onlandVisualTrashAssessmentGridDtos);
    }

    [HttpPost]
    [JurisdictionEditFeature]
    public async Task<ActionResult<OnlandVisualTrashAssessmentSimpleDto>> CreateNew([FromBody] OnlandVisualTrashAssessmentSimpleDto dto)
    {
        var onlandVisualTrashAssessment = await OnlandVisualTrashAssessments.CreateNew(DbContext, dto, CallingUser);
        return Ok(onlandVisualTrashAssessment);
    }

    [HttpGet("{onlandVisualTrashAssessmentID}")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public ActionResult<OnlandVisualTrashAssessmentDetailDto> GetByID([FromRoute] int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessmentDetailDto = OnlandVisualTrashAssessments.GetByID(DbContext, onlandVisualTrashAssessmentID).AsDetailDto();
        return Ok(onlandVisualTrashAssessmentDetailDto);
    }

    [HttpGet("{onlandVisualTrashAssessmentID}/add-or-remove-parcel")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public ActionResult<OnlandVisualTrashAssessmentAddRemoveParcelsDto> GetByIDForAddOrRemoveParcel([FromRoute] int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessmentAddRemoveParcelsDto = OnlandVisualTrashAssessments.GetByID(DbContext, onlandVisualTrashAssessmentID).AsAddRemoveParcelDto();
        return Ok(onlandVisualTrashAssessmentAddRemoveParcelsDto);
    }

    [HttpGet("{onlandVisualTrashAssessmentID}/review-and-finalize")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public ActionResult<OnlandVisualTrashAssessmentReviewAndFinalizeDto> GetByIDForReviewAndFinalize([FromRoute] int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessmentReviewAndFinalizeDto = OnlandVisualTrashAssessments.GetByID(DbContext, onlandVisualTrashAssessmentID).AsReviewAndFinalizeDto();
        return Ok(onlandVisualTrashAssessmentReviewAndFinalizeDto);
    }

    [HttpPost("{onlandVisualTrashAssessmentID}/review-and-finalize")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public async Task<ActionResult> UpdateReviewAndFinalize([FromRoute] int onlandVisualTrashAssessmentID, [FromBody] OnlandVisualTrashAssessmentReviewAndFinalizeDto dto)
    {
        await OnlandVisualTrashAssessments.Update(dbContext, onlandVisualTrashAssessmentID, dto);
        return Ok();
    }

    [HttpGet("preliminary-source-identification-types")]
    [JurisdictionEditFeature]
    public ActionResult<List<PreliminarySourceIdentificationTypeSimpleDto>> GetPreliminarySourceIdentificationTypes()
    {
        var preliminarySourceIdentificationTypeSimpleDtos = OnlandVisualTrashAssessments.GetPreliminarySourceIdentificationTypeSimpleDtos(DbContext);
        return Ok(preliminarySourceIdentificationTypeSimpleDtos);
    }

    [HttpGet("{onlandVisualTrashAssessmentID}/progress")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public ActionResult<OnlandVisualTrashAssessmentWorkflowProgress.OnlandVisualTrashAssessmentWorkflowProgressDto> GetWorkflowProgress([FromRoute] int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByID(DbContext, onlandVisualTrashAssessmentID);
        var onlandVisualTrashAssessmentProgressDto = OnlandVisualTrashAssessmentWorkflowProgress.GetProgress(onlandVisualTrashAssessment);
        return Ok(onlandVisualTrashAssessmentProgressDto);
    }

    [HttpGet("{onlandVisualTrashAssessmentID}/refine-area")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public ActionResult<OnlandVisualTrashAssessmentRefineAreaDto> GetByIDForRefineArea([FromRoute] int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessmentRefineAreaDto = OnlandVisualTrashAssessments.GetByID(DbContext, onlandVisualTrashAssessmentID).AsRefineAreaDto();
        return Ok(onlandVisualTrashAssessmentRefineAreaDto);
    }

    [HttpPost("{onlandVisualTrashAssessmentID}/parcel-geometries")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public async Task<ActionResult> UpdateOnlandVisualTrashAssessmentWithParcels([FromRoute] int onlandVisualTrashAssessmentID, [FromBody] List<int> parcelIDs)
    {
        await OnlandVisualTrashAssessments.UpdateGeometry(dbContext, onlandVisualTrashAssessmentID, parcelIDs);
        return Ok();
    }

    [HttpPost("{onlandVisualTrashAssessmentID}/refine-area")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public async Task<ActionResult> UpdateOnlandVisualTrashAssessmentWithRefinedArea([FromRoute] int onlandVisualTrashAssessmentID, OnlandVisualTrashAssessmentRefineAreaDto onlandVisualTrashAssessmentRefineAreaDto)
    {
        await OnlandVisualTrashAssessments.UpdateGeometry(dbContext, onlandVisualTrashAssessmentID,
            onlandVisualTrashAssessmentRefineAreaDto);
        return Ok();
    }

    [HttpPost("{onlandVisualTrashAssessmentID}/refresh-parcels")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public async Task<ActionResult> RefreshOnlandVisualTrashAssessmentParcels([FromRoute] int onlandVisualTrashAssessmentID)
    {
        await OnlandVisualTrashAssessments.RefreshParcels(dbContext, onlandVisualTrashAssessmentID);
        return Ok();
    }
}