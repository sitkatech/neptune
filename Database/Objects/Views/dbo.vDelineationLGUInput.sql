Drop View If Exists dbo.vDelineationLGUInput
GO

Create view dbo.vDelineationLGUInput
as
Select
	DelineationID as DelinID,
	DelineationGeometry
from
	dbo.Delineation d join dbo.TreatmentBMP t
		on d.TreatmentBMPID = t.TreatmentBMPID
	join dbo.TreatmentBMPType ttype
		on ttype.TreatmentBMPTypeID = t.TreatmentBMPTypeID
Where d.DelineationTypeID = 2
 and ttype.TreatmentBMPModelingTypeID is not null
GO