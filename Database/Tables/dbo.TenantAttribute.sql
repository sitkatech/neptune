SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TenantAttribute](
	[TenantAttributeID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[DefaultBoundingBox] [geometry] NOT NULL,
	[MinimumYear] [int] NOT NULL,
	[PrimaryContactPersonID] [int] NULL,
	[TenantSquareLogoFileResourceID] [int] NULL,
	[TenantBannerLogoFileResourceID] [int] NULL,
	[TenantStyleSheetFileResourceID] [int] NULL,
	[TenantDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ToolDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RecaptchaPublicKey] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RecaptchaPrivateKey] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_TenantAttribute_TenantAttributeID] PRIMARY KEY CLUSTERED 
(
	[TenantAttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TenantAttribute_TenantDisplayName] UNIQUE NONCLUSTERED 
(
	[TenantDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TenantAttribute_TenantID] UNIQUE NONCLUSTERED 
(
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[TenantAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TenantAttribute_FileResource_TenantBannerLogoFileResourceID_FileResourceID] FOREIGN KEY([TenantBannerLogoFileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[TenantAttribute] CHECK CONSTRAINT [FK_TenantAttribute_FileResource_TenantBannerLogoFileResourceID_FileResourceID]
GO
ALTER TABLE [dbo].[TenantAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TenantAttribute_FileResource_TenantBannerLogoFileResourceID_TenantID_FileResourceID_TenantID] FOREIGN KEY([TenantBannerLogoFileResourceID], [TenantID])
REFERENCES [dbo].[FileResource] ([FileResourceID], [TenantID])
GO
ALTER TABLE [dbo].[TenantAttribute] CHECK CONSTRAINT [FK_TenantAttribute_FileResource_TenantBannerLogoFileResourceID_TenantID_FileResourceID_TenantID]
GO
ALTER TABLE [dbo].[TenantAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TenantAttribute_FileResource_TenantSquareLogoFileResourceID_FileResourceID] FOREIGN KEY([TenantSquareLogoFileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[TenantAttribute] CHECK CONSTRAINT [FK_TenantAttribute_FileResource_TenantSquareLogoFileResourceID_FileResourceID]
GO
ALTER TABLE [dbo].[TenantAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TenantAttribute_FileResource_TenantSquareLogoFileResourceID_TenantID_FileResourceID_TenantID] FOREIGN KEY([TenantSquareLogoFileResourceID], [TenantID])
REFERENCES [dbo].[FileResource] ([FileResourceID], [TenantID])
GO
ALTER TABLE [dbo].[TenantAttribute] CHECK CONSTRAINT [FK_TenantAttribute_FileResource_TenantSquareLogoFileResourceID_TenantID_FileResourceID_TenantID]
GO
ALTER TABLE [dbo].[TenantAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TenantAttribute_FileResource_TenantStyleSheetFileResourceID_FileResourceID] FOREIGN KEY([TenantStyleSheetFileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[TenantAttribute] CHECK CONSTRAINT [FK_TenantAttribute_FileResource_TenantStyleSheetFileResourceID_FileResourceID]
GO
ALTER TABLE [dbo].[TenantAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TenantAttribute_FileResource_TenantStyleSheetFileResourceID_TenantID_FileResourceID_TenantID] FOREIGN KEY([TenantStyleSheetFileResourceID], [TenantID])
REFERENCES [dbo].[FileResource] ([FileResourceID], [TenantID])
GO
ALTER TABLE [dbo].[TenantAttribute] CHECK CONSTRAINT [FK_TenantAttribute_FileResource_TenantStyleSheetFileResourceID_TenantID_FileResourceID_TenantID]
GO
ALTER TABLE [dbo].[TenantAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TenantAttribute_Person_PrimaryContactPersonID_PersonID] FOREIGN KEY([PrimaryContactPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[TenantAttribute] CHECK CONSTRAINT [FK_TenantAttribute_Person_PrimaryContactPersonID_PersonID]
GO
ALTER TABLE [dbo].[TenantAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TenantAttribute_Person_PrimaryContactPersonID_TenantID_PersonID_TenantID] FOREIGN KEY([PrimaryContactPersonID], [TenantID])
REFERENCES [dbo].[Person] ([PersonID], [TenantID])
GO
ALTER TABLE [dbo].[TenantAttribute] CHECK CONSTRAINT [FK_TenantAttribute_Person_PrimaryContactPersonID_TenantID_PersonID_TenantID]
GO
ALTER TABLE [dbo].[TenantAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TenantAttribute_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TenantAttribute] CHECK CONSTRAINT [FK_TenantAttribute_Tenant_TenantID]