IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.pRebuildTrashGeneratingUnitTable')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE dbo.pRebuildTrashGeneratingUnitTable
GO
Create Procedure dbo.pRebuildTrashGeneratingUnitTable
as
print concat('Begin pRebuildTrashGeneratingUnitTable ', convert(varchar, GetDate(), 121) )

/* 0. Preprocess the land use blocks table with "sentinel" rows indicating where there is no priority land use block data. */
print concat('Begin rebuild adjusted land use blocks ', convert(varchar, GetDate(), 121) )
DROP TABLE IF EXISTS #LandUseBlocksAdjusted

Select * into #LandUseBlocksAdjusted
from
(select
	LandUseBlockID,
	PriorityLandUseTypeID,
	LandUseDescription,
	LandUseBlockGeometry
from LandUseBlock
	--select count(*) from StormwaterJurisdiction

union all

SELECT
	Null as LandUseBlockID,
	Null as PriorityLandUseTypeID,
	Null as LandUseDescription,
	case when UGeometry is null then sj.StormwaterJurisdictionGeometry else sj.StormwaterJurisdictionGeometry.STDifference(sq.UGeometry) end AS LandUseBlockGeometry
FROM
(
  SELECT GEOMETRY::UnionAggregate(lub.LandUseBlockGeometry) AS UGeometry, sj.StormwaterJurisdictionID AS StormwaterJurisdictionID
  FROM dbo.StormwaterJurisdiction sj
  left JOIN dbo.LandUseBlock lub
  ON sj.StormwaterJurisdictionGeometry.STIntersects(lub.LandUseBlockGeometry) = 1
  GROUP BY sj.StormwaterJurisdictionID
) sq
 LEFT JOIN dbo.StormwaterJurisdiction sj ON sq.StormwaterJurisdictionID = sj.StormwaterJurisdictionID) s

print concat('End rebuild adjusted land use blocks ', convert(varchar, GetDate(), 121) )

/* 1. Produce the Jurisdiction-Delineation-OVTA layer using the BCF algorithm */

print concat('Begin BCF algorithm ', convert(varchar, GetDate(), 121) )

-- StormwaterJurisdiction will serve as our "Background Layer" for the Boundary/Clip phase
declare @BackgroundLayer geometry;
select @BackgroundLayer = geometry::UnionAggregate(StormwaterJurisdictionGeometry) from StormwaterJurisdiction;
--select @BackgroundLayer;

print concat('Begin Boundary ', convert(varchar, GetDate(), 121) )

declare @BufferDelta decimal(11,11) = .000000001;

-- Assemble the boundary layer from the Delineation and OVTAA layers
Declare @BoundaryLayer geometry;
Select @BoundaryLayer = geometry::UnionAggregate(BoundaryGeometry) from (

select OnlandVisualTrashAssessmentAreaGeometry.STBoundary().STBuffer(@BufferDelta) as BoundaryGeometry from vOnlandVisualTrashAssessmentAreaDated
union all
Select DelineationGeometry.STBoundary().STBuffer(@BufferDelta) as BoundaryGeometry from Delineation where Delineation.IsVerified = 1
Union all
Select StormwaterJurisdictionGeometry.STBoundary().STBuffer(@BufferDelta) as BoundaryGeometry from StormwaterJurisdiction
) q
print concat('End Boundary ', convert(varchar, GetDate(), 121) )


-- Clip

print concat('Begin clip ', convert(varchar, GetDate(), 121) )

Declare @ClipLayer geometry;
Select @ClipLayer = @BackgroundLayer.STDifference(@BoundaryLayer);

print concat('End clip ', convert(varchar, GetDate(), 121) )

-- Flatten

print concat('Begin flatten multipoly ', convert(varchar, GetDate(), 121) )

DROP TABLE IF EXISTS #TempNumbers
DROP TABLE IF EXISTS #JurisdictionDelineationOvta

Declare @Num int = @ClipLayer.STNumGeometries()
    -- see https://alastaira.wordpress.com/2011/01/21/splitting-multi-geometries-into-single-geometries/
    -- and https://stackoverflow.com/questions/1393951/what-is-the-best-way-to-create-and-populate-a-numbers-table
    SELECT TOP (@Num) IDENTITY(int,1,1) AS n
    INTO #TempNumbers
    FROM MASTER..spt_values a, MASTER..spt_values b;

Select @ClipLayer.STGeometryN(n) as Geom
    into #JurisdictionDelineationOvta
    from #TempNumbers

print concat('End flatten multipoly ', convert(varchar, GetDate(), 121) )

