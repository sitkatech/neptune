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

using Neptune.Web.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;
using Microsoft.AspNetCore.StaticFiles;
using Neptune.Web.Services.Filters;

namespace Neptune.Web.Controllers
{
    public class FileResourceController : NeptuneBaseController<FileResourceController>
    {
        public FileResourceController(NeptuneDbContext dbContext, ILogger<FileResourceController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

        //[CrossAreaRoute]
        [HttpGet("{fileResourceGuidAsString}")]
        public IActionResult DisplayResource([FromRoute] string fileResourceGuidAsString)
        {
            var isStringAGuid = Guid.TryParse(fileResourceGuidAsString, out var fileResourceGuid);
            if (isStringAGuid)
            {
                var fileResource = _dbContext.FileResources.AsNoTracking().SingleOrDefault(x => x.FileResourceGUID == fileResourceGuid);
                if (fileResource != null)
                {
                    return DisplayFile(fileResource.OriginalBaseFilename, fileResource.FileResourceData);
                }
            }
            // Unhappy path - return an HTTP 404
            // ---------------------------------
            var message = $"File Resource {fileResourceGuidAsString} Not Found in database. It may have been deleted.";
            _logger.LogError(message);
            return new NotFoundResult();
        }

        private IActionResult DisplayFile(string fileName, byte[] fileStream)
        {
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = fileName,
                Inline = false
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(fileStream, contentType);
        }

        private IActionResult DisplayFile(string fileName, Stream fileStream)
        {
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = fileName,
                Inline = false
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(fileStream, contentType);
        }

        [HttpGet("{fileResourcePrimaryKey}")]
        [LoggedInUnclassifiedFeature]
        //[CrossAreaRoute]
        [ValidateEntityExistsAndPopulateParameterFilter("fileResourcePrimaryKey")]
        public IActionResult DisplayResourceByID([FromRoute] FileResourcePrimaryKey fileResourcePrimaryKey)
        {
            var fileResource = fileResourcePrimaryKey.EntityObject;
            return DisplayFile(fileResource.OriginalBaseFilename, fileResource.FileResourceData);
        }

        [HttpGet("{fileResourceGuidAsString}/{maxWidth}/{maxHeight}")]
        //[CrossAreaRoute]
        public ActionResult GetFileResourceResized([FromRoute] string fileResourceGuidAsString, [FromRoute] int maxWidth, [FromRoute] int maxHeight)
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
                        //case FileResourceMimeTypeEnum.PJPEG:
                        //    using (var scaledImage = ImageHelper.ScaleImage(fileResource.FileResourceData, maxWidth, maxHeight))
                        //    {
                        //        using (var ms = new MemoryStream())
                        //        {
                        //            scaledImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        //            return File(ms.ToArray(), FileResourceMimeType.PNG.FileResourceMimeTypeName);
                        //        }
                        //    }
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
