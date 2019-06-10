
Alter Table dbo.Delineation
Add TreatmentBMPID int null
	Constraint FK_Delineation_TreatmentBMP_TreatmentBMPID references dbo.TreatmentBMP(TreatmentBMPID)
go

Update d 
set d.TreatmentBMPID = t.TreatmentBMPID
From
dbo.Delineation d inner join TreatmentBMP t on t.DelineationID = d.DelineationID

Alter Table dbo.Delineation
Alter Column TreatmentBMPID int not null
go

Alter Table dbo.Delineation
Add Constraint AK_Delineation_TreatmentBMPID unique (TreatmentBMPID)