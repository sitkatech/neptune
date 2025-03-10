﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
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

    [HttpGet("onland-visual-trash-assessment-areas/{onlandVisualTrashAssessmentAreaID}")]
    [JurisdictionEditFeature]
    [EntityNotFound(typeof(OnlandVisualTrashAssessmentArea), "onlandVisualTrashAssessmentAreaID")]
    public ActionResult<List<OnlandVisualTrashAssessmentGridDto>> GetByAreaID([FromRoute] int onlandVisualTrashAssessmentAreaID)
    {
        var visualTrashAssessmentGridDtos = OnlandVisualTrashAssessments.ListByOnlandVisualTrashAssessmentAreaID(DbContext, onlandVisualTrashAssessmentAreaID).Select(x => x.AsGridDto());
        return Ok(visualTrashAssessmentGridDtos);
    }
}