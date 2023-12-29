//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordObservation]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class MaintenanceRecordObservationExtensionMethods
    {
        public static MaintenanceRecordObservationSimpleDto AsSimpleDto(this MaintenanceRecordObservation maintenanceRecordObservation)
        {
            var dto = new MaintenanceRecordObservationSimpleDto()
            {
                MaintenanceRecordObservationID = maintenanceRecordObservation.MaintenanceRecordObservationID,
                MaintenanceRecordID = maintenanceRecordObservation.MaintenanceRecordID,
                TreatmentBMPTypeCustomAttributeTypeID = maintenanceRecordObservation.TreatmentBMPTypeCustomAttributeTypeID,
                TreatmentBMPTypeID = maintenanceRecordObservation.TreatmentBMPTypeID,
                CustomAttributeTypeID = maintenanceRecordObservation.CustomAttributeTypeID
            };
            return dto;
        }
    }
}