create view dbo.vGeoServerOnlandVisualTrashAssessmentArea
as
select area.*, aggscore.Score from ( select area.OnlandVisualTrashAssessmentAreaID, cast(round(avg(cast (score.NumericValue as decimal (5,4))), 0) as int) as Score
 from OnlandVisualTrashAssessment ovta
	join OnlandVisualTrashAssessmentArea area on ovta.OnlandVisualTrashAssessmentAreaID = area.OnlandVisualTrashAssessmentAreaID
	join OnlandVisualTrashAssessmentScore score on ovta.OnlandVisualTrashAssessmentScoreID = score.OnlandVisualTrashAssessmentScoreID
	where ovta.OnlandVisualTrashAssessmentStatusID = 2
	group by area.OnlandVisualTrashAssessmentAreaID ) aggscore join OnlandVisualTrashAssessmentArea area on area.OnlandVisualTrashAssessmentAreaID = aggscore.OnlandVisualTrashAssessmentAreaID

