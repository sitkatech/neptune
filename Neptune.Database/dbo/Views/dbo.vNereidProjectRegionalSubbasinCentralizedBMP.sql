create view dbo.vNereidProjectRegionalSubbasinCentralizedBMP
As
Select
	rsb.RegionalSubbasinID as PrimaryKey,
	rsb.RegionalSubbasinID,
	rsb.OCSurveyCatchmentID,
	bmp.ProjectID,
	bmp.TreatmentBMPID,
	bmp.UpstreamBMPID,
	ROW_NUMBER() over (partition by bmp.ProjectID, rsb.RegionalSubbasinID order by d.DelineationID) as RowNumber
from dbo.Delineation d
	join dbo.TreatmentBMP bmp
		on d.TreatmentBMPID = bmp.TreatmentBMPID
	join dbo.RegionalSubbasin rsb
		on bmp.RegionalSubbasinID = rsb.RegionalSubbasinID
where
	DelineationTypeID = 1 
	and rsb.IsInModelBasin = 1 
	and bmp.ProjectID is not null
GO
