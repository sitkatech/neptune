if exists (select * from dbo.sysobjects where id = object_id('dbo.vProjectLoadGeneratingResult'))
    drop view dbo.vProjectLoadGeneratingResult
go

create view dbo.vProjectLoadGeneratingResult
as

select  ProjectNereidResultID as PrimaryKey,        
        ProjectNereidResultID,
        p.ProjectID,
        p.ProjectName,
        IsBaselineCondition,
        pnr.WaterQualityManagementPlanID,
        pnr.RegionalSubbasinID,
        d.DelineationID,
        NodeID,
--        FullResponse,
        LastUpdate,
        WetWeatherVolumeGenerated,
        WetWeatherTSSGenerated * uc.PoundsToKilogramsFactor as WetWeatherTSSGenerated,
        WetWeatherTNGenerated * uc.PoundsToKilogramsFactor as WetWeatherTNGenerated,
        WetWeatherTPGenerated * uc.PoundsToKilogramsFactor as WetWeatherTPGenerated,
        WetWeatherFCGenerated / uc.BillionsFactor as WetWeatherFCGenerated,
        WetWeatherTCuGenerated * uc.PoundsToGramsFactor as WetWeatherTCuGenerated,
        WetWeatherTPbGenerated * uc.PoundsToGramsFactor as WetWeatherTPbGenerated,
        WetWeatherTZnGenerated * uc.PoundsToGramsFactor as WetWeatherTZnGenerated,
        SummerDryWeatherVolumeGenerated,
        SummerDryWeatherTSSGenerated * uc.PoundsToKilogramsFactor as SummerDryWeatherTSSGenerated,
        SummerDryWeatherTNGenerated * uc.PoundsToKilogramsFactor as SummerDryWeatherTNGenerated,
        SummerDryWeatherTPGenerated * uc.PoundsToKilogramsFactor as SummerDryWeatherTPGenerated,
        SummerDryWeatherFCGenerated / uc.BillionsFactor as SummerDryWeatherFCGenerated,
        SummerDryWeatherTCuGenerated * uc.PoundsToGramsFactor as SummerDryWeatherTCuGenerated,
        SummerDryWeatherTPbGenerated * uc.PoundsToGramsFactor as SummerDryWeatherTPbGenerated,
        SummerDryWeatherTZnGenerated * uc.PoundsToGramsFactor as SummerDryWeatherTZnGenerated,
        WinterDryWeatherVolumeGenerated,
        WinterDryWeatherTSSGenerated * uc.PoundsToKilogramsFactor as WinterDryWeatherTSSGenerated,
        WinterDryWeatherTNGenerated * uc.PoundsToKilogramsFactor as WinterDryWeatherTNGenerated,
        WinterDryWeatherTPGenerated * uc.PoundsToKilogramsFactor as WinterDryWeatherTPGenerated,
        WinterDryWeatherFCGenerated / uc.BillionsFactor as WinterDryWeatherFCGenerated,
        WinterDryWeatherTCuGenerated * uc.PoundsToGramsFactor as WinterDryWeatherTCuGenerated,
        WinterDryWeatherTPbGenerated * uc.PoundsToGramsFactor as WinterDryWeatherTPbGenerated,
        WinterDryWeatherTZnGenerated * uc.PoundsToGramsFactor as WinterDryWeatherTZnGenerated

        from dbo.ProjectNereidResult pnr
        join dbo.Project p on pnr.ProjectID = p.ProjectID
        left join dbo.Delineation d on pnr.DelineationID = d.DelineationID
        cross apply openjson(fullresponse) 
                    with (
            WetWeatherVolumeGenerated float '$.runoff_volume_cuft',
            WetWeatherTSSGenerated float '$.TSS_load_lbs',
            WetWeatherTNGenerated float '$.TN_load_lbs',
            WetWeatherTPGenerated float '$.TP_load_lbs',
            WetWeatherFCGenerated float '$.FC_load_mpn',
            WetWeatherTCuGenerated float '$.TCu_load_lbs',
            WetWeatherTPbGenerated float '$.TPb_load_lbs',
            WetWeatherTZnGenerated float '$.TZn_load_lbs',
            SummerDryWeatherVolumeGenerated float '$.summer_dry_weather_flow_cuft',
            SummerDryWeatherTSSGenerated float '$.summer_dwTSS_load_lbs',
            SummerDryWeatherTNGenerated float '$.summer_dwTN_load_lbs',
            SummerDryWeatherTPGenerated float '$.summer_dwTP_load_lbs',
            SummerDryWeatherFCGenerated float '$.summer_dwFC_load_mpn',
            SummerDryWeatherTCuGenerated float '$.summer_dwTCu_load_lbs',
            SummerDryWeatherTPbGenerated float '$.summer_dwTPb_load_lbs',
            SummerDryWeatherTZnGenerated float '$.summer_dwTZn_load_lbs',
            WinterDryWeatherVolumeGenerated float '$.winter_dry_weather_flow_cuft',
            WinterDryWeatherTSSGenerated float '$.winter_dwTSS_load_lbs',
            WinterDryWeatherTNGenerated float '$.winter_dwTN_load_lbs',
            WinterDryWeatherTPGenerated float '$.winter_dwTP_load_lbs',
            WinterDryWeatherFCGenerated float '$.winter_dwFC_load_mpn',
            WinterDryWeatherTCuGenerated float '$.winter_dwTCu_load_lbs',
            WinterDryWeatherTPbGenerated float '$.winter_dwTPb_load_lbs',
            WinterDryWeatherTZnGenerated float '$.winter_dwTZn_load_lbs'
            ) as ModelResults
        cross join dbo.vModelingResultUnitConversion uc
GO
/*
select * from dbo.vProjectLoadGeneratingResult where ProjectID = 14
*/