IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.pRebuildTrashGeneratingUnitTableRelative')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE dbo.pRebuildTrashGeneratingUnitTableRelative
GO

Create Procedure dbo.pRebuildTrashGeneratingUnitTableRelative
	@ObjectIDs varchar(max),
	@ObjectType varchar(max)
as

/*-2. Identify the TGUs that are made dirty by an update to the given Delineation. Save the geom of their unions and delete them */
Declare @SpliceSeed Geometry;
Declare @SpliceBase Geometry;
Select @SpliceSeed = dbo.fGetTGUInputGeometry(@ObjectIDs, @ObjectType);

Select @SpliceBase = geometry::UnionAggregate(TrashGeneratingUnitGeometry) from dbo.TrashGeneratingUnit where TrashGeneratingUnitGeometry.STIntersects(@SpliceSeed) = 1

-- if for some reason no TGUs were found there, don't die
if @SpliceBase is null
	Select @SpliceBase = @SpliceSeed;

Select TrashGeneratingUnitID into #ToDelete
from dbo.TrashGeneratingUnit where TrashGeneratingUnitGeometry.STIntersects(@SpliceBase) = 1
Delete from dbo.TrashGeneratingUnit where TrashGeneratingUnitID in (select * from #ToDelete)

/*-1. Restrict the input layers */

Select 
	LandUseBlockID,
	PriorityLandUseTypeID,
	LandUseDescription,
	@SpliceBase.STIntersection(LandUseBlockGeometry) as LandUseBlockGeometry
into #LandUseBlocksRestricted
from dbo.LandUseBlock
where @SpliceBase.STIntersects(LandUseBlockGeometry) = 1


Select 
	StormwaterJurisdictionID,
	OrganizationID,
	@SpliceBase.STIntersection(StormwaterJurisdictionGeometry) as StormwaterJurisdictionGeometry,
	StateProvinceID,
	IsTransportationJurisdiction
into #JurisdictionsRestricted
from dbo.StormwaterJurisdiction
where @SpliceBase.STIntersects(StormwaterJurisdictionGeometry) = 1


Select
	DelineationID,
	@SpliceBase.STIntersection(DelineationGeometry) as DelineationGeometry,
	DelineationTypeID,
	IsVerified,
	DateLastVerified,
	VerifiedByPersonID
into #DelineationsRestricted
from dbo.Delineation
where @SpliceBase.STIntersects(DelineationGeometry) = 1


Select
	OnlandVisualTrashAssessmentAreaID,
	OnlandVisualTrashAssessmentAreaGeometry,
	MostRecentAssessmentDate,
	MostRecentAssessmentScore
into #OnlandVisualTrashAssessmentAreasRestricted
from dbo.vOnlandVisualTrashAssessmentAreaDated
where @SpliceBase.STIntersects(OnlandVisualTrashAssessmentAreaGeometry) = 1


/* From here on out, everything is identical to dbo.pRebuildTrashGeneratingUnitTable,
but referencing the temp tables created in step -1 instead of the living breathing tables*/

/* 0. Preprocess the land use blocks table with "sentinel" rows indicating where there is no priority land use block data. */
DROP TABLE IF EXISTS #LandUseBlocksAdjusted

Select * into #LandUseBlocksAdjusted
from
(select * from #LandUseBlocksRestricted

union all

SELECT
	Null as LandUseBlockID,
	Null as PriorityLandUseTypeID,
	Null as LandUseDescription,
	case when UGeometry is null then sj.StormwaterJurisdictionGeometry else sj.StormwaterJurisdictionGeometry.STDifference(sq.UGeometry) end AS LandUseBlockGeometry
FROM
(
  SELECT GEOMETRY::UnionAggregate(lub.LandUseBlockGeometry) AS UGeometry, sj.StormwaterJurisdictionID AS StormwaterJurisdictionID
  FROM dbo.#JurisdictionsRestricted sj
  left JOIN dbo.#LandUseBlocksRestricted lub
  ON sj.StormwaterJurisdictionGeometry.STIntersects(lub.LandUseBlockGeometry) = 1
  GROUP BY sj.StormwaterJurisdictionID
) sq
 LEFT JOIN dbo.#JurisdictionsRestricted sj ON sq.StormwaterJurisdictionID = sj.StormwaterJurisdictionID) s

/* 1. Produce the Jurisdiction-#DelineationsRestricted-OVTA layer using the BCF algorithm */

-- #JurisdictionsRestricted will serve as our "Background Layer" for the Boundary/Clip phase
declare @BackgroundLayer geometry;
select @BackgroundLayer = geometry::UnionAggregate(StormwaterJurisdictionGeometry) from #JurisdictionsRestricted;
--select @BackgroundLayer;

declare @BufferDelta decimal(11,11) = .000000001;

-- Assemble the boundary layer from the #DelineationsRestricted and OVTAA layers
Declare @BoundaryLayer geometry;
Select @BoundaryLayer = geometry::UnionAggregate(BoundaryGeometry) from (

select OnlandVisualTrashAssessmentAreaGeometry.STBoundary().STBuffer(@BufferDelta) as BoundaryGeometry from #OnlandVisualTrashAssessmentAreasRestricted
union all
Select DelineationGeometry.STBoundary().STBuffer(@BufferDelta) as BoundaryGeometry from #DelineationsRestricted where IsVerified = 1
Union all
Select StormwaterJurisdictionGeometry.STBoundary().STBuffer(@BufferDelta) as BoundaryGeometry from #JurisdictionsRestricted
) q

-- Clip

Declare @ClipLayer geometry;
Select @ClipLayer = @BackgroundLayer.STDifference(@BoundaryLayer);

-- Flatten

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

/* 2. Jurisdiction, #DelineationsRestricted, and Assessment Area data are recovered by joining back to those tables */

-- do an alter table #JurisdictionDelineationOVTA to add a JurisdictionID, TreatmentBMPID (which will take the place of DelineationID), and OVTAAID
Alter Table #JurisdictionDelineationOvta
Add JdoID int not null identity (1,1), 
JurisdictionID int null,
TreatmentBMPID int null,
OnlandVisualTrashAssessmentAreaID int null

--set the jurisdiction ID
Update jdo
set jdo.JurisdictionID = sj.StormwaterJurisdictionID
from #JurisdictionDelineationOvta jdo join #JurisdictionsRestricted sj on jdo.Geom.STIntersects(sj.StormwaterJurisdictionGeometry) = 1

-- set the ovta area ID. Requires a sub-query to select the most recent OVTA from the pullback

Update jdo
set jdo.OnlandVisualTrashAssessmentAreaID = x.OnlandVisualTrashAssessmentAreaID
from #JurisdictionDelineationOvta jdo join (
	select
		JdoId, ovta.OnlandVisualTrashAssessmentAreaID,
		rowNumber = ROW_NUMBER() over (partition by jdoid order by ovta.MostRecentAssessmentDate desc)
	from #JurisdictionDelineationOvta jdo
		join #OnlandVisualTrashAssessmentAreasRestricted ovta
			on jdo.Geom.STIntersects(ovta.OnlandVisualTrashAssessmentAreaGeometry) = 1
) x on jdo.JdoId = x.JdoId
where
	x.rowNumber = 1

-- set the treatment bmp id. Requires a sub-query to select the most TC-effective BMP from the pullback
update jdo
set jdo.TreatmentBMPId = x.TreatmentBMPID
from #JurisdictionDelineationOvta jdo join (
	select
		t.TreatmentBMPID, d.DelineationGeometry, tcs.TrashCaptureStatusTypePriority, jdo.JdoID,
		rowNumber = ROW_NUMBER() over (partition by jdo.JdoID order by tcs.TrashCaptureStatusTypePriority asc, t.TrashCaptureEffectiveness desc)
	from
		#DelineationsRestricted d inner join TreatmentBMP t
			on d.DelineationID = t.DelineationID
		join TrashCaptureStatusType tcs
			on t.TrashCaptureStatusTypeID = tcs.TrashCaptureStatusTypeID
		join #JurisdictionDelineationOvta jdo
			on jdo.Geom.STIntersects(d.DelineationGeometry) = 1
	where d.IsVerified = 1
) x on jdo.JdoId = x.JdoId
where x.rowNumber = 1

/*3. PLU data is integrated by joining out to the #LandUseBlocksRestricted table; result set is inserted directly to TGU table (release script 153) */

Insert into dbo.TrashGeneratingUnit (
	StormwaterJurisdictionID,
	TreatmentBMPID,
	OnlandVisualTrashAssessmentAreaID,
	LandUseBlockID,
	TrashGeneratingUnitGeometry,
	LastUpdateDate
)
select
	jdo.JurisdictionID,
	jdo.TreatmentBMPID,
	jdo.OnlandVisualTrashAssessmentAreaID,
	lub.LandUseBlockID,
	jdo.Geom.STIntersection(lub.LandUseBlockGeometry),
	GetDate()
from
	#JurisdictionDelineationOvta jdo join #LandUseBlocksAdjusted lub
		on jdo.Geom.STIntersects(lub.LandUseBlockGeometry) = 1
GO