SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StormwaterJurisdiction](
	[StormwaterJurisdictionID] [int] NOT NULL,
	[TenantID] [int] NOT NULL,
	[OrganizationID] [int] NOT NULL,
	[StormwaterJurisdictionGeometry] [geometry] NULL,
	[RoadNetworkGeometry] [geometry] NULL,
	[RoadAreaOfInterestGeometry] [geometry] NULL,
	[StateProvinceID] [int] NULL,
	[IsTransportationJurisdiction] [bit] NOT NULL,
 CONSTRAINT [PK_StormwaterJurisdiction_StormwaterJurisdictionID] PRIMARY KEY CLUSTERED 
(
	[StormwaterJurisdictionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_StormwaterJurisdiction_OrganizationID] UNIQUE NONCLUSTERED 
(
	[OrganizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_StormwaterJurisdiction_OrganizationID_TenantID] UNIQUE NONCLUSTERED 
(
	[OrganizationID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_StormwaterJurisdiction_StormwaterJurisdictionID_TenantID] UNIQUE NONCLUSTERED 
(
	[StormwaterJurisdictionID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[StormwaterJurisdiction]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdiction_Organization_OrganizationID] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organization] ([OrganizationID])
GO
ALTER TABLE [dbo].[StormwaterJurisdiction] CHECK CONSTRAINT [FK_StormwaterJurisdiction_Organization_OrganizationID]
GO
ALTER TABLE [dbo].[StormwaterJurisdiction]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdiction_Organization_OrganizationID_TenantID] FOREIGN KEY([OrganizationID], [TenantID])
REFERENCES [dbo].[Organization] ([OrganizationID], [TenantID])
GO
ALTER TABLE [dbo].[StormwaterJurisdiction] CHECK CONSTRAINT [FK_StormwaterJurisdiction_Organization_OrganizationID_TenantID]
GO
ALTER TABLE [dbo].[StormwaterJurisdiction]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdiction_StateProvince_StateProvinceID] FOREIGN KEY([StateProvinceID])
REFERENCES [dbo].[StateProvince] ([StateProvinceID])
GO
ALTER TABLE [dbo].[StormwaterJurisdiction] CHECK CONSTRAINT [FK_StormwaterJurisdiction_StateProvince_StateProvinceID]
GO
ALTER TABLE [dbo].[StormwaterJurisdiction]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdiction_StateProvince_StateProvinceID_TenantID] FOREIGN KEY([StateProvinceID], [TenantID])
REFERENCES [dbo].[StateProvince] ([StateProvinceID], [TenantID])
GO
ALTER TABLE [dbo].[StormwaterJurisdiction] CHECK CONSTRAINT [FK_StormwaterJurisdiction_StateProvince_StateProvinceID_TenantID]
GO
ALTER TABLE [dbo].[StormwaterJurisdiction]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdiction_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[StormwaterJurisdiction] CHECK CONSTRAINT [FK_StormwaterJurisdiction_Tenant_TenantID]