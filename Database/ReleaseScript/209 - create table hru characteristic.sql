Create Table dbo.HRUCharacteristic(
HRUCharacteristicID int not null identity(1,1) constraint PK_HRUCharacteristic_HRUCharacteristicID primary key,
LSPCLandUseDescription varchar(100) not null,
HydrologicSoilGroup varchar(5) not null,
SlopePercentage int not null,
ImperviousAcres float not null,
TreatmentBMPID int null constraint FK_HRUCharacteristic_TreatmentBMP_TreatmentBMPID foreign key references dbo.TreatmentBMP(TreatmentBMPID),
constraint CK_HRUCharacteristic_SlopePercentageIsAPercentage check (SlopePercentage >= 0 and SlopePercentage <= 100)
)