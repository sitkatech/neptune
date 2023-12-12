//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordObservationValue]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class MaintenanceRecordObservationValueExtensionMethods
    {
        public static MaintenanceRecordObservationValueSimpleDto AsSimpleDto(this MaintenanceRecordObservationValue maintenanceRecordObservationValue)
        {
            var dto = new MaintenanceRecordObservationValueSimpleDto()
            {
                MaintenanceRecordObservationValueID = maintenanceRecordObservationValue.MaintenanceRecordObservationValueID,
                MaintenanceRecordObservationID = maintenanceRecordObservationValue.MaintenanceRecordObservationID,
                ObservationValue = maintenanceRecordObservationValue.ObservationValue
            };
            return dto;
        }
    }
}