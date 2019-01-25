alter table dbo.OnlandVisualTrashAssessment
Add Notes varchar(500) null
go

alter table dbo.OnlandVisualTrashAssessment
Add StormwaterJurisdictionID int null
Go

alter table dbo.OnlandVisualTrashAssessment
Add Constraint FK_OnlandVisualTrashAssessment_StormwaterJurisdiction_StormwaterJurisdictionID
Foreign Key (StormwaterJurisdictionID) references dbo.StormwaterJurisdiction(StormwaterJurisdictionID)
Go

alter table dbo.OnlandVisualTrashAssessment
Add AssessingNewArea bit null
go