print concat('End BCF algorithm ', convert(varchar, GetDate(), 121) )
	
/* 2. Jurisdiction, Delineation, and Assessment Area data are recovered by joining back to those tables */

-- do an alter table #JurisdictionDelineationOVTA to add a JurisdictionID, TreatmentBMPID (which will take the place of DelineationID), and OVTAAID
Alter Table #JurisdictionDelineationOvta
Add JdoID int not null identity (1,1), 
JurisdictionID int null,
DelineationID int null,
OnlandVisualTrashAssessmentAreaID int null

print concat('Begin set jurisdiction ', convert(varchar, GetDate(), 121) )

--set the jurisdiction ID
Update jdo
set jdo.JurisdictionID = sj.StormwaterJurisdictionID
from #JurisdictionDelineationOvta jdo join StormwaterJurisdiction sj on jdo.Geom.STIntersects(sj.StormwaterJurisdictionGeometry) = 1

print concat('End set jurisdiction ', convert(varchar, GetDate(), 121) )

-- set the ovta area ID. Requires a sub-query to select the most recent OVTA from the pullback

print concat('Begin set OVTA Area ', convert(varchar, GetDate(), 121) )

Update jdo
set jdo.OnlandVisualTrashAssessmentAreaID = x.OnlandVisualTrashAssessmentAreaID
from #JurisdictionDelineationOvta jdo join (
	select
		JdoId, ovta.OnlandVisualTrashAssessmentAreaID,
		rowNumber = ROW_NUMBER() over (partition by jdoid order by ovta.MostRecentAssessmentDate desc)
	from #JurisdictionDelineationOvta jdo
		join vOnlandVisualTrashAssessmentAreaDated ovta
			on jdo.Geom.STIntersects(ovta.OnlandVisualTrashAssessmentAreaGeometry) = 1
) x on jdo.JdoId = x.JdoId
where
	x.rowNumber = 1

print concat('End set OVTA Area ', convert(varchar, GetDate(), 121) )


-- set the treatment bmp id. Requires a sub-query to select the most TC-effective BMP from the pullback
print concat('Begin set Delineation ', convert(varchar, GetDate(), 121) )

update jdo
set jdo.DelineationID = x.DelineationID
from #JurisdictionDelineationOvta jdo join (
	select
		t.TreatmentBMPID, d.DelineationID, d.DelineationGeometry, tcs.TrashCaptureStatusTypePriority, jdo.JdoID,
		rowNumber = ROW_NUMBER() over (partition by jdo.JdoID order by tcs.TrashCaptureStatusTypePriority asc, t.TrashCaptureEffectiveness desc)
	from
		Delineation d inner join TreatmentBMP t
			on d.TreatmentBMPID = t.TreatmentBMPID
		join TrashCaptureStatusType tcs
			on t.TrashCaptureStatusTypeID = tcs.TrashCaptureStatusTypeID
		join #JurisdictionDelineationOvta jdo
			on jdo.Geom.STIntersects(d.DelineationGeometry) = 1
	where d.IsVerified = 1
) x on jdo.JdoId = x.JdoId
where x.rowNumber = 1

print concat('End set Delineation ', convert(varchar, GetDate(), 121) )


/*3. Delete all existing TrashGeneratingUnit */

print concat('Begin delete all TGUs ', convert(varchar, GetDate(), 121) )
Delete from dbo.TrashGeneratingUnit
print concat('End delete all TGUs ', convert(varchar, GetDate(), 121) )

/*4. PLU data is integrated by joining out to the LandUseBlock table; result set is inserted directly to TGU table (release script 153) */

print concat('Begin set Land Use Block and reinsert ', convert(varchar, GetDate(), 121) )

Insert into dbo.TrashGeneratingUnit (
	StormwaterJurisdictionID,
	DelineationID,
	OnlandVisualTrashAssessmentAreaID,
	LandUseBlockID,
	TrashGeneratingUnitGeometry,
	LastUpdateDate
)
select
	jdo.JurisdictionID,
	jdo.DelineationID,
	jdo.OnlandVisualTrashAssessmentAreaID,
	lub.LandUseBlockID,
	jdo.Geom.STIntersection(lub.LandUseBlockGeometry),
	GetDate()
from
	#JurisdictionDelineationOvta jdo join #LandUseBlocksAdjusted lub
		on jdo.Geom.STIntersects(lub.LandUseBlockGeometry) = 1

print concat('End set Land Use Block and reinsert ', convert(varchar, GetDate(), 121) )
print concat('End pRebuildTrashGeneratingUnitTable ', convert(varchar, GetDate(), 121) )

GO