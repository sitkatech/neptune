alter table dbo.Project
add UpdatePersonID int null CONSTRAINT FK_Project_Person_UpdatePersonID foreign key (UpdatePersonID) REFERENCES dbo.Person(PersonID)

alter table dbo.Project
add DateUpdated datetime null
