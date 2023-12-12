//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecord]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class MaintenanceRecordExtensionMethods
    {

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