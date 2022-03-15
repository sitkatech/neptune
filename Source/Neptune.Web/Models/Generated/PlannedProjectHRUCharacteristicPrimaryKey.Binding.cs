//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PlannedProjectHRUCharacteristic
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class PlannedProjectHRUCharacteristicPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PlannedProjectHRUCharacteristic>
    {
        public PlannedProjectHRUCharacteristicPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PlannedProjectHRUCharacteristicPrimaryKey(PlannedProjectHRUCharacteristic plannedProjectHRUCharacteristic) : base(plannedProjectHRUCharacteristic){}

        public static implicit operator PlannedProjectHRUCharacteristicPrimaryKey(int primaryKeyValue)
        {
            return new PlannedProjectHRUCharacteristicPrimaryKey(primaryKeyValue);
        }

        public static implicit operator PlannedProjectHRUCharacteristicPrimaryKey(PlannedProjectHRUCharacteristic plannedProjectHRUCharacteristic)
        {
            return new PlannedProjectHRUCharacteristicPrimaryKey(plannedProjectHRUCharacteristic);
        }
    }
}