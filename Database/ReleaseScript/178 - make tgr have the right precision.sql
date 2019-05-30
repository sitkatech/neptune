alter table dbo.OnlandVisualTrashAssessmentScore
Alter column TrashGenerationRate decimal(4,1) not null

alter table dbo.LandUseBlock
Alter column TrashGenerationRate decimal(4,1) not null

update dbo.LandUseblock
set TrashGenerationRate = 2.5 where TrashGenerationRate = 3

update dbo.LandUseblock
set TrashGenerationRate = 7.5 where TrashGenerationRate = 8