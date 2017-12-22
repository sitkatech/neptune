CREATE TABLE dbo.ModeledCatchment(
	ModeledCatchmentID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ModeledCatchment_ModeledCatchmentID PRIMARY KEY,
	TenantID int not null,
	ModeledCatchmentName varchar(100) NOT NULL,
	StormwaterJurisdictionID int NOT NULL,
	Notes varchar(1000) NULL,
	ModeledCatchmentGeometry geometry NULL,
	CONSTRAINT AK_ModeledCatchment_ModeledCatchmentName_StormwaterJurisdictionID UNIQUE (ModeledCatchmentName, StormwaterJurisdictionID),
	constraint AK_ModeledCatchment_ModeledCatchmentID_TenantID unique (ModeledCatchmentID, TenantID),
)

CREATE TABLE dbo.ModeledCatchmentGeometryStaging(
	ModeledCatchmentGeometryStagingID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ModeledCatchmentGeometryStaging_ModeledCatchmentGeometryStagingID PRIMARY KEY,
	TenantID int not null,
	PersonID int NOT NULL,
	FeatureClassName varchar(255) NOT NULL,
	GeoJson varchar(max) NOT NULL,
	SelectedProperty varchar(255) NULL,
	ShouldImport bit NOT NULL,
)

CREATE TABLE dbo.ObservationType(
	ObservationTypeID int NOT NULL CONSTRAINT PK_ObservationType_ObservationTypeID PRIMARY KEY,
	ObservationTypeName varchar(100) NOT NULL CONSTRAINT AK_ObservationType_ObservationTypeName UNIQUE (ObservationTypeName),
	ObservationTypeDisplayName varchar(100) NOT NULL,
	SortOrder int NOT NULL,
	MeasurementUnitTypeID int NOT NULL,
	HasBenchmarkAndThreshold bit NOT NULL,
	ThresholdPercentDecline bit NOT NULL,
	ThresholdPercentDeviation bit NOT NULL,
)

CREATE TABLE dbo.ObservationValueType(
	ObservationValueTypeID int NOT NULL CONSTRAINT PK_ObservationValueType_ObservationValueTypeID PRIMARY KEY,
	ObservationValueTypeName varchar(100) NOT NULL CONSTRAINT AK_ObservationValueType_ObservationValueTypeName UNIQUE,
	ObservationValueTypeDisplayName varchar(100) NOT NULL CONSTRAINT AK_ObservationValueType_ObservationValueTypeDisplayName UNIQUE,
	SortOrder int NOT NULL,
)

CREATE TABLE dbo.StormwaterAssessmentType(
	StormwaterAssessmentTypeID int NOT NULL CONSTRAINT PK_StormwaterAssessmentType_StormwaterAssessmentTypeID PRIMARY KEY,
	StormwaterAssessmentTypeName varchar(100) NOT NULL CONSTRAINT AK_StormwaterAssessmentType_StormwaterAssessmentTypeName UNIQUE,
	StormwaterAssessmentTypeDisplayName varchar(100) NOT NULL CONSTRAINT AK_StormwaterAssessmentType_StormwaterAssessmentTypeDisplayName UNIQUE,
	SortOrder int NOT NULL,
)

CREATE TABLE dbo.StormwaterBreadCrumbEntity(
	StormwaterBreadCrumbEntityID int NOT NULL CONSTRAINT PK_StormwaterBreadCrumbEntity_StormwaterBreadCrumbEntityID PRIMARY KEY,
	StormwaterBreadCrumbEntityName varchar(100) NOT NULL,
	StormwaterBreadCrumbEntityDisplayName varchar(100) NOT NULL,
	GlyphIconClass varchar(100) NOT NULL,
	ColorClass varchar(100) NOT NULL
)

CREATE TABLE dbo.StormwaterJurisdiction(
	StormwaterJurisdictionID int NOT NULL CONSTRAINT PK_StormwaterJurisdiction_StormwaterJurisdictionID PRIMARY KEY,
	TenantID int not null,
	OrganizationID int NOT NULL CONSTRAINT AK_StormwaterJurisdiction_OrganizationID UNIQUE,
	StormwaterJurisdictionGeometry geometry NULL,
	StateProvinceID int NULL,
	IsTransportationJurisdiction bit NOT NULL,
	constraint AK_StormwaterJurisdiction_StormwaterJurisdictionID_TenantID unique (StormwaterJurisdictionID, TenantID),
	constraint AK_StormwaterJurisdiction_OrganizationID_TenantID unique (OrganizationID, TenantID)
)

