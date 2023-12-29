//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.MaintenanceRecordObservationValue


namespace Neptune.EFModels.Entities
{
    public class MaintenanceRecordObservationValuePrimaryKey : EntityPrimaryKey<MaintenanceRecordObservationValue>
    {
        public MaintenanceRecordObservationValuePrimaryKey() : base(){}
        public MaintenanceRecordObservationValuePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public MaintenanceRecordObservationValuePrimaryKey(MaintenanceRecordObservationValue maintenanceRecordObservationValue) : base(maintenanceRecordObservationValue){}

        public static implicit operator MaintenanceRecordObservationValuePrimaryKey(int primaryKeyValue)
        {
            return new MaintenanceRecordObservationValuePrimaryKey(primaryKeyValue);
        }

        public static implicit operator MaintenanceRecordObservationValuePrimaryKey(MaintenanceRecordObservationValue maintenanceRecordObservationValue)
        {
            return new MaintenanceRecordObservationValuePrimaryKey(maintenanceRecordObservationValue);
        }
    }
}