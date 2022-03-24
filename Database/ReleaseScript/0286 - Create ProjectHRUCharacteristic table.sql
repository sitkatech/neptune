CREATE TABLE [dbo].[ProjectHRUCharacteristic](
	[ProjectHRUCharacteristicID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[HydrologicSoilGroup] [varchar](5) NOT NULL,
	[SlopePercentage] [int] NOT NULL,
	[ImperviousAcres] [float] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[Area] [float] NOT NULL,
	[HRUCharacteristicLandUseCodeID] [int] NOT NULL,
	[ProjectLoadGeneratingUnitID] [int] NOT NULL,
	[BaselineImperviousAcres] [float] NOT NULL,
	[BaselineHRUCharacteristicLandUseCodeID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectHRUCharacteristic_ProjectHRUCharacteristicID] PRIMARY KEY CLUSTERED 
(
	[ProjectHRUCharacteristicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProjectHRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [FK_ProjectHRUCharacteristic_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO

ALTER TABLE [dbo].[ProjectHRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [FK_ProjectHRUCharacteristic_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeID] FOREIGN KEY([HRUCharacteristicLandUseCodeID])
REFERENCES [dbo].[HRUCharacteristicLandUseCode] ([HRUCharacteristicLandUseCodeID])
GO

ALTER TABLE [dbo].[ProjectHRUCharacteristic] CHECK CONSTRAINT [FK_ProjectHRUCharacteristic_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeID]
GO

ALTER TABLE [dbo].[ProjectHRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [FK_ProjectHRUCharacteristic_HRUCharacteristicLandUseCodeID] FOREIGN KEY([BaselineHRUCharacteristicLandUseCodeID])
REFERENCES [dbo].[HRUCharacteristicLandUseCode] ([HRUCharacteristicLandUseCodeID])
GO

ALTER TABLE [dbo].[ProjectHRUCharacteristic] CHECK CONSTRAINT [FK_ProjectHRUCharacteristic_HRUCharacteristicLandUseCodeID]
GO

ALTER TABLE [dbo].[ProjectHRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [FK_ProjectHRUCharacteristic_ProjectLoadGeneratingUnit_ProjectLoadGeneratingUnitID] FOREIGN KEY([ProjectLoadGeneratingUnitID])
REFERENCES [dbo].[ProjectLoadGeneratingUnit] ([ProjectLoadGeneratingUnitID])
GO

ALTER TABLE [dbo].[ProjectHRUCharacteristic] CHECK CONSTRAINT [FK_ProjectHRUCharacteristic_ProjectLoadGeneratingUnit_ProjectLoadGeneratingUnitID]
GO

ALTER TABLE [dbo].[ProjectHRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [CK_ProjectHRUCharacteristic_SlopePercentageIsAPercentage] CHECK  (([SlopePercentage]>=(0) AND [SlopePercentage]<=(100)))
GO

ALTER TABLE [dbo].[ProjectHRUCharacteristic] CHECK CONSTRAINT [CK_ProjectHRUCharacteristic_SlopePercentageIsAPercentage]
GO


