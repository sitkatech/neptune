Create View dbo.vGeoServerObservationPointExport
as
Select
	OnlandVisualTrashAssessmentAreaName as OVTAAreaName,
	a.OnlandVisualTrashAssessmentID as AssessmentID,
	o.LocationPoint,
	o.Note,
	--The third argument in CONVERT defines the style for the date
	--107 produces the date in the format Mon dd, yyyy
	--Consult https://docs.microsoft.com/en-us/sql/t-sql/functions/cast-and-convert-transact-sql?view=sql-server-ver15 if a new format is desired
	CONVERT(VARCHAR(12), a.CompletedDate, 107) CompletedDate,
	Score.OnlandVisualTrashAssessmentScoreDisplayName as Score,
	area.StormwaterJurisdictionID as JurisID,
	org.OrganizationName as JurisName,
	case
		when fr.FileResourceGUID is not null then concat( '/FileResource/DisplayResource/', fr.FileResourceGUID) 
		else null
	end as PhotoUrl
from
	dbo.OnlandVisualTrashAssessmentObservation o join dbo.OnlandVisualTrashAssessment a
		on o.OnlandVisualTrashAssessmentID = a.OnlandVisualTrashAssessmentID
	join dbo.OnlandVisualTrashAssessmentArea area on a.OnlandVisualTrashAssessmentAreaID = area.OnlandVisualTrashAssessmentAreaID
	join dbo.OnlandVisualTrashAssessmentScore score on a.OnlandVisualTrashAssessmentScoreID = score.OnlandVisualTrashAssessmentScoreID
	left join dbo.StormwaterJurisdiction sj
		on area.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
	left join dbo.Organization org
		on sj.OrganizationID = org.OrganizationID
	left join dbo.OnlandVisualTrashAssessmentObservationPhoto photo
		on photo.OnlandVisualTrashAssessmentObservationID = o.OnlandVisualTrashAssessmentObservationID
	left join dbo.FileResource fr
		on fr.FileResourceID = photo.FileResourceID
