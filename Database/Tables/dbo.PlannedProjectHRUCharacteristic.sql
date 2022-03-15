SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlannedProjectHRUCharacteristic](
	[PlannedProjectHRUCharacteristicID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[HydrologicSoilGroup] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SlopePercentage] [int] NOT NULL,
	[ImperviousAcres] [float] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[Area] [float] NOT NULL,
	[HRUCharacteristicLandUseCodeID] [int] NOT NULL,
	[PlannedProjectLoadGeneratingUnitID] [int] NOT NULL,
	[BaselineImperviousAcres] [float] NOT NULL,
	[BaselineHRUCharacteristicLandUseCodeID] [int] NOT NULL,
 CONSTRAINT [PK_PlannedProjectHRUCharacteristic_PlannedProjectHRUCharacteristicID] PRIMARY KEY CLUSTERED 
(
	[PlannedProjectHRUCharacteristicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PlannedProjectHRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [FK_PlannedProjectHRUCharacteristic_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeID] FOREIGN KEY([HRUCharacteristicLandUseCodeID])
REFERENCES [dbo].[HRUCharacteristicLandUseCode] ([HRUCharacteristicLandUseCodeID])
GO
ALTER TABLE [dbo].[PlannedProjectHRUCharacteristic] CHECK CONSTRAINT [FK_PlannedProjectHRUCharacteristic_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeID]
GO
ALTER TABLE [dbo].[PlannedProjectHRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [FK_PlannedProjectHRUCharacteristic_HRUCharacteristicLandUseCodeID] FOREIGN KEY([BaselineHRUCharacteristicLandUseCodeID])
REFERENCES [dbo].[HRUCharacteristicLandUseCode] ([HRUCharacteristicLandUseCodeID])
GO
ALTER TABLE [dbo].[PlannedProjectHRUCharacteristic] CHECK CONSTRAINT [FK_PlannedProjectHRUCharacteristic_HRUCharacteristicLandUseCodeID]
GO
ALTER TABLE [dbo].[PlannedProjectHRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [FK_PlannedProjectHRUCharacteristic_PlannedProjectLoadGeneratingUnit_PlannedProjectLoadGeneratingUnitID] FOREIGN KEY([PlannedProjectLoadGeneratingUnitID])
REFERENCES [dbo].[PlannedProjectLoadGeneratingUnit] ([PlannedProjectLoadGeneratingUnitID])
GO
ALTER TABLE [dbo].[PlannedProjectHRUCharacteristic] CHECK CONSTRAINT [FK_PlannedProjectHRUCharacteristic_PlannedProjectLoadGeneratingUnit_PlannedProjectLoadGeneratingUnitID]
GO
ALTER TABLE [dbo].[PlannedProjectHRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [FK_PlannedProjectHRUCharacteristic_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[PlannedProjectHRUCharacteristic] CHECK CONSTRAINT [FK_PlannedProjectHRUCharacteristic_Project_ProjectID]
GO
ALTER TABLE [dbo].[PlannedProjectHRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [CK_PlannedProjectHRUCharacteristic_SlopePercentageIsAPercentage] CHECK  (([SlopePercentage]>=(0) AND [SlopePercentage]<=(100)))
GO
ALTER TABLE [dbo].[PlannedProjectHRUCharacteristic] CHECK CONSTRAINT [CK_PlannedProjectHRUCharacteristic_SlopePercentageIsAPercentage]