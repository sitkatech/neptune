create view  dbo.vGeoServerLandUseBlock 
as
select
	LandUseBlockID,
	PriorityLandUseTypeID,
	LandUseDescription,
    PermitTypeID,
	LandUseBlockGeometry4326 as LandUseBlockGeometry 
from dbo.LandUseBlock
GO
