CREATE TYPE dbo.html FROM varchar(max) NULL
GO


CREATE TABLE dbo.Tenant(
	TenantID int NOT NULL CONSTRAINT PK_Tenant_TenantID PRIMARY KEY,
	TenantName varchar(100) NOT NULL CONSTRAINT AK_Tenant_TenantName UNIQUE,
	TenantDomain varchar(100) NOT NULL,
	TenantSubdomain varchar(100) NULL,
	CONSTRAINT AK_Tenant_TenantDomain_TenantSubdomain UNIQUE (TenantDomain, TenantSubdomain),
)

CREATE TABLE dbo.FileResourceMimeType(
	FileResourceMimeTypeID int NOT NULL CONSTRAINT PK_FileResourceMimeType_FileResourceMimeTypeID PRIMARY KEY,
	FileResourceMimeTypeName varchar(100) NOT NULL CONSTRAINT AK_FileResourceMimeType_FileResourceMimeTypeName UNIQUE,
	FileResourceMimeTypeDisplayName varchar(100) NOT NULL CONSTRAINT AK_FileResourceMimeType_FileResourceMimeTypeDisplayName UNIQUE,
	FileResourceMimeTypeContentTypeName varchar(100) NOT NULL,
	FileResourceMimeTypeIconSmallFilename varchar(100) NULL,
	FileResourceMimeTypeIconNormalFilename varchar(100) NULL,
)


CREATE TABLE dbo.FileResource(
	FileResourceID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_FileResource_FileResourceID PRIMARY KEY,
	TenantID int NOT NULL,
	FileResourceMimeTypeID int NOT NULL CONSTRAINT FK_FileResource_FileResourceMimeType_FileResourceMimeTypeID FOREIGN KEY REFERENCES dbo.FileResourceMimeType (FileResourceMimeTypeID),
	OriginalBaseFilename varchar(255) NOT NULL,
	OriginalFileExtension varchar(255) NOT NULL,
	FileResourceGUID uniqueidentifier NOT NULL CONSTRAINT AK_FileResource_FileResourceGUID UNIQUE,
	FileResourceData varbinary(max) NOT NULL,
	CreatePersonID int NOT NULL,
	CreateDate datetime NOT NULL,
	CONSTRAINT AK_FileResource_FileResourceID_TenantID UNIQUE (FileResourceID, TenantID)
)


CREATE TABLE dbo.OrganizationType(
	OrganizationTypeID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_OrganizationType_OrganizationTypeID PRIMARY KEY,
	TenantID int NOT NULL CONSTRAINT FK_OrganizationType_Tenant_TenantID FOREIGN KEY REFERENCES dbo.Tenant (TenantID),
	OrganizationTypeName varchar(200) NOT NULL,
	OrganizationTypeAbbreviation varchar(100) NOT NULL,
	LegendColor varchar(10) NOT NULL,
	IsDefaultOrganizationType bit NOT NULL,
	CONSTRAINT AK_OrganizationType_OrganizationTypeID_TenantID UNIQUE (OrganizationTypeID, TenantID),
	CONSTRAINT AK_OrganizationType_OrganizationTypeName_TenantID UNIQUE (OrganizationTypeName, TenantID)
)



CREATE TABLE dbo.Organization(
	OrganizationID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Organization_OrganizationID PRIMARY KEY,
	TenantID int NOT NULL,
	OrganizationGuid uniqueidentifier NULL,
	OrganizationName varchar(200) NOT NULL,
	OrganizationShortName varchar(50) NULL,
	PrimaryContactPersonID int NULL,
	IsActive bit NOT NULL,
	OrganizationUrl varchar(200) NULL,
	LogoFileResourceID int NULL,
	OrganizationTypeID int NOT NULL,
	CONSTRAINT AK_Organization_OrganizationID_TenantID UNIQUE (OrganizationID, TenantID),
	CONSTRAINT AK_Organization_OrganizationName_TenantID UNIQUE (OrganizationName, TenantID)
)



CREATE TABLE dbo.Role(
	RoleID int NOT NULL CONSTRAINT PK_Role_RoleID PRIMARY KEY,
	RoleName varchar(100) NOT NULL CONSTRAINT AK_Role_RoleName UNIQUE,
	RoleDisplayName varchar(100) NOT NULL CONSTRAINT AK_Role_RoleDisplayName UNIQUE,
	RoleDescription varchar(255) NULL
)



