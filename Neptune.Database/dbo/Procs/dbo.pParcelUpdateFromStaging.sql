create procedure dbo.pParcelUpdateFromStaging
with execute as owner
as
begin


    delete from dbo.WaterQualityManagementPlanParcel
    TRUNCATE TABLE dbo.Parcel
    DBCC CHECKIDENT ('dbo.WaterQualityManagementPlanParcel', RESEED, 0);

    update dbo.ParcelStaging set ParcelStagingGeometry = ParcelStagingGeometry.MakeValid() where ParcelStagingGeometry.STIsValid() = 0

    insert into dbo.Parcel(ParcelNumber, ParcelAddress, ParcelAreaInAcres)
    select ps.ParcelNumber, case when len(ps.ParcelAddress) = 0 then null else ps.ParcelAddress end, ps.ParcelStagingAreaSquareFeet / 43560
    from dbo.ParcelStaging ps
    where len(ps.ParcelNumber) > 0 and ps.ParcelStagingGeometry.STIsValid() = 1

    insert into dbo.ParcelGeometry(ParcelID, GeometryNative)
    select p.ParcelID, ps.ParcelStagingGeometry
    from dbo.ParcelStaging ps
    join dbo.Parcel p on ps.ParcelNumber = p.ParcelNumber
    where len(ps.ParcelNumber) > 0 and ps.ParcelStagingGeometry.STIsValid() = 1

end

GO