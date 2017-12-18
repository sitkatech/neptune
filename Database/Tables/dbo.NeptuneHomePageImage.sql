SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NeptuneHomePageImage](
	[NeptuneHomePageImageID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[FileResourceID] [int] NOT NULL,
	[Caption] [varchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [PK_NeptuneHomePageImage_NeptuneHomePageImageID] PRIMARY KEY CLUSTERED 
(
	[NeptuneHomePageImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[NeptuneHomePageImage]  WITH CHECK ADD  CONSTRAINT [FK_NeptuneHomePageImage_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[NeptuneHomePageImage] CHECK CONSTRAINT [FK_NeptuneHomePageImage_FileResource_FileResourceID]
GO
ALTER TABLE [dbo].[NeptuneHomePageImage]  WITH CHECK ADD  CONSTRAINT [FK_NeptuneHomePageImage_FileResource_FileResourceID_TenantID] FOREIGN KEY([FileResourceID], [TenantID])
REFERENCES [dbo].[FileResource] ([FileResourceID], [TenantID])
GO
ALTER TABLE [dbo].[NeptuneHomePageImage] CHECK CONSTRAINT [FK_NeptuneHomePageImage_FileResource_FileResourceID_TenantID]
GO
ALTER TABLE [dbo].[NeptuneHomePageImage]  WITH CHECK ADD  CONSTRAINT [FK_NeptuneHomePageImage_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[NeptuneHomePageImage] CHECK CONSTRAINT [FK_NeptuneHomePageImage_Tenant_TenantID]