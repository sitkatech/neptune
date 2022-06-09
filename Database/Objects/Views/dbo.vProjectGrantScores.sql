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
),
octaProject(ProjectID, TotalProjectArea, CatchIDN, OCTAPrioritizationKey, OverlapArea, Watershed, TPI, WQNLU, WQNMON, IMPAIR, MON, SEA, SEA_PCTL, PC_VOL_PCT, PC_NUT_PCT, PC_BAC_PCT, PC_MET_PCT, PC_TSS_PCT)
as
(
	select  pg.ProjectID, pg.ProjectGeometry.STArea() as TotalProjectArea, o.CatchIDN, o.OCTAPrioritizationKey, o.OCTAPrioritizationGeometry.STIntersection(pg.ProjectGeometry).STArea() as OverlapArea,
[Watershed], [TPI], [WQNLU], [WQNMON], [IMPAIR], [MON], [SEA], [SEA_PCTL], [PC_VOL_PCT], [PC_NUT_PCT], [PC_BAC_PCT], [PC_MET_PCT], [PC_TSS_PCT]
	from dbo.OCTAPrioritization o
    join projectGeometries pg on o.OCTAPrioritizationGeometry.STIntersects(pg.ProjectGeometry) = 1
)
select o.ProjectID as PrimaryKey,
       o.ProjectID,
       o.ProjectArea,
       o2.Watersheds,
       o.PollutantVolume,
       o.PollutantMetals,
       o.PollutantBacteria,
       o.PollutantNutrients,
       o.PollutantTSS,
       o.TPI,
       o.SEA,
       cast(null as float) as DryWeatherWQLRI,
       cast(null as float) as WetWeatherWQLRI
from
(
    select  ProjectID,
            Sum(OverlapArea) as ProjectArea
            , Sum(OverlapArea / TotalProjectArea * PC_VOL_PCT) / 100 as PollutantVolume
            , Sum(OverlapArea / TotalProjectArea * PC_MET_PCT) / 100 as PollutantMetals
            , Sum(OverlapArea / TotalProjectArea * PC_BAC_PCT) / 100 as PollutantBacteria
            , Sum(OverlapArea / TotalProjectArea * PC_NUT_PCT) / 100 as PollutantNutrients
            , Sum(OverlapArea / TotalProjectArea * PC_TSS_PCT) / 100 as PollutantTSS
            , Sum(OverlapArea / TotalProjectArea * TPI) / 100 as TPI
            , Sum(OverlapArea / TotalProjectArea * SEA) / 100 as SEA
    from octaProject
    group by ProjectID
) o
join (
    select ProjectID, STRING_AGG(Watershed, ', ') as Watersheds
    from (
        select distinct ProjectID, Watershed from octaProject
    ) b 
    group by b.ProjectID
) o2 on o.ProjectID = o2.ProjectID

GO
/*
select * from dbo.vProjectGrantScores where ProjectID = 14
*/