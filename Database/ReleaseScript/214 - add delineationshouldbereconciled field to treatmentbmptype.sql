alter table dbo.TreatmentBMPType add DelineationShouldBeReconciled bit null
go

update dbo.TreatmentBMPType set DelineationShouldBeReconciled = 1
update dbo.TreatmentBMPType set DelineationShouldBeReconciled = 0 where TreatmentBMPTypeName in ('Catch Basin / Inlet (unscreened)', 'Inlet and Pipe Screens', 'In-stream Trash Capture')

alter table dbo.TreatmentBMPType alter column DelineationShouldBeReconciled bit not null
go