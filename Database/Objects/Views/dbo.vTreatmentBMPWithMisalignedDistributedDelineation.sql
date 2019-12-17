Drop view if exists dbo.vTreatmentBMPWithMisalignedDistributedDelineation
GO

Create View dbo.vTreatmentBMPWithMisalignedDistributedDelineation
as

select 	tb.TreatmentBMPID as PrimaryKey, tb.TreatmentBMPID, tb.TreatmentBMPName, tbt.TreatmentBMPTypeID, tbt.TreatmentBMPTypeName, d.DelineationID, d.DelineationTypeID, dt.DelineationTypeDisplayName, d.DelineationGeometry.STArea() as DelineationArea, d.DateLastModified, d.DateLastVerified
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
join dbo.TreatmentBMP tb on a.TreatmentBMPID = tb.TreatmentBMPID
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.Delineation d on a.TreatmentBMPID = d.TreatmentBMPID
join dbo.DelineationType dt on d.DelineationTypeID = dt.DelineationTypeID


Go

-- select * from dbo.vTreatmentBMPWithMisalignedDistributedDelineation