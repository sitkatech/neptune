/*-----------------------------------------------------------------------
<copyright file="FileResource.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;

namespace Neptune.Web.Models
{
    public partial class FileResource : IAuditableEntity
    {
        public static int MaxUploadFileSizeInBytes = NeptuneWebConfiguration.MaximumAllowedUploadFileSize;

        public static readonly UrlTemplate<int> FileResourceByIDUrlTemplate =
            new UrlTemplate<int>(SitkaRoute<FileResourceController>.BuildUrlFromExpression(t => t.DisplayResourceByID(UrlTemplate.Parameter1Int)));
        public static readonly UrlTemplate<string> FileResourceByGuidUrlTemplate =
            new UrlTemplate<string>(SitkaRoute<FileResourceController>.BuildUrlFromExpression(t => t.DisplayResource(UrlTemplate.Parameter1String)));

        public string GetFileResourceUrl()
        {
            return FileResourceByGuidUrlTemplate.ParameterReplace(GetFileResourceGUIDAsString());
        }

        public string FileResourceUrlScaledThumbnail(int maxHeight)
        {
            return SitkaRoute<FileResourceController>.BuildUrlFromExpression(x => x.GetFileResourceResized(GetFileResourceGUIDAsString(), maxHeight, maxHeight));
        }

        public string GetFileResourceUrlScaledForPrint()
        {
            return SitkaRoute<FileResourceController>.BuildUrlFromExpression(x =>
                x.GetFileResourceResized(GetFileResourceGUIDAsString(), 500, 500));
        }

        public string GetFileResourceDataLengthString()
        {
            return $"(~{(FileResourceData.Length / 1000).ToGroupedNumeric()} KB)";
        }

        public string GetOriginalCompleteFileName()
        {
            return $"{OriginalBaseFilename}{OriginalFileExtension}";
        }

        public static string MaxFileSizeHumanReadable
        {
            get { return $"{MaxUploadFileSizeInBytes / (1024 ^ 2):0.0} KB"; }
        }

        private Jpeg _photo;
        private bool _hasCheckedPhoto;

        public int GetImageWidth()
        {
            if (!_hasCheckedPhoto)
            {
                PopulateFileResourceDataAsImage();
            }

            return _photo.Width;
        }

        private void PopulateFileResourceDataAsImage()
        {
            var fileResourceDataAsMemoryStream = new MemoryStream(FileResourceData);
            _photo = new Jpeg(new Bitmap(fileResourceDataAsMemoryStream));
            _hasCheckedPhoto = true;
        }

        public int GetImageHeight()
        {
            if (!_hasCheckedPhoto)
            {
                PopulateFileResourceDataAsImage();
            }

            return _photo.Height;
        }

        public FileResourceOrientation GetOrientation()
        {
            return GetImageHeight() >= GetImageWidth() ? FileResourceOrientation.Portrait : FileResourceOrientation.Landscape;
        }

        public string GetFullGuidBasedFilename()
        {
            return $"{FileResourceGUID}{OriginalFileExtension}";
        }

        /// <summary>
        /// Prepare the file bytes for going into the database
        /// </summary>
        /// <param name="httpPostedFileBase"></param>
        /// <returns></returns>
        private static byte[] ConvertHttpPostedFileToByteArray(HttpPostedFileBase httpPostedFileBase)
        {
            byte[] fileResourceData;
            using (var binaryReader = new BinaryReader(httpPostedFileBase.InputStream))
            {
                fileResourceData = binaryReader.ReadBytes(httpPostedFileBase.ContentLength);
                binaryReader.Close();
            }
            return fileResourceData;
        }

        public static FileResourceMimeType GetFileResourceMimeTypeForFile(HttpPostedFileBase file)
        {
            var fileResourceMimeTypeForFile = FileResourceMimeType.All.SingleOrDefault(mt => mt.FileResourceMimeTypeContentTypeName == file.ContentType);
            Check.RequireNotNull(fileResourceMimeTypeForFile, $"Unhandled MIME type: {file.ContentType}");
            return fileResourceMimeTypeForFile;
        }

        public static FileResource CreateNewFromHttpPostedFileAndSave(HttpPostedFileBase httpPostedFileBase, Person currentPerson)
        {
            var fileResource = CreateNewFromHttpPostedFile(httpPostedFileBase, currentPerson);
            HttpRequestStorage.DatabaseEntities.AllFileResources.Add(fileResource);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            return fileResource;
        }

        //Only public for unit testing
        public static FileResource CreateNewFromHttpPostedFile(HttpPostedFileBase httpPostedFileBase, Person currentPerson)
        {
            var originalFilenameInfo = new FileInfo(httpPostedFileBase.FileName);
            var baseFilenameWithoutExtension = originalFilenameInfo.Name.Remove(originalFilenameInfo.Name.Length - originalFilenameInfo.Extension.Length, originalFilenameInfo.Extension.Length);
            var fileResourceData = ConvertHttpPostedFileToByteArray(httpPostedFileBase);
            var fileResourceMimeTypeID = GetFileResourceMimeTypeForFile(httpPostedFileBase).FileResourceMimeTypeID;
            var fileResource = new FileResource(fileResourceMimeTypeID, baseFilenameWithoutExtension, originalFilenameInfo.Extension, Guid.NewGuid(), fileResourceData, currentPerson.PersonID, DateTime.Now);
            return fileResource;
        }

        public static void ValidateFileSize(HttpPostedFileBase httpPostedFileBase, List<ValidationResult> errors, string propertyName)
        {
            if (httpPostedFileBase.ContentLength > MaxUploadFileSizeInBytes)
            {
                var formattedUploadSize = $"~{(httpPostedFileBase.ContentLength / 1000).ToGroupedNumeric()} KB";
                errors.Add(new ValidationResult(
                    $"File is too large - must be less than {MaxFileSizeHumanReadable} [Provided file was {formattedUploadSize}]", new[] { propertyName }));
            }
        }

        public static readonly Regex FileResourceUrlRegEx =
            new Regex(@"FileResource\/DisplayResource\/(?<fileResourceGuidCapture>[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Based on a string that has embedded file resource URLs in it, parse out the URLs and look up the corresponding FileResource stuff
        /// Made public for testing purposes.
        /// </summary>
        public static List<Guid> FindAllFileResourceGuidsFromStringContainingFileResourceUrls(string textWithReferences)
        {
            if (String.IsNullOrWhiteSpace(textWithReferences))
            {
                return new List<Guid>();
            }
            var guidCaptures = FileResourceUrlRegEx.Matches(textWithReferences).Cast<Match>().Select(x => x.Groups["fileResourceGuidCapture"].Value).ToList();
            var theseGuids = guidCaptures.Select(x => new Guid(x)).Distinct().ToList();
            return theseGuids;
        }

        public string GetAuditDescriptionString()
        {
            return $"{GetOriginalCompleteFileName()}";
        }

        public string GetFileResourceGUIDAsString()
        {
            return FileResourceGUID.ToString();
        }
    }
}
