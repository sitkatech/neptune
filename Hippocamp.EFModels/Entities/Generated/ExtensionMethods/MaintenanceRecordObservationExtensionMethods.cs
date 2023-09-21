//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordObservation]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class MaintenanceRecordObservationExtensionMethods
    {
        public static MaintenanceRecordObservationDto AsDto(this MaintenanceRecordObservation maintenanceRecordObservation)
        {
            var maintenanceRecordObservationDto = new MaintenanceRecordObservationDto()
            {
                MaintenanceRecordObservationID = maintenanceRecordObservation.MaintenanceRecordObservationID,
                MaintenanceRecord = maintenanceRecordObservation.MaintenanceRecord.AsDto(),
                TreatmentBMPTypeCustomAttributeType = maintenanceRecordObservation.TreatmentBMPTypeCustomAttributeType.AsDto(),
                TreatmentBMPType = maintenanceRecordObservation.TreatmentBMPType.AsDto(),
                CustomAttributeType = maintenanceRecordObservation.CustomAttributeType.AsDto()
            };
            DoCustomMappings(maintenanceRecordObservation, maintenanceRecordObservationDto);
            return maintenanceRecordObservationDto;
        }

        static partial void DoCustomMappings(MaintenanceRecordObservation maintenanceRecordObservation, MaintenanceRecordObservationDto maintenanceRecordObservationDto);

        public static MaintenanceRecordObservationSimpleDto AsSimpleDto(this MaintenanceRecordObservation maintenanceRecordObservation)
        {
            var maintenanceRecordObservationSimpleDto = new MaintenanceRecordObservationSimpleDto()
            {
                MaintenanceRecordObservationID = maintenanceRecordObservation.MaintenanceRecordObservationID,
                MaintenanceRecordID = maintenanceRecordObservation.MaintenanceRecordID,
                TreatmentBMPTypeCustomAttributeTypeID = maintenanceRecordObservation.TreatmentBMPTypeCustomAttributeTypeID,
                TreatmentBMPTypeID = maintenanceRecordObservation.TreatmentBMPTypeID,
                CustomAttributeTypeID = maintenanceRecordObservation.CustomAttributeTypeID
            };
            DoCustomSimpleDtoMappings(maintenanceRecordObservation, maintenanceRecordObservationSimpleDto);
            return maintenanceRecordObservationSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(MaintenanceRecordObservation maintenanceRecordObservation, MaintenanceRecordObservationSimpleDto maintenanceRecordObservationSimpleDto);
    }
}