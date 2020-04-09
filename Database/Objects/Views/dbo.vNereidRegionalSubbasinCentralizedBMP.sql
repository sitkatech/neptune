drop view if exists dbo.vNereidRegionalSubbasinCentralizedBMP
GO

create view dbo.vNereidRegionalSubbasinCentralizedBMP
As
Select
	rsb.RegionalSubbasinID as PrimaryKey,
	rsb.RegionalSubbasinID,
	bmp.TreatmentBMPID
from dbo.Delineation d
	join dbo.TreatmentBMP bmp
		on d.TreatmentBMPID = bmp.TreatmentBMPID
	join dbo.RegionalSubbasin rsb
		on bmp.RegionalSubbasinID = rsb.RegionalSubbasinID
where
	DelineationTypeID = 1 
	and rsb.IsInLSPCBasin = 1 
GO
