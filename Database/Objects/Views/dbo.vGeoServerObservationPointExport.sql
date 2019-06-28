Drop View If Exists dbo.vGeoServerObservationPointExport
go

Create View dbo.vGeoServerObservationPointExport
as
Select
	OnlandVisualTrashAssessmentAreaName as OVTAAreaName,
	a.OnlandVisualTrashAssessmentID as AssessmentID,
	o.LocationPoint,
	o.Note,
	a.CompletedDate,
	Score.OnlandVisualTrashAssessmentScoreDisplayName as Score,
	area.StormwaterJurisdictionID as JurisID,
	org.OrganizationName as JurisName
from
	dbo.OnlandVisualTrashAssessmentObservation o join dbo.OnlandVisualTrashAssessment a
		on o.OnlandVisualTrashAssessmentID = a.OnlandVisualTrashAssessmentID
	join dbo.OnlandVisualTrashAssessmentArea area on a.OnlandVisualTrashAssessmentAreaID = area.OnlandVisualTrashAssessmentAreaID
	join dbo.OnlandVisualTrashAssessmentScore score on a.OnlandVisualTrashAssessmentScoreID = score.OnlandVisualTrashAssessmentScoreID
	left join dbo.StormwaterJurisdiction sj
			on area.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
		left join dbo.Organization org
			on sj.OrganizationID = org.OrganizationID
