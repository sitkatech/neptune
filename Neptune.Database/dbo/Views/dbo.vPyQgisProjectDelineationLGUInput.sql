--We are going to use this to get ALL delineations, and then in our python script filter down
--to non-project delineations and our delineations only
Create view dbo.vPyQgisProjectDelineationLGUInput
as
Select
	DelineationID as DelinID,
	ProjectID,
	DelineationGeometry
from
	dbo.Delineation d join dbo.TreatmentBMP t
		on d.TreatmentBMPID = t.TreatmentBMPID
	join dbo.TreatmentBMPType ttype
		on ttype.TreatmentBMPTypeID = t.TreatmentBMPTypeID
Where d.DelineationTypeID = 2  -- distributed
 --and d.IsVerified = 1 -- allow provisional delineations; they will be excluded from model results later.
 and ttype.TreatmentBMPModelingTypeID is not null  --only include modeling BMPs
GO