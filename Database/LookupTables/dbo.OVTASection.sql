delete from dbo.OVTASection

insert into dbo.OVTASection(OVTASectionID, OVTASectionName, OVTASectionDisplayName, SectionHeader, SortOrder, HasCompletionStatus)
values
(1, 'Instructions', 'Instructions', 'Instructions', 10, 0),
(2, 'InitiateOVTA', 'Initiate OVTA', 'Initiate OVTA', 20, 1),
(3, 'RecordObservations', 'Record Observations', 'Record Observations', 30, 1),
(4, 'AddOrRemoveParcels', 'Add or Remove Parcels', 'Add or Remove Parcels', 40, 1), -- todo: this and RAA might have completion status after all
(5, 'RefineAssessmentArea', 'Refine Assessment Area', 'Refine Assessment Area', 50, 0),
(6, 'FinalizeOVTA', 'Review and Finalize OVTA', 'Finalize OVTA', 60, 0)