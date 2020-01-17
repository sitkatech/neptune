alter table dbo.TreatmentBMP drop constraint FK_TreatmentBMP_Watershed_WatershedID
GO

update dbo.TreatmentBMP
set WatershedID = null


exec sp_rename 'dbo.PK_Watershed_WatershedID', 'PK_DroolToolWatershed_DroolToolWatershedID', 'OBJECT';
exec sp_rename 'dbo.Watershed.WatershedID', 'DroolToolWatershedID', 'COLUMN';
exec sp_rename 'dbo.Watershed.WatershedGeometry', 'DroolToolWatershedGeometry', 'COLUMN';
exec sp_rename 'dbo.Watershed.WatershedName', 'DroolToolWatershedName', 'COLUMN';
exec sp_rename 'dbo.Watershed.WatershedGeometry4326', 'DroolToolWatershedGeometry4326', 'COLUMN';
exec sp_rename 'dbo.Watershed', 'DroolToolWatershed';
GO

CREATE TABLE dbo.Watershed
(
	WatershedID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Watershed_WatershedID PRIMARY KEY,
	WatershedGeometry geometry NULL,
	WatershedName varchar(50) NULL,
	WatershedGeometry4326 geometry NULL
)
GO

insert into dbo.Watershed(WatershedName, WatershedGeometry, WatershedGeometry4326)
select Watershed, geometry::UnionAggregate(CatchmentGeometry) as WatershedGeometry, geometry::UnionAggregate(CatchmentGeometry4326) as WatershedGeometry4326
from dbo.RegionalSubbasin rs
group by Watershed
order by Watershed


update t
set t.WatershedID = l.WatershedID
from dbo.TreatmentBMP t
left join dbo.Watershed l on t.LocationPoint.STIntersects(l.WatershedGeometry) = 1

alter table dbo.TreatmentBMP add constraint FK_TreatmentBMP_Watershed_WatershedID foreign key (WatershedID) references dbo.Watershed(WatershedID)