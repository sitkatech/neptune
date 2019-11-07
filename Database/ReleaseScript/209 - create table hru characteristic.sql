Create Table dbo.HRUCharacteristic(
HRUCharacteristicID int not null identity(1,1) constraint PK_HRUCharacteristic_HRUCharacteristicID primary key,
LSPCLandUseDescription varchar(100) not null,
HydrologicSoilGroup varchar(5) not null,
SlopePercentage int not null,
ImperviousAcres float not null,
LastUpdated datetime not null,
TreatmentBMPID int null constraint FK_HRUCharacteristic_TreatmentBMP_TreatmentBMPID foreign key references dbo.TreatmentBMP(TreatmentBMPID),
WaterQualityManagementPlanID int null constraint FK_HRUCharacteristic_WaterQualityManagementPlan_WaterQualityManagementPlanID foreign key references dbo.WaterQualityManagementPlan(WaterQualityManagementPlanID),
constraint CK_HRUCharacteristic_SlopePercentageIsAPercentage check (SlopePercentage >= 0 and SlopePercentage <= 100),
constraint CK_HRUCharacteristic_XorForeignKeys check(
	(TreatmentBMPID is not null and WaterQualityManagementPlanID is null)
	OR (TreatmentBMPID is null and WaterQualityManagementPlanID is not null)
)
)

insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(45, 'HRUCharacteristics', 'HRU Characteristics', 2)

Insert into dbo.NeptunePage(NeptunePageTypeID)
Values
(45)

Alter table dbo.WaterQualityManagementPlan
Add WaterQualityManagementPlanBoundary geometry null
GO

Update w
set WaterQualityManagementPlanBoundary = t.WaterQualityManagementPlanGeometry
from dbo.WaterQualityManagementPlan w join dbo.vWaterQualityManagementPlanTGUInput t
	on w.WaterQualityManagementPlanID = t.WaterQualityManagementPlanID