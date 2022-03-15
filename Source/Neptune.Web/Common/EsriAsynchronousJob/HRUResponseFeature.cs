using System;
using System.Linq;
using LtInfo.Common;
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

        public HRUCharacteristic ToHRUCharacteristic()
        {
            var hruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.SingleOrDefault(x => x.HRUCharacteristicLandUseCodeName == Attributes.ModelBasinLandUseDescription);
            var baselineHruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.SingleOrDefault(x => x.HRUCharacteristicLandUseCodeName == Attributes.BaselineLandUseDescription);

            var hruCharacteristic = new HRUCharacteristic(Attributes.HydrologicSoilGroup, Attributes.SlopePercentage.GetValueOrDefault(),
                Attributes.ImperviousAcres.GetValueOrDefault(), DateTime.Now,
                Attributes.Acres.GetValueOrDefault(),
                hruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID, Attributes.QueryFeatureID,
                Attributes.BaselineImperviousAcres.GetValueOrDefault(),
                baselineHruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID);
                
            return hruCharacteristic;
        }

        public PlannedProjectHRUCharacteristic ToPlannedProjectHRUCharacteristic(int plannedProjectID)
        {
            var hruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.SingleOrDefault(x => x.HRUCharacteristicLandUseCodeName == Attributes.ModelBasinLandUseDescription);
            var baselineHruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.SingleOrDefault(x => x.HRUCharacteristicLandUseCodeName == Attributes.BaselineLandUseDescription);

            var hruCharacteristic = new PlannedProjectHRUCharacteristic(plannedProjectID, Attributes.HydrologicSoilGroup, Attributes.SlopePercentage.GetValueOrDefault(),
                Attributes.ImperviousAcres.GetValueOrDefault(), DateTime.Now,
                Attributes.Acres.GetValueOrDefault(),
                hruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID, Attributes.QueryFeatureID,
                Attributes.BaselineImperviousAcres.GetValueOrDefault(),
                baselineHruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID);

            return hruCharacteristic;
        }
    }
}