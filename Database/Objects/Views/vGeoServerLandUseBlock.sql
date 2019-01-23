if exists (select * from dbo.sysobjects where id = object_id('dbo. vGeoServerLandUseBlock '))
	drop view dbo.vGeoServerParcel
go

create view  dbo . vGeoServerLandUseBlock 
as
select
	LandUseBlockID
      ,PriorityLandUseTypeID
      ,LandUseDescription
      ,LandUseBlockGeometry

from dbo.LandUseBlock
GO
