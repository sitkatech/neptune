using System;
using Neptune.Web.Models;
using Newtonsoft.Json;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class HRUResponseFeature
    {
        [JsonProperty("attributes")]
        public HRUResponseFeatureAttributes Attributes { get; set; }
        [JsonProperty("geometry")]
        public EsriPolygonGeometry Geometry { get; set; }

        // todo: overload
        public HRUCharacteristic ToHRUCharacteristic(IHaveHRUCharacteristics iHaveHRUCharacteristics)
        {
            if (iHaveHRUCharacteristics is TreatmentBMP)
            {
                return new HRUCharacteristic(Attributes.LSPCLandUseDescription,
                        Attributes.HydrologicSoilGroup, Attributes.SlopePercentage, Attributes.ImperviousAcres)
                    {TreatmentBMPID = iHaveHRUCharacteristics.PrimaryKey};
            }
            else
            {
                throw new NotImplementedException(
                    "Tried to add HRU characteristics to an entity that hasn't fully implemented them yet.");
            }
        }
    }
}