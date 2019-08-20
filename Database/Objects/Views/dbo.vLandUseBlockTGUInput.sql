drop view if exists dbo.vLandUseBlockTGUInput
go

create view dbo.vLandUseBlockTGUInput as
select 
	LandUseBlockID,
	PriorityLandUseTypeID,
	LandUseDescription,
	LandUseBlockGeometry,
	TrashGenerationRate,
	LandUseForTGR,
	MedianHouseholdIncomeResidential,
	MedianHouseholdIncomeRetail,
	StormwaterJurisdictionID,
	PermitTypeID
from dbo.LandUseBlock
Go