alter table dbo.Person add WebServiceAccessToken uniqueidentifier null
GO

update dbo.Person set WebServiceAccessToken = '709859C8-7376-4709-A298-7606E820DA05'
where PersonID = 1