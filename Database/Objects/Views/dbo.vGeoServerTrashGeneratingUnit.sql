Drop view if exists dbo.vGeoServerTrashGeneratingUnit
Go

Create view dbo.vGeoServerTrashGeneratingUnit as
select
	TrashGeneratingUnitID,
	StormwaterJurisdictionID,
	TreatmentBMPID,
	OnlandVisualTrashAssessmentAreaID as AssessmentAreaID,
	LandUseBlockID,
	TrashGeneratingUnitGeometry
from dbo.TrashGeneratingUnit
Go