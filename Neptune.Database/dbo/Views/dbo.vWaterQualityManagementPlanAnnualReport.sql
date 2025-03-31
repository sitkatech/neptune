create view dbo.vWaterQualityManagementPlanAnnualReport
as

select 
	   concat(wqmp.WaterQualityManagementPlanID,'_', wqmpv.WaterQualityManagementPlanVerifyID, '_', fv.FieldVisitID) as PrimaryKey,
	   wqmp.WaterQualityManagementPlanID,
       wqmp.WaterQualityManagementPlanName,
       wqmp.WaterQualityManagementPlanStatusID,
       wqmp.StormwaterJurisdictionID,
       sj.StormwaterJurisdictionPublicWQMPVisibilityTypeID,
       wqmp.ApprovalDate,
	   bmpcount.TreatmentBMPCount,
	   qbmpcount.QuickBMPCount,
	   qbmpcount.QuickBMPNumberOfIndividualBMPs,
       wqmpv.WaterQualityManagementPlanVerifyID,
       wqmpv.IsDraft as WaterQualityManagementPlanVerifyIsDraft,
       wqmpv.VerificationDate as WaterQualityManagementPlanVerifyVerificationDate,
	   vbmpnotes.WaterQualityManagementPlanVerifyTreatmentBMPNotes,
	   vqnotes.WaterQualityManagementPlanVerifyQuickBMPNotes,
	   fv.FieldVisitID,
       fv.FieldVisitStatusID,
       fv.VisitDate as FieldVisitDate
from dbo.WaterQualityManagementPlan wqmp
join dbo.StormwaterJurisdiction sj on wqmp.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
left join dbo.WaterQualityManagementPlanVerify wqmpv on wqmp.WaterQualityManagementPlanID = wqmpv.WaterQualityManagementPlanID

left join dbo.TreatmentBMP bmp on wqmp.WaterQualityManagementPlanID = bmp.WaterQualityManagementPlanID
left join dbo.FieldVisit fv on bmp.TreatmentBMPID = fv.TreatmentBMPID
left join dbo.FieldVisitStatus fvs on fv.FieldVisitStatusID = fvs.FieldVisitStatusID

left join (
	select count(*) as TreatmentBMPCount
	,WaterQualityManagementPlanID
	from dbo.TreatmentBMP 
	group by WaterQualityManagementPlanID
			
) bmpcount on bmpcount.WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID

left join (
	select sum(NumberOfIndividualBMPs) as QuickBMPNumberOfIndividualBMPs
	,count(*) as QuickBMPCount
	,WaterQualityManagementPlanID
	from dbo.QuickBMP 
	group by WaterQualityManagementPlanID
			
) qbmpcount on qbmpcount.WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID

left join (
	select
		STRING_AGG(WaterQualityManagementPlanVerifyTreatmentBMPNote, '; ') as WaterQualityManagementPlanVerifyTreatmentBMPNotes,
		WaterQualityManagementPlanVerifyID
	from dbo.WaterQualityManagementPlanVerifyTreatmentBMP vbmp
	group by WaterQualityManagementPlanVerifyID
) vbmpnotes on vbmpnotes.WaterQualityManagementPlanVerifyID = wqmpv.WaterQualityManagementPlanVerifyID

left join (
	select
		STRING_AGG(WaterQualityManagementPlanVerifyQuickBMPNote, '; ') as WaterQualityManagementPlanVerifyQuickBMPNotes,
		WaterQualityManagementPlanVerifyID
	from dbo.WaterQualityManagementPlanVerifyQuickBMP wqmpqv
	group by WaterQualityManagementPlanVerifyID
) vqnotes on vqnotes.WaterQualityManagementPlanVerifyID = wqmpv.WaterQualityManagementPlanVerifyID

where  wqmp.ApprovalDate is not null and (wqmpv.IsDraft is null or wqmpv.IsDraft = 0) and (fv.FieldVisitStatusID is null or fv.FieldVisitStatusID = 2)

GO