if exists (select * from dbo.sysobjects where id = object_id('dbo.vProjectGrantScore'))
    drop view dbo.vProjectGrantScore
go

create view dbo.vProjectGrantScore
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
       dry.PercentReducedVolume * o.PollutantVolume as DryWeatherWeightedReductionVolume,
       dry.PercentReducedMetals * o.PollutantMetals as DryWeatherWeightedReductionMetals,
       dry.PercentReducedBacteria * o.PollutantBacteria as DryWeatherWeightedReductionBacteria,
       dry.PercentReducedNutrients * o.PollutantNutrients as DryWeatherWeightedReductionNutrients,
       dry.PercentReducedTSS * o.PollutantTSS as DryWeatherWeightedReductionTSS,
       100 * (dry.PercentReducedVolume * o.PollutantVolume + dry.PercentReducedMetals * o.PollutantMetals + dry.PercentReducedBacteria * o.PollutantBacteria + dry.PercentReducedNutrients * o.PollutantNutrients + dry.PercentReducedTSS * o.PollutantTSS) as DryWeatherWQLRI,
       wet.PercentReducedVolume * o.PollutantVolume as WetWeatherWeightedReductionVolume,
       wet.PercentReducedMetals * o.PollutantMetals as WetWeatherWeightedReductionMetals,
       wet.PercentReducedBacteria * o.PollutantBacteria as WetWeatherWeightedReductionBacteria,
       wet.PercentReducedNutrients * o.PollutantNutrients as WetWeatherWeightedReductionNutrients,
       wet.PercentReducedTSS * o.PollutantTSS as WetWeatherWeightedReductionTSS,
       100 * (wet.PercentReducedVolume * o.PollutantVolume + wet.PercentReducedMetals * o.PollutantMetals + wet.PercentReducedBacteria * o.PollutantBacteria + wet.PercentReducedNutrients * o.PollutantNutrients + wet.PercentReducedTSS * o.PollutantTSS) as WetWeatherWQLRI
from
(
    select  ProjectID,
            Sum(OverlapArea) as ProjectArea
            , Sum(OverlapArea / TotalProjectArea * PC_VOL_PCT) / 100 as PollutantVolume
            , Sum(OverlapArea / TotalProjectArea * PC_MET_PCT) / 100 as PollutantMetals
            , Sum(OverlapArea / TotalProjectArea * PC_BAC_PCT) / 100 as PollutantBacteria
            , Sum(OverlapArea / TotalProjectArea * PC_NUT_PCT) / 100 as PollutantNutrients
            , Sum(OverlapArea / TotalProjectArea * PC_TSS_PCT) / 100 as PollutantTSS
            , Sum(OverlapArea / TotalProjectArea * TPI) as TPI
            , Sum(OverlapArea / TotalProjectArea * SEA) as SEA
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
left join dbo.vProjectDryWeatherWQLRIScore dry on o.ProjectID = dry.ProjectID
left join dbo.vProjectWetWeatherWQLRIScore wet on o.ProjectID = wet.ProjectID
GO
/*
select * from dbo.vProjectGrantScore where ProjectID = 14
*/