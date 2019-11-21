drop procedure if exists dbo.pUpdateNetworkCatchmentLiveFromStaging
GO

Create Procedure dbo.pUpdateNetworkCatchmentLiveFromStaging
As

Merge NetworkCatchment t Using NetworkCatchmentStaging s
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
		t.CatchmentGeometry4326 = null
When not matched by Target
	Then insert (
		DrainID,
		Watershed,
		CatchmentGeometry,
		OCSurveyCatchmentID,
		OCSurveyDownstreamCatchmentID,
		CatchmentGeometry4326,
		LastUpdate
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
		GetDate()
	)
When Not Matched by Source
	Then Delete;
GO