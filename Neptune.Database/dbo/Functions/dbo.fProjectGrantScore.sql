create function dbo.fProjectGrantScore
(
    @projectID int
)
returns table
as return
(

    with projectDelineations(ProjectID, TreatmentBMPID, DelineationID, DelineationTypeID, RegionalSubbasinID)
    as
    (
        select t.ProjectID, t.TreatmentBMPID, d.DelineationID, d.DelineationTypeID, t.RegionalSubbasinID
        from dbo.TreatmentBMP t
        join dbo.Delineation d on t.TreatmentBMPID = d.TreatmentBMPID
        where t.ProjectID = @projectID
    ),
    projectCentralizedRSBs as (
     select r.RegionalSubbasinID, r.RegionalSubbasinID as BaseID, OCSurveyCatchmentID, OCSurveyDownstreamCatchmentID
         from dbo.RegionalSubbasin r
         join projectDelineations pd on r.RegionalSubbasinID = pd.RegionalSubbasinID
         where pd.DelineationTypeID = 1
     union all
     select r.RegionalSubbasinID, c.BaseID, r.OCSurveyCatchmentID, r.OCSurveyDownstreamCatchmentID
         from dbo.RegionalSubbasin r
         join projectCentralizedRSBs c on r.OCSurveyDownstreamCatchmentID = c.OCSurveyCatchmentID
    ),
    projectIsDistributedOnly(ProjectID, IsDistributedOnly)
    as
    (
        select ProjectID, case when max(DelineationTypeID) = 2 and min(DelineationTypeID) = 2  then 1 else 0 end as IsDistributedOnly
        from projectDelineations
        group by ProjectID
    ),
    projectGeometryUnits(ProjectID, ProjectLoadGeneratingUnitID, ProjectLoadGeneratingUnitGeometry, ModelBasinID, RegionalSubbasinID, DelineationID, WaterQualityManagementPlanID, DelineationTypeID)
    as
    (
            -- distributed delineations
            select plgu.ProjectID
            , plgu.ProjectLoadGeneratingUnitID
            , plgu.ProjectLoadGeneratingUnitGeometry
            , plgu.ModelBasinID
            , plgu.RegionalSubbasinID
            , plgu.DelineationID
            , plgu.WaterQualityManagementPlanID
            , pd.DelineationTypeID
            from dbo.ProjectLoadGeneratingUnit plgu
            join projectDelineations pd on plgu.DelineationID = pd.DelineationID and pd.ProjectID = plgu.ProjectID
            where pd.DelineationTypeID = 2 and plgu.ProjectID = @projectID

            union all

            -- centralized delineations that are within your upstream
            select plgu.ProjectID        
            , plgu.ProjectLoadGeneratingUnitID
            , plgu.ProjectLoadGeneratingUnitGeometry
            , plgu.ModelBasinID
            , plgu.RegionalSubbasinID
            , plgu.DelineationID
            , plgu.WaterQualityManagementPlanID
            , pd.DelineationTypeID
            from projectDelineations pd
            join projectCentralizedRSBs u on pd.RegionalSubbasinID = u.BaseID
            join dbo.ProjectLoadGeneratingUnit plgu on u.RegionalSubbasinID = plgu.RegionalSubbasinID and pd.ProjectID = plgu.ProjectID
            where pd.DelineationTypeID = 1
    ),
    projectGeometries(ProjectID, ProjectGeometry)
    as
    (
        select plg.ProjectID, geometry::UnionAggregate(plg.ProjectLoadGeneratingUnitGeometry)
        from  projectGeometryUnits plg
        group by ProjectID
    ),
    octaProject(ProjectID, TotalProjectArea, CatchIDN, OCTAPrioritizationKey, OverlapArea, Watershed, TPI, WQNLU, WQNMON, IMPAIR, MON, SEA, SEA_PCTL, PC_VOL_PCT, PC_NUT_PCT, PC_BAC_PCT, PC_MET_PCT, PC_TSS_PCT)
    as
    (
	    select  pg.ProjectID, pg.ProjectGeometry.STArea() as TotalProjectArea, o.CatchIDN, o.OCTAPrioritizationKey, o.OCTAPrioritizationGeometry.STIntersection(pg.ProjectGeometry).STArea() as OverlapArea,
    [Watershed], [TPI], [WQNLU], [WQNMON], [IMPAIR], [MON], [SEA], [SEA_PCTL], [PC_VOL_PCT], [PC_NUT_PCT], [PC_BAC_PCT], [PC_MET_PCT], [PC_TSS_PCT]
	    from dbo.OCTAPrioritization o
        join projectGeometries pg on o.OCTAPrioritizationGeometry.STIntersects(pg.ProjectGeometry) = 1
    ),
    relevantProjectNeriedResults(ProjectNereidResultID, ProjectID, NodeID, NereidResultType)
    as
    (
        -- LG
        -- LG for distributed delineations inside centralized upstream RSBs
        select pnr.ProjectNereidResultID, pnr.ProjectID, pnr.NodeID, 'LoadGenerating' as NereidResultType
        from dbo.ProjectNereidResult pnr
        join dbo.Delineation d on pnr.DelineationID = d.DelineationID
        join dbo.TreatmentBMP t on d.TreatmentBMPID = t.TreatmentBMPID
        join projectCentralizedRSBs prsb on t.RegionalSubbasinID = prsb.RegionalSubbasinID
        where pnr.ProjectID = @projectID

        union all

        -- LG for RSBs and WQMPs inside centralized upstream RSBs
        select pnr.ProjectNereidResultID, pnr.ProjectID, pnr.NodeID, 'LoadGenerating' as NereidResultType
        from dbo.ProjectNereidResult pnr
        join projectCentralizedRSBs prsb on pnr.RegionalSubbasinID = prsb.RegionalSubbasinID
        where pnr.ProjectID = @projectID and TreatmentBMPID is null and NodeID not like '%-TMNT'

        union all

        -- LG for distributed delineations for this project
        select pnr.ProjectNereidResultID, pnr.ProjectID, pnr.NodeID, 'LoadGenerating' as NereidResultType
        from dbo.ProjectNereidResult pnr
        join projectDelineations pd on pnr.ProjectID = pd.ProjectID and pnr.DelineationID = pd.DelineationID 

        union all
        -- LR existing
        select pnr.ProjectNereidResultID, pnr.ProjectID, pnr.NodeID, 'LoadReducingExisting' as NereidResultType
        from dbo.ProjectNereidResult pnr
        join dbo.TreatmentBMP pd on pnr.TreatmentBMPID = pd.TreatmentBMPID and pnr.ProjectID != isnull(pd.ProjectID, 0)
        join projectCentralizedRSBs prsb on pd.RegionalSubbasinID = prsb.RegionalSubbasinID
        where pnr.ProjectID = @projectID

        union all
        select pnr.ProjectNereidResultID, pnr.ProjectID, pnr.NodeID, 'LoadReducingExisting' as NereidResultType
        from dbo.ProjectNereidResult pnr
        join dbo.WaterQualityManagementPlan t on pnr.WaterQualityManagementPlanID = t.WaterQualityManagementPlanID
        join projectCentralizedRSBs prsb on pnr.RegionalSubbasinID = prsb.RegionalSubbasinID
        where pnr.ProjectID = @projectID and NodeID like '%-TMNT'

        union all

        -- LR for Project
        select pnr.ProjectNereidResultID, pnr.ProjectID, pnr.NodeID, 'LoadReducingNew' as NereidResultType
        from dbo.ProjectNereidResult pnr
        join projectDelineations pd on pnr.ProjectID = pd.ProjectID and pnr.TreatmentBMPID = pd.TreatmentBMPID
    ),
    projectLoadGenerated(ProjectID, DryWeatherVolumeGenerated, DryWeatherTSSGenerated, DryWeatherTNGenerated, DryWeatherTPGenerated, DryWeatherFCGenerated, DryWeatherTCuGenerated, DryWeatherTPbGenerated, DryWeatherTZnGenerated, WetWeatherVolumeGenerated, WetWeatherTSSGenerated, WetWeatherTNGenerated, WetWeatherTPGenerated, WetWeatherFCGenerated, WetWeatherTCuGenerated, WetWeatherTPbGenerated, WetWeatherTZnGenerated, ImperviousAreaTreatedAcres)
    as
    (
        select p.ProjectID,
            sum(isnull(SummerDryWeatherVolumeGenerated, 0) + isnull(WinterDryWeatherVolumeGenerated, 0)) as DryWeatherVolumeGenerated,
            sum(isnull(SummerDryWeatherTSSGenerated, 0) + isnull(WinterDryWeatherTSSGenerated, 0)) as DryWeatherTSSGenerated,
            sum(isnull(SummerDryWeatherTNGenerated, 0) + isnull(WinterDryWeatherTNGenerated, 0)) as DryWeatherTNGenerated,
            sum(isnull(SummerDryWeatherTPGenerated, 0) + isnull(WinterDryWeatherTPGenerated, 0)) as DryWeatherTPGenerated,
            sum(isnull(SummerDryWeatherFCGenerated, 0) + isnull(WinterDryWeatherFCGenerated, 0)) as DryWeatherFCGenerated,
            sum(isnull(SummerDryWeatherTCuGenerated, 0) + isnull(WinterDryWeatherTCuGenerated, 0)) as DryWeatherTCuGenerated,
            sum(isnull(SummerDryWeatherTPbGenerated, 0) + isnull(WinterDryWeatherTPbGenerated, 0)) as DryWeatherTPbGenerated,
            sum(isnull(SummerDryWeatherTZnGenerated, 0) + isnull(WinterDryWeatherTZnGenerated, 0)) as DryWeatherTZnGenerated,
            sum(WetWeatherVolumeGenerated) as WetWeatherVolumeGenerated,
            sum(WetWeatherTSSGenerated) as WetWeatherTSSGenerated,
            sum(WetWeatherTNGenerated) as WetWeatherTNGenerated,
            sum(WetWeatherTPGenerated) as WetWeatherTPGenerated,
            sum(WetWeatherFCGenerated) as WetWeatherFCGenerated,
            sum(WetWeatherTCuGenerated) as WetWeatherTCuGenerated,
            sum(WetWeatherTPbGenerated) as WetWeatherTPbGenerated,
            sum(WetWeatherTZnGenerated) as WetWeatherTZnGenerated,
            sum(isnull(p.ImperviousAreaTreatedAcres, 0)) as ImperviousAreaTreatedAcres

        from dbo.vProjectLoadGeneratingResult p
        join relevantProjectNeriedResults pnr on p.ProjectNereidResultID = pnr.ProjectNereidResultID
        group by p.ProjectID
    ),
    projectLoadReduced(ProjectID, DryWeatherRetained, DryWeatherTSSReduced, DryWeatherTPbReduced, DryWeatherTCuReduced, DryWeatherTNReduced, DryWeatherFCReduced, DryWeatherTPReduced, DryWeatherTZnReduced, WetWeatherRetained, WetWeatherTSSReduced, WetWeatherTPbReduced, WetWeatherTCuReduced, WetWeatherTNReduced, WetWeatherFCReduced, WetWeatherTPReduced, WetWeatherTZnReduced, IsPartOfProject)
    as
    (
        select p.ProjectID,
            isnull(SummerDryWeatherRetained, 0) + isnull(WinterDryWeatherRetained, 0) as DryWeatherRetained, 
            isnull(SummerDryWeatherTSSReduced, 0) + isnull(WinterDryWeatherTSSReduced, 0) as DryWeatherTSSReduced,
            isnull(SummerDryWeatherTPbReduced, 0) + isnull(WinterDryWeatherTPbReduced, 0) as DryWeatherTPbReduced,
            isnull(SummerDryWeatherTCuReduced, 0) + isnull(WinterDryWeatherTCuReduced, 0) as DryWeatherTCuReduced,
            isnull(SummerDryWeatherTNReduced, 0) + isnull(WinterDryWeatherTNReduced, 0) as DryWeatherTNReduced,
            isnull(SummerDryWeatherFCReduced, 0) + isnull(WinterDryWeatherFCReduced, 0) as DryWeatherFCReduced,
            isnull(SummerDryWeatherTPReduced, 0) + isnull(WinterDryWeatherTPReduced, 0) as DryWeatherTPReduced,
            isnull(SummerDryWeatherTZnReduced, 0) + isnull(WinterDryWeatherTZnReduced, 0) as DryWeatherTZnReduced,

            isnull(p.WetWeatherRetained, 0) as WetWeatherRetained, 
            isnull(p.WetWeatherTSSReduced, 0) as WetWeatherTSSReduced,
            isnull(p.WetWeatherTPbReduced, 0) as WetWeatherTPbReduced,
            isnull(p.WetWeatherTCuReduced, 0) as WetWeatherTCuReduced,
            isnull(p.WetWeatherTNReduced, 0) as WetWeatherTNReduced,
            isnull(p.WetWeatherFCReduced, 0) as WetWeatherFCReduced,
            isnull(p.WetWeatherTPReduced, 0) as WetWeatherTPReduced,
            isnull(p.WetWeatherTZnReduced, 0) as WetWeatherTZnReduced,

            p.IsPartOfProject
        from dbo.vProjectLoadReducingResult p
        join relevantProjectNeriedResults pnr on p.ProjectNereidResultID = pnr.ProjectNereidResultID
    )


    select o.ProjectID as PrimaryKey,
           o.ProjectID,
           o.ProjectArea * 0.000247105 as ProjectArea,
           impAcres.ImperviousAreaTreatedAcres as ImperviousAreaTreatedAcres,
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
                , Sum(TotalProjectArea) as TotalProjectArea
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
    left join 
    (
        select  ProjectID,
                Sum(ImperviousAreaTreatedAcres) as ImperviousAreaTreatedAcres
        from projectLoadGenerated
        group by ProjectID
    ) impAcres  on o.ProjectID = impAcres.ProjectID
    join (
        select ProjectID, STRING_AGG(Watershed, ', ') as Watersheds
        from (
            select distinct ProjectID, Watershed from octaProject
        ) b 
        group by b.ProjectID
    ) o2 on o.ProjectID = o2.ProjectID
    left join 
    (
        select lg.ProjectID,
            new.DryWeatherRetained / (lg.DryWeatherVolumeGenerated - exist.DryWeatherRetained) as PercentReducedVolume,
            new.DryWeatherTSSReduced / (lg.DryWeatherTSSGenerated - exist.DryWeatherTSSReduced) as PercentReducedTSS,
            new.DryWeatherFCReduced / (lg.DryWeatherFCGenerated - exist.DryWeatherFCReduced) as PercentReducedBacteria,

            new.DryWeatherTNReduced / (lg.DryWeatherTNGenerated - exist.DryWeatherTNReduced) as PercentReducedTN,
            new.DryWeatherTPReduced / (lg.DryWeatherTPGenerated - exist.DryWeatherTPReduced) as PercentReducedTP,
            (((new.DryWeatherTNReduced / (lg.DryWeatherTNGenerated - exist.DryWeatherTNReduced)) + (new.DryWeatherTPReduced / (lg.DryWeatherTPGenerated - exist.DryWeatherTPReduced))) / 2.0) as PercentReducedNutrients,

            new.DryWeatherTPbReduced / (lg.DryWeatherTPbGenerated - exist.DryWeatherTPbReduced) as PercentReducedTPb,
            new.DryWeatherTCuReduced / (lg.DryWeatherTCuGenerated - exist.DryWeatherTCuReduced) as PercentReducedTCu,
            new.DryWeatherTZnReduced / (lg.DryWeatherTZnGenerated - exist.DryWeatherTZnReduced) as PercentReducedTZn,
            (((new.DryWeatherTPbReduced / (lg.DryWeatherTPbGenerated - exist.DryWeatherTPbReduced)) + (new.DryWeatherTCuReduced / (lg.DryWeatherTCuGenerated - exist.DryWeatherTCuReduced)) + (new.DryWeatherTZnReduced / (lg.DryWeatherTZnGenerated - exist.DryWeatherTZnReduced))) / 3.0) as PercentReducedMetals

            from
            (
                select p.ProjectID,
                sum(DryWeatherVolumeGenerated) as DryWeatherVolumeGenerated,
                sum(DryWeatherTSSGenerated) as DryWeatherTSSGenerated,
                sum(DryWeatherTNGenerated) as DryWeatherTNGenerated,
                sum(DryWeatherTPGenerated) as DryWeatherTPGenerated,
                sum(DryWeatherFCGenerated) as DryWeatherFCGenerated,
                sum(DryWeatherTCuGenerated) as DryWeatherTCuGenerated,
                sum(DryWeatherTPbGenerated) as DryWeatherTPbGenerated,
                sum(DryWeatherTZnGenerated) as DryWeatherTZnGenerated
                from projectLoadGenerated p
                group by p.ProjectID
            ) lg 
            left join
            (
                select p.ProjectID,
                sum(DryWeatherRetained) as DryWeatherRetained, 
                sum(DryWeatherTSSReduced) as DryWeatherTSSReduced,
                sum(DryWeatherTPbReduced) as DryWeatherTPbReduced,
                sum(DryWeatherTCuReduced) as DryWeatherTCuReduced,
                sum(DryWeatherTNReduced) as DryWeatherTNReduced,
                sum(DryWeatherFCReduced) as DryWeatherFCReduced,
                sum(DryWeatherTPReduced) as DryWeatherTPReduced,
                sum(DryWeatherTZnReduced) as DryWeatherTZnReduced
                from projectLoadReduced p
                where p.IsPartOfProject = 1
                group by p.ProjectID
            ) new on lg.ProjectID = new.ProjectID 
            left join 
            (
                select pidu.ProjectID,
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(DryWeatherRetained, 0)) end as DryWeatherRetained, 
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(DryWeatherTSSReduced, 0)) end as DryWeatherTSSReduced,
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(DryWeatherTPbReduced, 0)) end as DryWeatherTPbReduced,
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(DryWeatherTCuReduced, 0)) end as DryWeatherTCuReduced,
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(DryWeatherTNReduced, 0)) end as DryWeatherTNReduced,
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(DryWeatherFCReduced, 0)) end as DryWeatherFCReduced,
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(DryWeatherTPReduced, 0)) end as DryWeatherTPReduced,
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(DryWeatherTZnReduced, 0)) end as DryWeatherTZnReduced
                from projectIsDistributedOnly pidu
                left join projectLoadReduced p on pidu.ProjectID = p.ProjectID and p.IsPartOfProject = 0
                group by pidu.ProjectID, pidu.IsDistributedOnly
            ) exist on lg.ProjectID = exist.ProjectID
    ) dry on o.ProjectID = dry.ProjectID

    left join 
    (
        select lg.ProjectID,
            new.WetWeatherRetained / (lg.WetWeatherVolumeGenerated - exist.WetWeatherRetained) as PercentReducedVolume,
            new.WetWeatherTSSReduced / (lg.WetWeatherTSSGenerated - exist.WetWeatherTSSReduced) as PercentReducedTSS,
            new.WetWeatherFCReduced / (lg.WetWeatherFCGenerated - exist.WetWeatherFCReduced) as PercentReducedBacteria,

            new.WetWeatherTNReduced / (lg.WetWeatherTNGenerated - exist.WetWeatherTNReduced) as PercentReducedTN,
            new.WetWeatherTPReduced / (lg.WetWeatherTPGenerated - exist.WetWeatherTPReduced) as PercentReducedTP,
            (((new.WetWeatherTNReduced / (lg.WetWeatherTNGenerated - exist.WetWeatherTNReduced)) + (new.WetWeatherTPReduced / (lg.WetWeatherTPGenerated - exist.WetWeatherTPReduced))) / 2.0) as PercentReducedNutrients,

            new.WetWeatherTPbReduced / (lg.WetWeatherTPbGenerated - exist.WetWeatherTPbReduced) as PercentReducedTPb,
            new.WetWeatherTCuReduced / (lg.WetWeatherTCuGenerated - exist.WetWeatherTCuReduced) as PercentReducedTCu,
            new.WetWeatherTZnReduced / (lg.WetWeatherTZnGenerated - exist.WetWeatherTZnReduced) as PercentReducedTZn,
            (((new.WetWeatherTPbReduced / (lg.WetWeatherTPbGenerated - exist.WetWeatherTPbReduced)) + (new.WetWeatherTCuReduced / (lg.WetWeatherTCuGenerated - exist.WetWeatherTCuReduced)) + (new.WetWeatherTZnReduced / (lg.WetWeatherTZnGenerated - exist.WetWeatherTZnReduced))) / 3.0) as PercentReducedMetals

            from
            (
                select p.ProjectID,
                sum(WetWeatherVolumeGenerated) as WetWeatherVolumeGenerated,
                sum(WetWeatherTSSGenerated) as WetWeatherTSSGenerated,
                sum(WetWeatherTNGenerated) as WetWeatherTNGenerated,
                sum(WetWeatherTPGenerated) as WetWeatherTPGenerated,
                sum(WetWeatherFCGenerated) as WetWeatherFCGenerated,
                sum(WetWeatherTCuGenerated) as WetWeatherTCuGenerated,
                sum(WetWeatherTPbGenerated) as WetWeatherTPbGenerated,
                sum(WetWeatherTZnGenerated) as WetWeatherTZnGenerated
                from projectLoadGenerated p
                group by p.ProjectID
            ) lg
            left join
            (
                select p.ProjectID,
                sum(p.WetWeatherRetained) as WetWeatherRetained, 
                sum(p.WetWeatherTSSReduced) as WetWeatherTSSReduced,
                sum(p.WetWeatherTPbReduced) as WetWeatherTPbReduced,
                sum(p.WetWeatherTCuReduced) as WetWeatherTCuReduced,
                sum(p.WetWeatherTNReduced) as WetWeatherTNReduced,
                sum(p.WetWeatherFCReduced) as WetWeatherFCReduced,
                sum(p.WetWeatherTPReduced) as WetWeatherTPReduced,
                sum(p.WetWeatherTZnReduced) as WetWeatherTZnReduced
                from projectLoadReduced p
                where p.IsPartOfProject = 1
                group by p.ProjectID
            ) new on lg.ProjectID = new.ProjectID 
            left join 
            (
                select pidu.ProjectID,
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(p.WetWeatherRetained, 0)) end as WetWeatherRetained, 
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(p.WetWeatherTSSReduced, 0)) end as WetWeatherTSSReduced,
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(p.WetWeatherTPbReduced, 0)) end as WetWeatherTPbReduced,
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(p.WetWeatherTCuReduced, 0)) end as WetWeatherTCuReduced,
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(p.WetWeatherTNReduced, 0)) end as WetWeatherTNReduced,
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(p.WetWeatherFCReduced, 0)) end as WetWeatherFCReduced,
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(p.WetWeatherTPReduced, 0)) end as WetWeatherTPReduced,
                case when pidu.IsDistributedOnly = 1 then 0 else sum(isnull(p.WetWeatherTZnReduced, 0)) end as WetWeatherTZnReduced
                from projectIsDistributedOnly pidu
                left join projectLoadReduced p on pidu.ProjectID = p.ProjectID and p.IsPartOfProject = 0
                group by pidu.ProjectID, pidu.IsDistributedOnly
            ) exist on lg.ProjectID = exist.ProjectID
    ) wet on o.ProjectID = wet.ProjectID
)
GO

/*
select * from dbo.fProjectGrantScore(1033)
*/