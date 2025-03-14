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
                FileResourceGUID = x.OnlandVisualTrashAssessmentObservationPhotos.SingleOrDefault()?.FileResource.GetFileResourceGUIDAsString(),
            });

        return Ok(visualTrashAssessmentObservationUpsertDtos);
    }

    [HttpGet("{onlandVisualTrashAssessmentID}/observation-locations")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public ActionResult<List<OnlandVisualTrashAssessmentObservationLocationDto>> GetObservationLocations([FromRoute] int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessmentObservationLocationDtos = OnlandVisualTrashAssessmentObservations
            .ListByOnlandVisualTrashAssessmentID(dbContext, onlandVisualTrashAssessmentID)
            .Select(x => new OnlandVisualTrashAssessmentObservationLocationDto() {
                OnlandVisualTrashAssessmentObservationID = x.OnlandVisualTrashAssessmentObservationID,
                OnlandVisualTrashAssessmentID = x.OnlandVisualTrashAssessmentID,
                Longitude = x.LocationPoint4326.Coordinate[0],
                Latitude = x.LocationPoint4326.Coordinate[1],
            });

        return Ok(onlandVisualTrashAssessmentObservationLocationDtos);
    }

    [HttpPost("{onlandVisualTrashAssessmentID}/observations")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public async Task<ActionResult> UpdateObservations([FromRoute] int onlandVisualTrashAssessmentID, [FromBody] List<OnlandVisualTrashAssessmentObservationUpsertDto> onlandVisualTrashAssessmentObservationUpsertDtos)
    {
        await OnlandVisualTrashAssessmentObservations.Update(dbContext, onlandVisualTrashAssessmentID, onlandVisualTrashAssessmentObservationUpsertDtos);
        return Ok();
    }

    [HttpPost("{onlandVisualTrashAssessmentID}/observation-photo-staging")]
    [JurisdictionEditFeature]
    [RequestSizeLimit(30 * 1024 * 1024)]
    [RequestFormLimits(MultipartBodyLengthLimit = 30 * 1024 * 1024), ProducesResponseType(StatusCodes.Status413RequestEntityTooLarge)]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public async Task<ActionResult<OnlandVisualTrashAssessmentObservationPhotoStagingDto>> StageObservationPhoto([FromRoute]
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
        await dbContext.OnlandVisualTrashAssessmentObservationPhotoStagings
            .Entry(onlandVisualTrashAssessmentObservationPhotoStaging).ReloadAsync();

        var onlandVisualTrashAssessmentObservationPhotoStagingDto = new OnlandVisualTrashAssessmentObservationPhotoStagingDto()
        {
            OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentID,
            OnlandVisualTrashAssessmentObservationPhotoStagingID = onlandVisualTrashAssessmentObservationPhotoStaging.OnlandVisualTrashAssessmentObservationPhotoStagingID,
            FileResourceID = onlandVisualTrashAssessmentObservationPhotoStaging.FileResourceID,
            FileResourceGUID = onlandVisualTrashAssessmentObservationPhotoStaging.FileResource.GetFileResourceGUIDAsString(),
            PhotoStagingID = onlandVisualTrashAssessmentObservationPhotoStaging.OnlandVisualTrashAssessmentObservationPhotoStagingID
        };
        return Ok(onlandVisualTrashAssessmentObservationPhotoStagingDto);
    }

    [HttpDelete("{onlandVisualTrashAssessmentID}/observation-photo")]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public async Task<ActionResult> DeleteObservationPhoto([FromRoute] int onlandVisualTrashAssessmentID,
        [FromBody] OnlandVisualTrashAssessmentObservationPhotoStagingDto onlandVisualTrashAssessmentObservationPhotoStaging)
    {
        //await dbContext.FileResources
        //    .Where(x => x.FileResourceID == onlandVisualTrashAssessmentObservationPhotoStaging.FileResourceID)
        //    .ExecuteDeleteAsync();
        if (onlandVisualTrashAssessmentObservationPhotoStaging.PhotoStagingID != null)
        {
            await dbContext.OnlandVisualTrashAssessmentObservationPhotoStagings.Where(x =>
                    x.OnlandVisualTrashAssessmentObservationPhotoStagingID ==
                    onlandVisualTrashAssessmentObservationPhotoStaging
                        .OnlandVisualTrashAssessmentObservationPhotoStagingID)
                .ExecuteDeleteAsync();
        }
        else
        {
            await dbContext.OnlandVisualTrashAssessmentObservationPhotos.Where(x =>
                x.OnlandVisualTrashAssessmentObservationID == onlandVisualTrashAssessmentObservationPhotoStaging
                    .OnlandVisualTrashAssessmentObservationID).ExecuteDeleteAsync();
        }

        return Ok();
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