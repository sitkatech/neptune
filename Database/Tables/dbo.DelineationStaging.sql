SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DelineationStaging](
	[DelineationStagingID] [int] IDENTITY(1,1) NOT NULL,
	[DelineationStagingGeometry] [geometry] NOT NULL,
	[UploadedByPersonID] [int] NOT NULL,
	[TreatmentBMPName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
 CONSTRAINT [PK_DelineationStaging_DelineationStagingID] PRIMARY KEY CLUSTERED 
(
	[DelineationStagingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_DelineationStaging_TreatmentBMPName_StormwaterJurisdictionID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPName] ASC,
	[StormwaterJurisdictionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[DelineationStaging]  WITH CHECK ADD  CONSTRAINT [FK_DelineationStaging_Person_UploadedByPersonID_PersonID] FOREIGN KEY([UploadedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[DelineationStaging] CHECK CONSTRAINT [FK_DelineationStaging_Person_UploadedByPersonID_PersonID]