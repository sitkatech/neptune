IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.pDelineationMarkThoseThatHaveDiscrepancies')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE dbo.pDelineationMarkThoseThatHaveDiscrepancies
GO
Create Procedure dbo.pDelineationMarkThoseThatHaveDiscrepancies
as

-- re-running job so first set all to not have discrepancies
update d
set d.HasDiscrepancies = 0
from dbo.Delineation d
join dbo.TreatmentBMP tb on d.TreatmentBMPID = tb.TreatmentBMPID
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
where tbt.DelineationShouldBeReconciled = 1

-- Distributed delineations can only intersect one Network Catchment; flag those that have more than 1
update d
set d.HasDiscrepancies = 1
from
(
	select TreatmentBMPID
	from
	(
		-- we are doing this inner view on purpose because sql query slows down significantly if we try to do the group by all at once
		select tb.TreatmentBMPID, nc.NetworkCatchmentID
		from dbo.Delineation d
		join dbo.TreatmentBMP tb on d.TreatmentBMPID = tb.TreatmentBMPID
		join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
		join dbo.NetworkCatchment nc on d.DelineationGeometry.STIntersects(nc.CatchmentGeometry) = 1
		where d.DelineationTypeID = 2 and tbt.DelineationShouldBeReconciled = 1
	) intersections
	group by TreatmentBMPID having count(*) > 1
) a
join dbo.Delineation d on a.TreatmentBMPID = d.TreatmentBMPID


-- Centralized delineations need to fully contain all the Network Catchments they intersect; flag those that have a tolerance of greater than 400 sq ft
update d
set d.HasDiscrepancies = 1
from dbo.Delineation d
join (
	select d.TreatmentBMPID, geometry::UnionAggregate(nc.CatchmentGeometry) as NetworkCatchmentsIntesectedByDelineationGeometry
	from dbo.Delineation d
	join dbo.TreatmentBMP tb on d.TreatmentBMPID = tb.TreatmentBMPID
	join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
	join dbo.NetworkCatchment nc on d.DelineationGeometry.STIntersection(nc.CatchmentGeometry).STArea() > 400
	where d.DelineationTypeID = 1 and tbt.DelineationShouldBeReconciled = 1
	group by d.TreatmentBMPID
) a on d.TreatmentBMPID = a.TreatmentBMPID
where d.DelineationTypeID = 1 and d.DelineationGeometry.STSymDifference(a.NetworkCatchmentsIntesectedByDelineationGeometry).STArea() > 400


truncate table dbo.DelineationOverlap;

with distributedBMPs (DelineationID, DelineationGeometry)
as
(
		select d.DelineationID, d.DelineationGeometry
		from dbo.Delineation d
		join dbo.TreatmentBMP tb on d.TreatmentBMPID = tb.TreatmentBMPID
		join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
		where d.DelineationTypeID = 2 and tbt.DelineationShouldBeReconciled = 1
)


insert into dbo.DelineationOverlap(DelineationOverlapID, DelineationID, OverlappingDelineationID, OverlappingGeometry)
select row_number() over(order by a.DelineationID) as DelineationOverlapID,  DelineationID, OverlappingDelineationID, OverlappingGeometry
from
(
	-- again, intentionally having this in an inner query; it is much faster to have it stintersects both sets and remove self intersections after
	select d1.DelineationID, d2.DelineationID as OverlappingDelineationID, d1.DelineationGeometry.STIntersection(d2.DelineationGeometry) as OverlappingGeometry
	from distributedBMPs d1, distributedBMPs d2
	where d1.DelineationGeometry.STIntersection(d2.DelineationGeometry).STArea() > 400
) a
where a.DelineationID != a.OverlappingDelineationID and OverlappingGeometry.STArea() > 0

GO
