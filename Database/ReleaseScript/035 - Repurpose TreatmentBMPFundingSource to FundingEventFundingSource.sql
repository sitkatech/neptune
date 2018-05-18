-- hotwiring the "drop and create" script to transform the TreatmentBMPFundingSource table into the FundingEventFundingSource table
-- it's easy to do this since there's no data needs to be migrated

ALTER TABLE dbo.TreatmentBMPFundingSource DROP CONSTRAINT FK_TreatmentBMPFundingSource_TreatmentBMP_TreatmentBMPID_TenantID
GO

ALTER TABLE dbo.TreatmentBMPFundingSource DROP CONSTRAINT FK_TreatmentBMPFundingSource_TreatmentBMP_TreatmentBMPID
GO

ALTER TABLE dbo.TreatmentBMPFundingSource DROP CONSTRAINT FK_TreatmentBMPFundingSource_Tenant_TenantID
GO

ALTER TABLE dbo.TreatmentBMPFundingSource DROP CONSTRAINT FK_TreatmentBMPFundingSource_FundingSource_FundingSourceID_TenantID
GO

ALTER TABLE dbo.TreatmentBMPFundingSource DROP CONSTRAINT FK_TreatmentBMPFundingSource_FundingSource_FundingSourceID
GO

/****** Object:  Table dbo.TreatmentBMPFundingSource    Script Date: 5/17/2018 11:50:51 AM ******/
DROP TABLE dbo.TreatmentBMPFundingSource
GO


CREATE TABLE dbo.FundingEventFundingSource(
	FundingEventFundingSourceID int IDENTITY(1,1) NOT NULL,
	TenantID int NOT NULL,
	FundingSourceID int NOT NULL,
	FundingEventID int NOT NULL,
	Amount money NULL,
 CONSTRAINT PK_FundingEventFundingSource_FundingEventFundingSourceID PRIMARY KEY CLUSTERED 
(
	FundingEventFundingSourceID ASC
),
 CONSTRAINT AK_FundingEventFundingSource_FundingSourceID_FundingEventID UNIQUE NONCLUSTERED 
(
	FundingSourceID ASC,
	FundingEventID ASC
)
)
GO

ALTER TABLE dbo.FundingEventFundingSource  WITH CHECK ADD  CONSTRAINT FK_FundingEventFundingSource_FundingSource_FundingSourceID FOREIGN KEY(FundingSourceID)
REFERENCES dbo.FundingSource (FundingSourceID)
GO

ALTER TABLE dbo.FundingEventFundingSource CHECK CONSTRAINT FK_FundingEventFundingSource_FundingSource_FundingSourceID
GO

ALTER TABLE dbo.FundingEventFundingSource  WITH CHECK ADD  CONSTRAINT FK_FundingEventFundingSource_FundingSource_FundingSourceID_TenantID FOREIGN KEY(FundingSourceID, TenantID)
REFERENCES dbo.FundingSource (FundingSourceID, TenantID)
GO

ALTER TABLE dbo.FundingEventFundingSource CHECK CONSTRAINT FK_FundingEventFundingSource_FundingSource_FundingSourceID_TenantID
GO

ALTER TABLE dbo.FundingEventFundingSource  WITH CHECK ADD  CONSTRAINT FK_FundingEventFundingSource_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)
GO

ALTER TABLE dbo.FundingEventFundingSource CHECK CONSTRAINT FK_FundingEventFundingSource_Tenant_TenantID
GO

ALTER TABLE dbo.FundingEventFundingSource  WITH CHECK ADD  CONSTRAINT FK_FundingEventFundingSource_FundingEvent_FundingEventID FOREIGN KEY(FundingEventID)
REFERENCES dbo.FundingEvent (FundingEventID)
GO

ALTER TABLE dbo.FundingEventFundingSource CHECK CONSTRAINT FK_FundingEventFundingSource_FundingEvent_FundingEventID
GO

ALTER TABLE dbo.FundingEventFundingSource  WITH CHECK ADD  CONSTRAINT FK_FundingEventFundingSource_FundingEvent_FundingEventID_TenantID FOREIGN KEY(FundingEventID, TenantID)
REFERENCES dbo.FundingEvent (FundingEventID, TenantID)
GO

ALTER TABLE dbo.FundingEventFundingSource CHECK CONSTRAINT FK_FundingEventFundingSource_FundingEvent_FundingEventID_TenantID
GO

