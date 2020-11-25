Alter Table dbo.HRUCharacteristic
Add BaselineImperviousAcres float null,
	BaselineHRUCharacteristicLandUseCodeID int null
		constraint FK_HRUCharacteristic_HRUCharacteristicLandUseCodeID
			foreign key references dbo.HRUCharacteristicLandUseCode(HRUCharacteristicLandUseCodeID)
