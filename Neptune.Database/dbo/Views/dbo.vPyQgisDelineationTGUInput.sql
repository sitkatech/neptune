Create View dbo.vPyQgisDelineationTGUInput
As
Select
	d.DelineationID as DelinID,
	DelineationGeometry,
	t.StormwaterJurisdictionID as SJID,
	ISNULL(Case
		when tcs.TrashCaptureStatusTypeDisplayName = 'Full' then 100
		when tcs.TrashCaptureStatusTypeDisplayName = 'None' or tcs.TrashCaptureStatusTypeDisplayName = 'Not Provided' then 0
		when t.TrashCaptureEffectiveness is Null then 0
		else t.TrashCaptureEffectiveness
	end, 0.0) as TCEffect
From dbo.Delineation d
	join dbo.TreatmentBMP t
		on d.TreatmentBMPID = t.TreatmentBMPID
	join dbo.TrashCaptureStatusType tcs
		on tcs.TrashCaptureStatusTypeID = t.TrashCaptureStatusTypeID
	Where IsVerified = 1
GO