CREATE TABLE dbo.Person(
	PersonID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Person_PersonID PRIMARY KEY,
	TenantID int NOT NULL,
	PersonGuid uniqueidentifier NOT NULL,
	FirstName varchar(100) NOT NULL,
	LastName varchar(100) NOT NULL,
	Email varchar(255) NOT NULL,
	Phone varchar(30) NULL,
	RoleID int NOT NULL,
	CreateDate datetime NOT NULL,
	UpdateDate datetime NULL,
	LastActivityDate datetime NULL,
	IsActive bit NOT NULL,
	OrganizationID int NOT NULL,
	ReceiveSupportEmails bit NOT NULL,
	LoginName varchar(128) NOT NULL,
	CONSTRAINT AK_Person_Email_TenantID UNIQUE (Email, TenantID),
	CONSTRAINT AK_Person_PersonGuid_TenantID UNIQUE (PersonGuid, TenantID),
	CONSTRAINT AK_Person_PersonID_TenantID UNIQUE (PersonID, TenantID)
)



CREATE TABLE dbo.TenantAttribute(
	TenantAttributeID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TenantAttribute_TenantAttributeID PRIMARY KEY,
	TenantID int NOT NULL CONSTRAINT AK_TenantAttribute_TenantID UNIQUE,
	DefaultBoundingBox geometry NOT NULL,
	MinimumYear int NOT NULL,
	PrimaryContactPersonID int NULL,
	TenantSquareLogoFileResourceID int NULL,
	TenantBannerLogoFileResourceID int NULL,
	TenantStyleSheetFileResourceID int NULL,
	TenantDisplayName varchar(100) NOT NULL CONSTRAINT AK_TenantAttribute_TenantDisplayName UNIQUE,
	ToolDisplayName varchar(100) NOT NULL,
	RecaptchaPublicKey varchar(100) NULL,
	RecaptchaPrivateKey varchar(100) NULL,
)





CREATE TABLE dbo.AuditLog(
	AuditLogID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_AuditLog_AuditLogID PRIMARY KEY,
	TenantID int NOT NULL,
	PersonID int NOT NULL,
	AuditLogDate datetime NOT NULL,
	AuditLogEventTypeID int NOT NULL,
	TableName varchar(500) NOT NULL,
	RecordID int NOT NULL,
	ColumnName varchar(500) NOT NULL,
	OriginalValue varchar(max) NULL,
	NewValue varchar(max) NOT NULL,
	AuditDescription varchar(max) NULL,
)



CREATE TABLE dbo.AuditLogEventType(
	AuditLogEventTypeID int NOT NULL CONSTRAINT PK_AuditLogEventType_AuditLogEventTypeID PRIMARY KEY,
	AuditLogEventTypeName varchar(100) NOT NULL CONSTRAINT AK_AuditLogEventType_AuditLogEventTypeName UNIQUE,
	AuditLogEventTypeDisplayName varchar(100) NOT NULL CONSTRAINT AK_AuditLogEventType_AuditLogEventTypeDisplayName UNIQUE 
)



CREATE TABLE dbo.County(
	CountyID int NOT NULL CONSTRAINT PK_County_CountyID PRIMARY KEY,
	TenantID int NOT NULL,
	CountyName varchar(100) NOT NULL,
	StateProvinceID int NOT NULL,
	CountyFeature geometry NULL,
	CONSTRAINT AK_County_CountyName_StateProvinceID UNIQUE (CountyName, StateProvinceID)
)



CREATE TABLE dbo.FieldDefinition(
	FieldDefinitionID int NOT NULL CONSTRAINT PK_FieldDefinition_FieldDefinitionID PRIMARY KEY,
	FieldDefinitionName varchar(300) NOT NULL CONSTRAINT AK_FieldDefinition_FieldDefinitionName UNIQUE,
	FieldDefinitionDisplayName varchar(300) NOT NULL CONSTRAINT AK_FieldDefinition_FieldDefinitionDisplayName UNIQUE,
	DefaultDefinition dbo.html NOT NULL,
	CanCustomizeLabel bit NOT NULL
)



CREATE TABLE dbo.FieldDefinitionData(
	FieldDefinitionDataID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_FieldDefinitionData_FieldDefinitionDataID PRIMARY KEY,
	TenantID int NOT NULL,
	FieldDefinitionID int NOT NULL,
	FieldDefinitionDataValue dbo.html NULL,
	FieldDefinitionLabel varchar(300) NULL,
	CONSTRAINT AK_FieldDefinitionData_FieldDefinitionDataID_TenantID UNIQUE (FieldDefinitionDataID, TenantID),
	CONSTRAINT AK_FieldDefinitionData_FieldDefinitionID_TenantID UNIQUE (FieldDefinitionID, TenantID)
)



