alter table dbo.TreatmentBMP alter column SystemOfRecordID varchar(100) null
GO

update dbo.TreatmentBMP
set SystemOfRecordID = null
