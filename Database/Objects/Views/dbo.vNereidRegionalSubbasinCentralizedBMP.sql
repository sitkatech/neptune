drop view if exists dbo.vNereidRegionalSubbasinCentralizedBMP
GO

create view dbo.vNereidRegionalSubbasinCentralizedBMP
As
Select
	rsb.RegionalSubbasinID as PrimaryKey,
	rsb.RegionalSubbasinID,
	rsb.OCSurveyCatchmentID,
	bmp.TreatmentBMPID,
	ROW_NUMBER() over (partition by rsb.RegionalSubbasinID order by d.DelineationID) as RowNumber
from dbo.Delineation d
	join dbo.TreatmentBMP bmp
		on d.TreatmentBMPID = bmp.TreatmentBMPID
	join dbo.RegionalSubbasin rsb
		on bmp.RegionalSubbasinID = rsb.RegionalSubbasinID
where
	DelineationTypeID = 1 
	and rsb.IsInLSPCBasin = 1 
GO
