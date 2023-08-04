Drop View If Exists dbo.vPyQgisDelineationLGUInput
GO

Create view dbo.vPyQgisDelineationLGUInput
as
Select
	DelineationID as DelinID,
	DelineationGeometry
from
	dbo.Delineation d join dbo.TreatmentBMP t
		on d.TreatmentBMPID = t.TreatmentBMPID
	join dbo.TreatmentBMPType ttype
		on ttype.TreatmentBMPTypeID = t.TreatmentBMPTypeID
Where d.DelineationTypeID = 2  -- distributed
 --and d.IsVerified = 1 -- allow provisional delineations; they will be excluded from model results later.
 and ttype.TreatmentBMPModelingTypeID is not null  --only include modeling BMPs
 and t.ProjectID is null --don't include project treatment bmps
GO