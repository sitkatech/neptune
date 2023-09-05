using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hippocamp.API.Services;
using Hippocamp.EFModels.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hippocamp.API.Hangfire;

public class BlobFileTransferJob : ScheduledBackgroundJobBase<BlobFileTransferJob>
{
    private readonly AzureBlobStorageService _blobStorageService;
    private const int FileResourcesPerJob = 200;

    public BlobFileTransferJob(
        ILogger<BlobFileTransferJob> logger,
        IWebHostEnvironment webHostEnvironment,
        HippocampDbContext dbContext,
        IOptions<HippocampConfiguration> hippocampConfiguration,
        SitkaSmtpClientService sitkaSmtpClient, AzureBlobStorageService blobStorageService) : base(JobName, logger, webHostEnvironment, dbContext, hippocampConfiguration, sitkaSmtpClient)
    {
        _blobStorageService = blobStorageService;
    }
    
    public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Production };

    public const string JobName = "Blob File Transfer Job";

    protected override void RunJobImplementation()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var nextFileResourcesToMove = DbContext.FileResources
            .Where(x => !x.InBlobStorage)
            .OrderBy(x => x.FileResourceID)
            .Take(FileResourcesPerJob).ToList();

        foreach (var fileResource in nextFileResourcesToMove)
        {
            Logger.LogInformation($"Begin: Transferring {fileResource.OriginalBaseFilename} to blob storage container.");
            var created = _blobStorageService.UploadFileResource(fileResource);
            Logger.LogInformation($"Finished: Transferring {fileResource.OriginalBaseFilename} to blob storage container.");

            fileResource.InBlobStorage = created;
        }

        DbContext.SaveChanges();
        stopwatch.Stop();

        Logger.LogInformation($"Finished transferring {FileResourcesPerJob} FileResources. Job took {stopwatch.Elapsed.TotalSeconds} seconds.");
    }
    

    
}