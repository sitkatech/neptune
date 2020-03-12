update dbo.FieldDefinition
set FieldDefinitionName = 'DrawdownTimeForDetentionVolume', FieldDefinitionDisplayName = 'Drawdown Time For Detention Volume'
where FieldDefinitionID = 96

exec sp_rename 'dbo.TreatmentBMPModelingAttribute.TotalDrawdownTime', 'DrawdownTimeForDetentionVolume', 'COLUMN'