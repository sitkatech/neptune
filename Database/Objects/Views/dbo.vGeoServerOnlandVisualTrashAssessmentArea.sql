DROP VIEW IF EXISTS dbo.vGeoServerOnlandVisualTrashAssessmentArea
GO

create view dbo.vGeoServerOnlandVisualTrashAssessmentArea as
select
	area.OnlandVisualTrashAssessmentAreaID,
	area.OnlandVisualTrashAssessmentAreaName,
	area.StormwaterJurisdictionID,
	area.OnlandVisualTrashAssessmentAreaGeometry,
	case when area.OnlandVisualTrashAssessmentScoreID is null then 0 else Score.NumericValue end as Score,
	score.OnlandVisualTrashAssessmentScoreDisplayName,
	ovta.CompletedDate
from dbo.OnlandVisualTrashAssessmentArea area left join
	dbo.OnlandVisualTrashAssessmentScore score
		on area.OnlandVisualTrashAssessmentScoreID = score.OnlandVisualTrashAssessmentScoreID 
		left join dbo.OnlandVisualTrashAssessment ovta
		on area.OnlandVisualTrashAssessmentAreaID = ovta.OnlandVisualTrashAssessmentAreaID