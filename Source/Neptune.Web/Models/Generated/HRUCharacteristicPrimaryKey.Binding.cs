//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.HRUCharacteristic
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class HRUCharacteristicPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<HRUCharacteristic>
    {
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