CREATE TABLE [dbo].[DelineationOverlap](
	[DelineationOverlapID] [int] NOT NULL CONSTRAINT [PK_DelineationOverlap_DelineationOverlapID] PRIMARY KEY,
	[DelineationID] [int] NOT NULL CONSTRAINT [FK_DelineationOverlap_Delineation_DelineationID] FOREIGN KEY REFERENCES [dbo].[Delineation] ([DelineationID]),
	[OverlappingDelineationID] [int] NOT NULL CONSTRAINT [FK_DelineationOverlap_Delineation_OverlappingDelineationID_DelineationID] FOREIGN KEY REFERENCES [dbo].[Delineation] ([DelineationID]),
	[OverlappingGeometry] [geometry] NOT NULL
)
GO

create spatial index [SPATIAL_DelineationOverlap_OverlappingGeometry] on [dbo].[DelineationOverlap]
(
	[OverlappingGeometry]
)
with (BOUNDING_BOX=(1.83342e+006, 644621, 1.87338e+006, 696443))