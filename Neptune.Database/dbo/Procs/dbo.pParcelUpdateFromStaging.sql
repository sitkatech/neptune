create procedure dbo.pParcelUpdateFromStaging
with execute as owner
as
begin


    delete from dbo.WaterQualityManagementPlanParcel
    truncate table dbo.ParcelGeometry
    delete from dbo.Parcel
    DBCC CHECKIDENT ('dbo.WaterQualityManagementPlanParcel', RESEED, 0);
    DBCC CHECKIDENT ('dbo.Parcel', RESEED, 0);

    update dbo.ParcelStaging set [Geometry] = [Geometry].MakeValid() where [Geometry].STIsValid() = 0

    set identity_insert dbo.Parcel on

    insert into dbo.Parcel(ParcelID, ParcelNumber, ParcelAddress, ParcelAreaInAcres)
    select ps.ParcelStagingID, ps.ParcelNumber, case when len(ps.ParcelAddress) = 0 then null else ps.ParcelAddress end, ps.ParcelStagingAreaSquareFeet / 43560
    from dbo.ParcelStaging ps
    where len(ps.ParcelNumber) > 0 and ps.[Geometry].STIsValid() = 1
    order by ps.ParcelStagingID, ps.ParcelNumber

    set identity_insert dbo.Parcel off

    insert into dbo.ParcelGeometry(ParcelID, GeometryNative)
    select p.ParcelID, ps.[Geometry]
    from dbo.ParcelStaging ps
    join dbo.Parcel p on ps.ParcelStagingID = p.ParcelID
    where len(ps.ParcelNumber) > 0 and ps.[Geometry].STIsValid() = 1
    order by ps.ParcelStagingID, ps.ParcelNumber

end

GO