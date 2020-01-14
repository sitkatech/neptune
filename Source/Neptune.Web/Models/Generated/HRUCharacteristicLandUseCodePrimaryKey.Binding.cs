//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.HRUCharacteristicLandUseCode
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class HRUCharacteristicLandUseCodePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<HRUCharacteristicLandUseCode>
    {
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