using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Controllers;

[ApiController]
[Route("project-documents")]
public class ProjectDocumentController(
    NeptuneDbContext dbContext,
    ILogger<ProjectController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<ProjectController>(dbContext, logger, keystoneService, neptuneConfiguration)
{
    [HttpGet("{projectDocumentID}")]
    [EntityNotFound(typeof(ProjectDocument), "projectDocumentID")]
    [JurisdictionEditFeature]
    public ActionResult<ProjectDocumentDto> GetAttachmentByID([FromRoute] int projectDocumentID)
    {
        var projectDocument = ProjectDocuments.GetByID(DbContext, projectDocumentID);
        return Ok(projectDocument.AsDto());
    }


    [HttpPut("{projectDocumentID}")]
    [EntityNotFound(typeof(ProjectDocument), "projectDocumentID")]
    [JurisdictionEditFeature]
    public async Task<ActionResult<ProjectDocumentDto>> UpdateAttachment([FromRoute] int projectDocumentID, [FromBody] ProjectDocumentUpdateDto projectDocumentUpdateDto)
    {
        var projectDocument = ProjectDocuments.GetByIDWithTracking(DbContext, projectDocumentID);
        if (!(await CallingUser.CanEditJurisdiction(projectDocument.Project.StormwaterJurisdiction.StormwaterJurisdictionID, DbContext)))
        {
            return Forbid();
        }

        var updatedProjectDocument = ProjectDocuments.Update(DbContext, projectDocument, projectDocumentUpdateDto);
        return Ok(updatedProjectDocument.AsDto());
    }

    [HttpDelete("{projectDocumentID}")]
    [EntityNotFound(typeof(ProjectDocument), "projectDocumentID")]
    [JurisdictionEditFeature]
    public async Task<IActionResult> DeleteAttachment([FromRoute] int projectDocumentID)
    {
        var projectDocument = ProjectDocuments.GetByIDWithTracking(DbContext, projectDocumentID);
        if (!(await CallingUser.CanEditJurisdiction(projectDocument.Project.StormwaterJurisdiction.StormwaterJurisdictionID, DbContext)))
        {
            return Forbid();
        }
        ProjectDocuments.Delete(DbContext, projectDocument);
        return Ok();
    }
}