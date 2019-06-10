Delete From dbo.FieldDefinitionData
Where FieldDefinitionID = 24

Delete From dbo.NeptunePage
Where NeptunePageTypeID = 8

Alter table dbo.TreatmentBMP
Drop Column ModeledCatchmentID