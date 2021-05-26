Drop view if exists dbo.vPowerBIWaterQualityManagementPlanOAndMVerification
GO

Create view dbo.vPowerBIWaterQualityManagementPlanOAndMVerification
as
select wqmp.WaterQualityManagementPlanID as PrimaryKey,
	   wqmp.WaterQualityManagementPlanName as 'WQMPName', 
	   o.OrganizationName as 'Jurisdiction', 
	   wqmpv.VerificationDate, 
	   wqmpv.LastEditedDate,
	   p.FirstName + ' ' + p.LastName as 'LastEditedBy',
	   wqmpvt.WaterQualityManagementPlanVerifyTypeName as 'TypeOfVerification',
	   wqmpvss.WaterQualityManagementPlanVisitStatusName as 'VisitStatus',
	   wqmpvs.WaterQualityManagementPlanVerifyStatusName as 'VerificationStatus',
	   wqmpv.SourceControlCondition,
	   wqmpv.EnforcementOrFollowupActions,
	   case
		when wqmpv.IsDraft = 1 then 'Draft' else 'Finalized'
	   end as 'IsDraft'
from dbo.WaterQualityManagementPlanVerify wqmpv
join dbo.WaterQualityManagementPlanVerifyType wqmpvt on wqmpv.WaterQualityManagementPlanVerifyTypeID = wqmpvt.WaterQualityManagementPlanVerifyTypeID
left join dbo.WaterQualityManagementPlanVerifyStatus wqmpvs on wqmpv.WaterQualityManagementPlanVerifyStatusID = wqmpvs.WaterQualityManagementPlanVerifyStatusID
join dbo.WaterQualityManagementPlanVisitStatus wqmpvss on wqmpv.WaterQualityManagementPlanVisitStatusID = wqmpvss.WaterQualityManagementPlanVisitStatusID
join dbo.WaterQualityManagementPlan wqmp on wqmpv.WaterQualityManagementPlanID = wqmpv.WaterQualityManagementPlanID
left join dbo.Person p on wqmpv.LastEditedByPersonID = p.PersonID
join dbo.StormwaterJurisdiction sj on wqmp.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
join dbo.Organization o on sj.OrganizationID = o.OrganizationID