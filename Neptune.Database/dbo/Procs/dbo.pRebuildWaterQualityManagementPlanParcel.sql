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
			from dbo.WaterQualityManagementPlanBoundary wqmp, dbo.ParcelGeometry p
			where wqmp.GeometryNative.STIntersection(p.GeometryNative).STArea() > @toleranceInSquareMeters -- tolerance here is even less, .05 acre
		) a
	)
end

GO