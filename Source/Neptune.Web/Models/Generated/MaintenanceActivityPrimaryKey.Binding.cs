//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.MaintenanceActivity
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class MaintenanceActivityPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<MaintenanceActivity>
    {
        public MaintenanceActivityPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public MaintenanceActivityPrimaryKey(MaintenanceActivity maintenanceActivity) : base(maintenanceActivity){}

        public static implicit operator MaintenanceActivityPrimaryKey(int primaryKeyValue)
        {
            return new MaintenanceActivityPrimaryKey(primaryKeyValue);
        }

        public static implicit operator MaintenanceActivityPrimaryKey(MaintenanceActivity maintenanceActivity)
        {
            return new MaintenanceActivityPrimaryKey(maintenanceActivity);
        }
    }
}