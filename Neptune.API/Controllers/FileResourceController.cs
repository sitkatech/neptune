using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Neptune.API.Controllers
{
    [ApiController]
    [Route("file-resources")]
    public class FileResourceController(
        NeptuneDbContext dbContext,
        ILogger<FileResourceController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration,
        AzureBlobStorageService azureBlobStorageService)
        : SitkaController<FileResourceController>(dbContext, logger, keystoneService, neptuneConfiguration)
    {
        [HttpGet("{fileResourceGuidAsString}")]
        public async Task<IActionResult> DisplayResource(string fileResourceGuidAsString)
        {
            var isStringAGuid = Guid.TryParse(fileResourceGuidAsString, out var fileResourceGuid);
            if (isStringAGuid)
            {
                var fileResource = DbContext.FileResources.AsNoTracking().SingleOrDefault(x => x.FileResourceGUID == fileResourceGuid);
                if (fileResource != null)
                {
                    return await DisplayFileResource(fileResource);
                }
            }
            // Unhappy path - return an HTTP 404
            // ---------------------------------
            var message = $"File Resource {fileResourceGuidAsString} Not Found in database. It may have been deleted.";
            Logger.LogError(message);
            return new NotFoundResult();
        }

        private async Task<IActionResult> DisplayFileResource(FileResource fileResource)
        {
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = fileResource.GetOriginalCompleteFileName(),
                Inline = false
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            var blobDownloadResult =
                await azureBlobStorageService.DownloadFileResourceFromBlobStorage(fileResource);

            return File(blobDownloadResult.Content.ToArray(), blobDownloadResult.Details.ContentType);
        }

    }
}
