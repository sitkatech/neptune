if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerParcel'))
	drop view dbo.vGeoServerParcel
go

create view dbo.vGeoServerParcel
as
select ParcelID, ParcelNumber, ParcelGeometry, OwnerName, ParcelStreetNumber, ParcelAddress, ParcelZipCode, LandUse
from dbo.Parcel