CREATE TABLE dbo.StormwaterJurisdictionPerson(
	StormwaterJurisdictionPersonID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_StormwaterJurisdictionPerson_StormwaterJurisdictionPersonID PRIMARY KEY,
	TenantID int not null,
	StormwaterJurisdictionID int NOT NULL,
	PersonID int NOT NULL,
	constraint AK_StormaterJurisdictionPerson_StormwaterJurisdictionPersonID_TenantID unique (StormwaterJurisdictionPersonID, TenantID)
)

CREATE TABLE dbo.TreatmentBMP(
	TreatmentBMPID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TreatmentBMP_TreatmentBMPID PRIMARY KEY,
	TenantID int not null,
	TreatmentBMPName varchar(30) NOT NULL,
	TreatmentBMPTypeID int NOT NULL,
	LocationPoint geometry NULL,
	StormwaterJurisdictionID int NOT NULL,
	ModeledCatchmentID int NULL,
	InletCount int NOT NULL,
	OutletCount int NOT NULL,
	DesignDepth float NULL,
	Notes varchar(200) NULL,
	CONSTRAINT AK_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID UNIQUE (TreatmentBMPID, TreatmentBMPTypeID),
	CONSTRAINT CK_TreatmentBMP_DesignDepthForTreatmentBMPType127 CHECK (((TreatmentBMPTypeID=(7) OR TreatmentBMPTypeID=(2) OR TreatmentBMPTypeID=(1)) AND DesignDepth IS NOT NULL OR DesignDepth IS NULL)),
	constraint AK_TreatmentBMP_TreatmentBMPID_TenantID unique (TreatmentBMPID, TenantID),
	constraint AK_TreatmentBMP_TreatmentBMPName_TenantID unique (TreatmentBMPName, TenantID),
)

CREATE TABLE dbo.TreatmentBMPAssessment(
	TreatmentBMPAssessmentID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TreatmentBMPAssessment_TreatmentBMPAssessmentID PRIMARY KEY,
	TenantID int not null,
	TreatmentBMPID int NOT NULL,
	StormwaterAssessmentTypeID int NOT NULL,
	AssessmentDate datetime NOT NULL,
	PersonID int NOT NULL,
	AlternateAssessmentScore float NULL,
	AlternateAssessmentRationale varchar(1000) NULL,
	IsPrivate bit NOT NULL,
	Notes varchar(1000) NULL,
	CONSTRAINT CK_TreatmentBMPAssessment_AlternateAssessmentScoreAndAlternateAssessmentRationaleBothOrNone CHECK  ((AlternateAssessmentScore IS NULL AND AlternateAssessmentRationale IS NULL OR AlternateAssessmentScore IS NOT NULL AND AlternateAssessmentRationale IS NOT NULL)),
	constraint AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TenantID unique (TreatmentBMPAssessmentID, TenantID),
)

CREATE TABLE dbo.TreatmentBMPBenchmarkAndThreshold(
	TreatmentBMPBenchmarkAndThresholdID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPBenchmarkAndThresholdID PRIMARY KEY,
	TenantID int not null,
	TreatmentBMPID int NOT NULL,
	ObservationTypeID int NOT NULL,
	BenchmarkValue float NOT NULL,
	ThresholdValue float NOT NULL,
	CONSTRAINT AK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPID_ObservationTypeID UNIQUE (TreatmentBMPID, ObservationTypeID),
	constraint AK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPBenchmarkAndThresholdID_TenantID unique (TreatmentBMPBenchmarkAndThresholdID, TenantID),
)

