MERGE INTO dbo.OVTASection AS Target
USING (VALUES
(1, 'Instructions', 'Instructions', 'Instructions', 10, 0),
(2, 'InitiateOVTA', 'Initiate OVTA', 'Initiate OVTA', 20, 1),
(3, 'RecordObservations', 'Record Observations', 'Record Observations', 30, 1),
(4, 'AddOrRemoveParcels', 'Add or Remove Parcels', 'Add or Remove Parcels', 40, 1), -- todo: this and RAA might have completion status after all
(5, 'RefineAssessmentArea', 'Refine Assessment Area', 'Refine Assessment Area', 50, 0),
(6, 'FinalizeOVTA', 'Review and Finalize OVTA', 'Finalize OVTA', 60, 0)
)
AS Source (OVTASectionID, OVTASectionName, OVTASectionDisplayName, SectionHeader, SortOrder, HasCompletionStatus)
ON Target.OVTASectionID = Source.OVTASectionID
WHEN MATCHED THEN
UPDATE SET
	OVTASectionName = Source.OVTASectionName,
	OVTASectionDisplayName = Source.OVTASectionDisplayName,
	SectionHeader = Source.SectionHeader,
	SortOrder = Source.SortOrder,
	HasCompletionStatus = Source.HasCompletionStatus
WHEN NOT MATCHED BY TARGET THEN
	INSERT (OVTASectionID, OVTASectionName, OVTASectionDisplayName, SectionHeader, SortOrder, HasCompletionStatus)
	VALUES (OVTASectionID, OVTASectionName, OVTASectionDisplayName, SectionHeader, SortOrder, HasCompletionStatus)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;