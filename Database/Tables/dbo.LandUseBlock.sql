SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LandUseBlock](
	[LandUseBlockID] [int] IDENTITY(1,1) NOT NULL,
	[PriorityLandUseTypeID] [int] NULL,
	[LandUseDescription] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LandUseBlockGeometry] [geometry] NOT NULL,
	[TrashGenerationRate] [decimal](4, 1) NULL,
	[LandUseForTGR] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MedianHouseholdIncomeResidential] [numeric](18, 0) NULL,
	[MedianHouseholdIncomeRetail] [numeric](18, 0) NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[PermitTypeID] [int] NOT NULL,
	[LandUseBlock4326] [geometry] NULL,
 CONSTRAINT [PK_LandUseBlock_LandUseBlockID] PRIMARY KEY CLUSTERED 
(
	[LandUseBlockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[LandUseBlock]  WITH CHECK ADD  CONSTRAINT [FK_LandUseBlock_PermitType_PermitTypeID] FOREIGN KEY([PermitTypeID])
REFERENCES [dbo].[PermitType] ([PermitTypeID])
GO
ALTER TABLE [dbo].[LandUseBlock] CHECK CONSTRAINT [FK_LandUseBlock_PermitType_PermitTypeID]
GO
ALTER TABLE [dbo].[LandUseBlock]  WITH CHECK ADD  CONSTRAINT [FK_LandUseBlock_PriorityLandUseType_PriorityLandUseTypeID] FOREIGN KEY([PriorityLandUseTypeID])
REFERENCES [dbo].[PriorityLandUseType] ([PriorityLandUseTypeID])
GO
ALTER TABLE [dbo].[LandUseBlock] CHECK CONSTRAINT [FK_LandUseBlock_PriorityLandUseType_PriorityLandUseTypeID]
GO
ALTER TABLE [dbo].[LandUseBlock]  WITH CHECK ADD  CONSTRAINT [FK_LandUseBlock_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[LandUseBlock] CHECK CONSTRAINT [FK_LandUseBlock_StormwaterJurisdiction_StormwaterJurisdictionID]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
CREATE SPATIAL INDEX [SPATIAL_LandUseBlock_LandUseBlockGeometry] ON [dbo].[LandUseBlock]
(
	[LandUseBlockGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]