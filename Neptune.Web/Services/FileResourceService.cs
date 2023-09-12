using Microsoft.Extensions.Options;
using Neptune.Common.DesignByContract;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;

namespace Neptune.Web.Services;

public class FileResourceService
{
    private readonly NeptuneDbContext _dbContext;
    private readonly AzureBlobStorageService _azureBlobStorageService;
    private readonly WebConfiguration _webConfiguration;

    public FileResourceService(NeptuneDbContext dbContext, IOptions<WebConfiguration> webConfiguration, AzureBlobStorageService azureBlobStorageService)
    {
        _dbContext = dbContext;
        _azureBlobStorageService = azureBlobStorageService;
        _webConfiguration = webConfiguration.Value;
    }

    public async Task<FileResource> CreateNew(int fileResourceMimeTypeID,
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
            InBlobStorage = true
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

    public async Task<FileResource> CreateNewResizedImageFileResource(IFormFile formFile, byte[] resizedImageBytes,
        Person currentPerson)
    {
        var fileName = formFile.FileName;
        if (string.IsNullOrWhiteSpace(fileName))
        {
            fileName = Guid.NewGuid().ToString() + ".jpg";
        }

        var originalFilenameInfo = new FileInfo(fileName);
        var baseFilenameWithoutExtension = originalFilenameInfo.Name.Remove(originalFilenameInfo.Name.Length - originalFilenameInfo.Extension.Length, originalFilenameInfo.Extension.Length);
        var fileResourceData = resizedImageBytes;
        var fileResourceMimeTypeID = GetFileResourceMimeTypeForFile(formFile).FileResourceMimeTypeID;

        var fileResource = await CreateNew(fileResourceMimeTypeID,
            baseFilenameWithoutExtension, originalFilenameInfo.Extension, fileResourceData, currentPerson);

        return fileResource;
    }

    private static FileResourceMimeType GetFileResourceMimeTypeForFile(IFormFile file)
    {
        var fileResourceMimeTypeForFile = FileResourceMimeType.All.SingleOrDefault(mt => mt.FileResourceMimeTypeContentTypeName == file.ContentType);
        Check.RequireNotNull(fileResourceMimeTypeForFile, $"Unhandled MIME type: {file.ContentType}");
        return fileResourceMimeTypeForFile;
    }
}