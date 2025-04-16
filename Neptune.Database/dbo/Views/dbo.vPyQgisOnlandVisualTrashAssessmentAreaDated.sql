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
			rownumber = Row_Number() over (partition by ovta.OnlandVisualTrashAssessmentAreaID order by ovta.CompletedDate desc),
			AssessmentCount = count(*) over (partition by ovta.OnlandVisualTrashAssessmentAreaID)
		from dbo.OnlandVisualTrashAssessment ovta
		where CompletedDate is not null
	) q 
		on a.OnlandVisualTrashAssessmentAreaID = q.OnlandVisualTrashAssessmentAreaID and q.AssessmentCount > 1 and q.rownumber = 1
GO