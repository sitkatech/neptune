if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerParcel'))
	drop view dbo.vGeoServerParcel
go

create view dbo.vGeoServerParcel
as
select
	ParcelID,
	ParcelNumber,
	ParcelGeometry,
	OwnerName,
	ParcelStreetNumber,
	ParcelAddress,
	ParcelZipCode,
	LandUse,
	isnull(ParcelAreaInAcres, 0) as ParcelArea 

from dbo.Parcel
