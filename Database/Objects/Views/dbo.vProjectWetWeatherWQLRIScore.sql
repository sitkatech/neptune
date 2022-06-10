if exists (select * from dbo.sysobjects where id = object_id('dbo.vProjectWetWeatherWQLRIScore'))
    drop view dbo.vProjectWetWeatherWQLRIScore
go

create view dbo.vProjectWetWeatherWQLRIScore
as

    select 
    a.ProjectID as PrimaryKey,
    a.ProjectID,
    a.WetWeatherRetained / b.WetWeatherVolumeGenerated as PercentReducedVolume,
    a.WetWeatherTSSReduced / b.WetWeatherTSSGenerated as PercentReducedTSS,
    a.WetWeatherFCReduced / b.WetWeatherFCGenerated as PercentReducedBacteria,

    a.WetWeatherTNReduced / b.WetWeatherTNGenerated as PercentReducedTN,
    a.WetWeatherTPReduced / b.WetWeatherTPGenerated as PercentReducedTP,
    (((a.WetWeatherTNReduced / b.WetWeatherTNGenerated) + (a.WetWeatherTPReduced / b.WetWeatherTPGenerated)) / 2.0) as PercentReducedNutrients,

    a.WetWeatherTPbReduced / b.WetWeatherTPbGenerated as PercentReducedTPb,
    a.WetWeatherTCuReduced / b.WetWeatherTCuGenerated as PercentReducedTCu,
    a.WetWeatherTZnReduced / b.WetWeatherTZnGenerated as PercentReducedTZn,
    (((a.WetWeatherTPbReduced / b.WetWeatherTPbGenerated) + (a.WetWeatherTCuReduced / b.WetWeatherTCuGenerated) + (a.WetWeatherTZnReduced / b.WetWeatherTZnGenerated)) / 3.0) as PercentReducedMetals

    from
    (
        select ProjectID,
        sum(p.WetWeatherRetained) as WetWeatherRetained, 
        sum(p.WetWeatherTSSReduced) as WetWeatherTSSReduced,
        sum(p.WetWeatherTPbReduced) as WetWeatherTPbReduced,
        sum(p.WetWeatherTCuReduced) as WetWeatherTCuReduced,
        sum(p.WetWeatherTNReduced) as WetWeatherTNReduced,
        sum(p.WetWeatherFCReduced) as WetWeatherFCReduced,
        sum(p.WetWeatherTPReduced) as WetWeatherTPReduced,
        sum(p.WetWeatherTZnReduced) as WetWeatherTZnReduced
        from vProjectLoadReducingResult p
        group by p.ProjectID
    ) a join 
    (
        select ProjectID,
        sum(WetWeatherVolumeGenerated) as WetWeatherVolumeGenerated,
        sum(WetWeatherTSSGenerated) as WetWeatherTSSGenerated,
        sum(WetWeatherTNGenerated) as WetWeatherTNGenerated,
        sum(WetWeatherTPGenerated) as WetWeatherTPGenerated,
        sum(WetWeatherFCGenerated) as WetWeatherFCGenerated,
        sum(WetWeatherTCuGenerated) as WetWeatherTCuGenerated,
        sum(WetWeatherTPbGenerated) as WetWeatherTPbGenerated,
        sum(WetWeatherTZnGenerated) as WetWeatherTZnGenerated
        from dbo.vProjectLoadGeneratingResult
        group by ProjectID
    ) b on a.ProjectID = b.ProjectID

GO
/*
select * from dbo.vProjectWetWeatherWQLRIScore where ProjectID = 14
*/