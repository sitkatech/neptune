delete from dbo.FieldVisitSection

insert into dbo.FieldVisitSection(FieldVisitSectionID, FieldVisitSectionName, FieldVisitSectionDisplayName, SectionHeader, SortOrder)
values
(1, 'Inventory', 'Inventory', 'Review and Update Inventory?', 10),
(2, 'Assessment', 'Assessment', 'Assessment', 20),
(3, 'Maintenance', 'Maintenance', 'Maintenance', 30),
(4, 'PostMaintenanceAssessment', 'Post-Maintenance Assessment', 'Post-Maintenance Assessment', 40),
(5, 'WrapUpVisit', 'Wrap-up Visit', 'Wrap-up Visit', 50),
(6, 'ManageVisit', 'Manage Visit', 'Manage Visit', 60)