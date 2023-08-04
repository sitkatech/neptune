create view dbo.vPyQgisLandUseBlockTGUInput as
select 
	LandUseBlockID as LUBID,
    StormwaterJurisdictionID as SJID,
	LandUseBlockGeometry
from dbo.LandUseBlock
Go