Drop View If Exists dbo.vGeoServerTreatmentBMPPointLocation
GO

Create View dbo.vGeoServerTreatmentBMPPointLocation as
Select
	TreatmentBMPID as PrimaryKey,
	LocationPoint4326 as LocationPoint,
	TreatmentBMPName
from
	dbo.TreatmentBMP