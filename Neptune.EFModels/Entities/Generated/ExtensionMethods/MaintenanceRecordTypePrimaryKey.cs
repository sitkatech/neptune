//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.MaintenanceRecordType


namespace Neptune.EFModels.Entities
{
    public class MaintenanceRecordTypePrimaryKey : EntityPrimaryKey<MaintenanceRecordType>
    {
        public MaintenanceRecordTypePrimaryKey() : base(){}
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