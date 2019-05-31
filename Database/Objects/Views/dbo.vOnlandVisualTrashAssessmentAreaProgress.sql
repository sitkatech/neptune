drop view if exists dbo.vOnlandVisualTrashAssessmentAreaProgress
go

Create View dbo.vOnlandVisualTrashAssessmentAreaProgress
as
Select
	subq.OnlandVisualTrashAssessmentAreaID as PrimaryKey,
	subq.OnlandVisualTrashAssessmentAreaID,
	score.OnlandVisualTrashAssessmentScoreDisplayName,
	score.OnlandVisualTrashAssessmentScoreID
From
(Select
	OnlandVisualTrashAssessmentAreaID,
	Avg(NumericValue) as NumericValue,
	Count(*) as NumberOfProgressAssessments
from
(select
	OnlandVisualTrashAssessmentAreaID,
	NumericValue,
	ROW_NUMBER() over (partition by OnlandVisualTrashAssessmentAreaID order by CompletedDate desc) as RowNumber
from dbo.OnlandVisualTrashAssessment ovta
	inner join dbo.OnlandVisualTrashAssessmentScore score
		on ovta.OnlandVisualTrashAssessmentScoreID = score.OnlandVisualTrashAssessmentScoreID
where IsProgressAssessment = 1
) subq
where RoWNumber <=3
group by (OnlandVisualTrashAssessmentAreaID)
having count(*) > 1
) subq inner join dbo.OnlandVisualTrashAssessmentScore score
	on subq.NumericValue = score.NumericValue
Go

select * from dbo.vOnlandVisualTrashAssessmentAreaProgress