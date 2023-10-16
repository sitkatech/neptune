Create view dbo.vRegionalSubbasinUpstreamCatchmentGeometry
as

with cteRSBs as (
 select RegionalSubbasinID, RegionalSubbasinID as BaseID, OCSurveyCatchmentID, OCSurveyDownstreamCatchmentID, CatchmentGeometry
     from dbo.RegionalSubbasin
 union all
 select r.RegionalSubbasinID, c.BaseID, r.OCSurveyCatchmentID, r.OCSurveyDownstreamCatchmentID, r.CatchmentGeometry
     from dbo.RegionalSubbasin r
     join cteRSBs c on r.OCSurveyDownstreamCatchmentID = c.OCSurveyCatchmentID
)

select BaseID as PrimaryKey, geometry::UnionAggregate(CatchmentGeometry) UpstreamCatchmentGeometry
from cteRSBs
group by BaseID
GO