CREATE TABLE dbo.FieldDefinitionDataImage(
	FieldDefinitionDataImageID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_FieldDefinitionDataImage_FieldDefinitionDataImageID PRIMARY KEY,
	TenantID int NOT NULL,
	FieldDefinitionDataID int NOT NULL,
	FileResourceID int NOT NULL,
)



CREATE TABLE dbo.NeptuneHomePageImage(
	NeptuneHomePageImageID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_NeptuneHomePageImage_NeptuneHomePageImageID PRIMARY KEY,
	TenantID int NOT NULL,
	FileResourceID int NOT NULL,
	Caption varchar(300) NOT NULL,
	SortOrder int NOT NULL,
)



CREATE TABLE dbo.NeptunePage(
	NeptunePageID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_NeptunePage_NeptunePageID PRIMARY KEY,
	TenantID int NOT NULL,
	NeptunePageTypeID int NOT NULL,
	NeptunePageContent dbo.html NULL,
	CONSTRAINT AK_NeptunePage_NeptunePageID_TenantID UNIQUE (NeptunePageID, TenantID),
	CONSTRAINT AK_NeptunePage_NeptunePageTypeID_TenantID UNIQUE (NeptunePageTypeID, TenantID)
)


CREATE TABLE dbo.NeptunePageImage(
	NeptunePageImageID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_NeptunePageImage_NeptunePageImageID PRIMARY KEY,
	TenantID int NOT NULL,
	NeptunePageID int NOT NULL,
	FileResourceID int NOT NULL,
	CONSTRAINT AK_NeptunePageImage_NeptunePageImageID_FileResourceID UNIQUE (NeptunePageImageID, FileResourceID)
)



CREATE TABLE dbo.NeptunePageRenderType(
	NeptunePageRenderTypeID int NOT NULL CONSTRAINT PK_NeptunePageRenderType_NeptunePageRenderTypeID PRIMARY KEY,
	NeptunePageRenderTypeName varchar(100) NOT NULL CONSTRAINT AK_NeptunePageRenderType_NeptunePageRenderTypeName UNIQUE ,
	NeptunePageRenderTypeDisplayName varchar(100) NOT NULL CONSTRAINT AK_NeptunePageRenderType_NeptunePageRenderTypeDisplayName UNIQUE
)


CREATE TABLE dbo.NeptunePageType(
	NeptunePageTypeID int NOT NULL CONSTRAINT PK_NeptunePageType_NeptunePageTypeID PRIMARY KEY,
	NeptunePageTypeName varchar(100) NOT NULL CONSTRAINT AK_NeptunePageType_NeptunePageTypeName UNIQUE,
	NeptunePageTypeDisplayName varchar(100) NOT NULL CONSTRAINT AK_NeptunePageType_NeptunePageTypeDisplayName UNIQUE,
	NeptunePageRenderTypeID int NOT NULL
)




CREATE TABLE dbo.GoogleChartType(
	GoogleChartTypeID int NOT NULL CONSTRAINT PK_GoogleChartType_GoogleChartTypeID PRIMARY KEY,
	GoogleChartTypeName varchar(50) NOT NULL CONSTRAINT AK_GoogleChartType_GoogleChartTypeName UNIQUE,
	GoogleChartTypeDisplayName varchar(50) NOT NULL CONSTRAINT AK_GoogleChartType_GoogleChartTypeDisplayName UNIQUE,
	SeriesDataDisplayType varchar(50) NULL
)



CREATE TABLE dbo.MeasurementUnitType(
	MeasurementUnitTypeID int NOT NULL CONSTRAINT PK_MeasurementUnitType_MeasurementUnitTypeID PRIMARY KEY,
	MeasurementUnitTypeName varchar(100) NOT NULL CONSTRAINT AK_MeasurementUnitType_MeasurementUnitTypeName UNIQUE,
	MeasurementUnitTypeDisplayName varchar(100) NOT NULL CONSTRAINT AK_MeasurementUnitType_MeasurementUnitTypeDisplayName UNIQUE,
	LegendDisplayName varchar(50) NULL,
	SingularDisplayName varchar(50) NULL,
	NumberOfSignificantDigits int NOT NULL,
	IncludeSpaceBeforeLegendLabel bit NOT NULL
)



