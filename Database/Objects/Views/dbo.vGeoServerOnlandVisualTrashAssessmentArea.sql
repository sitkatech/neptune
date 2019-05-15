DROP VIEW IF EXISTS dbo.vGeoServerOnlandVisualTrashAssessmentArea
GO

create view dbo.vGeoServerOnlandVisualTrashAssessmentArea as
select
	area.OnlandVisualTrashAssessmentAreaID,
	area.OnlandVisualTrashAssessmentAreaName,
	area.StormwaterJurisdictionID,
	area.OnlandVisualTrashAssessmentAreaGeometry,
	case when area.OnlandVisualTrashAssessmentProgressScoreID is null then 0 else Score.NumericValue end as Score,
	-- todo: this view might need to be updated to also include the Baseline score in the future; for now we'll use Progress for OVTA results
	score.OnlandVisualTrashAssessmentScoreDisplayName,
	ovta.CompletedDate
from dbo.OnlandVisualTrashAssessmentArea area left join
	dbo.OnlandVisualTrashAssessmentScore score
		on area.OnlandVisualTrashAssessmentProgressScoreID = score.OnlandVisualTrashAssessmentScoreID 
		left join dbo.OnlandVisualTrashAssessment ovta
		on area.OnlandVisualTrashAssessmentAreaID = ovta.OnlandVisualTrashAssessmentAreaID
where ovta.IsProgressAssessment = 1