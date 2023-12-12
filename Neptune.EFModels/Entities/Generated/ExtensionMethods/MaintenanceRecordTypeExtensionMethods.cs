//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class MaintenanceRecordTypeExtensionMethods
    {

        public static MaintenanceRecordTypeSimpleDto AsSimpleDto(this MaintenanceRecordType maintenanceRecordType)
        {
            var maintenanceRecordTypeSimpleDto = new MaintenanceRecordTypeSimpleDto()
            {
                MaintenanceRecordTypeID = maintenanceRecordType.MaintenanceRecordTypeID,
                MaintenanceRecordTypeName = maintenanceRecordType.MaintenanceRecordTypeName,
                MaintenanceRecordTypeDisplayName = maintenanceRecordType.MaintenanceRecordTypeDisplayName
            };
            DoCustomSimpleDtoMappings(maintenanceRecordType, maintenanceRecordTypeSimpleDto);
            return maintenanceRecordTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(MaintenanceRecordType maintenanceRecordType, MaintenanceRecordTypeSimpleDto maintenanceRecordTypeSimpleDto);
    }
}