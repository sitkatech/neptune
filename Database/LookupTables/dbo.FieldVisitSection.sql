delete from dbo.FieldVisitSection

insert into dbo.FieldVisitSection(FieldVisitSectionID, FieldVisitSectionName, FieldVisitSectionDisplayName, SortOrder)
values
(1, 'Inventory', 'Inventory', 10),
(2, 'Assessment', 'Assess', 20),
(3, 'Maintain', 'Maintain', 30),
(4, 'PostMaintenanceAssessment', 'Post-Maintenance Assessment', 40),
(5, 'WrapUpVisit', 'Wrap-up Visit', 50),
(6, 'ManageVisit', 'Manage Visit', 60)