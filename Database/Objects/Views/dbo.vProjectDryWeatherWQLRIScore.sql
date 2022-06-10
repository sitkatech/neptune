if exists (select * from dbo.sysobjects where id = object_id('dbo.vProjectDryWeatherWQLRIScore'))
    drop view dbo.vProjectDryWeatherWQLRIScore
go

create view dbo.vProjectDryWeatherWQLRIScore
as
    select 
    a.ProjectID as PrimaryKey,
    a.ProjectID,
    a.DryWeatherRetained / b.DryWeatherVolumeGenerated as PercentReducedVolume,
    a.DryWeatherTSSReduced / b.DryWeatherTSSGenerated as PercentReducedTSS,
    a.DryWeatherFCReduced / b.DryWeatherFCGenerated as PercentReducedBacteria,

    a.DryWeatherTNReduced / b.DryWeatherTNGenerated as PercentReducedTN,
    a.DryWeatherTPReduced / b.DryWeatherTPGenerated as PercentReducedTP,
    (((a.DryWeatherTNReduced / b.DryWeatherTNGenerated) + (a.DryWeatherTPReduced / b.DryWeatherTPGenerated)) / 2.0) as PercentReducedNutrients,

    a.DryWeatherTPbReduced / b.DryWeatherTPbGenerated as PercentReducedTPb,
    a.DryWeatherTCuReduced / b.DryWeatherTCuGenerated as PercentReducedTCu,
    a.DryWeatherTZnReduced / b.DryWeatherTZnGenerated as PercentReducedTZn,
    (((a.DryWeatherTPbReduced / b.DryWeatherTPbGenerated) + (a.DryWeatherTCuReduced / b.DryWeatherTCuGenerated) + (a.DryWeatherTZnReduced / b.DryWeatherTZnGenerated)) / 3.0) as PercentReducedMetals

    from
    (
        select ProjectID,
        sum(isnull(SummerDryWeatherRetained, 0) + isnull(WinterDryWeatherRetained, 0)) as DryWeatherRetained, 
        sum(isnull(SummerDryWeatherTSSReduced, 0) + isnull(WinterDryWeatherTSSReduced, 0)) as DryWeatherTSSReduced,
        sum(isnull(SummerDryWeatherTPbReduced, 0) + isnull(WinterDryWeatherTPbReduced, 0)) as DryWeatherTPbReduced,
        sum(isnull(SummerDryWeatherTCuReduced, 0) + isnull(WinterDryWeatherTCuReduced, 0)) as DryWeatherTCuReduced,
        sum(isnull(SummerDryWeatherTNReduced, 0) + isnull(WinterDryWeatherTNReduced, 0)) as DryWeatherTNReduced,
        sum(isnull(SummerDryWeatherFCReduced, 0) + isnull(WinterDryWeatherFCReduced, 0)) as DryWeatherFCReduced,
        sum(isnull(SummerDryWeatherTPReduced, 0) + isnull(WinterDryWeatherTPReduced, 0)) as DryWeatherTPReduced,
        sum(isnull(SummerDryWeatherTZnReduced, 0) + isnull(WinterDryWeatherTZnReduced, 0)) as DryWeatherTZnReduced
        from vProjectLoadReducingResult p
        group by p.ProjectID
    ) a join 
    (
        select ProjectID,
        sum(isnull(SummerDryWeatherVolumeGenerated, 0) + isnull(WinterDryWeatherVolumeGenerated, 0)) as DryWeatherVolumeGenerated,
        sum(isnull(SummerDryWeatherTSSGenerated, 0) + isnull(WinterDryWeatherTSSGenerated, 0)) as DryWeatherTSSGenerated,
        sum(isnull(SummerDryWeatherTNGenerated, 0) + isnull(WinterDryWeatherTNGenerated, 0)) as DryWeatherTNGenerated,
        sum(isnull(SummerDryWeatherTPGenerated, 0) + isnull(WinterDryWeatherTPGenerated, 0)) as DryWeatherTPGenerated,
        sum(isnull(SummerDryWeatherFCGenerated, 0) + isnull(WinterDryWeatherFCGenerated, 0)) as DryWeatherFCGenerated,
        sum(isnull(SummerDryWeatherTCuGenerated, 0) + isnull(WinterDryWeatherTCuGenerated, 0)) as DryWeatherTCuGenerated,
        sum(isnull(SummerDryWeatherTPbGenerated, 0) + isnull(WinterDryWeatherTPbGenerated, 0)) as DryWeatherTPbGenerated,
        sum(isnull(SummerDryWeatherTZnGenerated, 0) + isnull(WinterDryWeatherTZnGenerated, 0)) as DryWeatherTZnGenerated
        from dbo.vProjectLoadGeneratingResult
        group by ProjectID
    ) b on a.ProjectID = b.ProjectID

GO
/*
select * from dbo.vProjectDryWeatherWQLRIScore where ProjectID = 14
*/