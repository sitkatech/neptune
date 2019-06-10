Drop View If Exists dbo.vGeoServerDelineation
GO

Create View dbo.vGeoServerDelineation as
Select
	d.DelineationID,
	DelineationGeometry,
	DelineationTypeName as DelineationType,
	t.TreatmentBMPID,
	sj.StormwaterJurisdictionID,
	t.TreatmentBMPName,
	o.OrganizationName
from
	dbo.Delineation d inner join dbo.DelineationType dt
		on d.DelineationTypeID = dt.DelineationTypeID
	inner join dbo.TreatmentBMP t
		on d.TreatmentBMPID = t.TreatmentBMPID
	left join dbo.StormwaterJurisdiction sj
		on t.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
	left join dbo.Organization o
		on sj.OrganizationID = o.OrganizationID