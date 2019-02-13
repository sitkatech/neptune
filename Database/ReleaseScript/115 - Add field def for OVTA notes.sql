INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel) 
VALUES 
(59, 'OnlandVisualTrashAssessmentNotes', 'Notes', 'Enter the name of all assessors and any other notes about the assessment.', 1)

Insert Into dbo.FieldDefinitionData (FieldDefinitionID)
values
(59)