delete from dbo.TrashGeneratingUnit
delete from dbo.LandUseBlock
go

Alter table dbo.LandUseBlock
add TrashGenerationRate decimal not null

Alter table dbo.LandUseBlock
add LandUseForTGR varchar(80) null

Alter table dbo.LandUseBlock
add MedianHouseholdIncomeResidential numeric not null

Alter table dbo.LandUseBlock
add MedianHouseholdIncomeRetail numeric not null

Alter table dbo.LandUseBlock
add StormwaterJurisdictionID int not null constraint FK_LandUseBlock_StormwaterJurisdiction_StormwaterJurisdictionID foreign key references dbo.StormwaterJurisdiction(StormwaterJurisdictionID)

Create Table dbo.PermitType(
PermitTypeID int not null constraint PK_PermitType_PermitTypeID primary key,
PermitTypeName varchar(80) not null constraint AK_PermitType_PermitTypeName unique,
PermitTypeDisplayName varchar(80) not null constraint AK_PermitType_PermitTypeDisplayName unique
)

Insert Into dbo.PermitType (PermitTypeID, PermitTypeName, PermitTypeDisplayName)
values
(1, 'PhaseIMS4', 'Phase I MS4'),
(2, 'PhaseIIMS4', 'Phase II MS4'),
(3, 'IGP', 'IGP')
go

Alter table dbo.LandUseBlock
add PermitTypeID int not null constraint FK_LandUseBlock_PermitType_PermitTypeID Foreign Key references dbo.PermitType(PermitTypeID)