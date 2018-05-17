exec sp_rename 'dbo.PK_TreatmentBMPFundingSource_TreatmentBMPFundingSourceID', 'PK_FundingEvent_FundingEventID', 'OBJECT';
exec sp_rename 'dbo.AK_TreatmentBMPFundingSource_FundingSourceID_TreatmentBMPID', 'AK_FundingEvent_FundingSourceID_TreatmentBMPID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPFundingSource_FundingSource_FundingSourceID', 'FK_FundingEvent_FundingSource_FundingSourceID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPFundingSource_FundingSource_FundingSourceID_TenantID', 'FK_FundingEvent_FundingSource_FundingSourceID_TenantID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPFundingSource_Tenant_TenantID', 'FK_FundingEvent_Tenant_TenantID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPFundingSource_TreatmentBMP_TreatmentBMPID', 'FK_FundingEvent_TreatmentBMP_TreatmentBMPID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPFundingSource_TreatmentBMP_TreatmentBMPID_TenantID', 'FK_FundingEvent_TreatmentBMP_TreatmentBMPID_TenantID', 'OBJECT';
exec sp_rename 'dbo.TreatmentBMPFundingSource.TreatmentBMPFundingSourceID', 'FundingEventID', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPFundingSource', 'FundingEvent';