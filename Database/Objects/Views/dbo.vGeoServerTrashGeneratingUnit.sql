Drop view if exists dbo.vGeoServerTrashGeneratingUnit
Go

Create view dbo.vGeoServerTrashGeneratingUnit as
select
	TrashGeneratingUnitID,
	tgu.StormwaterJurisdictionID,
	case when tbmp.TreatmentBMPID is null then 'NotProvided' else tcs.TrashCaptureStatusTypeName end as TrashCaptureStatus,
	case when ovtaad.MostRecentAssessmentScore is null then 'NotProvided' else ovtaad.MostRecentAssessmentScore end as AssessmentScore,
	Case When lub.PriorityLandUseTypeID is null then 0 else 1 end as IsPriorityLandUse,
	Case when tgu.LandUseBlockID is null then 1 else 0 end as NoDataProvided,
	TrashGeneratingUnitGeometry,
	ovtaad.OnlandVisualTrashAssessmentAreaID,
	tbmp.TreatmentBMPID
from dbo.TrashGeneratingUnit tgu
	left join dbo.vOnlandVisualTrashAssessmentAreaDated ovtaad
		on tgu.OnlandVisualTrashAssessmentAreaID = ovtaad.OnlandVisualTrashAssessmentAreaID
	left join dbo.TreatmentBMP tbmp
		on tgu.TreatmentBMPID = tbmp.TreatmentBMPID
	left join dbo.TrashCaptureStatusType tcs
		on tcs.TrashCaptureStatusTypeID = tbmp.TrashCaptureStatusTypeID
	left join dbo.LandUseBlock lub
		on lub.LandUseBlockID = tgu.LandUseBlockID
Go