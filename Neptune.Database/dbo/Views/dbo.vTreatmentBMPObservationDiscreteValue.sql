Create View dbo.vTreatmentBMPObservationDiscreteValue
as

select	tbo.TreatmentBMPObservationID, tbo.TreatmentBMPAssessmentID, tbo.ObservationValue, tbo.Notes,
		tbaot.TreatmentBMPAssessmentObservationTypeID, tbaot.TreatmentBMPAssessmentObservationTypeName,-- tbaot.TreatmentBMPAssessmentObservationTypeSchema, 
        tbaot.ObservationTypeSpecificationID, tbaot.ObservationTypeSpecificationName, tbaot.ObservationTypeSpecificationDisplayName, 
        tbaot.ObservationTypeCollectionMethodID, tbaot.ObservationTypeCollectionMethodName, tbaot.ObservationTypeCollectionMethodDisplayName, tbaot.ObservationTypeCollectionMethodDescription,
        tbaot.PropertiesToObserve, tbaot.AssessmentDescription, 
        tbaot.BenchmarkDescription, tbaot.ThresholdDescription, tbaot.MeasurementUnitLabel, tbaot.MeasurementUnitTypeID, 
        tbaot.MinimumNumberOfObservations, tbaot.MaximumNumberOfObservations, tbaot.MinimumValueOfObservations, tbaot.MaximumValueOfObservations
from dbo.vTreatmentBMPObservation tbo
join dbo.vTreatmentBMPAssessmentObservationTypeDiscreteValue tbaot on tbo.TreatmentBMPAssessmentObservationTypeID = tbaot.TreatmentBMPAssessmentObservationTypeID


GO