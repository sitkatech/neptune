delete from dbo.OVTASection

insert into dbo.OVTASection(OVTASectionID, OVTASectionName, OVTASectionDisplayName, SectionHeader, SortOrder, HasCompletionStatus)
values
(1, 'Instructions', 'Instructions', 'Instructions', 10, 0),
(2, 'InitiateOVTA', 'Initiate OVTA', 'Initiate OVTA', 20, 1),
(3, 'RecordObservations', 'Record Observations', 'Record Observations', 30, 1),
(4, 'FinalizeOVTA', 'Review and Finalize OVTA', 'Finalize OVTA', 40, 0)