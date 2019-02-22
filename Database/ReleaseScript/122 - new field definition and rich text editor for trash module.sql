INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel) 
values (61, 'OVTAScore', 'OVTA Score', 'For an OVTA, scores range from A to D and indicate the condition of the assessed area at the time of the assessment. For an OVTA Area, the score is an aggregate of all of its Assessments'' scores.', 1)
go

Insert dbo.FieldDefinitionData(FieldDefinitionID)
values (61)

Insert dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values (36, 'TrashModuleProgramOverview', 'Trash Module Program Overview', 2)
go

Insert dbo.NeptunePage(NeptunePageTypeID)
values (36)