CREATE TABLE dbo.TreatmentBMPInfiltrationReading(
	TreatmentBMPInfiltrationReadingID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TreatmentBMPInfiltrationReading_TreatmentBMPInfiltrationReadingID PRIMARY KEY,
	TenantID int not null,
	TreatmentBMPObservationDetailID int NOT NULL,
	ReadingValue float NOT NULL,
	ReadingTime float NOT NULL,
	CONSTRAINT AK_TreatmentBMPInfiltrationReading_TreatmentBMPObservationDetailID_ReadingTime UNIQUE (TreatmentBMPObservationDetailID, ReadingTime),
	constraint AK_TreatmentBMPInfiltrationReading_TreatmentBMPInfiltrationReadingID_TenantID unique (TreatmentBMPInfiltrationReadingID, TenantID),
)

CREATE TABLE dbo.TreatmentBMPObservation(
	TreatmentBMPObservationID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TreatmentBMPObservation_TreatmentBMPObservationID PRIMARY KEY,
	TenantID int not null,
	TreatmentBMPAssessmentID int NOT NULL,
	ObservationTypeID int NOT NULL,
	ObservationValueTypeID int NOT NULL,
	constraint AK_TreatmentBMPObservation_TreatmentBMPObservationID_TenantID unique (TreatmentBMPObservationID, TenantID),
)

CREATE TABLE dbo.TreatmentBMPObservationDetail(
	TreatmentBMPObservationDetailID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TreatmentBMPObservationDetail_TreatmentBMPObservationDetailID PRIMARY KEY,
	TenantID int not null,
	TreatmentBMPObservationID int NOT NULL,
	TreatmentBMPObservationDetailTypeID int NOT NULL,
	TreatmentBMPObservationValue float NOT NULL,
	Notes varchar(300) NULL,
	constraint AK_TreatmentBMPObservationDetail_TreatmentBMPObservationDetailID_TenantID unique (TreatmentBMPObservationDetailID, TenantID),
)

CREATE TABLE dbo.TreatmentBMPObservationDetailType(
	TreatmentBMPObservationDetailTypeID int NOT NULL CONSTRAINT PK_TreatmentBMPObservationDetailType_TreatmentBMPObservationDetailTypeID PRIMARY KEY,
	TreatmentBMPObservationDetailTypeName varchar(100) NOT NULL CONSTRAINT AK_TreatmentBMPObservationDetailType_TreatmentBMPObservationDetailTypeName UNIQUE,
	TreatmentBMPObservationDetailTypeDisplayName varchar(100) NOT NULL,
	ObservationTypeID int NOT NULL,
	SortOrder int NOT NULL,
)

CREATE TABLE dbo.TreatmentBMPType(
	TreatmentBMPTypeID int NOT NULL CONSTRAINT PK_TreatmentBMPType_TreatmentBMPTypeID PRIMARY KEY,
	TreatmentBMPTypeName varchar(100) NOT NULL CONSTRAINT AK_TreatmentBMPType_TreatmentBMPTypeName UNIQUE,
	TreatmentBMPTypeDisplayName varchar(100) NOT NULL CONSTRAINT AK_TreatmentBMPType_TreatmentBMPTypeDisplayName UNIQUE,
	SortOrder int NOT NULL,
	DisplayColor varchar(20) NULL,
	GlyphIconClass varchar(20) NULL,
	IsDistributedBMPType bit NOT NULL,
)

CREATE TABLE dbo.TreatmentBMPTypeObservationType(
	TreatmentBMPTypeObservationTypeID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID PRIMARY KEY,
	TenantID int not null,
	TreatmentBMPTypeID int NOT NULL,
	ObservationTypeID int NOT NULL,
	AssessmentScoreWeight float NOT NULL,
	DefaultThresholdValue float NULL,
	DefaultBenchmarkValue float NULL,
	constraint AK_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID_TenantID unique (TreatmentBMPTypeObservationTypeID, TenantID),
)

go

-- FKs

