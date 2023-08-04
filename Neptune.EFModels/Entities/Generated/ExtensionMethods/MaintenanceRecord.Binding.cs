//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecord]
namespace Neptune.EFModels.Entities
{
    public partial class MaintenanceRecord
    {
        public MaintenanceRecordType MaintenanceRecordType => MaintenanceRecordTypeID.HasValue ? MaintenanceRecordType.AllLookupDictionary[MaintenanceRecordTypeID.Value] : null;
    }
}