Drop View If Exists dbo.vGeoServerDelineation
GO

Create View dbo.vGeoServerDelineation as
Select
	d.DelineationID,
	DelineationGeometry,
	DelineationTypeName as DelineationType,
	TreatmentBMPID,
	StormwaterJurisdictionID
from
	dbo.Delineation d inner join dbo.DelineationType dt
		on d.DelineationTypeID = dt.DelineationTypeID
	inner join dbo.TreatmentBMP t
		on d.DelineationID = t.DelineationID