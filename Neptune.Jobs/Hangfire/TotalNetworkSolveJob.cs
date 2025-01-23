using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.Jobs.Services;
using System.Text.Json.Nodes;

namespace Neptune.Jobs.Hangfire;

public class TotalNetworkSolveJob
{
    private readonly ILogger<TotalNetworkSolveJob> _logger;
    private readonly NeptuneDbContext _dbContext;
    private readonly NereidService _nereidService;
    private readonly BlobContainerClient _blobContainerClient;

    public TotalNetworkSolveJob(IOptions<NeptuneJobConfiguration> configuration, ILogger<TotalNetworkSolveJob> logger, NeptuneDbContext dbContext, NereidService nereidService)
    {
        _logger = logger;
        _dbContext = dbContext;
        _nereidService = nereidService;
        _blobContainerClient = new BlobServiceClient(configuration.Value.AzureBlobStorageConnectionString).GetBlobContainerClient("file-resource");
    }

    public async Task RunJob()
    {
        // clear out all dirty nodes since the whole network is being run.
        await _dbContext.DirtyModelNodes.ExecuteDeleteAsync();
        // clear out all the nereid results since we are rerunning it for the whole network
        await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pDeleteNereidResults");

        var networkSolveResultBaseline = await _nereidService.TotalNetworkSolve(_dbContext, true);
        var networkSolveResult = await _nereidService.TotalNetworkSolve(_dbContext, false);
        await CreateNereidResultsAsJsonFileAndPostToBlobStorage(networkSolveResultBaseline.NereidResults, "BaselineModelResults.json");
        await CreateNereidResultsAsJsonFileAndPostToBlobStorage(networkSolveResult.NereidResults, "ModelResults.json");
    }

    private async Task CreateNereidResultsAsJsonFileAndPostToBlobStorage(IEnumerable<NereidResult> nereidResults, string blobFilename)
    {
        var list = nereidResults.Select(x =>
        {
            var jsonObject = GeoJsonSerializer.Deserialize<JsonObject>(x.FullResponse);
            jsonObject["TreatmentBMPID"] = x.TreatmentBMPID;
            jsonObject["WaterQualityManagementPlanID"] = x.WaterQualityManagementPlanID;
            jsonObject["DelineationID"] = x.DelineationID;
            jsonObject["RegionalSubbasinID"] = x.RegionalSubbasinID;
            return jsonObject;
        }).ToList();

        try
        {

            var stream = new MemoryStream();
            await GeoJsonSerializer.SerializeToStream(list, GeoJsonSerializer.DefaultSerializerOptions, stream);
            stream.Position = 0;
            var blobClient = _blobContainerClient.GetBlobClient(blobFilename);
            await blobClient.UploadAsync(stream, overwrite: true);
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to write to blob storage; Exception details: " + ex.Message);
        }
    }
}