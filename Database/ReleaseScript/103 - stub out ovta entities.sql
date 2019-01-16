CREATE TABLE dbo.OVTASection(
	OVTASectionID int NOT NULL,
	OVTASectionName varchar(50) NOT NULL,
	OVTASectionDisplayName varchar(50) NOT NULL,
	SectionHeader varchar(100) NOT NULL,
	SortOrder int NOT NULL,
	HasCompletionStatus bit not null
 CONSTRAINT PK_OVTASection_OVTASectionID PRIMARY KEY CLUSTERED 
(
	OVTASectionID ASC
),
 CONSTRAINT AK_OVTASection_OVTASectionName UNIQUE NONCLUSTERED 
(
	OVTASectionName ASC
)
)
go

insert into dbo.OVTASection(OVTASectionID, OVTASectionName, OVTASectionDisplayName, SectionHeader, SortOrder, HasCompletionStatus)
values
(1, 'Instructions', 'Instructions', 'Instructions?', 10, 0),
(2, 'RecordObservations', 'Record Observations', 'Record Observations', 20, 1),
(3, 'VerifyOVTAArea', 'Verify OVTA Area', 'Verify OVTA Area', 30, 0),
(4, 'FinalizeOVTA', 'Finalize OVTA', 'Finalize OVTA', 40, 0)

insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(34, 'OVTAInstructions', 'OVTA Instructions', 2)

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
values
(34, null)

insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(35, 'OVTAIndex', 'OVTA Index', 2)

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
values
(35, null)

INSERT dbo.FieldDefinition (FieldDefinitionID, FieldDefinitionName, FieldDefinitionDisplayName, DefaultDefinition, CanCustomizeLabel) 
VALUES 
(58, 'OnlandVisualTrashAssessment', N'Onland Visual Trash Assessment', N'The assessing, visually, of trash on land.', 1)

Insert into dbo.FieldDefinitionData(FieldDefinitionID, FieldDefinitionLabel, FieldDefinitionDataValue)
Values
(58, null, null)

Create Table dbo.OnlandVisualTrashAssessment(
OnlandVisualTrashAssessmentID int not null identity(1,1) constraint PK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID primary key,
CreatedByPersonID int not null constraint FK_OnlandVisualTrashAssessment_Person_CreatedByPersonID_PersonID foreign key references dbo.Person(PersonID),
CreatedDate datetime not null
)

INSERT 
dbo.StormwaterBreadCrumbEntity (StormwaterBreadCrumbEntityID, StormwaterBreadCrumbEntityName, StormwaterBreadCrumbEntityDisplayName, GlyphIconClass, ColorClass) VALUES
 (10, 'OnlandVisualTrashAssessment', 'Onland Visual Trash Assessment', 'glyphicon-heart-empty', 'onlandVisualTrashAssessmentColor')