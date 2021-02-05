IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.pUpdateRegionalSubbasinIntersectionCache')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE dbo.pUpdateRegionalSubbasinIntersectionCache
GO

Create Procedure dbo.pUpdateRegionalSubbasinIntersectionCache 
As

update dbo.RegionalSubbasin
set IsInLSPCBasin = 0

-- Cache whether overlaps with an LSPC basin
update rsb
set IsInLSPCBasin = 1
from dbo.RegionalSubbasin rsb
	join dbo.LSPCBasin lspc
	on rsb.CatchmentGeometry.STIntersects(lspc.LSPCBasinGeometry) = 1

update dbo.RegionalSubbasin 
set IsInLSPCBasin = 0
where IsInLSPCBasin is null

-- store a reference to the largest intersecting LSPC basin per RSB
Update rsb
set rsb.LSPCBasinID = sub.LSPCBasinID
from dbo.RegionalSubbasin rsb join
(
	select *, ROW_NUMBER() over (partition by RegionalSubbasinID order by IntersectArea desc) as RowNumber from
	(
		select 
			RegionalSubbasinID,
			lspc.LSPCBasinID,
			rsb.CatchmentGeometry.STIntersection(LSPCBasinGeometry).STArea() as IntersectArea
		from dbo.RegionalSubbasin rsb
			inner join dbo.LSPCBasin lspc on rsb.CatchmentGeometry.STIntersects(LSPCBasinGeometry) = 1
	) sub
) sub on rsb.RegionalSubbasinID = sub.RegionalSubbasinID
where sub.RowNumber = 1

-- cache the containing RSB on BMP
update tbmp
set tbmp.RegionalSubbasinID = rsb.RegionalSubbasinID
from dbo.TreatmentBMP tbmp join dbo.RegionalSubbasin rsb
	on tbmp.LocationPoint.STWithin(rsb.CatchmentGeometry) = 1

go
