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
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Neptune.Web.Security;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security.Shared;
using LtInfo.Common;
using LtInfo.Common.MvcResults;

namespace Neptune.Web.Controllers
{
    public class FileResourceController : NeptuneBaseController
    {
        [AnonymousUnclassifiedFeature]
        [CrossAreaRoute]
        public ActionResult DisplayResource(string fileResourceGuidAsString)
        {
            var isStringAGuid = Guid.TryParse(fileResourceGuidAsString, out var fileResourceGuid);
            if (isStringAGuid)
            {
                var fileResource = HttpRequestStorage.DatabaseEntities.FileResources.SingleOrDefault(x => x.FileResourceGUID == fileResourceGuid);

                return DisplayResourceImpl(fileResourceGuidAsString, fileResource);
            }
            // Unhappy path - return an HTTP 404
            // ---------------------------------
            var message = $"File Resource {fileResourceGuidAsString} Not Found in database. It may have been deleted.";
            throw new HttpException(404, message);
        }

        private ActionResult DisplayResourceImpl(string fileResourcePrimaryKey, FileResource fileResource)
        {
            if (fileResource == null)
            {
                var message = $"File Resource {fileResourcePrimaryKey} Not Found in database. It may have been deleted.";
                throw new HttpException(404, message);
            }

            switch (fileResource.FileResourceMimeType.ToEnum)
            {
                case FileResourceMimeTypeEnum.ExcelXLS:
                case FileResourceMimeTypeEnum.ExcelXLSX:
                case FileResourceMimeTypeEnum.xExcelXLSX:
                    return new ExcelResult(new MemoryStream(fileResource.FileResourceData), fileResource.GetOriginalCompleteFileName());
                case FileResourceMimeTypeEnum.PDF:
                    return new PdfResult(fileResource);
                case FileResourceMimeTypeEnum.WordDOCX:
                case FileResourceMimeTypeEnum.WordDOC:
                case FileResourceMimeTypeEnum.PowerpointPPTX:
                case FileResourceMimeTypeEnum.PowerpointPPT:
                case FileResourceMimeTypeEnum.CSS:
                    return new FileResourceResult(fileResource.GetOriginalCompleteFileName(), fileResource.FileResourceData, fileResource.FileResourceMimeType);
                case FileResourceMimeTypeEnum.XPNG:
                case FileResourceMimeTypeEnum.PNG:
                case FileResourceMimeTypeEnum.TIFF:
                case FileResourceMimeTypeEnum.BMP:
                case FileResourceMimeTypeEnum.GIF:
                case FileResourceMimeTypeEnum.JPEG:
                case FileResourceMimeTypeEnum.PJPEG:
                    return File(fileResource.FileResourceData, fileResource.FileResourceMimeType.FileResourceMimeTypeName);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [LoggedInUnclassifiedFeature]
        [CrossAreaRoute]
        public ActionResult DisplayResourceByID(FileResourcePrimaryKey fileResourcePrimaryKey)
        {
            var fileResource = fileResourcePrimaryKey.EntityObject;
            return DisplayResourceImpl(fileResourcePrimaryKey.PrimaryKeyValue.ToString(), fileResource);
        }

        [AnonymousUnclassifiedFeature]
        [CrossAreaRoute]
        public ActionResult GetFileResourceResized(string fileResourceGuidAsString, int maxWidth, int maxHeight)
        {
            var isStringAGuid = Guid.TryParse(fileResourceGuidAsString, out var fileResourceGuid);
            if (isStringAGuid)
            {
                var fileResource = HttpRequestStorage.DatabaseEntities.FileResources.SingleOrDefault(x => x.FileResourceGUID == fileResourceGuid);
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
                            using (var scaledImage = ImageHelper.ScaleImage(fileResource.FileResourceData, maxWidth, maxHeight))
                            {
                                using (var ms = new MemoryStream())
                                {
                                    scaledImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                    return File(ms.ToArray(), FileResourceMimeType.PNG.FileResourceMimeTypeName);
                                }
                            }
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            // Unhappy path - return an HTTP 404
            // ---------------------------------
            var message = $"File Resource {fileResourceGuidAsString} Not Found in database. It may have been deleted.";
            throw new HttpException(404, message);
        }

        /// <summary>
        /// Dummy fake HTTP "GET" for <see cref="CkEditorUploadFileResource(NeptunePagePrimaryKey, CkEditorImageUploadViewModel)"/>
        /// </summary>
        /// <returns></returns>
        [CrossAreaRoute]
        [HttpGet]
        [NeptunePageManageFeature]
        public ContentResult CkEditorUploadFileResource(NeptunePagePrimaryKey neptunePagePrimaryKey)
        {
            return Content(String.Empty);
        }

        [CrossAreaRoute]
        [HttpPost]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        [NeptunePageManageFeature]
        public ContentResult CkEditorUploadFileResource(NeptunePagePrimaryKey neptunePagePrimaryKey, CkEditorImageUploadViewModel viewModel)
        {
            var fileResource = FileResource.CreateNewFromHttpPostedFileAndSave(viewModel.upload, CurrentPerson);
            var neptunePage = neptunePagePrimaryKey.EntityObject;
            var ppImage = new NeptunePageImage(neptunePage, fileResource);
            HttpRequestStorage.DatabaseEntities.NeptunePageImages.Add(ppImage);
            return Content(viewModel.GetCkEditorJavascriptContentToReturn(fileResource));
        }

        /// <summary>
        /// Dummy fake HTTP "GET" for <see cref="CkEditorUploadFileResource(NeptunePagePrimaryKey, CkEditorImageUploadViewModel)"/>
        /// </summary>
        /// <returns></returns>
        [CrossAreaRoute]
        [HttpGet]
        [NeptunePageManageFeature]
        public ContentResult CkEditorUploadFileResourceForNeptunePage(NeptunePagePrimaryKey neptunePagePrimaryKey)
        {
            return Content(String.Empty);
        }


        [CrossAreaRoute]
        [HttpPost]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        [NeptunePageManageFeature]
        public ContentResult CkEditorUploadFileResourceForNeptunePage(NeptunePagePrimaryKey neptunePagePrimaryKey, CkEditorImageUploadViewModel viewModel)
        {
            var fileResource = FileResource.CreateNewFromHttpPostedFileAndSave(viewModel.upload, CurrentPerson);
            var neptunePage = neptunePagePrimaryKey.EntityObject;
            var ppImage = new NeptunePageImage(neptunePage, fileResource);
            HttpRequestStorage.DatabaseEntities.NeptunePageImages.Add(ppImage);
            return Content(viewModel.GetCkEditorJavascriptContentToReturn(fileResource));
        }

        /// <summary>
        /// Dummy fake HTTP "GET" for <see cref="CkEditorUploadFileResourceForFieldDefinition(FieldDefinitionPrimaryKey, CkEditorImageUploadViewModel)"/>
        /// </summary>
        /// <returns></returns>
        [CrossAreaRoute]
        [HttpGet]
        [FieldDefinitionManageFeature]
        public ContentResult CkEditorUploadFileResourceForFieldDefinition(FieldDefinitionPrimaryKey fieldDefinitionPrimaryKey)
        {
            return Content(String.Empty);
        }

        [CrossAreaRoute]
        [HttpPost]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        [FieldDefinitionManageFeature]
        public ContentResult CkEditorUploadFileResourceForFieldDefinition(FieldDefinitionPrimaryKey fieldDefinitionPrimaryKey, CkEditorImageUploadViewModel viewModel)
        {
            var fileResource = FileResource.CreateNewFromHttpPostedFileAndSave(viewModel.upload, CurrentPerson);
            var fieldDefinition = fieldDefinitionPrimaryKey.EntityObject;
            var image = new FieldDefinitionDataImage(fieldDefinition.GetFieldDefinitionData().FieldDefinitionDataID, fileResource.FileResourceID);
            HttpRequestStorage.DatabaseEntities.FieldDefinitionDataImages.Add(image);
            return Content(viewModel.GetCkEditorJavascriptContentToReturn(fileResource));
        }

        public class CkEditorImageUploadViewModel
        {
            // ReSharper disable InconsistentNaming
            public string CKEditorFuncNum { get; set; }
            public HttpPostedFileBase upload { get; set; }
            // ReSharper restore InconsistentNaming

            public string GetCkEditorJavascriptContentToReturn(FileResource fileResource)
            {
                var ckEditorJavascriptContentToReturn = $@"
<script language=""javascript"" type=""text/javascript"">
    // <![CDATA[
    window.parent.CKEDITOR.tools.callFunction({CKEditorFuncNum}, {fileResource.GetFileResourceUrl().ToJS()});
    // ]]>
</script>";
                return ckEditorJavascriptContentToReturn;
            }
        }
    }
}
