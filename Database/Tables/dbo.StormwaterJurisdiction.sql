SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StormwaterJurisdiction](
	[StormwaterJurisdictionID] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationID] [int] NOT NULL,
	[StormwaterJurisdictionGeometry] [geometry] NULL,
	[StateProvinceID] [int] NOT NULL,
	[IsTransportationJurisdiction] [bit] NOT NULL,
	[StormwaterJurisdictionGeometry4326] [geometry] NULL,
 CONSTRAINT [PK_StormwaterJurisdiction_StormwaterJurisdictionID] PRIMARY KEY CLUSTERED 
(
	[StormwaterJurisdictionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_StormwaterJurisdiction_OrganizationID] UNIQUE NONCLUSTERED 
(
	[OrganizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[StormwaterJurisdiction]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdiction_Organization_OrganizationID] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organization] ([OrganizationID])
GO
ALTER TABLE [dbo].[StormwaterJurisdiction] CHECK CONSTRAINT [FK_StormwaterJurisdiction_Organization_OrganizationID]
GO
ALTER TABLE [dbo].[StormwaterJurisdiction]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdiction_StateProvince_StateProvinceID] FOREIGN KEY([StateProvinceID])
REFERENCES [dbo].[StateProvince] ([StateProvinceID])
GO
ALTER TABLE [dbo].[StormwaterJurisdiction] CHECK CONSTRAINT [FK_StormwaterJurisdiction_StateProvince_StateProvinceID]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
CREATE SPATIAL INDEX [SPATIAL_StormwaterJurisdiction_StormwaterJurisdictionGeometry] ON [dbo].[StormwaterJurisdiction]
(
	[StormwaterJurisdictionGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]