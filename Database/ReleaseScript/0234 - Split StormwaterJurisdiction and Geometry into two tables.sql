create table dbo.StormwaterJurisdictionGeometry
(
	StormwaterJurisdictionGeometryID int not null identity(1,1) constraint PK_StormwaterJurisdictionGeometry_StormwaterJurisdictionGeometryID primary key,
	StormwaterJurisdictionID int not null constraint AK_StormwaterJurisdictionGeometry_StormwaterJurisdictionID unique constraint FK_StormwaterJurisdictionGeometry_StormwaterJurisdiction_StormwaterJurisdictionID foreign key references dbo.StormwaterJurisdiction(StormwaterJurisdictionID),
	GeometryNative geometry not null,
	Geometry4326 geometry not null
)
GO

insert into dbo.StormwaterJurisdictionGeometry(StormwaterJurisdictionID, GeometryNative, Geometry4326)
select StormwaterJurisdictionID, StormwaterJurisdictionGeometry, StormwaterJurisdictionGeometry4326
from dbo.StormwaterJurisdiction
order by StormwaterJurisdictionID

CREATE SPATIAL INDEX SPATIAL_StormwaterJurisdictionGeometry_GeometryNative ON dbo.StormwaterJurisdictionGeometry
(
	GeometryNative
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8)
GO


DROP INDEX SPATIAL_StormwaterJurisdiction_StormwaterJurisdictionGeometry ON dbo.StormwaterJurisdiction
GO

alter table dbo.StormwaterJurisdiction drop column IsTransportationJurisdiction
alter table dbo.StormwaterJurisdiction drop column StormwaterJurisdictionGeometry
alter table dbo.StormwaterJurisdiction drop column StormwaterJurisdictionGeometry4326