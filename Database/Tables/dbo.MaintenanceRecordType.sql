SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceRecordType](
	[MaintenanceRecordTypeID] [int] NOT NULL,
	[MaintenanceRecordTypeName] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MaintenanceRecordTypeDisplayName] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_MaintenanceRecordType_MaintenanceRecordTypeID] PRIMARY KEY CLUSTERED 
(
	[MaintenanceRecordTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_MaintenanceRecordType_MaintenanceRecordTypeDisplayName] UNIQUE NONCLUSTERED 
(
	[MaintenanceRecordTypeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_MaintenanceRecordType_MaintenanceRecordTypeName] UNIQUE NONCLUSTERED 
(
	[MaintenanceRecordTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
