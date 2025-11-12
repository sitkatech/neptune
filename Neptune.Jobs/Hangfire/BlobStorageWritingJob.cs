using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.GeoSpatial;
using Neptune.Common.JsonConverters;
using Neptune.EFModels.Entities;

namespace Neptune.Jobs.Hangfire;

public abstract class BlobStorageWritingJob<T>(
    IOptions<NeptuneJobConfiguration> configuration,
    ILogger<T> logger,
    NeptuneDbContext dbContext)
{
    protected readonly ILogger<T> Logger = logger;
    protected readonly NeptuneDbContext DbContext = dbContext;
    protected readonly BlobContainerClient BlobContainerClient = new BlobServiceClient(configuration.Value.AzureBlobStorageConnectionString).GetBlobContainerClient("file-resource");

    public async Task SerializeAndUploadToBlobStorage(IEnumerable list, string blobName)
    {
        var blobClient = BlobContainerClient.GetBlobClient(blobName);

        try
        {
            var stream = new MemoryStream();
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                DefaultIgnoreCondition = JsonIgnoreCondition.Never,
                WriteIndented = false,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = null,
            };
            jsonSerializerOptions.Converters.Add(new DateTimeConverter());
            jsonSerializerOptions.Converters.Add(new NullableDateTimeConverter());
            jsonSerializerOptions.Converters.Add(new DoubleConverter(10));
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            await GeoJsonSerializer.SerializeToStream(list, jsonSerializerOptions, stream);
            stream.Position = 0;
            await blobClient.UploadAsync(stream, overwrite: true);
        }
        catch (Exception ex)
        {
            Logger.LogError("Failed to write to blob storage; Exception details: " + ex.Message);
        }
    }
}