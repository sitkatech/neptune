SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPAssessmentPhoto](
	[TreatmentBMPAssessmentPhotoID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[FileResourceID] [int] NOT NULL,
	[TreatmentBMPAssessmentID] [int] NOT NULL,
	[Caption] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_TreatmentBMPAssessmentPhoto_TreatmentBMPAssessmentPhotoID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPAssessmentPhotoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentPhoto]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentPhoto] CHECK CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_FileResource_FileResourceID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentPhoto]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_FileResource_FileResourceID_TenantID] FOREIGN KEY([FileResourceID], [TenantID])
REFERENCES [dbo].[FileResource] ([FileResourceID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentPhoto] CHECK CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_FileResource_FileResourceID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentPhoto]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentPhoto] CHECK CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_Tenant_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentPhoto]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_TreatmentBMPAssessment_TreatmentBMPAssessmentID] FOREIGN KEY([TreatmentBMPAssessmentID])
REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentPhoto] CHECK CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_TreatmentBMPAssessment_TreatmentBMPAssessmentID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentPhoto]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TenantID] FOREIGN KEY([TreatmentBMPAssessmentID], [TenantID])
REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentPhoto] CHECK CONSTRAINT [FK_TreatmentBMPAssessmentPhoto_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TenantID]