CREATE TABLE dbo.NotificationType(
	NotificationTypeID int NOT NULL CONSTRAINT PK_NotificationType_NotificationTypeID PRIMARY KEY,
	NotificationTypeName varchar(100) NOT NULL CONSTRAINT AK_NotificationType_NotificationTypeName UNIQUE,
	NotificationTypeDisplayName varchar(100) NOT NULL CONSTRAINT AK_NotificationType_NotificationTypeDisplayName UNIQUE
)



CREATE TABLE dbo.Notification(
	NotificationID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Notification_NotificationID PRIMARY KEY,
	TenantID int NOT NULL,
	NotificationTypeID int NOT NULL,
	PersonID int NOT NULL,
	NotificationDate datetime NOT NULL,
	CONSTRAINT AK_Notification_NotificationID_TenantID UNIQUE (NotificationID, TenantID)
)


CREATE TABLE dbo.StateProvince(
	StateProvinceID int NOT NULL CONSTRAINT PK_StateProvince_StateProvinceID PRIMARY KEY,
	TenantID int NOT NULL,
	StateProvinceName varchar(100) NOT NULL,
	StateProvinceAbbreviation char(2) NOT NULL,
	StateProvinceFeature geometry NULL,
	StateProvinceFeatureForAnalysis geometry NOT NULL,
	CONSTRAINT AK_StateProvince_StateProvinceAbbreviation_TenantID UNIQUE (StateProvinceAbbreviation, TenantID),
	CONSTRAINT AK_StateProvince_StateProvinceID_TenantID UNIQUE (StateProvinceID, TenantID),
	CONSTRAINT AK_StateProvince_StateProvinceName_TenantID UNIQUE (StateProvinceName, TenantID)
)


CREATE TABLE dbo.SupportRequestType(
	SupportRequestTypeID int NOT NULL CONSTRAINT PK_SupportRequestType_SupportRequestTypeID PRIMARY KEY,
	SupportRequestTypeName varchar(100) NOT NULL CONSTRAINT AK_SupportRequestType_SupportRequestTypeName UNIQUE,
	SupportRequestTypeDisplayName varchar(100) NOT NULL CONSTRAINT AK_SupportRequestType_SupportRequestTypeDisplayName UNIQUE,
	SupportRequestTypeSortOrder int NOT NULL,
)


CREATE TABLE dbo.SupportRequestLog(
	SupportRequestLogID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SupportRequestLog_SupportRequestLogID PRIMARY KEY,
	TenantID int NOT NULL,
	RequestDate datetime NOT NULL,
	RequestPersonName varchar(200) NOT NULL,
	RequestPersonEmail varchar(256) NOT NULL,
	RequestPersonID int NULL,
	SupportRequestTypeID int NOT NULL,
	RequestDescription varchar(2000) NOT NULL,
	RequestPersonOrganization varchar(500) NULL,
	RequestPersonPhone varchar(50) NULL,
)

GO


ALTER TABLE dbo.FileResource  WITH CHECK ADD  CONSTRAINT FK_FileResource_Person_CreatePersonID_PersonID FOREIGN KEY(CreatePersonID)
REFERENCES dbo.Person (PersonID)

ALTER TABLE dbo.FileResource  WITH CHECK ADD  CONSTRAINT FK_FileResource_Person_CreatePersonID_TenantID_PersonID_TenantID FOREIGN KEY(CreatePersonID, TenantID)
REFERENCES dbo.Person (PersonID, TenantID)


ALTER TABLE dbo.FileResource  WITH CHECK ADD  CONSTRAINT FK_FileResource_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)


ALTER TABLE dbo.SupportRequestLog  WITH CHECK ADD  CONSTRAINT FK_SupportRequestLog_Person_RequestPersonID_PersonID FOREIGN KEY(RequestPersonID)
REFERENCES dbo.Person (PersonID)


ALTER TABLE dbo.SupportRequestLog  WITH CHECK ADD  CONSTRAINT FK_SupportRequestLog_Person_RequestPersonID_TenantID_PersonID_TenantID FOREIGN KEY(RequestPersonID, TenantID)
REFERENCES dbo.Person (PersonID, TenantID)


