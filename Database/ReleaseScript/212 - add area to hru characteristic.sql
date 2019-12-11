Alter table dbo.HRUCharacteristic
Add Area float null
Go

update dbo.HRUCharacteristic
set Area = 0
Go

Alter table dbo.HRUCharacteristic
Alter column Area float not null