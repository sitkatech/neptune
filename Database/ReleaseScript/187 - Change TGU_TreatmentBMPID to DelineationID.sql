Alter Table dbo.TrashGeneratingUnit
Add DelineationID int null
constraint FK_TrashGeneratingUnit_Delineation_DelineationID foreign key references dbo.Delineation(DelineationID)
Go

Update tgu
set tgu.DelineationID = d.DelineationID

from dbo.TrashGeneratingUnit tgu join dbo.TreatmentBMP tbmp
		on tgu.TreatmentBMPID = tbmp.TreatmentBMPID
	join dbo.Delineation d
		on tbmp.TreatmentBMPID = d.TreatmentBMPID

Alter Table dbo.TrashGeneratingUnit
Drop Column TreatmentBMPID