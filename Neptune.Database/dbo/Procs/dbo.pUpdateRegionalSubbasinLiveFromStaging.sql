Create Procedure dbo.pUpdateRegionalSubbasinLiveFromStaging
As

-- make the rsb geometries valid
update dbo.RegionalSubbasinStaging set CatchmentGeometry = CatchmentGeometry.MakeValid() where CatchmentGeometry.STIsValid() = 0

Merge dbo.RegionalSubbasin t Using dbo.RegionalSubbasinStaging s
	on (t.OCSurveyCatchmentID = s.OCSurveyCatchmentID)
When Matched
	Then Update set
		t.DrainID = s.DrainID,
		t.Watershed = s.Watershed,
		t.CatchmentGeometry = s.CatchmentGeometry,
		t.OCSurveyDownstreamCatchmentID =
			case
				when s.OCSurveyDownstreamCatchmentID = 0 then null
				else s.OCSurveyDownstreamCatchmentID
			end,
		t.LastUpdate = GetDate(),
		t.CatchmentGeometry4326 = null,
		t.IsWaitingForLGURefresh = 
			Case
				when s.CatchmentGeometry.STAsText() <> t.CatchmentGeometry.STAsText() then 1
				else 0
			end
When not matched by Target
	Then insert (
		DrainID,
		Watershed,
		CatchmentGeometry,
		OCSurveyCatchmentID,
		OCSurveyDownstreamCatchmentID,
		CatchmentGeometry4326,
		LastUpdate,
		IsWaitingForLGURefresh
	)
	values (
		s.DrainID,
		s.Watershed,
		s.CatchmentGeometry,
		s.OCSurveyCatchmentID,
		case
			when s.OCSurveyDownstreamCatchmentID = 0 then null
			else s.OCSurveyDownstreamCatchmentID
		end,
		null,
		GetDate(),
		1
	)
When Not Matched by Source
	Then Delete;


-- Watershed table is made up from the dissolves/aggregation of the Regional Subbasins feature layer.
-- clear out any watershed associations
update dbo.TreatmentBMP
set WatershedID = null


select Watershed as WatershedName, geometry::UnionAggregate(CatchmentGeometry) as WatershedGeometry, geometry::UnionAggregate(CatchmentGeometry4326) as WatershedGeometry4326
into #WatershedStaging
from dbo.RegionalSubbasin rs
group by Watershed
order by Watershed

Merge dbo.Watershed t Using #WatershedStaging s
	on (t.WatershedName = s.WatershedName)
When Matched
	Then Update set
		t.WatershedGeometry = s.WatershedGeometry,
		t.WatershedGeometry4326 = null
When not matched by Target
	Then insert (
		WatershedName,
		WatershedGeometry
	)
	values (
		s.WatershedName,
		s.WatershedGeometry
	)
When Not Matched by Source
	Then Delete;

GO