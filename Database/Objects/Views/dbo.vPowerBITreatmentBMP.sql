drop view if exists dbo.vPowerBITreatmentBMP
GO

create view dbo.vPowerBITreatmentBMP
as
select 
	bmp.TreatmentBMPID as PrimaryKey,
	bmp.TreatmentBMPName,
	ty.TreatmentBMPTypeName,
	bmp.LocationPoint4326.STX as LocationLon,
	bmp.LocationPoint4326.STY as LocationLat,
	w.WatershedName as Watershed,
	bmp.UpstreamBMPID,
	dt.DelineationTypeDisplayName as DelineationType,
	bmp.WaterQualityManagementPlanID,
	att.*

from
	dbo.TreatmentBMP bmp 
	left join dbo.Delineation d
		on bmp.TreatmentBMPID = d.TreatmentBMPID
	left join dbo.Watershed w
		on bmp.WatershedID = w.WatershedID
	left join dbo.DelineationType dt
		on d.DelineationTypeID = dt.DelineationTypeID
	left join dbo.TreatmentBMPModelingAttribute att
		on bmp.TreatmentBMPID = att.TreatmentBMPID
	join dbo.TreatmentBMPType ty
		on bmp.TreatmentBMPTypeID = ty.TreatmentBMPTypeID
where ty.TreatmentBMPModelingTypeID is not null