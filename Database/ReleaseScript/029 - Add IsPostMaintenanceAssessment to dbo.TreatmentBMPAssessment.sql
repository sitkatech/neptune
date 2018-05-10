alter table dbo.TreatmentBMPAssessment
add IsPostMaintenanceAssessment bit null
go

update dbo.TreatmentBMPAssessment
set IsPostMaintenanceAssessment = 0

alter table dbo.TreatmentBMPAssessment
alter column IsPostMaintenanceAssessment bit not null