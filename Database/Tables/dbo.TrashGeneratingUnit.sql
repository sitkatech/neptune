SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrashGeneratingUnit](
	[TrashGeneratingUnitID] [int] IDENTITY(1,1) NOT NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[TreatmentBMPID] [int] NULL,
	[OnlandVisualTrashAssessmentAreaID] [int] NULL,
	[LandUseBlockID] [int] NULL,
	[TrashGeneratingUnitGeometry] [geometry] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TrashGeneratingUnit_TrashGeneratingUnitID] PRIMARY KEY CLUSTERED 
(
	[TrashGeneratingUnitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[TrashGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit_LandUseBlock_LandUseBlockID] FOREIGN KEY([LandUseBlockID])
REFERENCES [dbo].[LandUseBlock] ([LandUseBlockID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnit] CHECK CONSTRAINT [FK_TrashGeneratingUnit_LandUseBlock_LandUseBlockID]
GO
ALTER TABLE [dbo].[TrashGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID] FOREIGN KEY([OnlandVisualTrashAssessmentAreaID])
REFERENCES [dbo].[OnlandVisualTrashAssessmentArea] ([OnlandVisualTrashAssessmentAreaID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnit] CHECK CONSTRAINT [FK_TrashGeneratingUnit_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID]
GO
ALTER TABLE [dbo].[TrashGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnit] CHECK CONSTRAINT [FK_TrashGeneratingUnit_StormwaterJurisdiction_StormwaterJurisdictionID]
GO
ALTER TABLE [dbo].[TrashGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnit] CHECK CONSTRAINT [FK_TrashGeneratingUnit_TreatmentBMP_TreatmentBMPID]