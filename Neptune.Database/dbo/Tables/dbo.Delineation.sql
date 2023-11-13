CREATE TABLE [dbo].[Delineation](
	[DelineationID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Delineation_DelineationID] PRIMARY KEY,
	[DelineationGeometry] [geometry] NOT NULL,
	[DelineationTypeID] [int] NOT NULL CONSTRAINT [FK_Delineation_DelineationType_DelineationTypeID] FOREIGN KEY REFERENCES [dbo].[DelineationType] ([DelineationTypeID]),
	[IsVerified] [bit] NOT NULL,
	[DateLastVerified] [datetime] NULL,
	[VerifiedByPersonID] [int] NULL CONSTRAINT [FK_Delineation_Person_VerifiedByPersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[TreatmentBMPID] [int] NOT NULL CONSTRAINT [AK_Delineation_TreatmentBMPID] UNIQUE CONSTRAINT [FK_Delineation_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[DateLastModified] [datetime] NOT NULL,
	[DelineationGeometry4326] [geometry] NULL,
	[HasDiscrepancies] [bit] NOT NULL,
)
GO

CREATE SPATIAL INDEX [SPATIAL_Delineation_DelineationGeometry4326] ON [dbo].[Delineation]
(
	[DelineationGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE SPATIAL INDEX [SPATIAL_Delineation_DelineationGeometry] ON [dbo].[Delineation]
(
	[DelineationGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(1.82886e+006, 638268, 1.88829e+006, 698744), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
