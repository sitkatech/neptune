delete from dbo.Tenant
go

insert into dbo.Tenant(TenantID, TenantName, TenantDomain, TenantSubdomain)
values 
(1, 'SitkaTechnologyGroup', 'projectneptune.com', 'sitka'),
(2, 'OCStormwater', 'ocstormwatertools.org', null)
