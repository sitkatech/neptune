using System.Net.Http.Headers;

namespace Neptune.Common.Services.GDAL;

public class GdbInputsToGdbRequestDto
{
    public IList<GdbInput> GdbInputs { get; set; }
    public string GdbName { get; set; }
    public string BlobContainer { get; set; }

    public MultipartFormDataContent ToMultipartFormDataContent()
    {
        var multiPartFormDataContent = new MultipartFormDataContent();

        multiPartFormDataContent.Add(new StringContent(GdbName), "GdbName");
        multiPartFormDataContent.Add(new StringContent(BlobContainer), "BlobContainer");

        if (GdbInputs.Any())
        {
            for (var i = 0; i < GdbInputs.Count(); i++)
            {
                // If a file is found, use the file contents of that file
                var gdbInput = GdbInputs[i];
                if (gdbInput.FileContents != null)
                {
                    string fileExtension;
                    var byteContent = new ByteArrayContent(gdbInput.FileContents);
                    switch (gdbInput.GdbInputFileType)
                    {
                        case GdbInputFileTypeEnum.CSV:
                            byteContent.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
                            fileExtension = "csv";
                            break;
                        case GdbInputFileTypeEnum.GeoJson:
                        default:
                            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            fileExtension = "json";
                            break;
                    }
                    multiPartFormDataContent.Add(byteContent, $"GdbInputs[{i}].File", $"{gdbInput.LayerName}.{fileExtension}");
                }
                else // otherwise canonical name will do
                {
                    multiPartFormDataContent.Add(new StringContent(gdbInput.CanonicalName ?? "NULL"), $"GdbInputs[{i}].CanonicalName");
                }

                if (!string.IsNullOrWhiteSpace(gdbInput.BlobContainer))
                {
                    multiPartFormDataContent.Add(new StringContent(gdbInput.BlobContainer), $"GdbInputs[{i}].BlobContainer");
                }
                if (!string.IsNullOrWhiteSpace(gdbInput.LayerName))
                {
                    multiPartFormDataContent.Add(new StringContent(gdbInput.LayerName), $"GdbInputs[{i}].LayerName");
                }
                if (!string.IsNullOrWhiteSpace(gdbInput.GeometryTypeName))
                {
                    multiPartFormDataContent.Add(new StringContent(gdbInput.GeometryTypeName), $"GdbInputs[{i}].GeometryTypeName");
                }

                multiPartFormDataContent.Add(new StringContent(gdbInput.CoordinateSystemID.ToString()), $"GdbInputs[{i}].CoordinateSystemID");
                multiPartFormDataContent.Add(new StringContent(gdbInput.GdbInputFileType.ToString()), $"GdbInputs[{i}].GdbInputFileType");
            }
        }

        return multiPartFormDataContent;
    }
}