SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPImage](
	[TreatmentBMPImageID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[FileResourceID] [int] NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[Caption] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UploadDate] [date] NOT NULL,
 CONSTRAINT [PK_TreatmentBMPImage_TreatmentBMPImageID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPImage_FileResourceID_TreatmentBMPID] UNIQUE NONCLUSTERED 
(
	[FileResourceID] ASC,
	[TreatmentBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPImage]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPImage_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[TreatmentBMPImage] CHECK CONSTRAINT [FK_TreatmentBMPImage_FileResource_FileResourceID]
GO
ALTER TABLE [dbo].[TreatmentBMPImage]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPImage_FileResource_FileResourceID_TenantID] FOREIGN KEY([FileResourceID], [TenantID])
REFERENCES [dbo].[FileResource] ([FileResourceID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPImage] CHECK CONSTRAINT [FK_TreatmentBMPImage_FileResource_FileResourceID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPImage]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPImage_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPImage] CHECK CONSTRAINT [FK_TreatmentBMPImage_Tenant_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPImage]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPImage_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[TreatmentBMPImage] CHECK CONSTRAINT [FK_TreatmentBMPImage_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[TreatmentBMPImage]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPImage_TreatmentBMP_TreatmentBMPID_TenantID] FOREIGN KEY([TreatmentBMPID], [TenantID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPImage] CHECK CONSTRAINT [FK_TreatmentBMPImage_TreatmentBMP_TreatmentBMPID_TenantID]