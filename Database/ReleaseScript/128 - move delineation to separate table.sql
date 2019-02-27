CREATE TABLE dbo.Delineation(
	DelineationID int not null identity(1,1) constraint PK_Delineation_DelineationID primary key,
	DelineationGeometry geometry NOT NULL,
	DelineationTypeID int NOT NULL CONSTRAINT FK_Delineation_DelineationType_DelineationTypeID FOREIGN KEY REFERENCES dbo.DelineationType (DelineationTypeID),
	TreatmentBMPIDTemp int not null --temp
)
go




Insert into dbo.Delineation (DelineationGeometry, DelineationTypeID,TreatmentBMPIDTemp)
Select DelineationGeometry, DelineationTypeID, TreatmentBMPID from dbo.TreatmentBMP where DelineationGeometry is not null

Alter Table dbo.TreatmentBMP
Add DelineationID int null constraint FK_TreatmentBMP_Delineation_DelineationID foreign key references dbo.Delineation(DelineationID)
go

Update t
set t.DelineationID = d.DelineationID
from dbo.TreatmentBMP t inner join dbo.Delineation d on t.TreatmentBMPID = d.TreatmentBMPIDTemp
go

Alter Table dbo.Delineation drop column TreatmentBMPIDTemp
Alter Table dbo.TreatmentBMP drop constraint [CK_TreatmentBMP_BMPWithDelineationMustHaveDelineationType]
Alter Table dbo.TreatmentBMP drop constraint FK_TreatmentBMP_DelineationType_DelineationTypeID
Alter Table dbo.TreatmentBMP drop column DelineationGeometry
Alter Table dbo.TreatmentBMP drop column DelineationTypeID