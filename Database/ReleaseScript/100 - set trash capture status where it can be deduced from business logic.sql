-- see https://projects.sitkatech.com/projects/neptune/cards/209 for the logic behind this migration

-- provides full trash capture == yes ? definitely a full trash capture BMP
update TreatmentBMP
set TrashCaptureStatusTypeID = 1
where TreatmentBMPID in (
	select distinct TreatmentBMPID from dbo.CustomAttribute ca join dbo.CustomAttributeValue v
		on ca.CustomAttributeID = v.CustomAttributeID
	Where CustomAttributeTypeID = 26 --ProvidesFullTrashCapture
		and AttributeValue = 'Yes'
)

-- sizing basis == full ? definitely a full trash capture BMP  (valid because only hydrodynamic separator has sizing basis)
update TreatmentBMP
set TrashCaptureStatusTypeID = 1
where TreatmentBMPID in (
	select TreatmentBMPID from dbo.CustomAttribute ca join dbo.CustomAttributeValue v
		on ca.CustomAttributeID = v.CustomAttributeID
	Where CustomAttributeTypeID = 1131 --Sizing basis
		and AttributeValue = 'Full Trash Capture'
)

-- in-stream trash capture == type AND not already full trash capture ? partial trash capture (valid because the fulls were already dealt with)
update TreatmentBMP
set TrashCaptureStatusTypeID = 2
where TreatmentBMPTypeID = 41 and TrashCaptureStatusTypeID <> 1

-- hydrodynamic separator == type AND not already full trash capture ? partial trash (valid because the fulls were already dealt with)
update TreatmentBMP
set TrashCaptureStatusTypeID = 2
where TreatmentBMPTypeID = 36 and TrashCaptureStatusTypeID <> 1