SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceActivityType](
	[MaintenanceActivityTypeID] [int] NOT NULL,
	[MaintenanceActivityTypeName] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MaintenanceActivityTypeDisplayName] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_MaintenanceActivityType_MaintenanceActivityTypeID] PRIMARY KEY CLUSTERED 
(
	[MaintenanceActivityTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_MaintenanceActivityType_MaintenanceActivityTypeDisplayName] UNIQUE NONCLUSTERED 
(
	[MaintenanceActivityTypeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_MaintenanceActivityType_MaintenanceActivityTypeName] UNIQUE NONCLUSTERED 
(
	[MaintenanceActivityTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
