if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerParcel'))
	drop view dbo.vGeoServerParcel
go

create view dbo.vGeoServerParcel
as
select
	p.ParcelID,
	p.ParcelNumber,
	p.ParcelGeometry4326 as ParcelGeometry,
	p.OwnerName,
	p.ParcelStreetNumber,
	p.ParcelAddress,
	p.ParcelZipCode,
	p.LandUse,
	isnull(p.ParcelAreaInAcres, 0) as ParcelArea,
	Case when WQMPCount is null then 0 else WQMPCount end as WQMPCount
from dbo.Parcel p left join (select ParcelID, count(*) as WQMPCount from dbo.WaterQualityManagementPlanParcel group by ParcelID) w on p.ParcelID = w.ParcelID