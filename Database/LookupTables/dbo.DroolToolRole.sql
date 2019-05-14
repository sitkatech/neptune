delete from dbo.DroolToolRole
go

insert into dbo.DroolToolRole(DroolToolRoleID, DroolToolRoleName, DroolToolRoleDisplayName, DroolToolRoleDescription) 
values 
(1, 'Admin', 'Administrator', ''),
(2, 'Editor', 'Editor', ''),
(3, 'Unassigned', 'Unassigned', '')