ALTER TABLE dbo.SupportRequestLog  WITH CHECK ADD  CONSTRAINT FK_SupportRequestLog_SupportRequestType_SupportRequestTypeID FOREIGN KEY(SupportRequestTypeID)
REFERENCES dbo.SupportRequestType (SupportRequestTypeID)


ALTER TABLE dbo.SupportRequestLog  WITH CHECK ADD  CONSTRAINT FK_SupportRequestLog_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)



ALTER TABLE dbo.StateProvince  WITH CHECK ADD  CONSTRAINT FK_StateProvince_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)



ALTER TABLE dbo.NeptunePageType  WITH CHECK ADD  CONSTRAINT FK_NeptunePageType_NeptunePageRenderType_NeptunePageRenderTypeID FOREIGN KEY(NeptunePageRenderTypeID)
REFERENCES dbo.NeptunePageRenderType (NeptunePageRenderTypeID)


ALTER TABLE dbo.Notification  WITH CHECK ADD  CONSTRAINT FK_Notification_NotificationType_NotificationTypeID FOREIGN KEY(NotificationTypeID)
REFERENCES dbo.NotificationType (NotificationTypeID)


ALTER TABLE dbo.Notification  WITH CHECK ADD  CONSTRAINT FK_Notification_Person_PersonID FOREIGN KEY(PersonID)
REFERENCES dbo.Person (PersonID)


ALTER TABLE dbo.Notification  WITH CHECK ADD  CONSTRAINT FK_Notification_Person_PersonID_TenantID FOREIGN KEY(PersonID, TenantID)
REFERENCES dbo.Person (PersonID, TenantID)


ALTER TABLE dbo.Notification  WITH CHECK ADD  CONSTRAINT FK_Notification_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)


ALTER TABLE dbo.NeptunePageImage  WITH CHECK ADD  CONSTRAINT FK_NeptunePageImage_FileResource_FileResourceID FOREIGN KEY(FileResourceID)
REFERENCES dbo.FileResource (FileResourceID)


ALTER TABLE dbo.NeptunePageImage  WITH CHECK ADD  CONSTRAINT FK_NeptunePageImage_FileResource_FileResourceID_TenantID FOREIGN KEY(FileResourceID, TenantID)
REFERENCES dbo.FileResource (FileResourceID, TenantID)


ALTER TABLE dbo.NeptunePageImage  WITH CHECK ADD  CONSTRAINT FK_NeptunePageImage_NeptunePage_NeptunePageID FOREIGN KEY(NeptunePageID)
REFERENCES dbo.NeptunePage (NeptunePageID)


ALTER TABLE dbo.NeptunePageImage  WITH CHECK ADD  CONSTRAINT FK_NeptunePageImage_NeptunePage_NeptunePageID_TenantID FOREIGN KEY(NeptunePageID, TenantID)
REFERENCES dbo.NeptunePage (NeptunePageID, TenantID)


ALTER TABLE dbo.NeptunePageImage  WITH CHECK ADD  CONSTRAINT FK_NeptunePageImage_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)


ALTER TABLE dbo.NeptunePage  WITH CHECK ADD  CONSTRAINT FK_NeptunePage_NeptunePageType_NeptunePageTypeID FOREIGN KEY(NeptunePageTypeID)
REFERENCES dbo.NeptunePageType (NeptunePageTypeID)


ALTER TABLE dbo.NeptunePage  WITH CHECK ADD  CONSTRAINT FK_NeptunePage_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)


ALTER TABLE dbo.Person  WITH CHECK ADD  CONSTRAINT FK_Person_Organization_OrganizationID FOREIGN KEY(OrganizationID)
REFERENCES dbo.Organization (OrganizationID)



ALTER TABLE dbo.Person  WITH CHECK ADD  CONSTRAINT FK_Person_Organization_OrganizationID_TenantID FOREIGN KEY(OrganizationID, TenantID)
REFERENCES dbo.Organization (OrganizationID, TenantID)


ALTER TABLE dbo.Person  WITH CHECK ADD  CONSTRAINT FK_Person_Role_RoleID FOREIGN KEY(RoleID)
REFERENCES dbo.Role (RoleID)


ALTER TABLE dbo.Person  WITH CHECK ADD  CONSTRAINT FK_Person_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)



ALTER TABLE dbo.Organization  WITH CHECK ADD  CONSTRAINT FK_Organization_Person_PrimaryContactPersonID_TenantID_PersonID_TenantID FOREIGN KEY(PrimaryContactPersonID, TenantID)
REFERENCES dbo.Person (PersonID, TenantID)


