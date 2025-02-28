using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.API.Services.Authorization;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

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
            .ListByStormwaterJurisdictionIDList(dbContext, stormwaterJurisdictionIDs).Select(x => x.AsGridDto()).ToList();
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

    [HttpPost]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessmentArea), "onlandVisualTrashAssessmentAreaID")]
    public ActionResult Update([FromBody] OnlandVisualTrashAssessmentAreaDetailDto onlandVisualTrashAssessmentAreaDto)
    {
        OnlandVisualTrashAssessmentAreas.Update(DbContext, onlandVisualTrashAssessmentAreaDto);
        return Ok();
    }

    [HttpGet("{onlandVisualTrashAssessmentAreaID}/parcel-geometries")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessmentArea), "onlandVisualTrashAssessmentAreaID")]
    public ActionResult<List<ParcelGeometrySimpleDto>> GetParcelGeometries([FromRoute] int onlandVisualTrashAssessmentAreaID)
    {
        var visualTrashAssessmentArea = OnlandVisualTrashAssessmentAreas.GetByID(DbContext, onlandVisualTrashAssessmentAreaID);
        var geometries = ParcelGeometries.GetIntersected(dbContext,
            visualTrashAssessmentArea.TransectLine).Select(x => x.AsSimpleDto()).ToList();
        return Ok(geometries);
    }

    [HttpPost("{onlandVisualTrashAssessmentAreaID}/parcel-geometries")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessmentArea), "onlandVisualTrashAssessmentAreaID")]
    public async Task UpdateOnlandVisualTrashAssessmentWithParcels([FromRoute] int onlandVisualTrashAssessmentAreaID, [FromBody] List<int> parcelIDs)
    {
        var onlandVisualTrashAssessmentArea = dbContext.OnlandVisualTrashAssessmentAreas.Single(x =>
            x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentAreaID);
        onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry = ParcelGeometries.UnionAggregateByParcelIDs(dbContext, parcelIDs);
        onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry4326 = ParcelGeometries.UnionAggregate4326ByParcelIDs(dbContext, parcelIDs);
        await dbContext.SaveChangesAsync();
    }
}