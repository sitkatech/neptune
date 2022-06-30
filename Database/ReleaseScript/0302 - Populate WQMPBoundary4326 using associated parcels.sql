
update wqmp
set wqmp.WaterQualityManagementPlanBoundary4326 = 
(	
	select geometry::UnionAggregate(ParcelGeometry4326)
	from dbo.WaterQualityManagementPlanParcel wp	
	join dbo.Parcel p on wp.ParcelID = p.ParcelID
	where wp.WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID
	group by wp.WaterQualityManagementPlanID
)
from dbo.WaterQualityManagementPlan wqmp 
