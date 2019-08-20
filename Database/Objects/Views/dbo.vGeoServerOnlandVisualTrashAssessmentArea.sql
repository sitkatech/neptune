DROP VIEW IF EXISTS dbo.vGeoServerOnlandVisualTrashAssessmentArea
GO

create view dbo.vGeoServerOnlandVisualTrashAssessmentArea as
	Select
		area.OnlandVisualTrashAssessmentAreaID,
		area.OnlandVisualTrashAssessmentAreaName,
		area.StormwaterJurisdictionID,
		area.OnlandVisualTrashAssessmentAreaGeometry4326 as OnlandVisualTrashAssessmentAreaGeometry,
		score.OnlandVisualTrashAssessmentScoreDisplayName as Score,
		ovta.OnlandVisualTrashAssessmentID,
		ovta.CompletedDate,
		ovta.IsProgressAssessment
	from dbo.OnlandVisualTrashAssessmentArea area
		left join (
			Select
				*,
				Row_Number() over (partition by OnlandVisualTrashAssessmentAreaID order by CompletedDate desc) as RankByCompletedDate
			from dbo.OnlandVisualTrashAssessment
			where CompletedDate is not null
		) ovta on area.OnlandVisualTrashAssessmentAreaID = ovta.OnlandVisualTrashAssessmentAreaID
		left join dbo.OnlandVisualTrashAssessmentScore score
			on ovta.OnlandVisualTrashAssessmentScoreID = score.OnlandVisualTrashAssessmentScoreID
	where 
		RankByCompletedDate between 1 and 5
		or RankByCompletedDate is null -- have to account for this being null so we get the results of the left outer join
go