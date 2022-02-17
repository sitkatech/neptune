Drop View If Exists dbo.vRegionalSubbasinUpstreamCatchmentGeometry4326
GO

Create view dbo.vRegionalSubbasinUpstreamCatchmentGeometry4326
as

with cteRSBs as (
 select RegionalSubbasinID, RegionalSubbasinID as BaseID, OCSurveyDownstreamCatchmentID, CatchmentGeometry4326
     from RegionalSubbasin
 union all
 select r.RegionalSubbasinID, c.BaseID as 'BaseID', r.OCSurveyDownstreamCatchmentID, r.CatchmentGeometry4326
     from RegionalSubbasin r
         inner join cteRSBs c
             on r.OCSurveyDownstreamCatchmentID = c.RegionalSubbasinID
)

select BaseID as PrimaryKey, geometry::UnionAggregate(CatchmentGeometry4326) UpstreamCatchmentGeometry4326
from cteRSBs
group by BaseID
GO