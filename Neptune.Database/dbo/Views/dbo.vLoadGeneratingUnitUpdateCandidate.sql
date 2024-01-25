create view dbo.vLoadGeneratingUnitUpdateCandidate
as

select SpatialGridUnitID, LoadGeneratingUnitID, LoadGeneratingUnitGeometry, IsEmptyResponseFromHRUService, ModelBasinID, RegionalSubbasinID, WaterQualityManagementPlanID, DelineationID, DateHRURequested
from
(
    SELECT sgu.SpatialGridUnitID, lgu.LoadGeneratingUnitID, lgu.LoadGeneratingUnitGeometry, lgu.IsEmptyResponseFromHRUService, lgu.ModelBasinID, lgu.RegionalSubbasinID, lgu.WaterQualityManagementPlanID, lgu.DelineationID, lgu.DateHRURequested, rank() over (partition by lgu.LoadGeneratingUnitID order by lgu.LoadGeneratingUnitGeometry.STIntersection(sgu.SpatialGridUnitGeometry).STArea() desc) as Ranking
    FROM dbo.LoadGeneratingUnit lgu 
    join dbo.SpatialGridUnit sgu on lgu.LoadGeneratingUnitGeometry.STIntersects(sgu.SpatialGridUnitGeometry) = 1
    left join dbo.HRUCharacteristic hru on lgu.LoadGeneratingUnitID = hru.LoadGeneratingUnitID
    where lgu.LoadGeneratingUnitGeometry.STArea() > 10 and lgu.RegionalSubbasinID is not null
    and not(hru.HRUCharacteristicID is not null or isnull(lgu.IsEmptyResponseFromHRUService, 0) = 1)
) a
where a.Ranking = 1

GO