CREATE UNIQUE INDEX AK_Organization_OrganizationGuid_TenantID ON dbo.Organization
(
	OrganizationGuid,
	TenantID
)
WHERE (OrganizationGuid IS NOT NULL)


ALTER TABLE dbo.Organization  WITH CHECK ADD  CONSTRAINT FK_Organization_FileResource_LogoFileResourceID_FileResourceID FOREIGN KEY(LogoFileResourceID)
REFERENCES dbo.FileResource (FileResourceID)


ALTER TABLE dbo.Organization  WITH CHECK ADD  CONSTRAINT FK_Organization_FileResource_LogoFileResourceID_TenantID_FileResourceID_TenantID FOREIGN KEY(LogoFileResourceID, TenantID)
REFERENCES dbo.FileResource (FileResourceID, TenantID)


ALTER TABLE dbo.Organization  WITH CHECK ADD  CONSTRAINT FK_Organization_OrganizationType_OrganizationTypeID FOREIGN KEY(OrganizationTypeID)
REFERENCES dbo.OrganizationType (OrganizationTypeID)


ALTER TABLE dbo.Organization  WITH CHECK ADD  CONSTRAINT FK_Organization_OrganizationType_OrganizationTypeID_TenantID FOREIGN KEY(OrganizationTypeID, TenantID)
REFERENCES dbo.OrganizationType (OrganizationTypeID, TenantID)


ALTER TABLE dbo.Organization  WITH CHECK ADD  CONSTRAINT FK_Organization_Person_PrimaryContactPersonID_PersonID FOREIGN KEY(PrimaryContactPersonID)
REFERENCES dbo.Person (PersonID)


ALTER TABLE dbo.Organization  WITH CHECK ADD  CONSTRAINT FK_Organization_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)



ALTER TABLE dbo.TenantAttribute  WITH CHECK ADD  CONSTRAINT FK_TenantAttribute_FileResource_TenantBannerLogoFileResourceID_FileResourceID FOREIGN KEY(TenantBannerLogoFileResourceID)
REFERENCES dbo.FileResource (FileResourceID)


ALTER TABLE dbo.TenantAttribute  WITH CHECK ADD  CONSTRAINT FK_TenantAttribute_FileResource_TenantBannerLogoFileResourceID_TenantID_FileResourceID_TenantID FOREIGN KEY(TenantBannerLogoFileResourceID, TenantID)
REFERENCES dbo.FileResource (FileResourceID, TenantID)


ALTER TABLE dbo.TenantAttribute  WITH CHECK ADD  CONSTRAINT FK_TenantAttribute_FileResource_TenantSquareLogoFileResourceID_FileResourceID FOREIGN KEY(TenantSquareLogoFileResourceID)
REFERENCES dbo.FileResource (FileResourceID)


ALTER TABLE dbo.TenantAttribute  WITH CHECK ADD  CONSTRAINT FK_TenantAttribute_FileResource_TenantSquareLogoFileResourceID_TenantID_FileResourceID_TenantID FOREIGN KEY(TenantSquareLogoFileResourceID, TenantID)
REFERENCES dbo.FileResource (FileResourceID, TenantID)


ALTER TABLE dbo.TenantAttribute  WITH CHECK ADD  CONSTRAINT FK_TenantAttribute_FileResource_TenantStyleSheetFileResourceID_FileResourceID FOREIGN KEY(TenantStyleSheetFileResourceID)
REFERENCES dbo.FileResource (FileResourceID)


ALTER TABLE dbo.TenantAttribute  WITH CHECK ADD  CONSTRAINT FK_TenantAttribute_FileResource_TenantStyleSheetFileResourceID_TenantID_FileResourceID_TenantID FOREIGN KEY(TenantStyleSheetFileResourceID, TenantID)
REFERENCES dbo.FileResource (FileResourceID, TenantID)


ALTER TABLE dbo.TenantAttribute  WITH CHECK ADD  CONSTRAINT FK_TenantAttribute_Person_PrimaryContactPersonID_PersonID FOREIGN KEY(PrimaryContactPersonID)
REFERENCES dbo.Person (PersonID)


ALTER TABLE dbo.TenantAttribute  WITH CHECK ADD  CONSTRAINT FK_TenantAttribute_Person_PrimaryContactPersonID_TenantID_PersonID_TenantID FOREIGN KEY(PrimaryContactPersonID, TenantID)
REFERENCES dbo.Person (PersonID, TenantID)


