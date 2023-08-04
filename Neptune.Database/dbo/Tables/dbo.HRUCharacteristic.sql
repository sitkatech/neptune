CREATE TABLE [dbo].[HRUCharacteristic](
	[HRUCharacteristicID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_HRUCharacteristic_HRUCharacteristicID] PRIMARY KEY,
	[HydrologicSoilGroup] [varchar](5),
	[SlopePercentage] [int] NOT NULL,
	[ImperviousAcres] [float] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[Area] [float] NOT NULL,
	[HRUCharacteristicLandUseCodeID] [int] NOT NULL CONSTRAINT [FK_HRUCharacteristic_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeID] FOREIGN KEY REFERENCES [dbo].[HRUCharacteristicLandUseCode] ([HRUCharacteristicLandUseCodeID]),
	[LoadGeneratingUnitID] [int] NOT NULL CONSTRAINT [FK_HRUCharacteristic_LoadGeneratingUnit_LoadGeneratingUnitID] FOREIGN KEY REFERENCES [dbo].[LoadGeneratingUnit] ([LoadGeneratingUnitID]),
	[BaselineImperviousAcres] [float] NOT NULL,
	[BaselineHRUCharacteristicLandUseCodeID] [int] NOT NULL CONSTRAINT [FK_HRUCharacteristic_HRUCharacteristicLandUseCode_BaselineHRUCharacteristicLandUseCodeID_HRUCharacteristicLandUseCodeID] FOREIGN KEY REFERENCES [dbo].[HRUCharacteristicLandUseCode] ([HRUCharacteristicLandUseCodeID]),
	CONSTRAINT [CK_HRUCharacteristic_SlopePercentageIsAPercentage] CHECK  (([SlopePercentage]>=(0) AND [SlopePercentage]<=(100)))
)