alter table dbo.ModeledCatchment add constraint FK_ModeledCatchment_Tenant_TenantID foreign key (TenantID) references dbo.Tenant(TenantID)
alter table dbo.ModeledCatchmentGeometryStaging add constraint FK_ModeledCatchmentGeometryStaging_Tenant_TenantID FOREIGN KEY (TenantID) REFERENCES dbo.Tenant(TenantID)
alter table dbo.StormwaterJurisdiction add constraint FK_StormwaterJurisdiction_Tenant_TenantID FOREIGN KEY (TenantID) REFERENCES dbo.Tenant(TenantID)
alter table dbo.StormwaterJurisdictionPerson add constraint FK_StormwaterJurisdictionPerson_Tenant_TenantID FOREIGN KEY (TenantID) REFERENCES dbo.Tenant(TenantID)
alter table dbo.TreatmentBMP add constraint FK_TreatmentBMP_Tenant_TenantID FOREIGN KEY (TenantID) REFERENCES dbo.Tenant(TenantID)
alter table dbo.TreatmentBMPAssessment add constraint FK_TreatmentBMPAssessment_Tenant_TenantID FOREIGN KEY (TenantID) REFERENCES dbo.Tenant(TenantID)
alter table dbo.TreatmentBMPBenchmarkAndThreshold add constraint FK_TreatmentBMPBenchmarkAndThreshold_Tenant_TenantID FOREIGN KEY (TenantID) REFERENCES dbo.Tenant(TenantID)
alter table dbo.TreatmentBMPInfiltrationReading add constraint FK_TreatmentBMPInfiltrationReading_Tenant_TenantID FOREIGN KEY (TenantID) REFERENCES dbo.Tenant(TenantID)
alter table dbo.TreatmentBMPObservation add constraint FK_TreatmentBMPObservation_Tenant_TenantID FOREIGN KEY (TenantID) REFERENCES dbo.Tenant(TenantID)
alter table dbo.TreatmentBMPObservationDetail add constraint FK_TreatmentBMPObservationDetail_Tenant_TenantID FOREIGN KEY (TenantID) REFERENCES dbo.Tenant(TenantID)

ALTER TABLE dbo.ModeledCatchment ADD CONSTRAINT FK_ModeledCatchment_StormwaterJurisdiction_StormwaterJurisdictionID FOREIGN KEY(StormwaterJurisdictionID) REFERENCES dbo.StormwaterJurisdiction (StormwaterJurisdictionID)

ALTER TABLE dbo.ModeledCatchmentGeometryStaging ADD CONSTRAINT FK_ModeledCatchmentGeometryStaging_Person_PersonID FOREIGN KEY(PersonID) REFERENCES dbo.Person (PersonID)
ALTER TABLE dbo.ModeledCatchmentGeometryStaging ADD CONSTRAINT FK_ModeledCatchmentGeometryStaging_Person_PersonID_TenantID FOREIGN KEY(PersonID, TenantID) REFERENCES dbo.Person (PersonID, TenantID)

ALTER TABLE dbo.ObservationType ADD CONSTRAINT FK_ObservationType_MeasurementUnitType_MeasurementUnitTypeID FOREIGN KEY(MeasurementUnitTypeID) REFERENCES dbo.MeasurementUnitType (MeasurementUnitTypeID)

ALTER TABLE dbo.StormwaterJurisdiction ADD CONSTRAINT FK_StormwaterJurisdiction_Organization_OrganizationID FOREIGN KEY(OrganizationID) REFERENCES dbo.Organization (OrganizationID)
ALTER TABLE dbo.StormwaterJurisdiction ADD CONSTRAINT FK_StormwaterJurisdiction_Organization_OrganizationID_TenantID FOREIGN KEY(OrganizationID, TenantID) REFERENCES dbo.Organization (OrganizationID, TenantID)
ALTER TABLE dbo.StormwaterJurisdiction ADD CONSTRAINT FK_StormwaterJurisdiction_StateProvince_StateProvinceID FOREIGN KEY(StateProvinceID) REFERENCES dbo.StateProvince (StateProvinceID)
ALTER TABLE dbo.StormwaterJurisdiction ADD CONSTRAINT FK_StormwaterJurisdiction_StateProvince_StateProvinceID_TenantID FOREIGN KEY(StateProvinceID, TenantID) REFERENCES dbo.StateProvince (StateProvinceID, TenantID)

