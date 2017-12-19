SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPObservationDetail](
	[TreatmentBMPObservationDetailID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPObservationID] [int] NOT NULL,
	[TreatmentBMPObservationDetailTypeID] [int] NOT NULL,
	[TreatmentBMPObservationValue] [float] NOT NULL,
	[Notes] [varchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_TreatmentBMPObservationDetail_TreatmentBMPObservationDetailID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPObservationDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPObservationDetail_TreatmentBMPObservationDetailID_TenantID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPObservationDetailID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPObservationDetail]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPObservationDetail_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPObservationDetail] CHECK CONSTRAINT [FK_TreatmentBMPObservationDetail_Tenant_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPObservationDetail]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPObservationDetail_TreatmentBMPObservation_TreatmentBMPObservationID] FOREIGN KEY([TreatmentBMPObservationID])
REFERENCES [dbo].[TreatmentBMPObservation] ([TreatmentBMPObservationID])
GO
ALTER TABLE [dbo].[TreatmentBMPObservationDetail] CHECK CONSTRAINT [FK_TreatmentBMPObservationDetail_TreatmentBMPObservation_TreatmentBMPObservationID]
GO
ALTER TABLE [dbo].[TreatmentBMPObservationDetail]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPObservationDetail_TreatmentBMPObservation_TreatmentBMPObservationID_TenantID] FOREIGN KEY([TreatmentBMPObservationID], [TenantID])
REFERENCES [dbo].[TreatmentBMPObservation] ([TreatmentBMPObservationID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPObservationDetail] CHECK CONSTRAINT [FK_TreatmentBMPObservationDetail_TreatmentBMPObservation_TreatmentBMPObservationID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPObservationDetail]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPObservationDetail_TreatmentBMPObservationDetailType_TreatmentBMPObservationDetailTypeID] FOREIGN KEY([TreatmentBMPObservationDetailTypeID])
REFERENCES [dbo].[TreatmentBMPObservationDetailType] ([TreatmentBMPObservationDetailTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPObservationDetail] CHECK CONSTRAINT [FK_TreatmentBMPObservationDetail_TreatmentBMPObservationDetailType_TreatmentBMPObservationDetailTypeID]