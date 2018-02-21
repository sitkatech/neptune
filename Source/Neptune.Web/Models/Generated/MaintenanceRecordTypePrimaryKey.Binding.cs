//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.MaintenanceRecordType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class MaintenanceRecordTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<MaintenanceRecordType>
    {
        public MaintenanceRecordTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public MaintenanceRecordTypePrimaryKey(MaintenanceRecordType maintenanceRecordType) : base(maintenanceRecordType){}

        public static implicit operator MaintenanceRecordTypePrimaryKey(int primaryKeyValue)
        {
            return new MaintenanceRecordTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator MaintenanceRecordTypePrimaryKey(MaintenanceRecordType maintenanceRecordType)
        {
            return new MaintenanceRecordTypePrimaryKey(maintenanceRecordType);
        }
    }
}