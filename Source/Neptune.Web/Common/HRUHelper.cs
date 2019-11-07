using System.Linq;
using ApprovalUtilities.Utilities;
using Neptune.Web.Common.EsriAsynchronousJob;
using Neptune.Web.Models;
using Newtonsoft.Json;

namespace Neptune.Web.Common
{
    static internal class HRUHelper
    {
        public static void RetrieveAndSaveHRUCharacteristics(IHaveHRUCharacteristics iHaveHRUCharacteristics)
        {
            var postUrl = NeptuneWebConfiguration.HRUServiceBaseUrl;
            var esriAsynchronousJobRunner = new EsriAsynchronousJobRunner(postUrl, "HRU_Composite");

            var hruRequest = EsriGPRecordSetLayer.GetGPRecordSetLayer(iHaveHRUCharacteristics);

            var serializeObject = new
            {
                Input_Polygons = JsonConvert.SerializeObject(hruRequest),
                returnZ = false,
                returnM = false,
                returnTrueCurves = false,
                f = "pjson"
            };

            HttpRequestStorage.DatabaseEntities.HRUCharacteristics.DeleteHRUCharacteristic(iHaveHRUCharacteristics.HRUCharacteristics);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            var hruCharacteristics =
                esriAsynchronousJobRunner
                    .RunJob<EsriAsynchronousJobOutputParameter<EsriGPRecordSetLayer<HRUResponseFeature>>>(serializeObject).Value
                    .Features.Select(x => x.ToHRUCharacteristic(iHaveHRUCharacteristics));

            iHaveHRUCharacteristics.HRUCharacteristics.AddAll(hruCharacteristics);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
        }
    }
}