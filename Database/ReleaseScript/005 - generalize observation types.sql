ALTER TABLE [dbo].[TreatmentBMPType] DROP CONSTRAINT [AK_TreatmentBMPType_TreatmentBMPTypeDisplayName]
ALTER TABLE [dbo].[TreatmentBMPObservation] DROP CONSTRAINT [FK_TreatmentBMPObservation_ObservationValueType_ObservationValueTypeID]

alter table dbo.TreatmentBMPObservation drop column ObservationValueTypeID
go

drop table dbo.TreatmentBMPInfiltrationReading
go

drop table dbo.TreatmentBMPObservationDetail
drop table dbo.TreatmentBMPObservationDetailType
drop table dbo.ObservationValueType


CREATE TABLE dbo.ObservationThresholdType(
	ObservationThresholdTypeID int NOT NULL CONSTRAINT PK_ObservationThresholdType_ObservationThresholdTypeID PRIMARY KEY,
	ObservationThresholdTypeName varchar(100) NOT NULL CONSTRAINT AK_ObservationThresholdType_ObservationThresholdTypeName UNIQUE,
	ObservationThresholdTypeDisplayName varchar(100) NOT NULL CONSTRAINT AK_ObservationThresholdType_ObservationThresholdTypeDisplayName UNIQUE,
	SortOrder int NOT NULL,
	ObservationThresholdTypeDescription varchar(max) null
)

CREATE TABLE dbo.ObservationTargetType(
	ObservationTargetTypeID int NOT NULL CONSTRAINT PK_ObservationTargetType_ObservationTargetTypeID PRIMARY KEY,
	ObservationTargetTypeName varchar(100) NOT NULL CONSTRAINT AK_ObservationTargetType_ObservationTargetTypeName UNIQUE,
	ObservationTargetTypeDisplayName varchar(100) NOT NULL CONSTRAINT AK_ObservationTargetType_ObservationTargetTypeDisplayName UNIQUE,
	SortOrder int NOT NULL,
	ObservationTargetTypeDescription varchar(max) null
)

CREATE TABLE dbo.ObservationTypeCollectionMethod(
	ObservationTypeCollectionMethodID int NOT NULL CONSTRAINT PK_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodID PRIMARY KEY,
	ObservationTypeCollectionMethodName varchar(100) NOT NULL CONSTRAINT AK_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodName UNIQUE,
	ObservationTypeCollectionMethodDisplayName varchar(100) NOT NULL CONSTRAINT AK_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodDisplayName UNIQUE,
	SortOrder int NOT NULL,
)

go

CREATE TABLE dbo.ObservationTypeSpecification(
	ObservationTypeSpecificationID int NOT NULL CONSTRAINT PK_ObservationTypeSpecification_ObservationTypeSpecificationID PRIMARY KEY,
	ObservationTypeSpecificationName varchar(100) NOT NULL CONSTRAINT AK_ObservationTypeSpecification_ObservationTypeSpecificationName UNIQUE,
	ObservationTypeSpecificationDisplayName varchar(100) NOT NULL CONSTRAINT AK_ObservationTypeSpecification_ObservationTypeSpecificationDisplayName UNIQUE,
	SortOrder int NOT NULL,
	ObservationTypeCollectionMethodID int NOT NULL,
	ObservationTargetTypeID int NOT NULL,
	ObservationThresholdTypeID int NOT NULL
)

go

ALTER TABLE dbo.ObservationTypeSpecification ADD CONSTRAINT FK_ObservationTypeSpecification_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodID FOREIGN KEY(ObservationTypeCollectionMethodID) REFERENCES dbo.ObservationTypeCollectionMethod(ObservationTypeCollectionMethodID)
ALTER TABLE dbo.ObservationTypeSpecification ADD CONSTRAINT FK_ObservationTypeSpecification_ObservationTargetType_ObservationTargetTypeID FOREIGN KEY(ObservationTargetTypeID) REFERENCES dbo.ObservationTargetType(ObservationTargetTypeID)
ALTER TABLE dbo.ObservationTypeSpecification ADD CONSTRAINT FK_ObservationTypeSpecification_ObservationThresholdType_ObservationThresholdTypeID FOREIGN KEY(ObservationThresholdTypeID) REFERENCES dbo.ObservationThresholdType(ObservationThresholdTypeID)

go

alter table dbo.TreatmentBMP drop column InletCount
alter table dbo.TreatmentBMP drop column OutletCount

alter table dbo.TreatmentBMPObservation add ObservationData nvarchar(max) not null

alter table dbo.TreatmentBMPType drop column IsDistributedBMPType
alter table dbo.TreatmentBMPType drop column TreatmentBMPTypeDisplayName
alter table dbo.TreatmentBMPType drop column SortOrder
alter table dbo.TreatmentBMPType drop column DisplayColor
alter table dbo.TreatmentBMPType drop column GlyphiconClass

alter table dbo.ObservationType drop column ObservationTypeDisplayName
alter table dbo.ObservationType drop column sortOrder
alter table dbo.ObservationType drop column HasBenchmarkAndThreshold
alter table dbo.ObservationType drop column ThresholdPercentDecline
alter table dbo.ObservationType drop column ThresholdPercentDeviation

alter table dbo.ObservationType add ObservationTypeSpecificationID int not null
alter table dbo.ObservationType add BenchmarkDescription varchar(300) null
alter table dbo.ObservationType add ThresholdDescription varchar(300) null
alter table dbo.ObservationType add AssessmentDescription varchar(300) not null
alter table dbo.ObservationType add MeasurementLabel varchar(50) not null
alter table dbo.ObservationType add LabelForWhatIsObserved varchar(50) not null
alter table dbo.ObservationType add ObservationSchema nvarchar(max) not null

go

ALTER TABLE dbo.ObservationType ADD CONSTRAINT FK_ObservationType_ObservationTypeSpecification_ObservationTypeSpecificationID FOREIGN KEY(ObservationTypeSpecificationID) REFERENCES dbo.ObservationTypeSpecification(ObservationTypeSpecificationID)


insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(11, 'ObservationTypes', 'Observation Types', 2)

insert into dbo.neptunepage(TenantID, NeptunePageTypeID)
select 
	t.tenantid,
	npt.NeptunePageTypeID
from dbo.neptunepagetype npt
cross join dbo.tenant t
where npt.NeptunePageTypeID = 11