//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectHRUCharacteristic]
namespace Neptune.EFModels.Entities
{
    public partial class ProjectHRUCharacteristic : IHavePrimaryKey
    {
        public int PrimaryKey => ProjectHRUCharacteristicID;
        public HRUCharacteristicLandUseCode BaselineHRUCharacteristicLandUseCode => HRUCharacteristicLandUseCode.AllLookupDictionary[BaselineHRUCharacteristicLandUseCodeID];
        public HRUCharacteristicLandUseCode HRUCharacteristicLandUseCode => HRUCharacteristicLandUseCode.AllLookupDictionary[HRUCharacteristicLandUseCodeID];

        public static class FieldLengths
        {
            public const int HydrologicSoilGroup = 5;
        }
    }
}