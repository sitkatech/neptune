//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.MaintenanceRecord


namespace Neptune.EFModels.Entities
{
    public class MaintenanceRecordPrimaryKey : EntityPrimaryKey<MaintenanceRecord>
    {
        public MaintenanceRecordPrimaryKey() : base(){}
        public MaintenanceRecordPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public MaintenanceRecordPrimaryKey(MaintenanceRecord maintenanceRecord) : base(maintenanceRecord){}

        public static implicit operator MaintenanceRecordPrimaryKey(int primaryKeyValue)
        {
            return new MaintenanceRecordPrimaryKey(primaryKeyValue);
        }

        public static implicit operator MaintenanceRecordPrimaryKey(MaintenanceRecord maintenanceRecord)
        {
            return new MaintenanceRecordPrimaryKey(maintenanceRecord);
        }
    }
}