﻿/*-----------------------------------------------------------------------
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
using Neptune.Web.Common.MvcResults;
using Microsoft.AspNetCore.StaticFiles;

namespace Neptune.Web.Controllers
{
    public class FileResourceController : NeptuneBaseController<FileResourceController>
    {
        public FileResourceController(NeptuneDbContext dbContext, ILogger<FileResourceController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

        //[CrossAreaRoute]
        [HttpGet("{fileResourceGuidAsString}")]
        public IActionResult DisplayResource(string fileResourceGuidAsString)
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

        [LoggedInUnclassifiedFeature]
        //[CrossAreaRoute]
        public IActionResult DisplayResourceByID(FileResourcePrimaryKey fileResourcePrimaryKey)
        {
            var fileResource = fileResourcePrimaryKey.EntityObject;
            return DisplayFile(fileResource.OriginalBaseFilename, fileResource.FileResourceData);
        }

        //[CrossAreaRoute]
        public ActionResult GetFileResourceResized(string fileResourceGuidAsString, int maxWidth, int maxHeight)
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

        ///// <summary>
        ///// Dummy fake HTTP "GET" for <see cref="CkEditorUploadFileResource(NeptunePagePrimaryKey, CkEditorImageUploadViewModel)"/>
        ///// </summary>
        ///// <returns></returns>
        ////[CrossAreaRoute]
        //[HttpGet]
        //[NeptunePageManageFeature]
        //public ContentResult CkEditorUploadFileResource(NeptunePagePrimaryKey neptunePagePrimaryKey)
        //{
        //    return Content(String.Empty);
        //}

        ////[CrossAreaRoute]
        //[HttpPost]
        ////[AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        //[NeptunePageManageFeature]
        //public ContentResult CkEditorUploadFileResource(NeptunePagePrimaryKey neptunePagePrimaryKey, CkEditorImageUploadViewModel viewModel)
        //{
        //    var fileResource = FileResource.CreateNewFromHttpPostedFileAndSave(viewModel.upload, CurrentPerson);
        //    var neptunePage = neptunePagePrimaryKey.EntityObject;
        //    var ppImage = new NeptunePageImage(neptunePage, fileResource);
        //    _dbContext.NeptunePageImages.Add(ppImage);
        //    return Content(viewModel.GetCkEditorJavascriptContentToReturn(fileResource));
        //}

//        /// <summary>
//        /// Dummy fake HTTP "GET" for <see cref="CkEditorUploadFileResource(NeptunePagePrimaryKey, CkEditorImageUploadViewModel)"/>
//        /// </summary>
//        /// <returns></returns>
//        //[CrossAreaRoute]
//        [HttpGet]
//        [NeptunePageManageFeature]
//        public ContentResult CkEditorUploadFileResourceForNeptunePage(NeptunePagePrimaryKey neptunePagePrimaryKey)
//        {
//            return Content(String.Empty);
//        }


//        //[CrossAreaRoute]
//        [HttpPost]
//        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
//        [NeptunePageManageFeature]
//        public ContentResult CkEditorUploadFileResourceForNeptunePage(NeptunePagePrimaryKey neptunePagePrimaryKey, CkEditorImageUploadViewModel viewModel)
//        {
//            var fileResource = FileResource.CreateNewFromHttpPostedFileAndSave(viewModel.upload, CurrentPerson);
//            var neptunePage = neptunePagePrimaryKey.EntityObject;
//            var ppImage = new NeptunePageImage(neptunePage, fileResource);
//            _dbContext.NeptunePageImages.Add(ppImage);
//            return Content(viewModel.GetCkEditorJavascriptContentToReturn(fileResource));
//        }

//        /// <summary>
//        /// Dummy fake HTTP "GET" for <see cref="CkEditorUploadFileResourceForFieldDefinition(FieldDefinitionTypePrimaryKey, CkEditorImageUploadViewModel)"/>
//        /// </summary>
//        /// <returns></returns>
//        //[CrossAreaRoute]
//        [HttpGet]
//        [FieldDefinitionManageFeature]
//        public ContentResult CkEditorUploadFileResourceForFieldDefinition(FieldDefinitionTypePrimaryKey fieldDefinitionTypePrimaryKey)
//        {
//            return Content(String.Empty);
//        }

//        //[CrossAreaRoute]
//        [HttpPost]
//        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
//        [FieldDefinitionManageFeature]
//        public ContentResult CkEditorUploadFileResourceForFieldDefinition(FieldDefinitionTypePrimaryKey fieldDefinitionTypePrimaryKey, CkEditorImageUploadViewModel viewModel)
//        {
//            var fileResource = FileResource.CreateNewFromHttpPostedFileAndSave(viewModel.upload, CurrentPerson);
//            return Content(viewModel.GetCkEditorJavascriptContentToReturn(fileResource));
//        }

//        public class CkEditorImageUploadViewModel
//        {
//            // ReSharper disable InconsistentNaming
//            public string CKEditorFuncNum { get; set; }
//            public HttpPostedFileBase upload { get; set; }
//            // ReSharper restore InconsistentNaming

//            public string GetCkEditorJavascriptContentToReturn(FileResource fileResource)
//            {
//                var ckEditorJavascriptContentToReturn = $@"
//<script language=""javascript"" type=""text/javascript"">
//    // <![CDATA[
//    window.parent.CKEDITOR.tools.callFunction({CKEditorFuncNum}, {fileResource.GetFileResourceUrl().ToJS()});
//    // ]]>
//</script>";
//                return ckEditorJavascriptContentToReturn;
//            }
//        }
    }
}