ALTER TABLE dbo.StormwaterJurisdictionPerson ADD CONSTRAINT FK_StormwaterJurisdictionPerson_Person_PersonID FOREIGN KEY(PersonID) REFERENCES dbo.Person (PersonID)
ALTER TABLE dbo.StormwaterJurisdictionPerson ADD CONSTRAINT FK_StormwaterJurisdictionPerson_Person_PersonID_TenantID FOREIGN KEY(PersonID, TenantID) REFERENCES dbo.Person (PersonID, TenantID)
ALTER TABLE dbo.StormwaterJurisdictionPerson ADD CONSTRAINT FK_StormwaterJurisdictionPerson_StormwaterJurisdiction_StormwaterJurisdictionID FOREIGN KEY(StormwaterJurisdictionID) REFERENCES dbo.StormwaterJurisdiction (StormwaterJurisdictionID)
ALTER TABLE dbo.StormwaterJurisdictionPerson ADD CONSTRAINT FK_StormwaterJurisdictionPerson_StormwaterJurisdiction_StormwaterJurisdictionID_TenantID FOREIGN KEY (StormwaterJurisdictionID, TenantID) REFERENCES dbo.StormwaterJurisdiction (StormwaterJurisdictionID, TenantID)

ALTER TABLE dbo.TreatmentBMP ADD CONSTRAINT FK_TreatmentBMP_ModeledCatchment_ModeledCatchmentID FOREIGN KEY(ModeledCatchmentID) REFERENCES dbo.ModeledCatchment (ModeledCatchmentID)
ALTER TABLE dbo.TreatmentBMP ADD CONSTRAINT FK_TreatmentBMP_ModeledCatchment_ModeledCatchmentID_TenantID FOREIGN KEY(ModeledCatchmentID, TenantID) REFERENCES dbo.ModeledCatchment (ModeledCatchmentID, TenantID)
ALTER TABLE dbo.TreatmentBMP ADD CONSTRAINT FK_TreatmentBMP_StormwaterJurisdiction_StormwaterJurisdictionID FOREIGN KEY(StormwaterJurisdictionID) REFERENCES dbo.StormwaterJurisdiction (StormwaterJurisdictionID)
ALTER TABLE dbo.TreatmentBMP ADD CONSTRAINT FK_TreatmentBMP_StormwaterJurisdiction_StormwaterJurisdictionID_TenantID FOREIGN KEY(StormwaterJurisdictionID, TenantID) REFERENCES dbo.StormwaterJurisdiction (StormwaterJurisdictionID, TenantID)
ALTER TABLE dbo.TreatmentBMP ADD CONSTRAINT FK_TreatmentBMP_TreatmentBMPType_TreatmentBMPTypeID FOREIGN KEY(TreatmentBMPTypeID) REFERENCES dbo.TreatmentBMPType (TreatmentBMPTypeID)

ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT FK_TreatmentBMPAssessment_Person_PersonID FOREIGN KEY(PersonID) REFERENCES dbo.Person (PersonID)
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT FK_TreatmentBMPAssessment_Person_PersonID_TenantID FOREIGN KEY(PersonID, TenantID) REFERENCES dbo.Person (PersonID, TenantID)
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT FK_TreatmentBMPAssessment_StormwaterAssessmentType_StormwaterAssessmentTypeID FOREIGN KEY(StormwaterAssessmentTypeID) REFERENCES dbo.StormwaterAssessmentType (StormwaterAssessmentTypeID)
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID FOREIGN KEY(TreatmentBMPID) REFERENCES dbo.TreatmentBMP (TreatmentBMPID)
ALTER TABLE dbo.TreatmentBMPAssessment ADD CONSTRAINT FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID_TenantID FOREIGN KEY(TreatmentBMPID, TenantID) REFERENCES dbo.TreatmentBMP (TreatmentBMPID, TenantID)

ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold ADD CONSTRAINT FK_TreatmentBMPBenchmarkAndThreshold_ObservationType_ObservationTypeID FOREIGN KEY(ObservationTypeID) REFERENCES dbo.ObservationType (ObservationTypeID)
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold ADD CONSTRAINT FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID FOREIGN KEY(TreatmentBMPID) REFERENCES dbo.TreatmentBMP (TreatmentBMPID)
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold ADD CONSTRAINT FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID_TenantID FOREIGN KEY(TreatmentBMPID, TenantID) REFERENCES dbo.TreatmentBMP (TreatmentBMPID, TenantID)

