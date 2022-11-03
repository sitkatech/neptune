-- Water Quality Detention Volume (rename, redefine)
update FieldDefinitionType
set FieldDefinitionTypeDisplayName = 'Extended Detention Surcharge Volume'
where FieldDefinitionTypeID = 103

update FieldDefinition
set FieldDefinitionValue = 'Extended detention surcharge storage above permanent pool volume. Extended detention is > 24-hour drawdown time.'
where FieldDefinitionTypeID = 103

-- Permanent Pool or Wetland Volume (redefine)
update FieldDefinition
set FieldDefinitionValue = 'Constructed wetland or permanent pool volume below discharge elevation.'
where FieldDefinitionTypeID = 91
