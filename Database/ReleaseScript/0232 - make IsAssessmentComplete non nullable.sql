update dbo.TreatmentBMPAssessment
set IsAssessmentComplete = 0
where IsAssessmentComplete is null

alter table dbo.TreatmentBMPAssessment alter column IsAssessmentComplete bit not null