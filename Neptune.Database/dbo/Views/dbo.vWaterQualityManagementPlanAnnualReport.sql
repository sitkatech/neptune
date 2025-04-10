create view [dbo].[vWaterQualityManagementPlanAnnualReport]
as

select 
	   wqmp.WaterQualityManagementPlanID,
       wqmp.WaterQualityManagementPlanName,
       wqmp.WaterQualityManagementPlanStatusID,
       wqmp.StormwaterJurisdictionID,
       sj.StormwaterJurisdictionPublicWQMPVisibilityTypeID,
       wqmp.ApprovalDate,
       wqmpv.WaterQualityManagementPlanVerifyID,
	   wqmpv.WaterQualityManagementPlanVerifyStatusID,
       wqmpv.VerificationDate as WaterQualityManagementPlanVerifyVerificationDate,
	   wqmpv.EnforcementOrFollowupActions,
	   vbmp.WaterQualityManagementPlanVerifyTreatmentBMPCount,
	   vbmp.WaterQualityManagementPlanVerifyTreatmentBMPIsAdequateCount,
	   vbmp.WaterQualityManagementPlanVerifyTreatmentBMPIsDeficientCount,
	   vbmp.WaterQualityManagementPlanVerifyTreatmentBMPNotes,
	   vqbmp.WaterQualityManagementPlanVerifyQuickBMPCount, 
	   vqbmpadequate.WaterQualityManagementPlanVerifyQuickBMPIsAdequateCount,
	   vqbmp.WaterQualityManagementPlanVerifyQuickBMPCount - vqbmpadequate.WaterQualityManagementPlanVerifyQuickBMPIsAdequateCount as WaterQualityManagementPlanVerifyQuickBMPIsDeficient,
	   vqbmp.WaterQualityManagementPlanVerifyQuickBMPNotes
from dbo.WaterQualityManagementPlan wqmp
join dbo.StormwaterJurisdiction sj on wqmp.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
join dbo.WaterQualityManagementPlanVerify wqmpv on wqmp.WaterQualityManagementPlanID = wqmpv.WaterQualityManagementPlanID

left join (
	select
		STRING_AGG(WaterQualityManagementPlanVerifyTreatmentBMPNote, '; ') as WaterQualityManagementPlanVerifyTreatmentBMPNotes,
		count(*) as WaterQualityManagementPlanVerifyTreatmentBMPCount,
		count(IsAdequate) as WaterQualityManagementPlanVerifyTreatmentBMPIsAdequateCount,
		count(*) - count(IsAdequate) as WaterQualityManagementPlanVerifyTreatmentBMPIsDeficientCount,
		WaterQualityManagementPlanVerifyID
	from dbo.WaterQualityManagementPlanVerifyTreatmentBMP vbmp
	group by WaterQualityManagementPlanVerifyID
) vbmp on vbmp.WaterQualityManagementPlanVerifyID = wqmpv.WaterQualityManagementPlanVerifyID

left join (
	select
		STRING_AGG(WaterQualityManagementPlanVerifyQuickBMPNote, '; ') as WaterQualityManagementPlanVerifyQuickBMPNotes,
		sum(qbmp.NumberOfIndividualBMPs) as WaterQualityManagementPlanVerifyQuickBMPCount,
		WaterQualityManagementPlanVerifyID
	from dbo.WaterQualityManagementPlanVerifyQuickBMP wqmpqv
	join dbo.QuickBMP qbmp on wqmpqv.QuickBMPID = qbmp.QuickBMPID
	group by WaterQualityManagementPlanVerifyID
) vqbmp on vqbmp.WaterQualityManagementPlanVerifyID = wqmpv.WaterQualityManagementPlanVerifyID

left join (
	select
		sum(qbmp.NumberOfIndividualBMPs) as WaterQualityManagementPlanVerifyQuickBMPIsAdequateCount,
		WaterQualityManagementPlanVerifyID
	from dbo.WaterQualityManagementPlanVerifyQuickBMP wqmpqv
	join dbo.QuickBMP qbmp on wqmpqv.QuickBMPID = qbmp.QuickBMPID
	where wqmpqv.IsAdequate = 1
	group by WaterQualityManagementPlanVerifyID
) vqbmpadequate on vqbmpadequate.WaterQualityManagementPlanVerifyID = wqmpv.WaterQualityManagementPlanVerifyID

where  wqmp.ApprovalDate is not null and wqmpv.IsDraft = 0

GO