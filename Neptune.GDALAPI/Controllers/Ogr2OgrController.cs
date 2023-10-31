using Neptune.Common;
using Neptune.GDALAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using Neptune.Common.DesignByContract;
using Neptune.Common.Services.GDAL;

namespace Neptune.GDALAPI.Controllers
{
    [ApiController]
    public class Ogr2OgrController : ControllerBase
    {
        private readonly ILogger<Ogr2OgrController> _logger;
        private readonly Ogr2OgrService _ogr2OgrService;
        private readonly IAzureStorage _azureStorage;

        public Ogr2OgrController(ILogger<Ogr2OgrController> logger, Ogr2OgrService ogr2OgrService, IAzureStorage azureStorage)
        {
            _logger = logger;
            _ogr2OgrService = ogr2OgrService;
            _azureStorage = azureStorage;
        }

        [HttpGet("/")]
        public ActionResult Get()
        {
            return Ok("Hello from the GDAL API!");
        }
        
        [HttpPost("ogr2ogr/upsert-gdb")]
        [RequestSizeLimit(100_000_000_000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 100_000_000_000)]
        public async Task<IActionResult> UpsertGdb([FromForm] GdbInputToGdbRequestDto requestDto)
        {
            using var disposableTempGdbZipFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".gdb.zip");
            using var disposableTempGdbFile = DisposableTempDirectory.MakeDisposableTempDirectoryEndingIn(".gdb");
            var gdbFileFolder = disposableTempGdbFile.DirectoryInfo;

            var exists = await _azureStorage.ExistsAsync(requestDto.GdbInput.BlobContainer, requestDto.GdbName);
            if (exists)
            {
                await _azureStorage.DownloadToAsync(requestDto.GdbInput.BlobContainer, requestDto.GdbName, disposableTempGdbZipFile.FileInfo.FullName);

                ZipFile.ExtractToDirectory(disposableTempGdbZipFile.FileInfo.FullName, gdbFileFolder.FullName);
                gdbFileFolder = gdbFileFolder.GetDirectories().First();
            }

            await GdbInputToGdb(gdbFileFolder.FullName, requestDto.GdbInput, exists);

            using var disposableTempGdbZipFile2 = DisposableTempFile.MakeDisposableTempFileEndingIn(".gdb.zip");
            GdbFolderToZipFile(gdbFileFolder, requestDto.GdbName, disposableTempGdbZipFile2);

            var stream = new StreamContent(disposableTempGdbZipFile2.FileInfo.OpenRead());
            await _azureStorage.UploadAsync(requestDto.GdbInput.BlobContainer, requestDto.GdbName, stream);

            return Ok(true);
        }

        [HttpPost("ogr2ogr/upsert-gdb-as-zip")]
        [RequestSizeLimit(100_000_000_000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 100_000_000_000)]
        public async Task<FileStreamResult> UpsertGdbAndReturnAsZip([FromForm] GdbInputsToGdbRequestDto requestDto)
        {
            using var disposableTempGdbFile = DisposableTempDirectory.MakeDisposableTempDirectoryEndingIn(".gdb");
            var gdbFileFolder = disposableTempGdbFile.DirectoryInfo;

            await GdbInputsToGdb(requestDto, gdbFileFolder.FullName);

            using var disposableTempGdbZipFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".gdb.zip");
            GdbFolderToZipFile(gdbFileFolder, requestDto.GdbName, disposableTempGdbZipFile);

