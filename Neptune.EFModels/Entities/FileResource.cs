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

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Neptune.Common.DesignByContract;
using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class FileResource : IAuditableEntity
    {
        //public static int MaxUploadFileSizeInBytes = NeptuneWebConfiguration.MaximumAllowedUploadFileSize;

        public string GetFileResourceUrl()
        {
            return $"/FileResource/DisplayResource/{GetFileResourceGUIDAsString()}";
        }

        public string FileResourceUrlScaledThumbnail(int maxHeight)
        {
            return $"/FileResource/GetFileResourceResized/{GetFileResourceGUIDAsString()}/{maxHeight}/{maxHeight}";
        }

        public string GetFileResourceDataLengthString()
        {
            return $"(~{(FileResourceData.Length / 1000):##,###} KB)";
        }

        public string GetOriginalCompleteFileName()
        {
            return $"{OriginalBaseFilename}{OriginalFileExtension}";
        }

        //public static string MaxFileSizeHumanReadable => $"{MaxUploadFileSizeInBytes / (1024 ^ 2):0.0} KB";

        ///// <summary>
        ///// Prepare the file bytes for going into the database
        ///// </summary>
        ///// <param name="httpPostedFileBase"></param>
        ///// <returns></returns>
        //public static byte[] ConvertHttpPostedFileToByteArray(HttpPostedFileBase httpPostedFileBase)
        //{
        //    byte[] fileResourceData;
        //    using (var binaryReader = new BinaryReader(httpPostedFileBase.InputStream))
        //    {
        //        fileResourceData = binaryReader.ReadBytes(httpPostedFileBase.ContentLength);
        //        binaryReader.Close();
        //    }
        //    return fileResourceData;
        //}

        //public static FileResourceMimeType GetFileResourceMimeTypeForFile(HttpPostedFileBase file)
        //{
        //    var fileResourceMimeTypeForFile = FileResourceMimeType.All.SingleOrDefault(mt => mt.FileResourceMimeTypeContentTypeName == file.ContentType);
        //    Check.RequireNotNull(fileResourceMimeTypeForFile, $"Unhandled MIME type: {file.ContentType}");
        //    return fileResourceMimeTypeForFile;
        //}

        //public static FileResource CreateNewFromHttpPostedFileAndSave(HttpPostedFileBase httpPostedFileBase, Person currentPerson)
        //{
        //    var fileResource = CreateNewFromHttpPostedFile(httpPostedFileBase, currentPerson);
        //    _dbContext.FileResources.Add(fileResource);
        //    _dbContext.SaveChanges();
        //    return fileResource;
        //}

        ////Only public for unit testing
        //public static FileResource CreateNewFromHttpPostedFile(HttpPostedFileBase httpPostedFileBase, Person currentPerson)
        //{
        //    var fileName = httpPostedFileBase.FileName;
        //    if (string.IsNullOrWhiteSpace(fileName))
        //    {
        //        fileName = Guid.NewGuid().ToString() + ".jpg";
        //    }

        //    var originalFilenameInfo = new FileInfo(fileName);
        //    var baseFilenameWithoutExtension = originalFilenameInfo.Name.Remove(originalFilenameInfo.Name.Length - originalFilenameInfo.Extension.Length, originalFilenameInfo.Extension.Length);
        //    var fileResourceData = ConvertHttpPostedFileToByteArray(httpPostedFileBase);
        //    var fileResourceMimeTypeID = GetFileResourceMimeTypeForFile(httpPostedFileBase).FileResourceMimeTypeID;
        //    var fileResource = new FileResource(fileResourceMimeTypeID, baseFilenameWithoutExtension, originalFilenameInfo.Extension, Guid.NewGuid(), fileResourceData, currentPerson.PersonID, DateTime.Now);
        //    return fileResource;
        //}

        //public static FileResource CreateNewResizedImageFileResource(HttpPostedFileBase httpPostedFileBase, byte[] resizedImageBytes, Person currentPerson)
        //{
        //    var fileName = httpPostedFileBase.FileName;
        //    if (string.IsNullOrWhiteSpace(fileName))
        //    {
        //        fileName = Guid.NewGuid().ToString() + ".jpg";
        //    }

        //    var originalFilenameInfo = new FileInfo(fileName);
        //    var baseFilenameWithoutExtension = originalFilenameInfo.Name.Remove(originalFilenameInfo.Name.Length - originalFilenameInfo.Extension.Length, originalFilenameInfo.Extension.Length);
        //    var fileResourceData = resizedImageBytes;
        //    var fileResourceMimeTypeID = GetFileResourceMimeTypeForFile(httpPostedFileBase).FileResourceMimeTypeID;
        //    var fileResource = new FileResource(fileResourceMimeTypeID, baseFilenameWithoutExtension, originalFilenameInfo.Extension, Guid.NewGuid(), fileResourceData, currentPerson.PersonID, DateTime.Now);
        //    return fileResource;
        //}

        //public static void ValidateFileSize(HttpPostedFileBase httpPostedFileBase, List<ValidationResult> errors, string propertyName)
        //{
        //    if (httpPostedFileBase.ContentLength > MaxUploadFileSizeInBytes)
        //    {
        //        var formattedUploadSize = $"~{(httpPostedFileBase.ContentLength / 1000).ToGroupedNumeric()} KB";
        //        errors.Add(new ValidationResult(
        //            $"File is too large - must be less than {MaxFileSizeHumanReadable} [Provided file was {formattedUploadSize}]", new[] { propertyName }));
        //    }
        //}

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
