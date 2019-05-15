Drop view if exists dbo.vGeoServerTrashGeneratingUnit
Go

Create view dbo.vGeoServerTrashGeneratingUnit as
select
	TrashGeneratingUnitID,
	tgu.StormwaterJurisdictionID,
	o.OrganizationID,
	o.OrganizationName,
	case when tbmp.TreatmentBMPID is null then 'NotProvided' else tcs.TrashCaptureStatusTypeName end as TrashCaptureStatus,
	case when ovtaad.MostRecentAssessmentScore is null then 'NotProvided' else ovtaad.MostRecentAssessmentScore end as AssessmentScore,
	Case when tgu.LandUseBlockID is null then 0 when plut.PriorityLandUseTypeName = 'ALU' then 0 else 1 end as IsPriorityLandUse, -- ALUs are not PLUs
	Case when tgu.LandUseBlockID is null then 1 else 0 end as NoDataProvided,
	TrashGeneratingUnitGeometry,
	ovtaad.OnlandVisualTrashAssessmentAreaID,
	tbmp.TreatmentBMPID,
	tbmp.TreatmentBMPName,
	plut.PriorityLandUseTypeDisplayName as LandUseType,
	tgu.LastUpdateDate as LastCalculatedDate
from dbo.TrashGeneratingUnit tgu
	left join dbo.vOnlandVisualTrashAssessmentAreaDated ovtaad
		on tgu.OnlandVisualTrashAssessmentAreaID = ovtaad.OnlandVisualTrashAssessmentAreaID
	left join dbo.TreatmentBMP tbmp
		on tgu.TreatmentBMPID = tbmp.TreatmentBMPID
	left join dbo.TrashCaptureStatusType tcs
		on tcs.TrashCaptureStatusTypeID = tbmp.TrashCaptureStatusTypeID
	left join dbo.LandUseBlock lub
		on lub.LandUseBlockID = tgu.LandUseBlockID
	left join dbo.PriorityLandUseType plut
		on lub.PriorityLandUseTypeID = plut.PriorityLandUseTypeID
	left join dbo.StormwaterJurisdiction sj
		on tgu.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
	left join dbo.Organization o
		on o.OrganizationID = sj.OrganizationID
Go