ALTER TABLE dbo.TenantAttribute  WITH CHECK ADD  CONSTRAINT FK_TenantAttribute_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)



ALTER TABLE dbo.NeptuneHomePageImage  WITH CHECK ADD  CONSTRAINT FK_NeptuneHomePageImage_FileResource_FileResourceID FOREIGN KEY(FileResourceID)
REFERENCES dbo.FileResource (FileResourceID)


ALTER TABLE dbo.NeptuneHomePageImage  WITH CHECK ADD  CONSTRAINT FK_NeptuneHomePageImage_FileResource_FileResourceID_TenantID FOREIGN KEY(FileResourceID, TenantID)
REFERENCES dbo.FileResource (FileResourceID, TenantID)


ALTER TABLE dbo.NeptuneHomePageImage  WITH CHECK ADD  CONSTRAINT FK_NeptuneHomePageImage_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)


ALTER TABLE dbo.FieldDefinitionDataImage  WITH CHECK ADD  CONSTRAINT FK_FieldDefinitionDataImage_FieldDefinitionData_FieldDefinitionDataID FOREIGN KEY(FieldDefinitionDataID)
REFERENCES dbo.FieldDefinitionData (FieldDefinitionDataID)


ALTER TABLE dbo.FieldDefinitionDataImage  WITH CHECK ADD  CONSTRAINT FK_FieldDefinitionDataImage_FieldDefinitionData_FieldDefinitionDataID_TenantID FOREIGN KEY(FieldDefinitionDataID, TenantID)
REFERENCES dbo.FieldDefinitionData (FieldDefinitionDataID, TenantID)


ALTER TABLE dbo.FieldDefinitionDataImage  WITH CHECK ADD  CONSTRAINT FK_FieldDefinitionDataImage_FileResource_FileResourceID FOREIGN KEY(FileResourceID)
REFERENCES dbo.FileResource (FileResourceID)


ALTER TABLE dbo.FieldDefinitionDataImage  WITH CHECK ADD  CONSTRAINT FK_FieldDefinitionDataImage_FileResource_FileResourceID_TenantID FOREIGN KEY(FileResourceID, TenantID)
REFERENCES dbo.FileResource (FileResourceID, TenantID)


ALTER TABLE dbo.FieldDefinitionDataImage  WITH CHECK ADD  CONSTRAINT FK_FieldDefinitionDataImage_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)


ALTER TABLE dbo.FieldDefinitionData  WITH CHECK ADD  CONSTRAINT FK_FieldDefinitionData_FieldDefinition_FieldDefinitionID FOREIGN KEY(FieldDefinitionID)
REFERENCES dbo.FieldDefinition (FieldDefinitionID)


ALTER TABLE dbo.FieldDefinitionData  WITH CHECK ADD  CONSTRAINT FK_FieldDefinitionData_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)


ALTER TABLE dbo.County  WITH CHECK ADD  CONSTRAINT FK_County_StateProvince_StateProvinceID FOREIGN KEY(StateProvinceID)
REFERENCES dbo.StateProvince (StateProvinceID)


ALTER TABLE dbo.County  WITH CHECK ADD  CONSTRAINT FK_County_StateProvince_StateProvinceID_TenantID FOREIGN KEY(StateProvinceID, TenantID)
REFERENCES dbo.StateProvince (StateProvinceID, TenantID)


ALTER TABLE dbo.County  WITH CHECK ADD  CONSTRAINT FK_County_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)




ALTER TABLE dbo.AuditLog  WITH CHECK ADD  CONSTRAINT FK_AuditLog_AuditLogEventType_AuditLogEventTypeID FOREIGN KEY(AuditLogEventTypeID)
REFERENCES dbo.AuditLogEventType (AuditLogEventTypeID)


ALTER TABLE dbo.AuditLog  WITH CHECK ADD  CONSTRAINT FK_AuditLog_Person_PersonID FOREIGN KEY(PersonID)
REFERENCES dbo.Person (PersonID)


ALTER TABLE dbo.AuditLog  WITH CHECK ADD  CONSTRAINT FK_AuditLog_Person_PersonID_TenantID FOREIGN KEY(PersonID, TenantID)
REFERENCES dbo.Person (PersonID, TenantID)


ALTER TABLE dbo.AuditLog  WITH CHECK ADD  CONSTRAINT FK_AuditLog_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)


