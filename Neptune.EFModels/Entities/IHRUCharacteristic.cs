namespace Neptune.EFModels.Entities;

public interface IHRUCharacteristic
{
    string? HydrologicSoilGroup { get; set; }
    int SlopePercentage { get; set; }
    double ImperviousAcres { get; set; }
    DateTime LastUpdated { get; set; }
    double Area { get; set; }
    int HRUCharacteristicLandUseCodeID { get; set; }
    double BaselineImperviousAcres { get; set; }
    int BaselineHRUCharacteristicLandUseCodeID { get; set; }
}