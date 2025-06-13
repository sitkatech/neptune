Create Procedure dbo.pRegionalSubbasinGenerateNetwork

(
    @regionalSubbasinID int,
    @upstreamOnly bit = 0,
    @downstreamOnly bit = 0
)

As
        drop table if exists #rsbIDs
        create table #rsbIDs
        (
            RegionalSubbasinID int not null primary key,
            OCSurveyCatchmentID int not null,
            OCSurveyDownstreamCatchmentID int null,
            --This is the depth of the Regional Subbasin in the network, where 0 is the root Regional Subbasin, positive numbers are upstream and negative numbers are downstream
            Depth int not null
        )

        insert into #rsbIDs(RegionalSubbasinID, OCSurveyCatchmentID, OCSurveyDownstreamCatchmentID, Depth)
        select n.RegionalSubbasinID, n.OCSurveyCatchmentID, n.OCSurveyDownstreamCatchmentID, 0 as Depth
        from dbo.RegionalSubbasin n
        where n.RegionalSubbasinID = @regionalSubbasinID

        if @downstreamOnly <> 1
        begin
        -- Find all upstream Regional Subbasins
            declare @depth int = 0
            while(exists(select 1 from dbo.RegionalSubbasin n join #rsbIDs s on n.OCSurveyDownstreamCatchmentID = s.OCSurveyCatchmentID and s.Depth = @depth left join #rsbIDs s2 on n.RegionalSubbasinID = s2.RegionalSubbasinID and s2.RegionalSubbasinID is null))
            begin
                    insert into #rsbIDs(RegionalSubbasinID, OCSurveyCatchmentID, OCSurveyDownstreamCatchmentID, Depth)
                    select a.RegionalSubbasinID, a.OCSurveyCatchmentID, a.OCSurveyDownstreamCatchmentID, a.Depth
                    from
                    (
                        select distinct n.RegionalSubbasinID, n.OCSurveyCatchmentID, n.OCSurveyDownstreamCatchmentID, s.Depth + 1 as Depth
                        from dbo.RegionalSubbasin n
                        join #rsbIDs s on n.OCSurveyDownstreamCatchmentID = s.OCSurveyCatchmentID and s.Depth = @depth
                    ) a
                    left join #rsbIDs p on a.RegionalSubbasinID = p.RegionalSubbasinID
                    where p.RegionalSubbasinID is null
                set @depth = @depth + 1
            end
        end

        if @upstreamOnly <> 1
        begin
            -- Find all downstream Regional Subbasins
            set @depth = 0
            while(exists(select 1 from dbo.RegionalSubbasin n join #rsbIDs s on n.OCSurveyCatchmentID = s.OCSurveyDownstreamCatchmentID and s.Depth = @depth left join #rsbIDs s2 on n.RegionalSubbasinID = s2.RegionalSubbasinID and s2.RegionalSubbasinID is null))
            begin
                    insert into #rsbIDs(RegionalSubbasinID, OCSurveyCatchmentID, OCSurveyDownstreamCatchmentID, Depth)
                    select a.RegionalSubbasinID, a.OCSurveyCatchmentID, a.OCSurveyDownstreamCatchmentID, a.Depth
                    from
                    (
                        select distinct n.RegionalSubbasinID, n.OCSurveyCatchmentID, n.OCSurveyDownstreamCatchmentID, s.Depth - 1 as Depth
                        from dbo.RegionalSubbasin n
                        join #rsbIDs s on n.OCSurveyCatchmentID = s.OCSurveyDownstreamCatchmentID and s.Depth = @depth
                    ) a
                    left join #rsbIDs p on a.RegionalSubbasinID = p.RegionalSubbasinID
                    where p.RegionalSubbasinID is null
                set @depth = @depth - 1
            end
        end
        
        select cast(Row_number() over (order by Depth, #rsbIDs.RegionalSubbasinID) as int) as [ResultKey], @regionalSubbasinID as BaseRegionalSubbasinID, rsb.RegionalSubbasinID as CurrentNodeRegionalSubbasinID, #rsbIDs.OCSurveyCatchmentID, #rsbIDs.OCSurveyDownstreamCatchmentID, Depth, rsb.CatchmentGeometry4326, geometry::STGeomFromText('LINESTRING(' + CAST(rsb.CatchmentGeometry4326.STCentroid().STX AS VARCHAR(20)) + ' ' + CAST(rsb.CatchmentGeometry4326.STCentroid().STY AS VARCHAR(20)) + ', ' + CAST(rsbu.CatchmentGeometry4326.STCentroid().STX AS VARCHAR(20)) + ' ' + CAST(rsbu.CatchmentGeometry4326.STCentroid().STY AS VARCHAR(20)) + ')', 4326) as DownstreamLineGeometry
        from #rsbIDs
        join dbo.RegionalSubbasin rsb on #rsbIDs.OCSurveyCatchmentID = rsb.OCSurveyCatchmentID
        left join dbo.RegionalSubbasin rsbu on #rsbIDs.OCSurveyDownstreamCatchmentID = rsbu.OCSurveyCatchmentID
        order by Depth, #rsbIDs.RegionalSubbasinID

GO