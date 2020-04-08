update dbo.Person
set WebServiceAccessToken = NEWID()
where WebServiceAccessToken is null

alter table dbo.Person
alter column WebServiceAccessToken uniqueidentifier not null