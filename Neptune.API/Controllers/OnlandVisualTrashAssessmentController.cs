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
    public ActionResult<List<OnlandVisualTrashAssessmentSimpleDto>> ListByStormwaterJurisdictionID()
    {
        var stormwaterJurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(DbContext, CallingUser.PersonID);
        var ovtaSimpleDtos = OnlandVisualTrashAssessments.ListByStormwaterJurisdictionIDList(DbContext, stormwaterJurisdictionIDs).Select(x => x.AsSimpleDto());
        return Ok(ovtaSimpleDtos);
    }
}