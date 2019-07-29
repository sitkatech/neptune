SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrashGeneratingUnit](
	[TrashGeneratingUnitID] [int] IDENTITY(1,1) NOT NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[OnlandVisualTrashAssessmentAreaID] [int] NULL,
	[LandUseBlockID] [int] NULL,
	[TrashGeneratingUnitGeometry] [geometry] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
	[DelineationID] [int] NULL,
 CONSTRAINT [PK_TrashGeneratingUnit_TrashGeneratingUnitID] PRIMARY KEY CLUSTERED 
(
	[TrashGeneratingUnitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[TrashGeneratingUnit] ADD  CONSTRAINT [DF_LastUpdateDate]  DEFAULT (getdate()) FOR [LastUpdateDate]
GO
ALTER TABLE [dbo].[TrashGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit_Delineation_DelineationID] FOREIGN KEY([DelineationID])
REFERENCES [dbo].[Delineation] ([DelineationID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnit] CHECK CONSTRAINT [FK_TrashGeneratingUnit_Delineation_DelineationID]
GO
ALTER TABLE [dbo].[TrashGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit_LandUseBlock_LandUseBlockID] FOREIGN KEY([LandUseBlockID])
REFERENCES [dbo].[LandUseBlock] ([LandUseBlockID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnit] CHECK CONSTRAINT [FK_TrashGeneratingUnit_LandUseBlock_LandUseBlockID]
GO
ALTER TABLE [dbo].[TrashGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnit] CHECK CONSTRAINT [FK_TrashGeneratingUnit_StormwaterJurisdiction_StormwaterJurisdictionID]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
CREATE SPATIAL INDEX [SPATIAL_TrashGeneratingUnit_TrashGeneratingUnitGeometry] ON [dbo].[TrashGeneratingUnit]
(
	[TrashGeneratingUnitGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]