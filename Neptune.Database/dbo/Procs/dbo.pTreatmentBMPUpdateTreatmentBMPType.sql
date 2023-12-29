Create Procedure dbo.pTreatmentBMPUpdateTreatmentBMPType
(
	@treatmentBMPID int,
	@treatmentBMPTypeID int
)
As

delete from dbo.MaintenanceRecord
where TreatmentBMPID = @treatmentBMPID

delete from dbo.TreatmentBMPBenchmarkAndThreshold
where TreatmentBMPID = @treatmentBMPID

delete from dbo.TreatmentBMPAssessment
where TreatmentBMPID = @treatmentBMPID

delete cav
from dbo.CustomAttribute ca
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
left join 
(
	select tbtcat.CustomAttributeTypeID
	from dbo.TreatmentBMPTypeCustomAttributeType tbtcat
	where tbtcat.TreatmentBMPTypeID = @treatmentBMPTypeID
) existing on ca.CustomAttributeTypeID = existing.CustomAttributeTypeID and existing.CustomAttributeTypeID is null
where ca.TreatmentBMPID = @treatmentBMPID

delete ca
from dbo.CustomAttribute ca
left join 
(
	select tbtcat.CustomAttributeTypeID
	from dbo.TreatmentBMPTypeCustomAttributeType tbtcat
	where tbtcat.TreatmentBMPTypeID = @treatmentBMPTypeID
) existing on ca.CustomAttributeTypeID = existing.CustomAttributeTypeID and existing.CustomAttributeTypeID is null
where ca.TreatmentBMPID = @treatmentBMPID

update dbo.TreatmentBMP
set TreatmentBMPTypeID = @treatmentBMPTypeID
where TreatmentBMPID = @treatmentBMPID

GO