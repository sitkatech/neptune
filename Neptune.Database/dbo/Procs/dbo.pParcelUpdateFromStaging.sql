create procedure dbo.pParcelUpdateFromStaging
with execute as owner
as
begin


    -- NPT-1140: the caller (Parcels.SwapFromStagingAsync) runs this inside a transaction and
    -- verifies the result before committing, so a failure anywhere leaves the old parcels in place.
    -- Staging rows are already repaired and projected to 4326 in C#, so nothing is filtered here.
    set xact_abort on

    delete from dbo.WaterQualityManagementPlanParcel
    truncate table dbo.ParcelGeometry
    delete from dbo.Parcel

    set identity_insert dbo.Parcel on

    insert into dbo.Parcel(ParcelID, ParcelNumber, ParcelAddress, ParcelCityState, ParcelZipCode, ParcelAreaInAcres, LastUpdate)
    select ps.ParcelStagingID, ps.ParcelNumber
    , case when len(ps.ParcelAddress) = 0 then null else ps.ParcelAddress end
    , case when len(ps.ParcelCityState) = 0 then null else ps.ParcelCityState end
    , case when len(ps.ParcelZipCode) = 0 then null else ps.ParcelZipCode end
    , ps.ParcelAreaInSquareFeet / 43560
    , GETUTCDATE()
    from dbo.ParcelStaging ps
    order by ps.ParcelStagingID, ps.ParcelNumber

    set identity_insert dbo.Parcel off

    insert into dbo.ParcelGeometry(ParcelID, GeometryNative, Geometry4326)
    select p.ParcelID, ps.[Geometry], ps.Geometry4326
    from dbo.ParcelStaging ps
    join dbo.Parcel p on ps.ParcelStagingID = p.ParcelID
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