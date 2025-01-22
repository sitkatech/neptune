CREATE TABLE [dbo].[OnlandVisualTrashAssessmentAreaStaging](
	[OnlandVisualTrashAssessmentAreaStagingID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_OnlandVisualTrashAssessmentAreaStaging_OnlandVisualTrashAssessmentAreaStagingID] PRIMARY KEY,
	[AreaName] [varchar](100) NOT NULL,
	[StormwaterJurisdictionID] [int] NOT NULL CONSTRAINT [FK_OnlandVisualTrashAssessmentAreaStaging_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID]),
	[Geometry] [geometry] NOT NULL,
	[Description] [varchar](500) NULL,
	[UploadedByPersonID] [int] NOT NULL CONSTRAINT [FK_OnlandVisualTrashAssessmentAreaStaging_Person_UploadedByPersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	CONSTRAINT [AK_OnlandVisualTrashAssessmentAreaStaging_OnlandVisualTrashAssessmentAreaStagingName_StormwaterJurisdictionID] UNIQUE([AreaName], [StormwaterJurisdictionID])
)
GO
