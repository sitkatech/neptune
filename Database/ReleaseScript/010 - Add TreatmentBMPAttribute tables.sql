CREATE TABLE dbo.TreatmentBMPAttributeDataType(
	TreatmentBMPAttributeDataTypeID int NOT NULL CONSTRAINT PK_TreatmentBMPAttributeDataType_TreatmentBMPAttributeDataTypeID PRIMARY KEY,
	TreatmentBMPAttributeDataTypeName varchar(100) NOT NULL CONSTRAINT AK_TreatmentBMPAttributeDataType_TreatmentBMPAttributeDataTypeName UNIQUE,
	TreatmentBMPAttributeDataTypeDisplayName varchar(100) NOT NULL CONSTRAINT AK_TreatmentBMPAttributeDataType_TreatmentBMPAttributeDataTypeDisplayName UNIQUE
)

CREATE TABLE dbo.TreatmentBMPAttributeType(
	TreatmentBMPAttributeTypeID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID PRIMARY KEY,
	TenantID int NOT NULL CONSTRAINT FK_TreatmentBMPAttributeType_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	TreatmentBMPAttributeTypeName varchar(100) NOT NULL CONSTRAINT AK_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeName UNIQUE,
	TreatmentBMPAttributeDataTypeID int not null CONSTRAINT FK_TreatmentBMPAttributeType_TreatmentBMPAttributeDataType_TreatmentBMPAttributeDataTypeID FOREIGN KEY REFERENCES dbo.TreatmentBMPAttributeDataType (TreatmentBMPAttributeDataTypeID),
	MeasurementUnitTypeID int not null CONSTRAINT FK_TreatmentBMPAttributeType_MeasurementUnitType_MeasurementUnitTypeID FOREIGN KEY REFERENCES dbo.MeasurementUnitType(MeasurementUnitTypeID),
	IsRequired bit not null,
	TreatmentBMPAttributeTypeDescription varchar(1000) NULL,
	CONSTRAINT AK_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID UNIQUE (TreatmentBMPAttributeTypeID, TenantID)
)


CREATE TABLE dbo.TreatmentBMPTypeAttributeType(
	TreatmentBMPTypeAttributeTypeID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID PRIMARY KEY,
	TenantID int NOT NULL CONSTRAINT FK_TreatmentBMPTypeAttributeType_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	TreatmentBMPTypeID int NOT NULL CONSTRAINT FK_TreatmentBMPTypeAttributeType_TreatmentBMPType_TreatmentBMPTypeID FOREIGN KEY REFERENCES dbo.TreatmentBMPType (TreatmentBMPTypeID),
	TreatmentBMPAttributeTypeID int NOT NULL CONSTRAINT FK_TreatmentBMPTypeAttributeType_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID FOREIGN KEY REFERENCES dbo.TreatmentBMPAttributeType (TreatmentBMPAttributeTypeID),
	CONSTRAINT AK_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_TreatmentBMPAttributeTypeID UNIQUE (TreatmentBMPTypeID, TreatmentBMPAttributeTypeID),
	CONSTRAINT FK_TreatmentBMPTypeAttributeType_TreatmentBMPType_TreatmentBMPTypeID_TenantID foreign key (TreatmentBMPTypeID, TenantID) references dbo.TreatmentBMPType (TreatmentBMPTypeID, TenantID),
	CONSTRAINT FK_TreatmentBMPTypeAttributeType_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID foreign key (TreatmentBMPAttributeTypeID, TenantID) references dbo.TreatmentBMPAttributeType (TreatmentBMPAttributeTypeID, TenantID),
)

CREATE TABLE dbo.TreatmentBMPAttribute(
	TreatmentBMPAttributeID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TreatmentBMPAttribute_TreatmentBMPAttributeID PRIMARY KEY,
	TenantID int NOT NULL CONSTRAINT FK_TreatmentBMPAttribute_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	TreatmentBMPID int NOT NULL CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID FOREIGN KEY REFERENCES dbo.TreatmentBMP (TreatmentBMPID),
	TreatmentBMPTypeID int NOT NULL CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMPType_TreatmentBMPTypeID FOREIGN KEY REFERENCES dbo.TreatmentBMPType(TreatmentBMPTypeID),
	TreatmentBMPAttributeTypeID int NOT NULL CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID FOREIGN KEY REFERENCES dbo.TreatmentBMPAttributeType(TreatmentBMPAttributeTypeID),
	TreatmentBMPAttributeValue varchar(1000) NOT NULL,
	CONSTRAINT AK_TreatmentBMPAttribute_TreatmentBMPTypeID_TreatmentBMPTypeAttributeTypeID UNIQUE (TreatmentBMPID, TreatmentBMPTypeID, TreatmentBMPAttributeTypeID),
	CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID foreign key (TreatmentBMPID, TreatmentBMPTypeID) references dbo.TreatmentBMP (TreatmentBMPID, TreatmentBMPTypeID),
	CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_TreatmentBMPAttributeTypeID foreign key (TreatmentBMPTypeID, TreatmentBMPAttributeTypeID) references dbo.TreatmentBMPTypeAttributeType (TreatmentBMPTypeID, TreatmentBMPAttributeTypeID),
	CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID_TenantID foreign key (TreatmentBMPID, TenantID) references dbo.TreatmentBMP (TreatmentBMPID, TenantID),
	CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMPType_TreatmentBMPTypeID_TenantID foreign key (TreatmentBMPTypeID, TenantID) references dbo.TreatmentBMPType (TreatmentBMPTypeID, TenantID),
	CONSTRAINT FK_TreatmentBMPAttribute_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID foreign key (TreatmentBMPAttributeTypeID, TenantID) references dbo.TreatmentBMPAttributeType (TreatmentBMPAttributeTypeID, TenantID)
)


ALTER TABLE dbo.StormwaterJurisdictionPerson DROP CONSTRAINT AK_StormaterJurisdictionPerson_StormwaterJurisdictionPersonID_TenantID
ALTER TABLE dbo.TreatmentBMPObservation DROP CONSTRAINT AK_TreatmentBMPObservation_TreatmentBMPObservationID_TenantID
ALTER TABLE dbo.TreatmentBMPBenchmarkAndThreshold DROP CONSTRAINT AK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPBenchmarkAndThresholdID_TenantID
ALTER TABLE dbo.TreatmentBMPTypeObservationType DROP CONSTRAINT AK_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID_TenantID

alter table dbo.TreatmentBMPTypeObservationType add CONSTRAINT FK_TreatmentBMPTypeObservationType_TreatmentBMPType_TreatmentBMPTypeID_TenantID foreign key (TreatmentBMPTypeID, TenantID) references dbo.TreatmentBMPType (TreatmentBMPTypeID, TenantID)
alter table dbo.TreatmentBMPTypeObservationType add CONSTRAINT FK_TreatmentBMPTypeObservationType_ObservationType_ObservationTypeID_TenantID foreign key (ObservationTypeID, TenantID) references dbo.ObservationType (ObservationTypeID, TenantID)


insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(17, 'ManageTreatmentBMPAttributeTypeInstructions', 'Manage Treatment BMP Attribute Type Instructions', 2),
(18, 'ManageTreatmentBMPAttributeInstructions', 'Manage Treatment BMP Attribute Instructions', 2),
(19, 'ManageTreatmentBMPAttributeTypesList', 'Manage Treatment BMP Attribute Types List', 2)

insert into dbo.NeptunePage(NeptunePageTypeID, TenantID)
values
(17, 2),
(18, 2),
(19, 2)