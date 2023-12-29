CREATE TABLE [dbo].[HRUCharacteristicLandUseCode](
	[HRUCharacteristicLandUseCodeID] [int] NOT NULL CONSTRAINT [PK_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeID] PRIMARY KEY,
	[HRUCharacteristicLandUseCodeName] [varchar](100) CONSTRAINT [AK_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeName] UNIQUE,
	[HRUCharacteristicLandUseCodeDisplayName] [varchar](100) CONSTRAINT [AK_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeDisplayName] UNIQUE
)
