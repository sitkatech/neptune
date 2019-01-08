SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceRecordObservationValue](
	[MaintenanceRecordObservationValueID] [int] IDENTITY(1,1) NOT NULL,
	[MaintenanceRecordObservationID] [int] NOT NULL,
	[ObservationValue] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_MaintenanceRecordObservationValue_MaintenanceRecordObservationValueID] PRIMARY KEY CLUSTERED 
(
	[MaintenanceRecordObservationValueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MaintenanceRecordObservationValue]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecordObservationValue_MaintenanceRecordObservation_MaintenanceRecordObservationID] FOREIGN KEY([MaintenanceRecordObservationID])
REFERENCES [dbo].[MaintenanceRecordObservation] ([MaintenanceRecordObservationID])
GO
ALTER TABLE [dbo].[MaintenanceRecordObservationValue] CHECK CONSTRAINT [FK_MaintenanceRecordObservationValue_MaintenanceRecordObservation_MaintenanceRecordObservationID]