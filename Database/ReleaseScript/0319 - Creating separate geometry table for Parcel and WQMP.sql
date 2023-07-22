create table dbo.ParcelGeometry
(
    ParcelGeometryID int not null identity(1,1) constraint PK_ParcelGeometry_ParcelGeometryID primary key,
    ParcelID int not null constraint AK_ParcelGeometry_ParcelID unique constraint FK_ParcelGeometry_Parcel_ParcelID foreign key references dbo.Parcel(ParcelID),
    GeometryNative geometry not null,
    Geometry4326 geometry null
)


create table dbo.WaterQualityManagementPlanBoundary
(
    WaterQualityManagementPlanGeometryID int not null identity(1,1) constraint PK_WaterQualityManagementPlanGeometry_WaterQualityManagementPlanGeometryID primary key,
    WaterQualityManagementPlanID int not null constraint AK_WaterQualityManagementPlanGeometry_WaterQualityManagementPlanID unique constraint FK_WaterQualityManagementPlanGeometry_WaterQualityManagementPlan_WaterQualityManagementPlanID foreign key references dbo.WaterQualityManagementPlan(WaterQualityManagementPlanID),
    GeometryNative geometry null,
    Geometry4326 geometry null
)

DROP INDEX SPATIAL_Parcel_ParcelGeometry ON dbo.Parcel
DROP INDEX SPATIAL_Parcel_ParcelGeometry4326 ON dbo.Parcel
DROP INDEX SPATIAL_WaterQualityManagementPlan_WaterQualityManagementPlanBoundary ON dbo.WaterQualityManagementPlan
DROP INDEX SPATIAL_WaterQualityManagementPlan_WaterQualityManagementPlanBoundary4326 ON dbo.WaterQualityManagementPlan

CREATE SPATIAL INDEX SPATIAL_ParcelGeometry_GeometryNative ON dbo.ParcelGeometry
(
	GeometryNative
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(5993240, 2087140, 6207830, 2292500), 
CELLS_PER_OBJECT = 8)

CREATE SPATIAL INDEX SPATIAL_ParcelGeometry_Geometry4326 ON dbo.ParcelGeometry
(
	Geometry4326
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8)

CREATE SPATIAL INDEX SPATIAL_WaterQualityManagementPlanBoundary_GeometryNative ON dbo.WaterQualityManagementPlanBoundary
(
	GeometryNative
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(1830180, 637916, 1880090, 690975), 
CELLS_PER_OBJECT = 8)

CREATE SPATIAL INDEX SPATIAL_WaterQualityManagementPlanBoundary_Geometry4326 ON dbo.WaterQualityManagementPlanBoundary
(
	Geometry4326
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8)

GO

insert into dbo.ParcelGeometry(ParcelID, GeometryNative, Geometry4326)
select ParcelID, ParcelGeometry, ParcelGeometry4326
from dbo.Parcel



insert into dbo.WaterQualityManagementPlanBoundary(WaterQualityManagementPlanID, GeometryNative, Geometry4326)
select WaterQualityManagementPlanID, WaterQualityManagementPlanBoundary, WaterQualityManagementPlanBoundary4326
from dbo.WaterQualityManagementPlan
where WaterQualityManagementPlanBoundary is not null

alter table dbo.Parcel drop column ParcelGeometry
alter table dbo.Parcel drop column ParcelGeometry4326

alter table dbo.WaterQualityManagementPlan drop column WaterQualityManagementPlanBoundary
alter table dbo.WaterQualityManagementPlan drop column WaterQualityManagementPlanBoundary4326