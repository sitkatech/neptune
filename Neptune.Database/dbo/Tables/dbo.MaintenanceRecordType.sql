CREATE TABLE [dbo].[MaintenanceRecordType](
	[MaintenanceRecordTypeID] [int] NOT NULL CONSTRAINT [PK_MaintenanceRecordType_MaintenanceRecordTypeID] PRIMARY KEY,
	[MaintenanceRecordTypeName] [varchar](100) CONSTRAINT [AK_MaintenanceRecordType_MaintenanceRecordTypeName] UNIQUE,
	[MaintenanceRecordTypeDisplayName] [varchar](100) CONSTRAINT [AK_MaintenanceRecordType_MaintenanceRecordTypeDisplayName] UNIQUE
)
