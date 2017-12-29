ALTER TABLE [dbo].[TreatmentBMPObservation] DROP CONSTRAINT [FK_TreatmentBMPObservation_ObservationValueType_ObservationValueTypeID]
GO

alter table dbo.TreatmentBMPObservation drop column ObservationValueTypeID
go

drop table dbo.TreatmentBMPInfiltrationReading
go

drop table dbo.TreatmentBMPObservationDetail
drop table dbo.TreatmentBMPObservationDetailType
drop table dbo.ObservationValueType
