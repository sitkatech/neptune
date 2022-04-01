alter table dbo.Project
add DoesNotIncludeTreatmentBMPs bit null
go

update dbo.Project
set DoesNotIncludeTreatmentBMPs = 0

alter table dbo.Project
alter column DoesNotIncludeTreatmentBMPs bit not null