insert into dbo.Role(RoleID, RoleName, RoleDisplayName, RoleDescription) 
values 
(5, 'JurisdicationManager', 'Jurisdication Manager', ''),
(6, 'JurisdicationEditor', 'Jurisdication Editor', '')

go

update dbo.Person
set RoleID = 6
where RoleID = 2

go

delete dbo.StormwaterJurisdictionPerson
from dbo.StormwaterJurisdictionPerson sjp
join dbo.Person p on sjp.PersonID = p.PersonID
where p.RoleID in (1, 4)