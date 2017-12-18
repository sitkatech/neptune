delete from dbo.Role
go

insert into dbo.Role(RoleID, RoleName, RoleDisplayName, RoleDescription) 
values 
(1, 'Admin', 'Administrator', ''),
(2, 'Normal', 'Normal User', ''),
(3, 'Unassigned', 'Unassigned', ''),
(4, 'SitkaAdmin', 'Sitka Administrator', '')