alter table dbo.FieldVisit
Add InventoryUpdated bit null
go

Update dbo.FieldVisit
Set InventoryUpdated = 0

alter table dbo.FieldVisit
Alter column InventoryUpdated bit not null