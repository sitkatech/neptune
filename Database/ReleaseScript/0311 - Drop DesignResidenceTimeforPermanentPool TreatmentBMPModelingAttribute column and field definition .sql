alter table TreatmentBMPModelingAttribute 
drop column DesignResidenceTimeforPermanentPool
 

delete from FieldDefinition where FieldDefinitionTypeID = 82
go
delete from FieldDefinitionType where FieldDefinitionTypeID = 82