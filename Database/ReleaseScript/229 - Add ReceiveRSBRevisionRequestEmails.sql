Alter table dbo.Person
Add ReceiveRSBRevisionRequestEmails bit null
Go

Update dbo.Person
Set ReceiveRSBRevisionRequestEmails = 0

Alter table dbo.Person
Alter column ReceiveRSBRevisionRequestEmails bit not null