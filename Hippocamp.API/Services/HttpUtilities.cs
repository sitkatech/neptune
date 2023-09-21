using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Hippocamp.EFModels.Entities;

namespace Hippocamp.API.Services
{
    public static class HttpUtilities
    {
        public static async Task<FileResource> MakeFileResourceFromFormFile(IFormFile inputFile, HippocampDbContext hippocampDbContext, HttpContext httpContext, AzureBlobStorageService blobStorageService)
        {
            byte[] bytes;
            await using(var ms = new MemoryStream())
            {
                await inputFile.CopyToAsync(ms);
                bytes = ms.ToArray();
            }

            var personDto = UserContext.GetUserFromHttpContext(hippocampDbContext, httpContext);

            var fileResourceMimeType = FileResourceMimeType.GetFileResourceMimeTypeByContentTypeName(hippocampDbContext,
                inputFile.ContentType);
            
            var clientFilename = inputFile.FileName;
            var extension = clientFilename.Split('.').Last();
            var fileResourceGuid = Guid.NewGuid();

            var fileResource = new FileResource
            {
                CreateDate = DateTime.Now,
                CreatePersonID = personDto.PersonID,
                FileResourceData = bytes,
                FileResourceGUID = fileResourceGuid,
                FileResourceMimeTypeID = fileResourceMimeType.FileResourceMimeTypeID,
                OriginalBaseFilename = clientFilename,
                OriginalFileExtension = extension,
            };

            if (blobStorageService.UploadFileResource(fileResource))
            {
                fileResource.InBlobStorage = true;
            }

            return fileResource;
        }

        public static async Task<byte[]> GetData(this HttpRequest httpRequest)
        {
            byte[] bytes;


            using (var ms = new MemoryStream(2048))
            {
                await httpRequest.Body.CopyToAsync(ms);
                bytes = ms.ToArray();
            }

            return bytes;
        }
    }
    
}