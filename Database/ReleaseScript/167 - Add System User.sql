Insert into Person(PersonGuid, FirstName, LastName, Email, RoleID, CreateDate, IsActive, OrganizationID, ReceiveSupportEmails, LoginName)
values
(NEWID(), '', 'System', '', 4, GetDate(), 1, 1, 0, 'System')