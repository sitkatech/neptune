create view dbo.vProjectLoadGeneratingUnitUpdateCandidate
as

select ProjectID, SpatialGridUnitID, ProjectLoadGeneratingUnitID, ProjectLoadGeneratingUnitGeometry, IsEmptyResponseFromHRUService, ModelBasinID, RegionalSubbasinID, WaterQualityManagementPlanID, DelineationID, DateHRURequested
from
(
    SELECT plgu.ProjectID, sgu.SpatialGridUnitID, plgu.ProjectLoadGeneratingUnitID, plgu.ProjectLoadGeneratingUnitGeometry, plgu.IsEmptyResponseFromHRUService, plgu.ModelBasinID, plgu.RegionalSubbasinID, plgu.WaterQualityManagementPlanID, plgu.DelineationID, DateHRURequested, rank() over (partition by plgu.ProjectLoadGeneratingUnitID order by plgu.ProjectLoadGeneratingUnitGeometry.STIntersection(sgu.SpatialGridUnitGeometry).STArea() desc) as Ranking
    FROM dbo.ProjectLoadGeneratingUnit plgu 
    join dbo.SpatialGridUnit sgu on plgu.ProjectLoadGeneratingUnitGeometry.STIntersects(sgu.SpatialGridUnitGeometry) = 1
    where plgu.ProjectLoadGeneratingUnitGeometry.STArea() > 10
) a
where Ranking = 1

GO