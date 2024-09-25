Create Procedure dbo.pDeleteNereidResults
with execute as owner
As
truncate table dbo.NereidResult

update nl 
set LastRequestDate = null, NereidRequest = null, NereidResponse = null
from dbo.TreatmentBMPNereidLog nl
join dbo.TreatmentBMP tb on nl.TreatmentBMPID = tb.TreatmentBMPID
where tb.ProjectID is null

update dbo.WaterQualityManagementPlanNereidLog set LastRequestDate = null, NereidRequest = null, NereidResponse = null

GO