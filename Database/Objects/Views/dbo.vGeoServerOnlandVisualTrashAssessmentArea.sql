DROP VIEW IF EXISTS dbo.vGeoServerOnlandVisualTrashAssessmentArea
GO

create view dbo.vGeoServerOnlandVisualTrashAssessmentArea as
select
	area.OnlandVisualTrashAssessmentAreaID,
	area.OnlandVisualTrashAssessmentAreaName,
	area.StormwaterJurisdictionID,
	area.OnlandVisualTrashAssessmentAreaGeometry,
	case when area.OnlandVisualTrashAssessmentScoreID is null then 0 else Score.NumericValue end as Score
from dbo.OnlandVisualTrashAssessmentArea area left join
	dbo.OnlandVisualTrashAssessmentScore score
		on area.OnlandVisualTrashAssessmentScoreID = score.OnlandVisualTrashAssessmentScoreID