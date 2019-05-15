Alter Table dbo.OnlandVisualTrashAssessment
Add IsProgressAssessment Bit Null

GO

Update dbo.OnlandVisualTrashAssessment Set IsProgressAssessment = 0

GO

Alter Table dbo.OnlandVisualTrashAssessment
Alter Column IsProgressAssessment Bit Not Null

GO

Update dbo.FieldDefinition Set FieldDefinitionName = 'BaselineScore', FieldDefinitionDisplayName = 'Baseline Score' Where FieldDefinitionName = 'OVTAScore'

GO

Insert Into dbo.FieldDefinition (FieldDefinitionID, FieldDefinitionName, FieldDefinitionDisplayName, DefaultDefinition, CanCustomizeLabel)
Values (63, 'ProgressScore', 'Progress Score', '', 1)