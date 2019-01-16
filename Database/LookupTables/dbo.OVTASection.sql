delete from dbo.OVTASection

insert into dbo.OVTASection(OVTASectionID, OVTASectionName, OVTASectionDisplayName, SectionHeader, SortOrder)
values
(1, 'Instructions', 'Instructions', 'Instructions', 10),
(2, 'InitiateOVTA', 'Initiate OVTA', 'Initiate OVTA', 20),
(3, 'RecordObservations', 'Record Observations', 'Record Observations', 30),
(4, 'FinalizeOVTA', 'Review and Finalize OVTA', 'Finalize OVTA', 40)