CREATE TABLE dbo.DroolToolRole(
	DroolToolRoleID int NOT NULL CONSTRAINT PK_DroolToolRole_DroolToolRoleID PRIMARY KEY,
	DroolToolRoleName varchar(100) NOT NULL CONSTRAINT AK_DroolToolRole_DroolToolRoleName UNIQUE,
	DroolToolRoleDisplayName varchar(100) NOT NULL CONSTRAINT AK_DroolToolRole_DroolToolRoleDisplayName UNIQUE,
	DroolToolRoleDescription varchar(255) NULL
)

alter table dbo.Person add DroolToolRoleID int null
alter table dbo.Person add constraint FK_Person_DroolToolRole_DroolToolRoleID foreign key (DroolToolRoleID) references dbo.DroolToolRole(DroolToolRoleID)

GO

insert into dbo.DroolToolRole(DroolToolRoleID, DroolToolRoleName, DroolToolRoleDisplayName, DroolToolRoleDescription) 
values 
(1, 'Admin', 'Administrator', ''),
(2, 'Editor', 'Editor', ''),
(3, 'Unassigned', 'Unassigned', '')

update dbo.Person set DroolToolRoleID = 1 where RoleID in (1, 4)
update dbo.Person set DroolToolRoleID = 3 where RoleID not in (1, 4)
