CREATE TABLE [dbo].[DelineationStaging](
	[DelineationStagingID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_DelineationStaging_DelineationStagingID] PRIMARY KEY,
	[Geometry] [geometry] NOT NULL,
	[UploadedByPersonID] [int] NOT NULL CONSTRAINT [FK_DelineationStaging_Person_UploadedByPersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[TreatmentBMPName] [varchar](200),
	[StormwaterJurisdictionID] [int] NOT NULL CONSTRAINT [FK_DelineationStaging_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID]),
	[DelineationStatus] [varchar](20) NULL,
	CONSTRAINT [AK_DelineationStaging_TreatmentBMPName_StormwaterJurisdictionID] UNIQUE (
		[TreatmentBMPName] ASC,
		[StormwaterJurisdictionID] ASC,
		[UploadedByPersonID] ASC
	)
)
GO

create spatial index [SPATIAL_DelineationStaging_Geometry] on [dbo].[DelineationStaging]
(
	[Geometry]
)
with (BOUNDING_BOX=(1.83062e+006, 652938, 1.87529e+006, 689351))