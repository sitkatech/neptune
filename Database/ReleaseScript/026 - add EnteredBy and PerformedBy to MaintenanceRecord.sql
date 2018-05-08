-- story said "don't worry about migration since data will be wiped" which I would like to broadly interpret as "wipe Maintenance Record data if it makes this story easier"
--delete from dbo.MaintenanceRecord

-- these nulls should be not nulls but I'm waiting to determine if I should uncomment the above delete statement before I make that switch
alter table dbo.MaintenanceRecord
add EnteredByPersonID int null

alter table dbo.MaintenanceRecord
add constraint FK_MaintenanceRecord_Person_EnteredByPersonID_PersonID foreign key (EnteredByPersonID) references dbo.Person(PersonID)

alter table dbo.MaintenanceRecord
add constraint FK_MaintenanceRecord_Person_EnteredByPersonID_TenantID_PersonID_TenantID foreign key (EnteredByPersonID, TenantID) references dbo.Person(PersonID, TenantID)

alter table dbo.MaintenanceRecord
add PerformedByOrganizationID int null

alter table dbo.MaintenanceRecord
add constraint FK_MaintenanceRecord_Organization_PerformedByOrganizationID_OrganizationID foreign key (PerformedByOrganizationID) references dbo.Organization(OrganizationID)

alter table dbo.MaintenanceRecord
add constraint FK_MaintenanceRecord_Organization_PerformedByOrganizationID_TenantID_OrganizationID_TenantID foreign key (PerformedByOrganizationID, TenantID) references dbo.Organization(OrganizationID, TenantID)

-- kill the "PerformedByPersonID" column
alter table dbo.MaintenanceRecord
drop constraint FK_MaintenanceRecord_Person_PerformedByPersonID_PersonID

alter table dbo.MaintenanceRecord
drop column PerformedByPersonID