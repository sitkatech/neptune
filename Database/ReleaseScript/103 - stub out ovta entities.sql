CREATE TABLE [dbo].[OVTASection](
	[OVTASectionID] [int] NOT NULL,
	[OVTASectionName] [varchar](50) NOT NULL,
	[OVTASectionDisplayName] [varchar](50) NOT NULL,
	[SectionHeader] [varchar](100) NOT NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [PK_OVTASection_OVTASectionID] PRIMARY KEY CLUSTERED 
(
	[OVTASectionID] ASC
),
 CONSTRAINT [AK_OVTASection_OVTASectionName] UNIQUE NONCLUSTERED 
(
	[OVTASectionName] ASC
)
)
go

insert into dbo.OVTASection(OVTASectionID, OVTASectionName, OVTASectionDisplayName, SectionHeader, SortOrder)
values
(1, 'Inventory', 'Inventory', 'Review and Update Inventory?', 10),
(2, 'Assessment', 'Assessment', 'Assessment', 20),
(3, 'Maintenance', 'Maintenance', 'Maintenance', 30),
(4, 'PostMaintenanceAssessment', 'Post-Maintenance Assessment', 'Post-Maintenance Assessment', 40),
(5, 'VisitSummary', 'Visit Summary', 'Visit Summary', 50)

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

INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel) 
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