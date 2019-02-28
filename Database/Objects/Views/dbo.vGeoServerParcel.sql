if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerParcel'))
	drop view dbo.vGeoServerParcel
go

create view dbo.vGeoServerParcel
as
select
	p.ParcelID,
	p.ParcelNumber,
	p.ParcelGeometry,
	p.OwnerName,
	p.ParcelStreetNumber,
	p.ParcelAddress,
	p.ParcelZipCode,
	p.LandUse,
	isnull(p.ParcelAreaInAcres, 0) as ParcelArea,
	cast(case when w.ParcelID is null then 0 else 1 end as bit) as IsWQMPParcel
from dbo.Parcel p left join (select distinct parcelid from dbo.WaterQualityManagementPlanParcel) w on p.ParcelID = w.ParcelID
