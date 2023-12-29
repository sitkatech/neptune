Create View dbo.vTreatmentBMPObservationPassFail
as

select	tbo.TreatmentBMPObservationID, tbo.TreatmentBMPAssessmentID, tbo.ObservationValue, tbo.Notes,
		tbaot.TreatmentBMPAssessmentObservationTypeID, tbaot.TreatmentBMPAssessmentObservationTypeName,-- tbaot.TreatmentBMPAssessmentObservationTypeSchema, 
        tbaot.ObservationTypeSpecificationID, tbaot.ObservationTypeSpecificationName, tbaot.ObservationTypeSpecificationDisplayName, 
        tbaot.ObservationTypeCollectionMethodID, tbaot.ObservationTypeCollectionMethodName, tbaot.ObservationTypeCollectionMethodDisplayName, tbaot.ObservationTypeCollectionMethodDescription,
        tbaot.PropertiesToObserve, tbaot.AssessmentDescription, tbaot.PassingScoreLabel, tbaot.FailingScoreLabel
from dbo.vTreatmentBMPObservation tbo
join dbo.vTreatmentBMPAssessmentObservationTypePassFail tbaot on tbo.TreatmentBMPAssessmentObservationTypeID = tbaot.TreatmentBMPAssessmentObservationTypeID

GO