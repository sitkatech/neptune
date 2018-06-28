create table dbo.Parcel
(
	ParcelID int not null identity(1,1) constraint PK_Parcel_ParcelID primary key,
	TenantID int not null constraint FK_Parcel_Tenant_TenantID foreign key references dbo.Tenant(TenantID),
	ParcelNumber varchar(22) not null,-- constraint AK_Parcel_ParcelNumber unique,
	ParcelGeometry geometry not null,
	OwnerName varchar(100) null,
	ParcelStreetNumber varchar(10) null,
	ParcelAddress varchar(150) null,
	ParcelZipCode varchar(5) null,
	LandUse varchar(4) null
)
GO

insert into dbo.Parcel(ParcelNumber, ParcelGeometry, OwnerName, ParcelAddress, ParcelZipCode, LandUse, TenantID, ParcelStreetNumber)
select ltrim(rtrim(ll.ASSESSMENT_NO)) as ParcelNumber, ll.GEOM as ParcelGeometry, ltrim(rtrim(ll.OWNER_NAMES)) as OwnerName, ltrim(rtrim(ll.SITE_ADDRESS)) as ParcelAddress, ll.SITE_ZIP5 as ParcelZipCode, ll.USE_DQ_LANDUSE as LandUse, 2 as TenantID, case when DATALENGTH(ll.SITE_ADDR_NO) > 0 then ltrim(rtrim(ll.SITE_ADDR_NO)) else null end as ParcelStreetNumber
from dbo.LEGAL_LOTS_ATTRIBUTES_4326 ll
where ll.GEOM is not null and ASSESSMENT_NO is not null
order by ll.ASSESSMENT_NO


--create table dbo.ParcelGeometry
--(
--	ParcelGeometryID int not null identity(1,1) constraint PK_ParcelGeometry_ParcelGeometryID primary key,
--	ParcelID int not null constraint FK_ParcelGeometry_Parcel_ParcelID foreign key references dbo.Parcel (ParcelID),
--	ParcelGeometry geometry not null
--)

--insert into dbo.Parcel(ParcelNumber, OwnerName, ParcelAddress, ParcelZipCode, LandUse)
--select ll.ASSESSMENT_NO as ParcelNumber, ll.OWNER_NAMES as OwnerName, ll.SITE_ADDRESS as ParcelAddress, ll.SITE_ZIP5 as ParcelZipCode, ll.USE_DQ_LANDUSE as LandUse
--from dbo.LEGAL_LOTS_ATTRIBUTES_4326 ll
--where ll.GEOM is not null and ASSESSMENT_NO is not null
--order by ll.ASSESSMENT_NO


--insert into dbo.ParcelGeometry(ParcelID, ParcelGeometry)
--select p.ParcelID, ll.GEOM as ParcelGeometry
--from dbo.LEGAL_LOTS_ATTRIBUTES_4326 ll
--join dbo.Parcel p on ll.ASSESSMENT_NO = p.ParcelNumber
--where ll.GEOM is not null and ASSESSMENT_NO is not null
--order by ll.ASSESSMENT_NO