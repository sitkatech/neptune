update dbo.FieldDefinition
set FieldDefinitionDisplayName = 'Wet Weather % Captured'
where FieldDefinitionName = 'PercentCaptured'

update dbo.FieldDefinition
set FieldDefinitionDisplayName = 'Wet Weather % Retained'
where FieldDefinitionName = 'PercentRetained'