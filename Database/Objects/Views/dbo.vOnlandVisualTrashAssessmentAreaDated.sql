DROP VIEW IF EXISTS dbo.vOnlandVisualTrashAssessmentAreaDated
GO

Create View dbo.vOnlandVisualTrashAssessmentAreaDated as

Select
	a.OnlandVisualTrashAssessmentAreaID,
	a.OnlandVisualTrashAssessmentAreaGeometry,
	q.CompletedDate as MostRecentAssessmentDate,
	Score.OnlandVisualTrashAssessmentScoreDisplayName as MostRecentAssessmentScore
from dbo.OnlandVisualTrashAssessmentArea a
	inner join (
		Select
			ovta.OnlandVisualTrashAssessmentID,
			ovta.OnlandVisualTrashAssessmentAreaID,
			ovta.OnlandVisualTrashAssessmentScoreID,
			ovta.CompletedDate,
			rownumber = Row_Number() over (partition by ovta.OnlandVisualTrashAssessmentAreaID order by ovta.CompletedDate desc)
		from dbo.OnlandVisualTrashAssessment ovta
		where CompletedDate is not null and IsProgressAssessment = 1
	) q -- in the future we'll need to find a way to account for Baseline scores as well, but for now we'll only use Progress for results
		on a.OnlandVisualTrashAssessmentAreaID = q.OnlandVisualTrashAssessmentAreaID
	join  dbo.OnlandVisualTrashAssessmentScore score
		on score.OnlandVisualTrashAssessmentScoreID = q.OnlandVisualTrashAssessmentScoreID
GO
