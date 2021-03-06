Alter table dbo.Delineation
Add DelineationGeometry4326 Geometry null

Alter table dbo.StormwaterJurisdiction
Add StormwaterJurisdictionGeometry4326 Geometry null

Alter table dbo.TreatmentBMP
Add LocationPoint4326 Geometry null

Alter Table dbo.OnlandVisualTrashAssessmentArea
Add OnlandVisualTrashAssessmentAreaGeometry4326 geometry null

Alter table dbo.LandUseBlock
Add LandUseBlockGeometry4326 geometry null

Alter Table dbo.Parcel
Add ParcelGeometry4326 geometry null

Alter table dbo.BackboneSegment
Add BackboneSegmentGeometry4326 geometry null

Alter table dbo.OnlandVisualTrashAssessmentObservation
Add LocationPoint4326 geometry null

Alter table dbo.OnlandVisualTrashAssessmentArea
Add TransectLine4326 geometry null

Alter Table dbo.Watershed
Add WatershedGeometry4326 geometry null

Alter Table dbo.NetworkCatchment
Add CatchmentGeometry4326 geometry null

CREATE TABLE [dbo].[TrashGeneratingUnit4326](
	[TrashGeneratingUnit4326ID] [int] IDENTITY(1,1) NOT NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[OnlandVisualTrashAssessmentAreaID] [int] NULL,
	[LandUseBlockID] [int] NULL,
	[TrashGeneratingUnit4326Geometry] [geometry] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
	[DelineationID] [int] NULL,
	[WaterQualityManagementPlanID] [int] NULL,
 CONSTRAINT [PK_TrashGeneratingUnit4326_TrashGeneratingUnit4326ID] PRIMARY KEY CLUSTERED 
(
	[TrashGeneratingUnit4326ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[TrashGeneratingUnit4326] ADD  CONSTRAINT [DF_TrashGeneratingUnit_4326_LastUpdateDate]  DEFAULT (getdate()) FOR [LastUpdateDate]
GO

ALTER TABLE [dbo].[TrashGeneratingUnit4326]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit4326_Delineation_DelineationID] FOREIGN KEY([DelineationID])
REFERENCES [dbo].[Delineation] ([DelineationID])
GO

ALTER TABLE [dbo].[TrashGeneratingUnit4326] CHECK CONSTRAINT [FK_TrashGeneratingUnit4326_Delineation_DelineationID]
GO

ALTER TABLE [dbo].[TrashGeneratingUnit4326]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit4326_LandUseBlock_LandUseBlockID] FOREIGN KEY([LandUseBlockID])
REFERENCES [dbo].[LandUseBlock] ([LandUseBlockID])
GO

ALTER TABLE [dbo].[TrashGeneratingUnit4326] CHECK CONSTRAINT [FK_TrashGeneratingUnit4326_LandUseBlock_LandUseBlockID]
GO

ALTER TABLE [dbo].[TrashGeneratingUnit4326]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit4326_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO

ALTER TABLE [dbo].[TrashGeneratingUnit4326] CHECK CONSTRAINT [FK_TrashGeneratingUnit4326_StormwaterJurisdiction_StormwaterJurisdictionID]
GO

delete from geometry_columns where f_table_name not like '%Staging'

insert into geometry_columns values
('Neptune', 'dbo', 'vGeoServerTrashGeneratingUnit', 'TrashGeneratingUnitGeometry', 2, 4326, 'MULTIPOLYGON'),
('Neptune', 'dbo', 'TrashGeneratingUnit4326', 'TrashGeneratingUnit4326Geometry', 2, 4326, 'MULTIPOLYGON')