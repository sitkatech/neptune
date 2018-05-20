alter table dbo.TreatmentBMP
alter column TreatmentBMPName varchar(50) not null

ALTER TABLE [dbo].[TreatmentBMPAssessment] DROP CONSTRAINT [FK_TreatmentBMPAssessment_StormwaterAssessmentType_StormwaterAssessmentTypeID]
GO

alter table dbo.TreatmentBMPAssessment
drop column StormwaterAssessmentTypeID

drop table dbo.StormwaterAssessmentType