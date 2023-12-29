Create View dbo.vGeoServerTreatmentBMPDelineation as
Select
	d.DelineationID as PrimaryKey,
	DelineationGeometry4326 as DelineationGeometry,
	t.TreatmentBMPID,
	t.TreatmentBMPName,
	p.ProjectName
from
	dbo.Delineation d join dbo.DelineationType dt on d.DelineationTypeID = dt.DelineationTypeID
	join dbo.TreatmentBMP t on d.TreatmentBMPID = t.TreatmentBMPID
	join dbo.Project p on t.ProjectID = p.ProjectID
where t.ProjectID is not null