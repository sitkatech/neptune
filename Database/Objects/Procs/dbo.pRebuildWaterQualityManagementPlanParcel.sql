IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.pRebuildWaterQualityManagementPlanParcel'))
    drop procedure dbo.pRebuildWaterQualityManagementPlanParcel
go

create procedure dbo.pRebuildWaterQualityManagementPlanParcel
as
begin
	-- same tolerance as in pDelineationMarkThoseThatHaveDiscrepancies
	declare @toleranceInSquareMeters int
	select @toleranceInSquareMeters = 200

	insert into dbo.WaterQualityManagementPlanParcel (WaterQualityManagementPlanID, ParcelID) 
	(
		select WaterQualityManagementPlanID, ParcelID
		from
		(
			-- again, intentionally having this in an inner query; it is much faster to have it stintersects both sets
			select wqmp.WaterQualityManagementPlanID, p.ParcelID
			from dbo.WaterQualityManagementPlan wqmp, dbo.Parcel p
			where wqmp.WaterQualityManagementPlanBoundary.STIntersection(p.ParcelGeometry).STArea() > @toleranceInSquareMeters -- tolerance here is even less, .05 acre
		) a
	)
end

GO