IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.pParcelUpdateFromStaging'))
    drop procedure dbo.pParcelUpdateFromStaging
go

create procedure dbo.pParcelUpdateFromStaging
as
begin

    update dbo.ParcelStaging set ParcelStagingGeometry = ParcelStagingGeometry.MakeValid() where ParcelStagingGeometry.STIsValid() = 0

    insert into dbo.Parcel(ParcelNumber, ParcelGeometry, ParcelAddress, ParcelAreaInAcres)
    select ps.ParcelNumber, ps.ParcelStagingGeometry, case when len(ps.ParcelAddress) = 0 then null else ps.ParcelAddress end, ps.ParcelStagingAreaSquareFeet / 43560
    from dbo.ParcelStaging ps
    where len(ps.ParcelNumber) > 0 and ps.ParcelStagingGeometry.STIsValid() = 1
end

GO