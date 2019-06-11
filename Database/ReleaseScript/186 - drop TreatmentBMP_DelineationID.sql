
Alter Table dbo.TreatmentBMP
Drop constraint FK_TreatmentBMP_Delineation_DelineationID

Drop index TreatmentBMP.AK_TreatmentBMP_DelineationID

Alter  table dbo.TreatmentBMP
Drop column DelineationID