ALTER TABLE dbo.TreatmentBMPInfiltrationReading ADD CONSTRAINT FK_TreatmentBMPInfiltrationReading_TreatmentBMPObservationDetail_TreatmentBMPObservationDetailID FOREIGN KEY(TreatmentBMPObservationDetailID) REFERENCES dbo.TreatmentBMPObservationDetail (TreatmentBMPObservationDetailID)
ALTER TABLE dbo.TreatmentBMPInfiltrationReading ADD CONSTRAINT FK_TreatmentBMPInfiltrationReading_TreatmentBMPObservationDetail_TreatmentBMPObservationDetailID_TenantID FOREIGN KEY(TreatmentBMPObservationDetailID, TenantID) REFERENCES dbo.TreatmentBMPObservationDetail (TreatmentBMPObservationDetailID, TenantID)

ALTER TABLE dbo.TreatmentBMPObservation ADD CONSTRAINT FK_TreatmentBMPObservation_ObservationType_ObservationTypeID FOREIGN KEY(ObservationTypeID) REFERENCES dbo.ObservationType (ObservationTypeID)
ALTER TABLE dbo.TreatmentBMPObservation ADD CONSTRAINT FK_TreatmentBMPObservation_ObservationValueType_ObservationValueTypeID FOREIGN KEY(ObservationValueTypeID) REFERENCES dbo.ObservationValueType (ObservationValueTypeID)
ALTER TABLE dbo.TreatmentBMPObservation ADD CONSTRAINT FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID FOREIGN KEY(TreatmentBMPAssessmentID) REFERENCES dbo.TreatmentBMPAssessment (TreatmentBMPAssessmentID)
ALTER TABLE dbo.TreatmentBMPObservation ADD CONSTRAINT FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TenantID FOREIGN KEY(TreatmentBMPAssessmentID, TenantID) REFERENCES dbo.TreatmentBMPAssessment (TreatmentBMPAssessmentID, TenantID)

ALTER TABLE dbo.TreatmentBMPObservationDetailType ADD CONSTRAINT FK_TreatmentBMPObservationDetailType_ObservationType_ObservationTypeID FOREIGN KEY(ObservationTypeID) REFERENCES dbo.ObservationType (ObservationTypeID)

ALTER TABLE dbo.TreatmentBMPObservationDetail ADD CONSTRAINT FK_TreatmentBMPObservationDetail_TreatmentBMPObservation_TreatmentBMPObservationID FOREIGN KEY(TreatmentBMPObservationID) REFERENCES dbo.TreatmentBMPObservation (TreatmentBMPObservationID)
ALTER TABLE dbo.TreatmentBMPObservationDetail ADD CONSTRAINT FK_TreatmentBMPObservationDetail_TreatmentBMPObservation_TreatmentBMPObservationID_TenantID FOREIGN KEY(TreatmentBMPObservationID, TenantID) REFERENCES dbo.TreatmentBMPObservation (TreatmentBMPObservationID, TenantID)
ALTER TABLE dbo.TreatmentBMPObservationDetail ADD CONSTRAINT FK_TreatmentBMPObservationDetail_TreatmentBMPObservationDetailType_TreatmentBMPObservationDetailTypeID FOREIGN KEY(TreatmentBMPObservationDetailTypeID) REFERENCES dbo.TreatmentBMPObservationDetailType (TreatmentBMPObservationDetailTypeID)

ALTER TABLE dbo.TreatmentBMPTypeObservationType ADD CONSTRAINT FK_TreatmentBMPTypeObservationType_ObservationType_ObservationTypeID FOREIGN KEY(ObservationTypeID) REFERENCES dbo.ObservationType (ObservationTypeID)
ALTER TABLE dbo.TreatmentBMPTypeObservationType ADD CONSTRAINT FK_TreatmentBMPTypeObservationType_TreatmentBMPType_TreatmentBMPTypeID FOREIGN KEY(TreatmentBMPTypeID) REFERENCES dbo.TreatmentBMPType (TreatmentBMPTypeID)