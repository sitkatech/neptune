//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecord]
namespace Neptune.EFModels.Entities
{
    public partial class MaintenanceRecord : IHavePrimaryKey
    {
        public int PrimaryKey => MaintenanceRecordID;
        public MaintenanceRecordType? MaintenanceRecordType => MaintenanceRecordTypeID.HasValue ? MaintenanceRecordType.AllLookupDictionary[MaintenanceRecordTypeID.Value] : null;

        public static class FieldLengths
        {
            public const int MaintenanceRecordDescription = 500;
        }
    }
}