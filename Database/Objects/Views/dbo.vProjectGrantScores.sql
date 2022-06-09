if exists (select * from dbo.sysobjects where id = object_id('dbo.vProjectGrantScores'))
    drop view dbo.vProjectGrantScores
go

create view dbo.vProjectGrantScores
as

with projectGeometries(ProjectID, ProjectGeometry)
as
(
    select plg.ProjectID, geometry::UnionAggregate(plg.ProjectLoadGeneratingUnitGeometry)
    from  dbo.ProjectLoadGeneratingUnit plg
    group by ProjectID
)

select  a.ProjectID,
        Sum(OverlapArea) as ProjectArea
        , Sum(OverlapArea / TotalProjectArea * PC_VOL_PCT) as PollutantVolume
        , Sum(OverlapArea / TotalProjectArea * PC_MET_PCT) as PollutantMetals
        , Sum(OverlapArea / TotalProjectArea * PC_BAC_PCT) as PollutantBacteria
        , Sum(OverlapArea / TotalProjectArea * PC_NUT_PCT) as PollutantNutrients
        , Sum(OverlapArea / TotalProjectArea * PC_TSS_PCT) as PollutantTSS
        , Sum(OverlapArea / TotalProjectArea * TPI) as TPI
        , Sum(OverlapArea / TotalProjectArea * SEA) as SEA

from
(
	select  pg.ProjectID, pg.ProjectGeometry.STArea() as TotalProjectArea, o.CatchIDN, o.OCTAPrioritizationKey, o.OCTAPrioritizationGeometry.STIntersection(pg.ProjectGeometry) as OverlappedGeometry, o.OCTAPrioritizationGeometry.STIntersection(pg.ProjectGeometry).STArea() as OverlapArea,
[Watershed], [TPI], [WQNLU], [WQNMON], [IMPAIR], [MON], [SEA], [SEA_PCTL], [PC_VOL_PCT], [PC_NUT_PCT], [PC_BAC_PCT], [PC_MET_PCT], [PC_TSS_PCT]
	from dbo.OCTAPrioritization o
    join projectGeometries pg on o.OCTAPrioritizationGeometry.STIntersects(pg.ProjectGeometry) = 1
) a
group by a.ProjectID


GO
/*
select * from dbo.vProjectGrantScores where ProjectID = 15
*/