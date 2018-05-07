//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.MaintenanceRecordObservation
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class MaintenanceRecordObservationPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<MaintenanceRecordObservation>
    {
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