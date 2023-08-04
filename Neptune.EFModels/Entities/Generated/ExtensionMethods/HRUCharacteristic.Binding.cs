//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HRUCharacteristic]
namespace Neptune.EFModels.Entities
{
    public partial class HRUCharacteristic
    {
        public HRUCharacteristicLandUseCode BaselineHRUCharacteristicLandUseCode => HRUCharacteristicLandUseCode.AllLookupDictionary[BaselineHRUCharacteristicLandUseCodeID];
        public HRUCharacteristicLandUseCode HRUCharacteristicLandUseCode => HRUCharacteristicLandUseCode.AllLookupDictionary[HRUCharacteristicLandUseCodeID];
    }
}