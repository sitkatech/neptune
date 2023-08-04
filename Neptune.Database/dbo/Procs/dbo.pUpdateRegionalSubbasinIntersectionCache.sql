Create Procedure dbo.pUpdateRegionalSubbasinIntersectionCache 
As

update dbo.RegionalSubbasin
set IsInModelBasin = 0

-- Cache whether overlaps with an Model basin
update rsb
set IsInModelBasin = 1
from dbo.RegionalSubbasin rsb
	join dbo.ModelBasin Model
	on rsb.CatchmentGeometry.STIntersects(Model.ModelBasinGeometry) = 1

update dbo.RegionalSubbasin 
set IsInModelBasin = 0
where IsInModelBasin is null

-- store a reference to the largest intersecting Model basin per RSB
Update rsb
set rsb.ModelBasinID = sub.ModelBasinID
from dbo.RegionalSubbasin rsb join
(
	select *, ROW_NUMBER() over (partition by RegionalSubbasinID order by IntersectArea desc) as RowNumber from
	(
		select 
			RegionalSubbasinID,
			Model.ModelBasinID,
			rsb.CatchmentGeometry.STIntersection(ModelBasinGeometry).STArea() as IntersectArea
		from dbo.RegionalSubbasin rsb
			inner join dbo.ModelBasin Model on rsb.CatchmentGeometry.STIntersects(ModelBasinGeometry) = 1
	) sub
) sub on rsb.RegionalSubbasinID = sub.RegionalSubbasinID
where sub.RowNumber = 1

-- cache the containing RSB on BMP
update tbmp
set tbmp.RegionalSubbasinID = rsb.RegionalSubbasinID
from dbo.TreatmentBMP tbmp join dbo.RegionalSubbasin rsb
	on tbmp.LocationPoint.STWithin(rsb.CatchmentGeometry) = 1

go
