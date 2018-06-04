delete from TreatmentBMPObservation
	where TreatmentBMPAssessmentID not in (select InitialAssessmentID from FieldVisit)
	and TreatmentBMPAssessmentID not in (select PostMaintenanceAssessmentID from FieldVisit)
delete from TreatmentBMPAssessment
	where TreatmentBMPAssessmentID not in (select InitialAssessmentID from FieldVisit)
	and TreatmentBMPAssessmentID not in (select PostMaintenanceAssessmentID from FieldVisit)
