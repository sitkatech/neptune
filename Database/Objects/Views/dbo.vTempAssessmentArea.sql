drop view if exists dbo.vTempAssessmentArea
go

create view dbo.vTempAssessmentArea
as
Select
OnlandVisualTrashAssessmentAreaID,
OnlandVisualTrashAssessmentAreaGeometry
from dbo.OnlandVisualTrashAssessmentArea
go

drop view if exists dbo.vTempAssessmentTransectLine
go

create view dbo.vTempAssessmentTransectLine
as
Select
OnlandVisualTrashAssessmentAreaID,
TransectLine
from dbo.OnlandVisualTrashAssessmentArea
go
