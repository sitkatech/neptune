CREATE TABLE [dbo].[ProjectHRUCharacteristic](
	[ProjectHRUCharacteristicID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_ProjectHRUCharacteristic_ProjectHRUCharacteristicID] PRIMARY KEY,
	[ProjectID] [int] NOT NULL CONSTRAINT [FK_ProjectHRUCharacteristic_Project_ProjectID] FOREIGN KEY REFERENCES [dbo].[Project] ([ProjectID]),
	[HydrologicSoilGroup] [varchar](5),
	[SlopePercentage] [int] NOT NULL CONSTRAINT [CK_ProjectHRUCharacteristic_SlopePercentageIsAPercentage] CHECK  (([SlopePercentage]>=(0) AND [SlopePercentage]<=(100))),
	[ImperviousAcres] [float] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[Area] [float] NOT NULL,
	[HRUCharacteristicLandUseCodeID] [int] NOT NULL CONSTRAINT [FK_ProjectHRUCharacteristic_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeID] FOREIGN KEY REFERENCES [dbo].[HRUCharacteristicLandUseCode] ([HRUCharacteristicLandUseCodeID]),
	[ProjectLoadGeneratingUnitID] [int] NOT NULL CONSTRAINT [FK_ProjectHRUCharacteristic_ProjectLoadGeneratingUnit_ProjectLoadGeneratingUnitID] FOREIGN KEY REFERENCES [dbo].[ProjectLoadGeneratingUnit] ([ProjectLoadGeneratingUnitID]),
	[BaselineImperviousAcres] [float] NOT NULL,
	[BaselineHRUCharacteristicLandUseCodeID] [int] NOT NULL CONSTRAINT [FK_ProjectHRUCharacteristic_HRUCharacteristicLandUseCodeID] FOREIGN KEY REFERENCES [dbo].[HRUCharacteristicLandUseCode] ([HRUCharacteristicLandUseCodeID]),
)