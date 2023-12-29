CREATE TABLE [dbo].[LandUseBlock](
	[LandUseBlockID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_LandUseBlock_LandUseBlockID] PRIMARY KEY,
	[PriorityLandUseTypeID] [int] NULL CONSTRAINT [FK_LandUseBlock_PriorityLandUseType_PriorityLandUseTypeID] FOREIGN KEY REFERENCES [dbo].[PriorityLandUseType] ([PriorityLandUseTypeID]),
	[LandUseDescription] [varchar](500) NULL,
	[LandUseBlockGeometry] [geometry] NOT NULL,
	[TrashGenerationRate] [decimal](4, 1) NULL,
	[LandUseForTGR] [varchar](80) NULL,
	[MedianHouseholdIncomeResidential] [numeric](18, 0) NULL,
	[MedianHouseholdIncomeRetail] [numeric](18, 0) NULL,
	[StormwaterJurisdictionID] [int] NOT NULL CONSTRAINT [FK_LandUseBlock_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID]),
	[PermitTypeID] [int] NOT NULL CONSTRAINT [FK_LandUseBlock_PermitType_PermitTypeID] FOREIGN KEY REFERENCES [dbo].[PermitType] ([PermitTypeID]),
	[LandUseBlockGeometry4326] [geometry] NULL,
)
GO

CREATE SPATIAL INDEX [SPATIAL_LandUseBlock_LandUseBlockGeometry4326] ON [dbo].[LandUseBlock]
(
	[LandUseBlockGeometry4326]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE SPATIAL INDEX [SPATIAL_LandUseBlock_LandUseBlockGeometry] ON [dbo].[LandUseBlock]
(
	[LandUseBlockGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(1.83394e+006, 642121, 1.87034e+006, 698755), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)