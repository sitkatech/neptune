Create View dbo.vGeoServerTreatmentBMPPointLocation as
Select
	TreatmentBMPID as PrimaryKey,
	LocationPoint4326 as LocationPoint,
	TreatmentBMPName,
	p.ProjectName
from
	dbo.TreatmentBMP t
	join dbo.Project p on t.ProjectID = p.ProjectID
where 
	t.ProjectID is not null