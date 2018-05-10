//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.MaintenanceRecordObservationValue
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class MaintenanceRecordObservationValuePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<MaintenanceRecordObservationValue>
    {
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