drop procedure if exists dbo.pTreatmentBMPUpdateTreatmentBMPType
GO

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

delete from dbo.CustomAttribute
where CustomAttributeID = 
	(select CustomAttributeID
	from 
	((select *
	from dbo.TreatmentBMPTypeCustomAttributeType
	where TreatmentBMPTypeID = @treatmentBMPTypeID) tbmp
	inner join dbo.CustomAttribute ca on ca.TreatmentBMPTypeCustomAttributeTypeID != tbmp.TreatmentBMPTypeCustomAttributeTypeID
	)
	where ca.TreatmentBMPID = @treatmentBMPID)

update dbo.TreatmentBMP
set TreatmentBMPTypeID = @treatmentBMPTypeID
where TreatmentBMPID = @treatmentBMPID

GO