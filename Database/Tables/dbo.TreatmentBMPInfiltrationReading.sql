SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPInfiltrationReading](
	[TreatmentBMPInfiltrationReadingID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPObservationDetailID] [int] NOT NULL,
	[ReadingValue] [float] NOT NULL,
	[ReadingTime] [float] NOT NULL,
 CONSTRAINT [PK_TreatmentBMPInfiltrationReading_TreatmentBMPInfiltrationReadingID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPInfiltrationReadingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPInfiltrationReading_TreatmentBMPInfiltrationReadingID_TenantID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPInfiltrationReadingID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPInfiltrationReading_TreatmentBMPObservationDetailID_ReadingTime] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPObservationDetailID] ASC,
	[ReadingTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPInfiltrationReading]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPInfiltrationReading_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPInfiltrationReading] CHECK CONSTRAINT [FK_TreatmentBMPInfiltrationReading_Tenant_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPInfiltrationReading]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPInfiltrationReading_TreatmentBMPObservationDetail_TreatmentBMPObservationDetailID] FOREIGN KEY([TreatmentBMPObservationDetailID])
REFERENCES [dbo].[TreatmentBMPObservationDetail] ([TreatmentBMPObservationDetailID])
GO
ALTER TABLE [dbo].[TreatmentBMPInfiltrationReading] CHECK CONSTRAINT [FK_TreatmentBMPInfiltrationReading_TreatmentBMPObservationDetail_TreatmentBMPObservationDetailID]
GO
ALTER TABLE [dbo].[TreatmentBMPInfiltrationReading]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPInfiltrationReading_TreatmentBMPObservationDetail_TreatmentBMPObservationDetailID_TenantID] FOREIGN KEY([TreatmentBMPObservationDetailID], [TenantID])
REFERENCES [dbo].[TreatmentBMPObservationDetail] ([TreatmentBMPObservationDetailID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPInfiltrationReading] CHECK CONSTRAINT [FK_TreatmentBMPInfiltrationReading_TreatmentBMPObservationDetail_TreatmentBMPObservationDetailID_TenantID]