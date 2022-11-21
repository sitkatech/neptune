drop view if exists dbo.vPyQgisLandUseBlockTGUInput
go

create view dbo.vPyQgisLandUseBlockTGUInput as
select 
	LandUseBlockID as LUBID,
	LandUseBlockGeometry
from dbo.LandUseBlock
Go