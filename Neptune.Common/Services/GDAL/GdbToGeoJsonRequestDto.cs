using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace Neptune.Common.Services.GDAL;

public class GdbToGeoJsonRequestDto
{
    public IFormFile File { get; set; }
    public string BlobContainer { get; set; }
    public string CanonicalName { get; set; }
    public List<GdbLayerOutput> GdbLayerOutputs { get; set; }

    public MultipartFormDataContent ToMultipartFormDataContent()
    {
        var multiPartFormDataContent = new MultipartFormDataContent();

        if (File != null)
        {
            MemoryStream ms = new MemoryStream();
            File.CopyToAsync(ms);
            ms.Seek(0, SeekOrigin.Begin);
            var byteContent = new StreamContent(ms);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/*");
            multiPartFormDataContent.Add(byteContent, $"File", File.FileName);
        }
        else // otherwise canonical name will do
        {
            multiPartFormDataContent.Add(new StringContent(BlobContainer), "BlobContainer");
            multiPartFormDataContent.Add(new StringContent(CanonicalName), "CanonicalName");
        }

        if (GdbLayerOutputs.Any())
        {
            for (int i = 0; i < GdbLayerOutputs.Count(); i++)
            {
                if (!string.IsNullOrWhiteSpace(GdbLayerOutputs[i].FeatureLayerName))
                {
                    multiPartFormDataContent.Add(new StringContent(GdbLayerOutputs[i].FeatureLayerName), $"GdbLayerOutputs[{i}].FeatureLayerName");
                }
                if (!string.IsNullOrWhiteSpace(GdbLayerOutputs[i].Filter))
                {
                    multiPartFormDataContent.Add(new StringContent(GdbLayerOutputs[i].Filter), $"GdbLayerOutputs[{i}].Filter");
                }
                if (GdbLayerOutputs[i].Columns.Any())
                {
                    for (int j = 0; j < GdbLayerOutputs[i].Columns.Count; j++)
                    {
                        multiPartFormDataContent.Add(new StringContent(GdbLayerOutputs[i].Columns[j]), $"GdbLayerOutputs[{i}].Columns[{j}]");
                    }
                }

                if (GdbLayerOutputs[i].Extent != null)
                {
                    multiPartFormDataContent.Add(new StringContent(GdbLayerOutputs[i].Extent.MinX.ToString()), $"GdbLayerOutputs[{i}].Extent.MinX");
                    multiPartFormDataContent.Add(new StringContent(GdbLayerOutputs[i].Extent.MinY.ToString()), $"GdbLayerOutputs[{i}].Extent.MinY");
                    multiPartFormDataContent.Add(new StringContent(GdbLayerOutputs[i].Extent.MaxX.ToString()), $"GdbLayerOutputs[{i}].Extent.MaxX");
                    multiPartFormDataContent.Add(new StringContent(GdbLayerOutputs[i].Extent.MaxY.ToString()), $"GdbLayerOutputs[{i}].Extent.MaxY");
                }

                multiPartFormDataContent.Add(new StringContent(GdbLayerOutputs[i].CoordinateSystemID.ToString()), $"GdbLayerOutputs[{i}].CoordinateSystemID");
            }
        }

        return multiPartFormDataContent;
    }
}