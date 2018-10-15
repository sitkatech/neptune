ALTER TABLE dbo.Parcel ADD SquareFeetHome int, SquareFeetLot int, ParcelAreaInAcres float;
GO

if exists(
	select wp.WaterQualityManagementPlanID, p.ParcelNumber
	from dbo.WaterQualityManagementPlanParcel wp
	join dbo.Parcel p on wp.ParcelID = p.ParcelID

	except

	select *
	from
	(
	select 6 as WaterQualityManagementPlanID, '374-401-03' as ParcelNumber
	union all
	select 6 as WaterQualityManagementPlanID, '374-401-05' as ParcelNumber
	union all
	select 6 as WaterQualityManagementPlanID, '374-401-07' as ParcelNumber
	union all
	select 10 as WaterQualityManagementPlanID, '375-361-05' as ParcelNumber
	) a
)
begin
	raiserror('Found new parcels associated to WQMPs that we need to remap', 16, 1)
end

delete from dbo.WaterQualityManagementPlanParcel

delete from dbo.Parcel

DBCC CHECKIDENT ('dbo.Parcel', RESEED, 0)
GO

insert into dbo.Parcel(ParcelNumber, ParcelGeometry, OwnerName, ParcelAddress, ParcelZipCode, LandUse, TenantID, ParcelStreetNumber, SquareFeetHome, SquareFeetLot, ParcelAreaInAcres)
select ltrim(rtrim(ll.ASSESSMENT_NO)) as ParcelNumber, ll.GEOM as ParcelGeometry, ltrim(rtrim(ll.OWNER_NAMES)) as OwnerName, ltrim(rtrim(ll.SITE_ADDRESS)) as ParcelAddress, ll.SITE_ZIP5 as ParcelZipCode, ll.USE_DQ_LANDUSE as LandUse, 2 as TenantID, case when DATALENGTH(ll.SITE_ADDR_NO) > 0 then ltrim(rtrim(ll.SITE_ADDR_NO)) else null end as ParcelStreetNumber
, [SQFT_HOME] as SquareFeetHome, [SQFT_LOT] as SquareFeetLot, ll2.SHAPE_Area * 0.0002471053821147119 as ParcelAreaInAcres
from dbo.LEGAL_LOTS_ATTRIBUTES_4326 ll
join dbo.OCParcelAreas ll2 on ll.OBJECTID = ll2.OBJECTID
where ll.GEOM is not null and ll.ASSESSMENT_NO is not null
order by ll.ASSESSMENT_NO

insert into dbo.WaterQualityManagementPlanParcel(TenantID, WaterQualityManagementPlanID, ParcelID)
select 2 as TenantID, 6 as WaterQualityManagementPlanID, p.ParcelID
from dbo.Parcel p
where p.ParcelNumber in
(
'374-401-03',
'374-401-05',
'374-401-07'
)

insert into dbo.WaterQualityManagementPlanParcel(TenantID, WaterQualityManagementPlanID, ParcelID)
select 2 as TenantID, 10 as WaterQualityManagementPlanID, p.ParcelID
from dbo.Parcel p
where p.ParcelNumber in
(
'375-361-05'
)

drop table dbo.OCParcelAreas

