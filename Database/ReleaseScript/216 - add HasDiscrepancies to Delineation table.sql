alter table dbo.Delineation add HasDiscrepancies bit null
GO

update dbo.Delineation
set HasDiscrepancies = 0

alter table dbo.Delineation alter column HasDiscrepancies bit not null
GO