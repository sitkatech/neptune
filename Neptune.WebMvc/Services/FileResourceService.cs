﻿using System.ComponentModel.DataAnnotations;
using Neptune.Common.DesignByContract;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Services;

public class FileResourceService
{
    private const long MaxUploadFileSizeInBytes = 200000000;
    private readonly NeptuneDbContext _dbContext;
    private readonly AzureBlobStorageService _azureBlobStorageService;

    public FileResourceService(NeptuneDbContext dbContext, AzureBlobStorageService azureBlobStorageService)
    {
        _dbContext = dbContext;
        _azureBlobStorageService = azureBlobStorageService;
    }

    // 9/13/23 SMG: I'm hoping that this can essentially be the ONLY place that actually
    // we new up FileResources so that we can keep the pinch point accurate for the future.
    public async Task<FileResource> CreateNewAndSaveChanges(int fileResourceMimeTypeID,
        string baseFilenameWithoutExtension, string extension, byte[] fileResourceData, Person currentPerson)
    {
        var fileResource = new FileResource()
        {
            FileResourceMimeTypeID = fileResourceMimeTypeID,
            OriginalBaseFilename = baseFilenameWithoutExtension,
            OriginalFileExtension = extension,
            FileResourceGUID = Guid.NewGuid(),
            CreatePersonID = currentPerson.PersonID,
            CreateDate = DateTime.Now,
            ContentLength = fileResourceData.LongLength
        };
        await _dbContext.FileResources.AddAsync(fileResource);
        
        if (await _azureBlobStorageService.UploadFileResource(fileResource, fileResourceData))
        {
            await _dbContext.SaveChangesAsync();
            await _dbContext.Entry(fileResource).ReloadAsync();
            return fileResource;
        }

        throw new Exception($"New file resource was not uploaded to blob storage successfully.");
    }

    public async Task<bool> DeleteBlobForFileResource(FileResource fileResource)
    {
        return await _azureBlobStorageService.DeleteFileResourceBlob(fileResource);
    }

    public async Task<bool> DeleteFileResource(FileResource fileResource)
    {
        _dbContext.FileResources.Remove(fileResource);
        // there is a potential that we delete the blob, then a transaction fails, and the blob is no longer there.
        return await _azureBlobStorageService.DeleteFileResourceBlob(fileResource);
    }

    public async Task<FileResource> CreateNewFromIFormFile(IFormFile formFile, Person currentPerson)
    {
        var fileName = formFile.FileName;
        if (string.IsNullOrWhiteSpace(fileName))
        {
            fileName = Guid.NewGuid().ToString();
        }

        var originalFilenameInfo = new FileInfo(fileName);
        var baseFilenameWithoutExtension = originalFilenameInfo.Name.Remove(originalFilenameInfo.Name.Length - originalFilenameInfo.Extension.Length, originalFilenameInfo.Extension.Length);
        var fileResourceData = FileResource.ConvertHttpPostedFileToByteArray(formFile);
        var fileResourceMimeTypeID = GetFileResourceMimeTypeForFile(formFile).FileResourceMimeTypeID;

        var fileResource = await CreateNewAndSaveChanges(fileResourceMimeTypeID,
            baseFilenameWithoutExtension, originalFilenameInfo.Extension, fileResourceData, currentPerson);

        return fileResource;
    }

    private static FileResourceMimeType GetFileResourceMimeTypeForFile(IFormFile file)
    {
        var fileResourceMimeTypeForFile = FileResourceMimeType.All.SingleOrDefault(mt => mt.FileResourceMimeTypeContentTypeName == file.ContentType);
        Check.RequireNotNull(fileResourceMimeTypeForFile, $"Unhandled MIME type: {file.ContentType}");
        return fileResourceMimeTypeForFile;
    }

    public static void ValidateFileSize(IFormFile httpPostedFileBase, List<ValidationResult> errors, string propertyName)
    {
        if (httpPostedFileBase.Length > MaxUploadFileSizeInBytes)
        {
            var formattedUploadSize = $"~{(httpPostedFileBase.Length / 1000).ToGroupedNumeric()} KB";
            errors.Add(new ValidationResult(
                $"File is too large - must be less than {MaxUploadFileSizeInBytes / (1024 ^ 2):0.0} KB [Provided file was {formattedUploadSize}]", new[] { propertyName }));
        }
    }
}