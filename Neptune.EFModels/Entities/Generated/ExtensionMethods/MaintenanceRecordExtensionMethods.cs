//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecord]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class MaintenanceRecordExtensionMethods
    {
        public static MaintenanceRecordDto AsDto(this MaintenanceRecord maintenanceRecord)
        {
            var maintenanceRecordDto = new MaintenanceRecordDto()
            {
                MaintenanceRecordID = maintenanceRecord.MaintenanceRecordID,
                TreatmentBMP = maintenanceRecord.TreatmentBMP.AsDto(),
                TreatmentBMPType = maintenanceRecord.TreatmentBMPType.AsDto(),
                FieldVisit = maintenanceRecord.FieldVisit.AsDto(),
                MaintenanceRecordDescription = maintenanceRecord.MaintenanceRecordDescription,
                MaintenanceRecordType = maintenanceRecord.MaintenanceRecordType?.AsDto()
            };
            DoCustomMappings(maintenanceRecord, maintenanceRecordDto);
            return maintenanceRecordDto;
        }

        static partial void DoCustomMappings(MaintenanceRecord maintenanceRecord, MaintenanceRecordDto maintenanceRecordDto);

        public static MaintenanceRecordSimpleDto AsSimpleDto(this MaintenanceRecord maintenanceRecord)
        {
            var maintenanceRecordSimpleDto = new MaintenanceRecordSimpleDto()
            {
                MaintenanceRecordID = maintenanceRecord.MaintenanceRecordID,
                TreatmentBMPID = maintenanceRecord.TreatmentBMPID,
                TreatmentBMPTypeID = maintenanceRecord.TreatmentBMPTypeID,
                FieldVisitID = maintenanceRecord.FieldVisitID,
                MaintenanceRecordDescription = maintenanceRecord.MaintenanceRecordDescription,
                MaintenanceRecordTypeID = maintenanceRecord.MaintenanceRecordTypeID
            };
            DoCustomSimpleDtoMappings(maintenanceRecord, maintenanceRecordSimpleDto);
            return maintenanceRecordSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(MaintenanceRecord maintenanceRecord, MaintenanceRecordSimpleDto maintenanceRecordSimpleDto);
    }
}