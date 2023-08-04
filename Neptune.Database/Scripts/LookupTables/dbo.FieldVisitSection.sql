MERGE INTO dbo.FieldVisitSection AS Target
USING (VALUES
(1, 'Inventory', 'Inventory', 'Review and Update Inventory?', 10),
(2, 'Assessment', 'Assessment', 'Assessment', 20),
(3, 'Maintenance', 'Maintenance', 'Maintenance', 30),
(4, 'PostMaintenanceAssessment', 'Post-Maintenance Assessment', 'Post-Maintenance Assessment', 40),
(5, 'VisitSummary', 'Visit Summary', 'Visit Summary', 50)
)
AS Source (FieldVisitSectionID, FieldVisitSectionName, FieldVisitSectionDisplayName, SectionHeader, SortOrder)
ON Target.FieldVisitSectionID = Source.FieldVisitSectionID
WHEN MATCHED THEN
UPDATE SET
	FieldVisitSectionName = Source.FieldVisitSectionName,
	FieldVisitSectionDisplayName = Source.FieldVisitSectionDisplayName,
	SectionHeader = Source.SectionHeader,
	SortOrder = Source.SortOrder
WHEN NOT MATCHED BY TARGET THEN
	INSERT (FieldVisitSectionID, FieldVisitSectionName, FieldVisitSectionDisplayName, SectionHeader, SortOrder)
	VALUES (FieldVisitSectionID, FieldVisitSectionName, FieldVisitSectionDisplayName, SectionHeader, SortOrder)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;