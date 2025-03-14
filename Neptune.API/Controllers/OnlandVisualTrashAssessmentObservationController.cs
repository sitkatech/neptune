using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Controllers;

[ApiController]
[Route("onland-visual-trash-assessments/{onlandVisualTrashAssessmentID}/observations")]
public class OnlandVisualTrashAssessmentObservationController(
    NeptuneDbContext dbContext,
    ILogger<OnlandVisualTrashAssessmentObservationController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> neptuneConfiguration,
    AzureBlobStorageService azureBlobStorageService)
    : SitkaController<OnlandVisualTrashAssessmentObservationController>(dbContext, logger, keystoneService, neptuneConfiguration)
{
    [HttpGet]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public ActionResult<List<OnlandVisualTrashAssessmentObservationWithPhotoDto>> ListByOnlandVisualTrashAssessmentID([FromRoute] int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessmentObservations = OnlandVisualTrashAssessmentObservations.ListByOnlandVisualTrashAssessmentID(DbContext, onlandVisualTrashAssessmentID).Select(x => x.AsPhotoDto()).ToList();
        return Ok(onlandVisualTrashAssessmentObservations);
    }

    [HttpGet("feature-collection")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public ActionResult<List<OnlandVisualTrashAssessmentObservationLocationDto>> GetAsFeatureCollection([FromRoute] int onlandVisualTrashAssessmentID)
    {
        var onlandVisualTrashAssessmentObservationLocationDtos = OnlandVisualTrashAssessmentObservations
            .ListByOnlandVisualTrashAssessmentID(DbContext, onlandVisualTrashAssessmentID)
            .Select(x => new OnlandVisualTrashAssessmentObservationLocationDto() {
                OnlandVisualTrashAssessmentObservationID = x.OnlandVisualTrashAssessmentObservationID,
                OnlandVisualTrashAssessmentID = x.OnlandVisualTrashAssessmentID,
                Longitude = x.LocationPoint4326.Coordinate[0],
                Latitude = x.LocationPoint4326.Coordinate[1],
            });

        return Ok(onlandVisualTrashAssessmentObservationLocationDtos);
    }

    [HttpPost]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public async Task<ActionResult> UpdateObservations([FromRoute] int onlandVisualTrashAssessmentID, [FromBody] List<OnlandVisualTrashAssessmentObservationWithPhotoDto> onlandVisualTrashAssessmentObservationUpsertDtos)
    {
        await OnlandVisualTrashAssessmentObservations.Update(DbContext, onlandVisualTrashAssessmentID, onlandVisualTrashAssessmentObservationUpsertDtos);
        return Ok();
    }

    [HttpPost("observation-photo")]
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
        await DbContext.OnlandVisualTrashAssessmentObservationPhotoStagings.AddAsync(onlandVisualTrashAssessmentObservationPhotoStaging);
        await DbContext.SaveChangesAsync();
        await DbContext.OnlandVisualTrashAssessmentObservationPhotoStagings
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

    [HttpDelete("observation-photo")]
    [EntityNotFound(typeof(OnlandVisualTrashAssessment), "onlandVisualTrashAssessmentID")]
    public async Task<ActionResult> DeleteObservationPhoto([FromRoute] int onlandVisualTrashAssessmentID,
        [FromBody] OnlandVisualTrashAssessmentObservationPhotoStagingDto onlandVisualTrashAssessmentObservationPhotoStaging)
    {
        //await dbContext.FileResources
        //    .Where(x => x.FileResourceID == onlandVisualTrashAssessmentObservationPhotoStaging.FileResourceID)
        //    .ExecuteDeleteAsync();
        if (onlandVisualTrashAssessmentObservationPhotoStaging.PhotoStagingID != null)
        {
            await DbContext.OnlandVisualTrashAssessmentObservationPhotoStagings.Where(x =>
                    x.OnlandVisualTrashAssessmentObservationPhotoStagingID ==
                    onlandVisualTrashAssessmentObservationPhotoStaging
                        .OnlandVisualTrashAssessmentObservationPhotoStagingID)
                .ExecuteDeleteAsync();
        }
        else
        {
            await DbContext.OnlandVisualTrashAssessmentObservationPhotos.Where(x =>
                x.OnlandVisualTrashAssessmentObservationID == onlandVisualTrashAssessmentObservationPhotoStaging
                    .OnlandVisualTrashAssessmentObservationID).ExecuteDeleteAsync();
        }

        return Ok();
    }
}