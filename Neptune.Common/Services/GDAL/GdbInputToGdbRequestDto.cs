using System.Net.Http.Headers;

namespace Neptune.Common.Services.GDAL;

public class GdbInputToGdbRequestDto
{
    public GdbInput GdbInput { get; set; }
    public string GdbName { get; set; }
    public string BlobContainer { get; set; }

    public MultipartFormDataContent ToMultipartFormDataContent()
    {
        var multiPartFormDataContent = new MultipartFormDataContent();

        multiPartFormDataContent.Add(new StringContent(GdbName), "GdbName");
        multiPartFormDataContent.Add(new StringContent(BlobContainer), "BlobContainer");

        // If a file is found, use the file contents of that file
        var gdbInput = GdbInput;
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

            multiPartFormDataContent.Add(byteContent, $"GdbInput.File",
                $"{gdbInput.LayerName}.{fileExtension}");
        }
        else // otherwise canonical name will do
        {
            multiPartFormDataContent.Add(new StringContent(gdbInput.CanonicalName ?? "NULL"),
                $"GdbInput.CanonicalName");
        }

        if (!string.IsNullOrWhiteSpace(gdbInput.BlobContainer))
        {
            multiPartFormDataContent.Add(new StringContent(gdbInput.BlobContainer),
                $"GdbInput.BlobContainer");
        }

        if (!string.IsNullOrWhiteSpace(gdbInput.LayerName))
        {
            multiPartFormDataContent.Add(new StringContent(gdbInput.LayerName), $"GdbInput.LayerName");
        }

        if (!string.IsNullOrWhiteSpace(gdbInput.GeometryTypeName))
        {
            multiPartFormDataContent.Add(new StringContent(gdbInput.GeometryTypeName),
                $"GdbInput.GeometryTypeName");
        }

        multiPartFormDataContent.Add(new StringContent(gdbInput.CoordinateSystemID.ToString()),
            $"GdbInput.CoordinateSystemID");
        multiPartFormDataContent.Add(new StringContent(gdbInput.GdbInputFileType.ToString()),
            $"GdbInput.GdbInputFileType");

        return multiPartFormDataContent;
    }
}