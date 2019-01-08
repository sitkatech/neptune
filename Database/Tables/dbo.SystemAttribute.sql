SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemAttribute](
	[SystemAttributeID] [int] IDENTITY(1,1) NOT NULL,
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
	[MapServiceUrl] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ParcelLayerName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_SystemAttribute_SystemAttributeID] PRIMARY KEY CLUSTERED 
(
	[SystemAttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_SystemAttribute_TenantDisplayName] UNIQUE NONCLUSTERED 
(
	[TenantDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[SystemAttribute]  WITH CHECK ADD  CONSTRAINT [FK_SystemAttribute_FileResource_TenantBannerLogoFileResourceID_FileResourceID] FOREIGN KEY([TenantBannerLogoFileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[SystemAttribute] CHECK CONSTRAINT [FK_SystemAttribute_FileResource_TenantBannerLogoFileResourceID_FileResourceID]
GO
ALTER TABLE [dbo].[SystemAttribute]  WITH CHECK ADD  CONSTRAINT [FK_SystemAttribute_FileResource_TenantSquareLogoFileResourceID_FileResourceID] FOREIGN KEY([TenantSquareLogoFileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[SystemAttribute] CHECK CONSTRAINT [FK_SystemAttribute_FileResource_TenantSquareLogoFileResourceID_FileResourceID]
GO
ALTER TABLE [dbo].[SystemAttribute]  WITH CHECK ADD  CONSTRAINT [FK_SystemAttribute_FileResource_TenantStyleSheetFileResourceID_FileResourceID] FOREIGN KEY([TenantStyleSheetFileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[SystemAttribute] CHECK CONSTRAINT [FK_SystemAttribute_FileResource_TenantStyleSheetFileResourceID_FileResourceID]
GO
ALTER TABLE [dbo].[SystemAttribute]  WITH CHECK ADD  CONSTRAINT [FK_SystemAttribute_Person_PrimaryContactPersonID_PersonID] FOREIGN KEY([PrimaryContactPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[SystemAttribute] CHECK CONSTRAINT [FK_SystemAttribute_Person_PrimaryContactPersonID_PersonID]