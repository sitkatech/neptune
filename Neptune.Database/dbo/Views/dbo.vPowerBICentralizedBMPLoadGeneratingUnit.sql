create view dbo.vPowerBICentralizedBMPLoadGeneratingUnit
as
select
Row_number() over(order by bmp.TreatmentBMPID desc) as PrimaryKey, 
	bmp.TreatmentBMPID,
	lgu.LoadGeneratingUnitID
from
	LoadGeneratingUnit lgu join Delineation d
		on d.DelineationGeometry.STContains(lgu.LoadGeneratingUnitGeometry) = 1
	join TreatmentBMP bmp
		on d.TreatmentBMPID = bmp.TreatmentBMPID
where d.DelineationTypeID = 1 and bmp.ProjectID is null
