create table dbo.TrashGeneratingUnit(
TrashGeneratingUnitID int not null identity(1,1) constraint PK_TrashGeneratingUnit_TrashGeneratingUnitID primary key,
StormwaterJurisdictionID int not null constraint FK_TrashGeneratingUnit_StormwaterJurisdiction_StormwaterJurisdictionID foreign key references dbo.StormwaterJurisdiction(StormwaterJurisdictionID),
TreatmentBMPID int null constraint FK_TrashGeneratingUnit_TreatmentBMP_TreatmentBMPID foreign key references dbo.TreatmentBMP(TreatmentBMPID),
OnlandVisualTrashAssessmentAreaID int null constraint FK_TrashGeneratingUnit_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID foreign key references dbo.OnlandVisualTrashAssessmentArea(OnlandVisualTrashAssessmentAreaID),
LandUseBlockID int null constraint FK_TrashGeneratingUnit_LandUseBlock_LandUseBlockID foreign key references dbo.LandUseBlock(LandUseBlockID),
TrashGeneratingUnitGeometry geometry not null
)

Drop view if exists dbo.vGeoServerTrashGeneratingUnit
Go

Create view dbo.vGeoServerTrashGeneratingUnit
as select * from dbo.TrashGeneratingUnit
Go