//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.MaintenanceRecordObservation


namespace Neptune.EFModels.Entities
{
    public class MaintenanceRecordObservationPrimaryKey : EntityPrimaryKey<MaintenanceRecordObservation>
    {
        public MaintenanceRecordObservationPrimaryKey() : base(){}
        public MaintenanceRecordObservationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public MaintenanceRecordObservationPrimaryKey(MaintenanceRecordObservation maintenanceRecordObservation) : base(maintenanceRecordObservation){}

        public static implicit operator MaintenanceRecordObservationPrimaryKey(int primaryKeyValue)
        {
            return new MaintenanceRecordObservationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator MaintenanceRecordObservationPrimaryKey(MaintenanceRecordObservation maintenanceRecordObservation)
        {
            return new MaintenanceRecordObservationPrimaryKey(maintenanceRecordObservation);
        }
    }
}