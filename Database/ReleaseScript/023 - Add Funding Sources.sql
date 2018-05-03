CREATE TABLE dbo.FundingSource(
	FundingSourceID int IDENTITY(1,1) NOT NULL,
	TenantID int NOT NULL,
	OrganizationID int NOT NULL,
	FundingSourceName varchar(200) NOT NULL,
	IsActive bit NOT NULL,
	FundingSourceDescription varchar(500) NULL,
	CONSTRAINT PK_FundingSource_FundingSourceID PRIMARY KEY CLUSTERED 
	(
		FundingSourceID ASC
	),
	CONSTRAINT AK_FundingSource_FundingSourceID_TenantID UNIQUE NONCLUSTERED 
	(
		FundingSourceID ASC,
		TenantID ASC
	),
	CONSTRAINT AK_FundingSource_OrganizationID_FundingSourceName UNIQUE NONCLUSTERED 
	(
		OrganizationID ASC,
		FundingSourceName ASC
	)
)
GO

ALTER TABLE dbo.FundingSource  WITH CHECK ADD  CONSTRAINT FK_FundingSource_Organization_OrganizationID FOREIGN KEY(OrganizationID)
REFERENCES dbo.Organization (OrganizationID)
GO

ALTER TABLE dbo.FundingSource CHECK CONSTRAINT FK_FundingSource_Organization_OrganizationID
GO

ALTER TABLE dbo.FundingSource  WITH CHECK ADD  CONSTRAINT FK_FundingSource_Organization_OrganizationID_TenantID FOREIGN KEY(OrganizationID, TenantID)
REFERENCES dbo.Organization (OrganizationID, TenantID)
GO

ALTER TABLE dbo.FundingSource CHECK CONSTRAINT FK_FundingSource_Organization_OrganizationID_TenantID
GO

ALTER TABLE dbo.FundingSource  WITH CHECK ADD  CONSTRAINT FK_FundingSource_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)
GO

ALTER TABLE dbo.FundingSource CHECK CONSTRAINT FK_FundingSource_Tenant_TenantID
GO


CREATE TABLE dbo.TreatmentBMPFundingSource(
	TreatmentBMPFundingSourceID int IDENTITY(1,1) NOT NULL,
	TenantID int NOT NULL,
	FundingSourceID int NOT NULL,
	TreatmentBMPID int NOT NULL,
	Amount money NOT NULL,
	CONSTRAINT PK_TreatmentBMPFundingSource_TreatmentBMPFundingSourceID PRIMARY KEY CLUSTERED 
	(
		TreatmentBMPFundingSourceID ASC
	),
	CONSTRAINT AK_TreatmentBMPFundingSource_FundingSourceID_TreatmentBMPID UNIQUE NONCLUSTERED 
	(
		FundingSourceID ASC,
		TreatmentBMPID ASC
	)
)
GO

ALTER TABLE dbo.TreatmentBMPFundingSource  WITH CHECK ADD  CONSTRAINT FK_TreatmentBMPFundingSource_FundingSource_FundingSourceID FOREIGN KEY(FundingSourceID)
REFERENCES dbo.FundingSource (FundingSourceID)
GO

ALTER TABLE dbo.TreatmentBMPFundingSource CHECK CONSTRAINT FK_TreatmentBMPFundingSource_FundingSource_FundingSourceID
GO

ALTER TABLE dbo.TreatmentBMPFundingSource  WITH CHECK ADD  CONSTRAINT FK_TreatmentBMPFundingSource_FundingSource_FundingSourceID_TenantID FOREIGN KEY(FundingSourceID, TenantID)
REFERENCES dbo.FundingSource (FundingSourceID, TenantID)
GO

ALTER TABLE dbo.TreatmentBMPFundingSource CHECK CONSTRAINT FK_TreatmentBMPFundingSource_FundingSource_FundingSourceID_TenantID
GO

ALTER TABLE dbo.TreatmentBMPFundingSource  WITH CHECK ADD  CONSTRAINT FK_TreatmentBMPFundingSource_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)
GO

ALTER TABLE dbo.TreatmentBMPFundingSource CHECK CONSTRAINT FK_TreatmentBMPFundingSource_Tenant_TenantID
GO

ALTER TABLE dbo.TreatmentBMPFundingSource  WITH CHECK ADD  CONSTRAINT FK_TreatmentBMPFundingSource_TreatmentBMP_TreatmentBMPID FOREIGN KEY(TreatmentBMPID)
REFERENCES dbo.TreatmentBMP (TreatmentBMPID)
GO

ALTER TABLE dbo.TreatmentBMPFundingSource CHECK CONSTRAINT FK_TreatmentBMPFundingSource_TreatmentBMP_TreatmentBMPID
GO

ALTER TABLE dbo.TreatmentBMPFundingSource  WITH CHECK ADD  CONSTRAINT FK_TreatmentBMPFundingSource_TreatmentBMP_TreatmentBMPID_TenantID FOREIGN KEY(TreatmentBMPID, TenantID)
REFERENCES dbo.TreatmentBMP (TreatmentBMPID, TenantID)
GO

ALTER TABLE dbo.TreatmentBMPFundingSource CHECK CONSTRAINT FK_TreatmentBMPFundingSource_TreatmentBMP_TreatmentBMPID_TenantID
GO

INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel) 
VALUES (44, N'FundingSource', N'Funding Source', '', 1)
go

insert into dbo.FieldDefinitionData (TenantID, FieldDefinitionID)
select t.TenantID, 44
from dbo.Tenant t

insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values (21, 'FundingSourcesList', 'Funding Sources List', 2)
go

insert into dbo.NeptunePage (TenantID, NeptunePageTypeID)
select t.TenantID, 21
from dbo.Tenant t