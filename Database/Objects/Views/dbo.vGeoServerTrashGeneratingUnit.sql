Drop view if exists dbo.vGeoServerTrashGeneratingUnit
Go

Create view dbo.vGeoServerTrashGeneratingUnit as
select
	TrashGeneratingUnitID,
	tgu.StormwaterJurisdictionID,
	tcs.TrashCaptureStatusTypeName as TrashCaptureStatus,
	ovtaad.MostRecentAssessmentScore as AssessmentScore,
	Case When lub.PriorityLandUseTypeID is null then 0 else 1 end as IsPriorityLandUse,
	TrashGeneratingUnitGeometry
from dbo.TrashGeneratingUnit tgu
	left join dbo.vOnlandVisualTrashAssessmentAreaDated ovtaad
		on tgu.OnlandVisualTrashAssessmentAreaID = ovtaad.OnlandVisualTrashAssessmentAreaID
	left join dbo.TreatmentBMP tbmp
		on tgu.TreatmentBMPID = tbmp.TreatmentBMPID
	join dbo.TrashCaptureStatusType tcs
		on tcs.TrashCaptureStatusTypeID = tbmp.TrashCaptureStatusTypeID
	left join dbo.LandUseBlock lub
		on lub.LandUseBlockID = tgu.LandUseBlockID
Go