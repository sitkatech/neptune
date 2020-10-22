alter table dbo.WaterQualityManagementPlanVerify
add VerificationDate datetime null 
GO

update dbo.WaterQualityManagementPlanVerify
set VerificationDate = WQMPVerifyCreateEvent.CreatedByDate
from dbo.WaterQualityManagementPlanVerify as WQMPVerify
join
(select PersonID as CreatedByPersonID, Min(AuditLogDate) as CreatedByDate, RecordID as WaterQualityManagementPlanVerifyID 
from AuditLog 
where TableName = 'WaterQualityManagementPlanVerify'
and AuditLogEventTypeID = 1
group by PersonID, RecordID) WQMPVerifyCreateEvent
on WQMPVerifyCreateEvent.WaterQualityManagementPlanVerifyID = WQMPVerify.WaterQualityManagementPlanVerifyID

alter table dbo.WaterQualityManagementPlanVerify alter column VerificationDate datetime not null