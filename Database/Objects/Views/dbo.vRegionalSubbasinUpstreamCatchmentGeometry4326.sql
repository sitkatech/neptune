Drop View If Exists dbo.vRegionalSubbasinUpstreamCatchmentGeometry4326
GO

Create view dbo.vRegionalSubbasinUpstreamCatchmentGeometry4326
as

with cteRSBs as (
 select RegionalSubbasinID, RegionalSubbasinID as BaseID, OCSurveyCatchmentID, OCSurveyDownstreamCatchmentID, CatchmentGeometry4326
     from dbo.RegionalSubbasin
 union all
 select r.RegionalSubbasinID, c.BaseID, r.OCSurveyCatchmentID, r.OCSurveyDownstreamCatchmentID, r.CatchmentGeometry4326
     from dbo.RegionalSubbasin r
     join cteRSBs c on r.OCSurveyDownstreamCatchmentID = c.OCSurveyCatchmentID
)

select BaseID as PrimaryKey, geometry::UnionAggregate(CatchmentGeometry4326) UpstreamCatchmentGeometry4326
from cteRSBs
group by BaseID
GO