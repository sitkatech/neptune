Alter Table dbo.HRUCharacteristic
Add BaselineImperviousAcres float null,
	BaselineHRUCharacteristicLandUseCodeID int null
		constraint FK_HRUCharacteristic_HRUCharacteristicLandUseCodeID
			foreign key references dbo.HRUCharacteristicLandUseCode(HRUCharacteristicLandUseCodeID)
go

-- these columns need to be not null, and it's going to be rewritten immediately after release, so any valid default is fine.
Update dbo.HRUCharacteristic
Set BaselineHRUCharacteristicLandUseCodeID = 18,
	BaselineImperviousAcres = 0

Alter table dbo.HRUCharacteristic
Alter Column BaselineHRUCharacteristicLandUseCodeID int not null

Alter table dbo.HRUCharacteristic
Alter Column BaselineImperviousAcres float not null

Alter table dbo.NereidResult
Add IsBaselineCondition bit null
go

-- all existing results will be not for the baseline condition
Update dbo.NereidResult
Set IsBaselineCondition = 0

Alter table dbo.NereidResult
Alter Column IsBaselineCondition bit not null