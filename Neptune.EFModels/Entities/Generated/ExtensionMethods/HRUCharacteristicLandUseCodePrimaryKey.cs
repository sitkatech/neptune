//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.HRUCharacteristicLandUseCode


namespace Neptune.EFModels.Entities
{
    public class HRUCharacteristicLandUseCodePrimaryKey : EntityPrimaryKey<HRUCharacteristicLandUseCode>
    {
        public HRUCharacteristicLandUseCodePrimaryKey() : base(){}
        public HRUCharacteristicLandUseCodePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public HRUCharacteristicLandUseCodePrimaryKey(HRUCharacteristicLandUseCode hRUCharacteristicLandUseCode) : base(hRUCharacteristicLandUseCode){}

        public static implicit operator HRUCharacteristicLandUseCodePrimaryKey(int primaryKeyValue)
        {
            return new HRUCharacteristicLandUseCodePrimaryKey(primaryKeyValue);
        }

        public static implicit operator HRUCharacteristicLandUseCodePrimaryKey(HRUCharacteristicLandUseCode hRUCharacteristicLandUseCode)
        {
            return new HRUCharacteristicLandUseCodePrimaryKey(hRUCharacteristicLandUseCode);
        }
    }
}