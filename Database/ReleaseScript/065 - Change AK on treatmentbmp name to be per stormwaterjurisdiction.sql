ALTER TABLE dbo.TreatmentBMP DROP CONSTRAINT AK_TreatmentBMP_TreatmentBMPName_TenantID
GO

ALTER TABLE dbo.TreatmentBMP ADD  CONSTRAINT AK_TreatmentBMP_StormwaterJursidictionID_TreatmentBMPName UNIQUE (StormwaterJurisdictionID, TreatmentBMPName)


