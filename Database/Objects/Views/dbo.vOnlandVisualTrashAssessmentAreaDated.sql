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
		where CompletedDate is not null
	) q 
		on a.OnlandVisualTrashAssessmentAreaID = q.OnlandVisualTrashAssessmentAreaID
	join  dbo.OnlandVisualTrashAssessmentScore score
		on score.OnlandVisualTrashAssessmentScoreID = q.OnlandVisualTrashAssessmentScoreID
GO
