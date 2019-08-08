Delete from dbo.CustomAttribute where TreatmentBMPID in (select TreatmentBMPID from dbo.TreatmentBMP where TreatmentBMPTypeID = 42 and StormwaterJurisdictionID = 4)

delete fv
from dbo.TreatmentBMP b
join dbo.FieldVisit fv on b.TreatmentBMPID = fv.TreatmentBMPID
where TreatmentBMPTypeID = 42 and StormwaterJurisdictionID = 4

Delete from dbo.TreatmentBMP where TreatmentBMPTypeID = 42 and StormwaterJurisdictionID = 4