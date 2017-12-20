SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StormwaterBreadCrumbEntity](
	[StormwaterBreadCrumbEntityID] [int] NOT NULL,
	[TenantID] [int] NOT NULL,
	[StormwaterBreadCrumbEntityName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StormwaterBreadCrumbEntityDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GlyphIconClass] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ColorClass] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_StormwaterBreadCrumbEntity_StormwaterBreadCrumbEntityID] PRIMARY KEY CLUSTERED 
(
	[StormwaterBreadCrumbEntityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_StormwaterBreadCrumbEntity_StormwaterBreadCrumbEntityID_TenantID] UNIQUE NONCLUSTERED 
(
	[StormwaterBreadCrumbEntityID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[StormwaterBreadCrumbEntity]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterBreadCrumbEntity_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[StormwaterBreadCrumbEntity] CHECK CONSTRAINT [FK_StormwaterBreadCrumbEntity_Tenant_TenantID]