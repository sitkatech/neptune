DROP VIEW IF EXISTS dbo.vGeoServerAssessmentAreaExport
GO

create view dbo.vGeoServerAssessmentAreaExport as
	Select
		area.OnlandVisualTrashAssessmentAreaID as OVTAAreaID,
		area.OnlandVisualTrashAssessmentAreaName as OVTAAreaName,
		area.StormwaterJurisdictionID as JurisID,
		o.OrganizationName as JurisName,
		area.OnlandVisualTrashAssessmentAreaGeometry,
		score.OnlandVisualTrashAssessmentScoreDisplayName as Score,
		ovta.OnlandVisualTrashAssessmentID as AssessmentID,
		CONVERT(VARCHAR(12), ovta.CompletedDate, 107) CompletedDate,
		ovta.IsProgressAssessment,
		area.AssessmentAreaDescription as [Description]
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
		left join dbo.StormwaterJurisdiction sj
			on area.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
		left join dbo.Organization o
			on sj.OrganizationID = o.OrganizationID
	where 
		RankByCompletedDate between 1 and 5
		or RankByCompletedDate is null -- have to account for this being null so we get the results of the left outer join
go