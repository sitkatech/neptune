/*-----------------------------------------------------------------------
<copyright file="FileResourceController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using Azure.Storage.Blobs.Models;
using Neptune.Web.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.Web.Common;
using Neptune.Web.Services;
using Neptune.Web.Services.Filters;
using Microsoft.AspNetCore.StaticFiles;

namespace Neptune.Web.Controllers
{
    public class FileResourceController : NeptuneBaseController<FileResourceController>
    {
        private readonly AzureBlobStorageService _azureBlobStorageService;

        public FileResourceController(NeptuneDbContext dbContext,
            ILogger<FileResourceController> logger,
            IOptions<WebConfiguration> webConfiguration,
            LinkGenerator linkGenerator,
            AzureBlobStorageService azureBlobStorageService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _azureBlobStorageService = azureBlobStorageService;
        }

        //[CrossAreaRoute]
        [HttpGet("{fileResourceGuidAsString}")]
        public async Task<IActionResult> DisplayResource([FromRoute] string fileResourceGuidAsString)
        {
            var isStringAGuid = Guid.TryParse(fileResourceGuidAsString, out var fileResourceGuid);
            if (isStringAGuid)
            {
                var fileResource = _dbContext.FileResources.AsNoTracking().SingleOrDefault(x => x.FileResourceGUID == fileResourceGuid);
                if (fileResource != null)
                {
                    return await DisplayFileResource(fileResource);
                }
            }
            // Unhappy path - return an HTTP 404
            // ---------------------------------
            var message = $"File Resource {fileResourceGuidAsString} Not Found in database. It may have been deleted.";
            _logger.LogError(message);
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
                await _azureBlobStorageService.DownloadFileResourceFromBlobStorage(fileResource);

            return File(blobDownloadResult.Content.ToArray(), blobDownloadResult.Details.ContentType);
        }

        [HttpGet("{fileResourcePrimaryKey}")]
        [LoggedInUnclassifiedFeature]
        //[CrossAreaRoute]
        [ValidateEntityExistsAndPopulateParameterFilter("fileResourcePrimaryKey")]
        public async Task<IActionResult> DisplayResourceByID([FromRoute] FileResourcePrimaryKey fileResourcePrimaryKey)
        {
            var fileResource = fileResourcePrimaryKey.EntityObject;
            return await DisplayFileResource(fileResource);
        }

        [HttpGet("{fileResourceGuidAsString}/{maxWidth}/{maxHeight}")]
        //[CrossAreaRoute]
        public async Task<IActionResult> GetFileResourceResized([FromRoute] string fileResourceGuidAsString, [FromRoute] int maxWidth, [FromRoute] int maxHeight)
        {
            var isStringAGuid = Guid.TryParse(fileResourceGuidAsString, out var fileResourceGuid);
            if (isStringAGuid)
            {
                var fileResource = _dbContext.FileResources.AsNoTracking().SingleOrDefault(x => x.FileResourceGUID == fileResourceGuid);
                
                if (fileResource != null)
                {
                    // Happy path - return the resource
                    // ---------------------------------
                    switch (fileResource.FileResourceMimeType.ToEnum)
                    {
                        case FileResourceMimeTypeEnum.ExcelXLS:
                        case FileResourceMimeTypeEnum.ExcelXLSX:
                        case FileResourceMimeTypeEnum.xExcelXLSX:
                        case FileResourceMimeTypeEnum.PDF:
                        case FileResourceMimeTypeEnum.WordDOCX:
                        case FileResourceMimeTypeEnum.WordDOC:
                        case FileResourceMimeTypeEnum.PowerpointPPTX:
                        case FileResourceMimeTypeEnum.PowerpointPPT:
                            throw new ArgumentOutOfRangeException($"Not supported mime type {fileResource.FileResourceMimeType.FileResourceMimeTypeDisplayName}");
                        case FileResourceMimeTypeEnum.XPNG:
                        case FileResourceMimeTypeEnum.PNG:
                        case FileResourceMimeTypeEnum.TIFF:
                        case FileResourceMimeTypeEnum.BMP:
                        case FileResourceMimeTypeEnum.GIF:
                        case FileResourceMimeTypeEnum.JPEG:
                        case FileResourceMimeTypeEnum.PJPEG:

                            var fileResourceBlobDownloadResult = await _azureBlobStorageService.DownloadFileResourceFromBlobStorage(fileResource);
                            var scaledImage =
                                await ImageHelper.ScaleImage(fileResourceBlobDownloadResult.Content.ToArray(), maxWidth,
                                    maxHeight);
                            return File(scaledImage.ToArray(), "image/png");
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            // Unhappy path - return an HTTP 404
            // ---------------------------------
            var message = $"File Resource {fileResourceGuidAsString} Not Found in database. It may have been deleted.";
            _logger.LogError(message);
            return new NotFoundResult();
        }
    }
}
