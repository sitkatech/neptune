using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using NetTopologySuite.Features;

namespace Neptune.API.Controllers;

[ApiController]
[Route("onland-visual-trash-assessment-areas")]
public class OnlandVisualTrashAssessmentAreaController(
    NeptuneDbContext dbContext,
    ILogger<OnlandVisualTrashAssessmentAreaController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<OnlandVisualTrashAssessmentAreaController>(dbContext, logger, keystoneService, neptuneConfiguration)
{
    [HttpGet]
    [JurisdictionEditFeature]
    public ActionResult<List<OnlandVisualTrashAssessmentAreaGridDto>> ListForCallingUser()
    {
        var stormwaterJurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(DbContext, CallingUser.PersonID);
        var onlandVisualTrashAssessmentAreaGridDtos = OnlandVisualTrashAssessmentAreas
            .ListByStormwaterJurisdictionIDList(DbContext, stormwaterJurisdictionIDs).Select(x => x.AsGridDto()).ToList();
        return Ok(onlandVisualTrashAssessmentAreaGridDtos);
    }

    [HttpGet("{onlandVisualTrashAssessmentAreaID}")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessmentArea), "onlandVisualTrashAssessmentAreaID")]
    public ActionResult<OnlandVisualTrashAssessmentAreaDetailDto> GetByID([FromRoute] int onlandVisualTrashAssessmentAreaID)
    {
        var onlandVisualTrashAssessmentAreaDetailDto = OnlandVisualTrashAssessmentAreas.GetByID(DbContext, onlandVisualTrashAssessmentAreaID).AsDetailDto();
        return Ok(onlandVisualTrashAssessmentAreaDetailDto);
    }

    [HttpPut("{onlandVisualTrashAssessmentAreaID}")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessmentArea), "onlandVisualTrashAssessmentAreaID")]
    public async Task<ActionResult> Update([FromRoute] int onlandVisualTrashAssessmentAreaID, [FromBody] OnlandVisualTrashAssessmentAreaSimpleDto onlandVisualTrashAssessmentAreaDto)
    {
        await OnlandVisualTrashAssessmentAreas.Update(DbContext, onlandVisualTrashAssessmentAreaID, onlandVisualTrashAssessmentAreaDto);
        return Ok();
    }

    [HttpGet("{onlandVisualTrashAssessmentAreaID}/onland-visual-trash-assessments")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessmentArea), "onlandVisualTrashAssessmentAreaID")]
    public ActionResult<List<OnlandVisualTrashAssessmentGridDto>> GetByAreaID([FromRoute] int onlandVisualTrashAssessmentAreaID)
    {
        var visualTrashAssessmentGridDtos = OnlandVisualTrashAssessments.ListByOnlandVisualTrashAssessmentAreaID(DbContext, onlandVisualTrashAssessmentAreaID).Select(x => x.AsGridDto());
        return Ok(visualTrashAssessmentGridDtos);
    }

    [HttpGet("{onlandVisualTrashAssessmentAreaID}/parcel-geometries")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessmentArea), "onlandVisualTrashAssessmentAreaID")]
    public ActionResult<List<ParcelGeometrySimpleDto>> GetParcelGeometries([FromRoute] int onlandVisualTrashAssessmentAreaID)
    {
        var onlandVisualTrashAssessmentArea = OnlandVisualTrashAssessmentAreas.GetByID(DbContext, onlandVisualTrashAssessmentAreaID);
        var geometries = ParcelGeometries.GetIntersected(DbContext,
            onlandVisualTrashAssessmentArea.TransectLine).Select(x => x.AsSimpleDto()).ToList();
        return Ok(geometries);
    }

    [HttpPost("{onlandVisualTrashAssessmentAreaID}/parcel-geometries")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessmentArea), "onlandVisualTrashAssessmentAreaID")]
    public async Task<ActionResult> UpdateOnlandVisualTrashAssessmentWithParcels([FromRoute] int onlandVisualTrashAssessmentAreaID, [FromBody] OnlandVisualTrashAssessmentAreaGeometryDto onlandVisualTrashAssessmentAreaGeometryDto)
    {
        OnlandVisualTrashAssessmentAreas.UpdateGeometry(DbContext, onlandVisualTrashAssessmentAreaGeometryDto);
        await DbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{onlandVisualTrashAssessmentAreaID}/area-as-feature-collection")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessmentArea), "onlandVisualTrashAssessmentAreaID")]
    public ActionResult<FeatureCollection> GetAreaAsFeatureCollection([FromRoute] int onlandVisualTrashAssessmentAreaID)
    {
        var featureCollection = OnlandVisualTrashAssessmentAreas.GetAssessmentAreaByIDAsFeatureCollection(DbContext, onlandVisualTrashAssessmentAreaID);
        return Ok(featureCollection);
    }

    [HttpGet("{onlandVisualTrashAssessmentAreaID}/transect-line-as-feature-collection")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessmentArea), "onlandVisualTrashAssessmentAreaID")]
    public ActionResult<FeatureCollection> GetTransectLineAsFeatureCollection([FromRoute] int onlandVisualTrashAssessmentAreaID)
    {
        var featureCollection = OnlandVisualTrashAssessmentAreas.GetTransectLineByIDAsFeatureCollection(DbContext, onlandVisualTrashAssessmentAreaID);
        return Ok(featureCollection);
    }
}