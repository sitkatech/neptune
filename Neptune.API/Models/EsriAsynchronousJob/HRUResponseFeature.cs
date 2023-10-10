using System;
using System.Linq;
using System.Text.Json.Serialization;
using Neptune.EFModels.Entities;

namespace Neptune.API.Models.EsriAsynchronousJob
{
    public class HRUResponseFeature
    {
        [JsonPropertyName("attributes")]
        public HRUResponseFeatureAttributes Attributes { get; set; }
        [JsonPropertyName("geometry")]
        public EsriPolygonGeometry Geometry { get; set; }

        public HRUCharacteristic ToHRUCharacteristic()
        {
            var hruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.SingleOrDefault(x => x.HRUCharacteristicLandUseCodeName == Attributes.ModelBasinLandUseDescription);
            var baselineHruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.SingleOrDefault(x => x.HRUCharacteristicLandUseCodeName == Attributes.BaselineLandUseDescription);

            var hruCharacteristic = new HRUCharacteristic
            {
                HydrologicSoilGroup = Attributes.HydrologicSoilGroup,
                SlopePercentage = Attributes.SlopePercentage.GetValueOrDefault(),
                ImperviousAcres = Attributes.ImperviousAcres.GetValueOrDefault(),
                LastUpdated = DateTime.Now,
                Area = Attributes.Acres.GetValueOrDefault(),
                HRUCharacteristicLandUseCodeID = hruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID,
                LoadGeneratingUnitID = Attributes.QueryFeatureID,
                BaselineImperviousAcres = Attributes.BaselineImperviousAcres.GetValueOrDefault(),
                BaselineHRUCharacteristicLandUseCodeID = baselineHruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID
            };
                
            return hruCharacteristic;
        }

        public ProjectHRUCharacteristic ToProjectHRUCharacteristic(int projectID)
        {
            var hruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.SingleOrDefault(x => x.HRUCharacteristicLandUseCodeName == Attributes.ModelBasinLandUseDescription);
            var baselineHruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.SingleOrDefault(x => x.HRUCharacteristicLandUseCodeName == Attributes.BaselineLandUseDescription);

            var hruCharacteristic = new ProjectHRUCharacteristic
            {
                ProjectID = projectID,
                HydrologicSoilGroup = Attributes.HydrologicSoilGroup,
                SlopePercentage = Attributes.SlopePercentage.GetValueOrDefault(),
                ImperviousAcres = Attributes.ImperviousAcres.GetValueOrDefault(),
                LastUpdated = DateTime.Now,
                Area = Attributes.Acres.GetValueOrDefault(),
                HRUCharacteristicLandUseCodeID = hruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID,
                ProjectLoadGeneratingUnitID = Attributes.QueryFeatureID,
                BaselineImperviousAcres = Attributes.BaselineImperviousAcres.GetValueOrDefault(),
                BaselineHRUCharacteristicLandUseCodeID = baselineHruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID
            };

            return hruCharacteristic;
        }
    }
}