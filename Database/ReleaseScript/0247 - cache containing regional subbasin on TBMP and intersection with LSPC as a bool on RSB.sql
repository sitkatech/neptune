Alter table dbo.RegionalSubbasin
Add IsInLSPCBasin bit null
go

Alter table dbo.TreatmentBMP
Add RegionalSubbasinID int null
go