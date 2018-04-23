insert into dbo.Role(RoleID, RoleName, RoleDisplayName, RoleDescription) 
values 
(5, 'JurisdicationManager', 'Jurisdication Manager', ''),
(6, 'JurisdicationEditor', 'Jurisdication Editor', '')

go

update dbo.Person
set RoleID = 6
where RoleID = 2