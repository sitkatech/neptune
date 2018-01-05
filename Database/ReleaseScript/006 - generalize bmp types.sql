alter table dbo.TreatmentBMPType add TreatmentBMPTypeDescription varchar(1000) not null

alter table dbo.TreatmentBMPTypeObservationType add OverrideAssessmentScoreIfFailing bit null
alter table dbo.TreatmentBMPTypeObservationType alter column AssessmentScoreWeight float null

go

alter table dbo.TreatmentBMPTypeObservationType add constraint CK_AssessmentScoreWeightNullIfOverrideNotNull CHECK ((AssessmentScoreWeight IS NOT NULL AND OverrideAssessmentScoreIfFailing IS NULL) OR (AssessmentScoreWeight IS NULL AND OverrideAssessmentScoreIfFailing IS NOT NULL))