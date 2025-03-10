using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neptune.EFModels.Workflows;
using Microsoft.EntityFrameworkCore;
using Neptune.Common.GeoSpatial;
using System;
using Microsoft.AspNetCore.Http;

namespace Neptune.API.Controllers;

[ApiController]
[Route("onland-visual-trash-assessments")]
public class OnlandVisualTrashAssessmentController(
    NeptuneDbContext dbContext,
    ILogger<OnlandVisualTrashAssessmentController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> neptuneConfiguration,
    AzureBlobStorageService azureBlobStorageService)
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

    [HttpGet("{onlandVisualTrashAssessmentID}")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public ActionResult<OnlandVisualTrashAssessmentDetailDto> GetByID([FromRoute] int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessmentDetailDto = OnlandVisualTrashAssessments.GetByID(DbContext, onlandVisualTrashAssessmentID).AsDetailDto();
        return Ok(onlandVisualTrashAssessmentDetailDto);
    }

    [HttpGet("{onlandVisualTrashAssessmentID}/workflow")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public ActionResult<OnlandVisualTrashAssessmentWorkflowDto> GetByIDForWorkflow([FromRoute] int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessmentWorkflowDto = OnlandVisualTrashAssessments.GetByID(DbContext, onlandVisualTrashAssessmentID).AsWorkflowDto();
        return Ok(onlandVisualTrashAssessmentWorkflowDto);
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

    [HttpPost]
    [JurisdictionEditFeature]
    public async Task<OnlandVisualTrashAssessmentSimpleDto> CreateNew([FromBody] OnlandVisualTrashAssessmentSimpleDto dto)
    {
        var onlandVisualTrashAssessment = await OnlandVisualTrashAssessments.CreateNew(DbContext, dto, CallingUser);
        return onlandVisualTrashAssessment;
    }

    [HttpPut]
    [JurisdictionEditFeature]
    public async Task Update([FromBody] OnlandVisualTrashAssessmentWorkflowDto dto)
    {
        await OnlandVisualTrashAssessments.Update(DbContext, dto);
    }

    [HttpGet("onland-visual-trash-assessment-areas/{onlandVisualTrashAssessmentAreaID}")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessmentArea), "onlandVisualTrashAssessmentAreaID")]
    public ActionResult<List<OnlandVisualTrashAssessmentGridDto>> GetByAreaID([FromRoute] int onlandVisualTrashAssessmentAreaID)
    {
        var visualTrashAssessmentGridDtos = OnlandVisualTrashAssessments.ListByOnlandVisualTrashAssessmentAreaID(DbContext, onlandVisualTrashAssessmentAreaID).Select(x => x.AsGridDto());
        return Ok(visualTrashAssessmentGridDtos);
    }

    [HttpGet("{onlandVisualTrashAssessmentID}/observations")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public ActionResult<List<OnlandVisualTrashAssessmentObservationUpsertDto>> GetObservations([FromRoute] int onlandVisualTrashAssessmentID)
    {
        var visualTrashAssessmentObservationUpsertDtos = OnlandVisualTrashAssessmentObservations
            .ListByOnlandVisualTrashAssessmentID(dbContext, onlandVisualTrashAssessmentID)
            .Select(x => new OnlandVisualTrashAssessmentObservationUpsertDto()
            {
                OnlandVisualTrashAssessmentObservationID = x.OnlandVisualTrashAssessmentObservationID,
                OnlandVisualTrashAssessmentID = x.OnlandVisualTrashAssessmentID,
                Longitude = x.LocationPoint4326.Coordinate[0],
                Latitude = x.LocationPoint4326.Coordinate[1],
                Note = x.Note,
                FileResourceID = x.OnlandVisualTrashAssessmentObservationPhotos.SingleOrDefault()?.FileResourceID,
            });

        return Ok(visualTrashAssessmentObservationUpsertDtos);
    }

    [HttpPost("{onlandVisualTrashAssessmentID}/observations")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public async Task UpdateObservations([FromRoute] int onlandVisualTrashAssessmentID, [FromBody] List<OnlandVisualTrashAssessmentObservationUpsertDto> onlandVisualTrashAssessmentObservationUpsertDtos)
    {
        await OnlandVisualTrashAssessmentObservations.Update(dbContext, onlandVisualTrashAssessmentID, onlandVisualTrashAssessmentObservationUpsertDtos);
    }

    [HttpPost("{onlandVisualTrashAssessmentID}/observation-photo-staging")]
    [JurisdictionEditFeature]
    [RequestSizeLimit(30 * 1024 * 1024)]
    [RequestFormLimits(MultipartBodyLengthLimit = 30 * 1024 * 1024), ProducesResponseType(StatusCodes.Status413RequestEntityTooLarge)]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public async Task<OnlandVisualTrashAssessmentObservationPhotoStagingSimpleDto> StageObservationPhoto([FromRoute]
        int onlandVisualTrashAssessmentID, [FromForm] OnlandVisualTrashAssessmentObservationPhotoDto file)
    {

        var fileResource =
            await HttpUtilities.MakeFileResourceFromFormFile(file.file, DbContext,
                HttpContext, azureBlobStorageService);
        var onlandVisualTrashAssessmentObservationPhotoStaging = new OnlandVisualTrashAssessmentObservationPhotoStaging
        {
            FileResource = fileResource,
            OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentID
        };
        await dbContext.OnlandVisualTrashAssessmentObservationPhotoStagings.AddAsync(onlandVisualTrashAssessmentObservationPhotoStaging);
        await dbContext.SaveChangesAsync();

        return  new OnlandVisualTrashAssessmentObservationPhotoStagingSimpleDto()
        {
            OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentID,
            OnlandVisualTrashAssessmentObservationPhotoStagingID = onlandVisualTrashAssessmentObservationPhotoStaging.OnlandVisualTrashAssessmentObservationPhotoStagingID,
            FileResourceID = onlandVisualTrashAssessmentObservationPhotoStaging.FileResourceID//onlandVisualTrashAssessmentObservationPhotoStaging.FileResource.GetFileResourceUrl()
        };
    }

}