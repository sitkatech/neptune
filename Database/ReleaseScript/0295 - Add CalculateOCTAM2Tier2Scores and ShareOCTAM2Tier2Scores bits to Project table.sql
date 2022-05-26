alter table dbo.Project
add CalculateOCTAM2Tier2Scores bit null

alter table dbo.Project
add ShareOCTAM2Tier2Scores bit null

go

update dbo.Project
set CalculateOCTAM2Tier2Scores = 0

update dbo.Project
set ShareOCTAM2Tier2Scores = 0

go

alter table dbo.Project
alter column CalculateOCTAM2Tier2Scores bit not null

alter table dbo.Project
alter column ShareOCTAM2Tier2Scores bit not null