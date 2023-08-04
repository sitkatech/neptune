Create view dbo.vRegionalSubbasinUpstream
as

with cteRSBs as (
 select RegionalSubbasinID, RegionalSubbasinID as BaseID, OCSurveyCatchmentID, OCSurveyDownstreamCatchmentID, CatchmentGeometry4326
     from dbo.RegionalSubbasin
 union all
 select r.RegionalSubbasinID, c.BaseID, r.OCSurveyCatchmentID, r.OCSurveyDownstreamCatchmentID, r.CatchmentGeometry4326
     from dbo.RegionalSubbasin r
     join cteRSBs c on r.OCSurveyDownstreamCatchmentID = c.OCSurveyCatchmentID
)

select BaseID as PrimaryKey, cteRSBs.RegionalSubbasinID, cteRSBs.OCSurveyCatchmentID, cteRSBs.OCSurveyDownstreamCatchmentID
from cteRSBs

GO