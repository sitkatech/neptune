delete from dbo.OVTASection

insert into dbo.OVTASection(OVTASectionID, OVTASectionName, OVTASectionDisplayName, SectionHeader, SortOrder)
values
(1, 'Instructions', 'Instructions', 'Instructions?', 10),
(2, 'RecordObservations', 'Record Observations', 'Record Observations', 20),
(3, 'VerifyOVTAArea', 'Verify OVTA Area', 'Verify OVTA Area', 30),
(4, 'FinalizeOVTA', 'Finalize OVTA', 'Finalize OVTA', 40)