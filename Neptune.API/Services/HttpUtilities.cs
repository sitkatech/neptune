using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Services
{
    public static class HttpUtilities
    {
        public static async Task<FileResource> MakeFileResourceFromFormFile(IFormFile inputFile, NeptuneDbContext dbContext, HttpContext httpContext, AzureBlobStorageService blobStorageService)
        {
            byte[] bytes;
            await using(var ms = new MemoryStream())
            {
                await inputFile.CopyToAsync(ms);
                bytes = ms.ToArray();
            }

            var personDto = UserContext.GetUserFromHttpContext(dbContext, httpContext);

            var fileResourceMimeType = FileResourceMimeType.GetFileResourceMimeTypeByContentTypeName(dbContext,
                inputFile.ContentType);

            var fileResource = new FileResource()
            {
                FileResourceMimeTypeID = fileResourceMimeType.FileResourceMimeTypeID,
                OriginalBaseFilename = inputFile.FileName,
                OriginalFileExtension = inputFile.FileName.Split('.').Last(),
                FileResourceGUID = Guid.NewGuid(),
                CreatePersonID = personDto.PersonID,
                CreateDate = DateTime.Now,
                InBlobStorage = true,
                ContentLength = bytes.Length
            };
            await dbContext.FileResources.AddAsync(fileResource);

            if (await blobStorageService.UploadFileResource(fileResource, bytes))
            {
                await dbContext.SaveChangesAsync();
                await dbContext.Entry(fileResource).ReloadAsync();
                return fileResource;
            }

            throw new Exception($"New file resource was not uploaded to blob storage successfully.");
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