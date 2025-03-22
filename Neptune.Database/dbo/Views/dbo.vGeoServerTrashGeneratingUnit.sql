Create view dbo.vGeoServerTrashGeneratingUnit as

Select 
	TrashGeneratingUnitID,
	TreatmentBMPID,
	TreatmentBMPName,
	TrashGeneratingUnitGeometry,
	StormwaterJurisdictionID,
	OrganizationID,
	OrganizationName,
	PriorityLandUseTypeDisplayName,
	PriorityLandUseTypeID,
	CurrentLoadingRate,
	ProgressLoadingRate,
	LoadingRateDelta,
	LandUseBlockID,
	TrashCaptureStatus,
	TrashCaptureStatusSortOrder,
	AssessmentScore,
	IsPriorityLandUse -- ALUs are not PLUs
from dbo.vTrashGeneratingUnitLoadStatistic

GO