            var stream = new StreamContent(disposableTempGdbZipFile.FileInfo.OpenRead());
            return new FileStreamResult(await stream.ReadAsStreamAsync(), "application/zip");
        }

        private async Task GdbInputsToGdb(GdbInputsToGdbRequestDto requestDto, string gdbOutputPath)
        {
            for (var i = 0; i < requestDto.GdbInputs.Count; i++)
            {
                var update = i != 0;
                var gdbInput = requestDto.GdbInputs[i];
                await GdbInputToGdb(gdbOutputPath, gdbInput, update);
            }
        }

        private async Task GdbInputToGdb(string gdbOutputPath, GdbInput gdbInput, bool update)
        {
            if (gdbInput.File == null && string.IsNullOrWhiteSpace(gdbInput.CanonicalName))
            {
                _logger.LogWarning($"Received an input that doesn't have a file or canonical name.");
                return;
            }

            using var disposableJsonTempFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".json");
            if (gdbInput.File != null) // if there is a file uploaded, use that file
            {
                _logger.LogInformation($"Beginning processing of uploaded file {gdbInput.LayerName}");
                await using var fileStream = new FileStream(disposableJsonTempFile.FileInfo.FullName, FileMode.Create);
                await gdbInput.File.CopyToAsync(fileStream);
            }
            else // otherwise download from the blobcontainer the file with the canonical name
            {
                _logger.LogInformation($"Beginning processing of {gdbInput.CanonicalName}");
                await _azureStorage.DownloadToAsync(gdbInput.BlobContainer, gdbInput.CanonicalName,
                    disposableJsonTempFile.FileInfo.FullName);
            }

            var args = BuildCommandLineArgumentsForGeoJsonToFileGdb(disposableJsonTempFile.FileInfo.FullName,
                gdbInput.CoordinateSystemID, gdbOutputPath, gdbInput.LayerName, update,
                gdbInput.GeometryTypeName);

            _ogr2OgrService.Run(args);
        }

        [HttpPost("ogr2ogr/gdb-geojson-list")]
        [RequestSizeLimit(100_000_000_000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 100_000_000_000)]
        public async Task<ActionResult<List<string>>> GdbToGeoJsonList([FromForm] GdbToGeoJsonRequestDto requestDto)
        {
            using var disposableTempGdbZip = DisposableTempFile.MakeDisposableTempFileEndingIn(".gdb.zip");
            await RetrieveGdbFromFileOrBlobStorage(requestDto, disposableTempGdbZip);
            var result = requestDto.GdbLayerOutputs.Select(layerOutput => ExtractGeoJsonFromGdb(disposableTempGdbZip, layerOutput)).ToList();
            return Ok(result);
        }

        [HttpPost("ogr2ogr/gdb-geojson")]
        [RequestSizeLimit(100_000_000_000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 100_000_000_000)]
        public async Task<ActionResult<string>> GdbToGeoJson([FromForm] GdbToGeoJsonRequestDto requestDto)
        {
            if (requestDto.GdbLayerOutputs.Count != 1)
            {
                return BadRequest(
                    "Expecting to process only one layer for this gdb.  If you need to process more than one please use the ogr2ogr/gdb-geojsons end point!");
            }

            using var disposableTempGdbZip = DisposableTempFile.MakeDisposableTempFileEndingIn(".gdb.zip");
            await RetrieveGdbFromFileOrBlobStorage(requestDto, disposableTempGdbZip);
            var layerOutput = requestDto.GdbLayerOutputs.Single();
            var geoJson = ExtractGeoJsonFromGdb(disposableTempGdbZip, layerOutput);
            return Ok(geoJson);
        }

        private string ExtractGeoJsonFromGdb(DisposableTempFile disposableTempGdbZip, GdbLayerOutput layerOutput)
        {
            var args = BuildCommandLineArgumentsForFileGdbToGeoJson(disposableTempGdbZip.FileInfo.FullName,
                layerOutput.FeatureLayerName,
                layerOutput.FeatureLayerName,
                layerOutput.Columns,
                layerOutput.Filter,
                layerOutput.CoordinateSystemID,
                false,
                layerOutput.NumberOfSignificantDigits,
                layerOutput.Extent);
            var geoJson = _ogr2OgrService.Run(args);
            return geoJson.StdOut;
        }

        private async Task RetrieveGdbFromFileOrBlobStorage(GdbToGeoJsonRequestDto requestDto,
            DisposableTempFile disposableTempGdbZip)
        {
            if (requestDto.File != null) // if there is a file uploaded, use that file
            {
                _logger.LogInformation($"Using GDB File Uploaded {requestDto.File.FileName}");
                await using var fileStream = new FileStream(disposableTempGdbZip.FileInfo.FullName, FileMode.Create);
                await requestDto.File.CopyToAsync(fileStream);
            }
            else // otherwise download from the blobcontainer the file with the canonical name
            {
                _logger.LogInformation($"Retrieving GDB File from blob storage: {requestDto.CanonicalName}");
                await _azureStorage.DownloadToAsync(requestDto.BlobContainer, requestDto.CanonicalName,
                    disposableTempGdbZip.FileInfo.FullName);
            }
        }

        private static void GdbFolderToZipFile(DirectoryInfo folderToZip, string desiredName, DisposableTempFile zipDestination)
        {
            var directoryInfo = new DirectoryInfo(folderToZip.FullName);
            var gdbDirectoryFullPath = $"{directoryInfo.Parent}/{desiredName}.gdb";
            if (directoryInfo.FullName != gdbDirectoryFullPath)
            {
                directoryInfo.MoveTo(gdbDirectoryFullPath);
            }

            ZipFile.CreateFromDirectory(gdbDirectoryFullPath, zipDestination.FileInfo.FullName, CompressionLevel.Optimal, true);
            Directory.Delete(gdbDirectoryFullPath, true);
        }

        private static List<string> BuildCommandLineArgumentsForGeoJsonToFileGdb(string pathToSourceGeoJsonFile, int coordinateSystemId, string outputPath, string outputLayerName, bool update, string geometryType)
        {
            var commandLineArguments = new List<string>
            {
                update ? "-update" : null,
                "-s_srs",
                GetMapProjection(coordinateSystemId),
                "-a_srs",
                GetMapProjection(coordinateSystemId),
                "-f",
                "OpenFileGDB",
                outputPath,
                pathToSourceGeoJsonFile,
                "-nln",
                SanitizeStringForGdb(outputLayerName),
                "-nlt",
                geometryType,
                "-append",
            };

            return commandLineArguments.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        }

        private static List<string> BuildCommandLineArgumentsForCsvToFileGdb(string pathToSourceGeoJsonFile, string outputPath, string outputLayerName, bool update)
        {
            var commandLineArguments = new List<string>
            {
                update ? "-update" : null,
                "-f",
                "OpenFileGDB",
                outputPath,
                pathToSourceGeoJsonFile,
                "-nln",
                SanitizeStringForGdb(outputLayerName),
                "-append",
            };

            return commandLineArguments.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        }

        private static List<string> BuildCommandLineArgumentsForFileGdbToGeoJson(string inputGdbFilePath, string sourceLayerName, string targetTableName,
            List<string> columnNameList, string filter, int coordinateSystemId, bool explodeCollections,
            int? significantDigits, GdbExtent? extent)
        {
            var reservedFields = new[] { "Ogr_Fid", "Ogr_Geometry" };
            var filteredColumnNameList = columnNameList.Where(x => reservedFields.All(y => !String.Equals(x, y, StringComparison.InvariantCultureIgnoreCase))).ToList();
            const string ogr2OgrColumnListSeparator = ",";
            Check.Require(filteredColumnNameList.All(x => !x.Contains(ogr2OgrColumnListSeparator)),
                $"Found column names with separator character \"{ogr2OgrColumnListSeparator}\", can't continue. Columns:{String.Join("\r\n", filteredColumnNameList)}");

            var columnNames = filteredColumnNameList.Any() ? string.Join(ogr2OgrColumnListSeparator + " ", filteredColumnNameList) : "*";
            var selectStatement = $"select {columnNames} from {sourceLayerName} {filter}";

            var commandLineArguments = new List<string>
            {
                "-sql",
                selectStatement,
                "-t_srs",
                GetMapProjection(coordinateSystemId),
                explodeCollections ? "-explodecollections" : null,
                "-f",
                "GeoJSON",
                "/dev/stdout",
                inputGdbFilePath,
                "-nln",
                targetTableName
            };

            if (extent != null)
            {
                commandLineArguments.Add("-clipsrc");
                commandLineArguments.Add(extent.MinX.ToString());
                commandLineArguments.Add(extent.MinY.ToString());
                commandLineArguments.Add(extent.MaxX.ToString());
                commandLineArguments.Add(extent.MaxY.ToString());
            }

            // layer creation options: see https://gdal.org/drivers/vector/geojson.html
            var layerCreationOptions = new List<string>()
            {
                "-lco",
                "COORDINATE_PRECISION=3",
                significantDigits.HasValue ? "SIGNIFICANT_FIGURES=" + significantDigits : null
            };

            return commandLineArguments.Where(x => x != null).Union(layerCreationOptions.Where(x => !string.IsNullOrWhiteSpace(x))).ToList();
        }

        public static string GetMapProjection(int coordinateSystemId)
        {
            return $"EPSG:{coordinateSystemId}";
        }

        public static string SanitizeStringForGdb(string str)
        {
            var arr = str.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray();
            return new string(arr).Replace(" ", "_");
        }

    }
}
