DROP VIEW IF EXISTS dbo.vPyQgisOnlandVisualTrashAssessmentAreaDated
GO

Create View dbo.vPyQgisOnlandVisualTrashAssessmentAreaDated as

Select
	a.OnlandVisualTrashAssessmentAreaID as OVTAID,
	a.OnlandVisualTrashAssessmentAreaGeometry,
	q.CompletedDate as AssessDate
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
	where rownumber = 1
GO