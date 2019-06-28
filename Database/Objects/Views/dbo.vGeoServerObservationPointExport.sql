Drop View If Exists dbo.vGeoServerObservationPointExport
go

Create View dbo.vGeoServerObservationPointExport
as
Select
	OnlandVisualTrashAssessmentAreaName,
	a.OnlandVisualTrashAssessmentID,
	o.LocationPoint,
	o.Note,
	a.CompletedDate,
	Score.OnlandVisualTrashAssessmentScoreDisplayName as AssessmentScore,
	area.StormwaterJurisdictionID
from
	dbo.OnlandVisualTrashAssessmentObservation o join dbo.OnlandVisualTrashAssessment a
		on o.OnlandVisualTrashAssessmentID = a.OnlandVisualTrashAssessmentID
	join dbo.OnlandVisualTrashAssessmentArea area on a.OnlandVisualTrashAssessmentAreaID = area.OnlandVisualTrashAssessmentAreaID
	join dbo.OnlandVisualTrashAssessmentScore score on a.OnlandVisualTrashAssessmentScoreID = score.OnlandVisualTrashAssessmentScoreID
