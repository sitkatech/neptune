exec sp_rename 'dbo.FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID', 'FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID_TreatmentBMPTypeID_Observ', 'FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_Treat', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPObservation_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID', 'FK_TreatmentBMPObservation_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPObservation_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID_TreatmentBMPTypeID_ObservationTypeI', 'FK_TreatmentBMPObservation_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_TreatmentBMPTyp', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPTypeObservationType_ObservationType_ObservationTypeID', 'FK_TreatmentBMPTypeAssessmentObservationType_ObservationType_ObservationTypeID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPTypeObservationType_TreatmentBMPType_TreatmentBMPTypeID', 'FK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPType_TreatmentBMPTypeID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPTypeObservationType_TreatmentBMPType_TreatmentBMPTypeID_TenantID', 'FK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPType_TreatmentBMPTypeID_TenantID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPTypeObservationType_ObservationType_ObservationTypeID_TenantID', 'FK_TreatmentBMPTypeAssessmentObservationType_ObservationType_ObservationTypeID_TenantID', 'OBJECT';
exec sp_rename 'dbo.PK_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID', 'PK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID', 'OBJECT';
exec sp_rename 'dbo.AK_TreatmentBMPTypeObservationType_TreatmentBMPTypeID_ObservationTypeID', 'AK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeID_ObservationTypeID', 'OBJECT';
exec sp_rename 'dbo.AK_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID_TreatmentBMPTypeID_ObservationTypeID', 'AK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_TreatmentBMPTypeID_ObservationTypeID', 'OBJECT';
exec sp_rename 'dbo.TreatmentBMPTypeObservationType.TreatmentBMPTypeObservationTypeID', 'TreatmentBMPTypeAssessmentObservationTypeID', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPObservation.TreatmentBMPTypeObservationTypeID', 'TreatmentBMPTypeAssessmentObservationTypeID', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPBenchmarkAndThreshold.TreatmentBMPTypeObservationTypeID', 'TreatmentBMPTypeAssessmentObservationTypeID', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPTypeObservationType', 'TreatmentBMPTypeAssessmentObservationType';