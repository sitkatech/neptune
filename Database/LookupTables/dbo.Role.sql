delete from dbo.Role
go

insert into dbo.Role(RoleID, RoleName, RoleDisplayName, RoleDescription) 
values 
(1, 'Admin', 'Administrator', ''),
(3, 'Unassigned', 'Unassigned', ''),
(4, 'SitkaAdmin', 'Sitka Administrator', ''),
(5, 'JurisdictionManager', 'Jurisdication Manager', ''),
(6, 'JurisdictionEditor', 'Jurisdication Editor', '')