//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.HRUCharacteristic


namespace Neptune.EFModels.Entities
{
    public class HRUCharacteristicPrimaryKey : EntityPrimaryKey<HRUCharacteristic>
    {
        public HRUCharacteristicPrimaryKey() : base(){}
        public HRUCharacteristicPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public HRUCharacteristicPrimaryKey(HRUCharacteristic hRUCharacteristic) : base(hRUCharacteristic){}

        public static implicit operator HRUCharacteristicPrimaryKey(int primaryKeyValue)
        {
            return new HRUCharacteristicPrimaryKey(primaryKeyValue);
        }

        public static implicit operator HRUCharacteristicPrimaryKey(HRUCharacteristic hRUCharacteristic)
        {
            return new HRUCharacteristicPrimaryKey(hRUCharacteristic);
        }
    }
}