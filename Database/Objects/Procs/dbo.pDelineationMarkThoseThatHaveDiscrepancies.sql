IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.pDelineationMarkThoseThatHaveDiscrepancies')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE dbo.pDelineationMarkThoseThatHaveDiscrepancies
GO
Create Procedure dbo.pDelineationMarkThoseThatHaveDiscrepancies
as

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
	join dbo.NetworkCatchment nc on d.DelineationGeometry.STIntersects(nc.CatchmentGeometry) = 1
	where d.DelineationTypeID = 1 and tbt.DelineationShouldBeReconciled = 1
	group by d.TreatmentBMPID
) a on d.TreatmentBMPID = a.TreatmentBMPID
where d.DelineationTypeID = 1 and d.DelineationGeometry.STDifference(a.NetworkCatchmentsIntesectedByDelineationGeometry).STArea() > 400

GO
