Create Procedure dbo.pDeleteNereidResults
with execute as owner
As
truncate table dbo.NereidResult

update dbo.TreatmentBMPNereidLog set LastRequestDate = null, NereidRequest = null, NereidResponse = null
update dbo.WaterQualityManagementPlanNereidLog set LastRequestDate = null, NereidRequest = null, NereidResponse = null

GO