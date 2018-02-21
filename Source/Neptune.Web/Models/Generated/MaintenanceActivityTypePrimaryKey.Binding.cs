//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.MaintenanceActivityType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class MaintenanceActivityTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<MaintenanceActivityType>
    {
        public MaintenanceActivityTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public MaintenanceActivityTypePrimaryKey(MaintenanceActivityType maintenanceActivityType) : base(maintenanceActivityType){}

        public static implicit operator MaintenanceActivityTypePrimaryKey(int primaryKeyValue)
        {
            return new MaintenanceActivityTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator MaintenanceActivityTypePrimaryKey(MaintenanceActivityType maintenanceActivityType)
        {
            return new MaintenanceActivityTypePrimaryKey(maintenanceActivityType);
        }
    }
}