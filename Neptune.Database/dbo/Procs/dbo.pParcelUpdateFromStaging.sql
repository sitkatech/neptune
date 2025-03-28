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

    insert into dbo.Parcel(ParcelID, ParcelNumber, ParcelAddress, ParcelCityState, ParcelZipCode, ParcelAreaInAcres, LastUpdate)
    select ps.ParcelStagingID, ps.ParcelNumber
    , case when len(ps.ParcelAddress) = 0 then null else ps.ParcelAddress end
    , case when len(ps.ParcelCityState) = 0 then null else ps.ParcelCityState end
    , case when len(ps.ParcelZipCode) = 0 then null else ps.ParcelZipCode end
    , ps.ParcelAreaInSquareFeet / 43560
    , GETUTCDATE()
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


    -- recalculate wqmp parcel intersections
    insert into dbo.WaterQualityManagementPlanParcel(WaterQualityManagementPlanID, ParcelID)
    select WaterQualityManagementPlanID, ParcelID
    from dbo.WaterQualityManagementPlanBoundary wqmp
    join dbo.ParcelGeometry p on wqmp.GeometryNative.STIntersects(p.GeometryNative)  = 1
    where wqmp.GeometryNative.STIntersection(p.GeometryNative).STArea() > 200
    order by WaterQualityManagementPlanID, ParcelID

end

GO