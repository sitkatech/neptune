using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Neptune.Common.Services.GDAL;

namespace Neptune.Common.Services;

public class QGISAPIService
{
    /// <summary>
    /// A HttpClient is registered in the Startup.cs file for this service.
    /// That is where the BaseUrl is set from the projects Configuration.
    /// </summary>
    private readonly HttpClient _httpClient;
    private readonly ILogger<QGISAPIService> _logger;

    public QGISAPIService(ILogger<QGISAPIService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task GenerateTGUs(GenerateTrashGeneratingUnitRequestDto generateLoadGeneratingUnitRequestDto)
    {
        _logger.LogInformation("Sending request to QGIS API");
        var response = await _httpClient.PostAsJsonAsync("/qgis/generate-tgus", generateLoadGeneratingUnitRequestDto);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed");
        }
    }

    public async Task GenerateLGUs(GenerateLoadGeneratingUnitRequestDto generateLoadGeneratingUnitRequestDto)
    {
        _logger.LogInformation("Sending request to QGIS API");
        var response = await _httpClient.PostAsJsonAsync("/qgis/generate-lgus", generateLoadGeneratingUnitRequestDto);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed");
        }
    }

    public async Task GeneratePLGUs(GenerateProjectLoadGeneratingUnitRequestDto generateProjectLoadGeneratingUnitRequestDto)
    {
        _logger.LogInformation("Sending request to QGIS API");
        var response = await _httpClient.PostAsJsonAsync("/qgis/generate-plgus", generateProjectLoadGeneratingUnitRequestDto);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed");
        }
    }
}