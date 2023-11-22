Create View dbo.vFieldVisitDetailed
as

select	fv.FieldVisitID as PrimaryKey,
		tb.TreatmentBMPID, tb.TreatmentBMPName, tbt.TreatmentBMPTypeID, tbt.TreatmentBMPTypeName, sj.StormwaterJurisdictionID, o.OrganizationName
		, fv.FieldVisitID, fv.VisitDate, fvt.FieldVisitTypeID, fvt.FieldVisitTypeDisplayName, fv.PerformedByPersonID, p.FirstName + ' ' + p.LastName as PerformedByPersonName
		, fvs.FieldVisitStatusID, fvs.FieldVisitStatusDisplayName, fv.IsFieldVisitVerified, fv.InventoryUpdated
		, isnull(reqAtt.NumberOfRequiredAttributes, 0) as NumberOfRequiredAttributes, isnull(reqAttEnt.NumberRequiredAttributesEntered, 0) as NumberRequiredAttributesEntered
		, tbasInit.TreatmentBMPAssessmentID as TreatmentBMPAssessmentIDInitial, isnull(tbasInit.IsAssessmentComplete, 0) as IsAssessmentCompleteInitial, tbasInit.AssessmentScore as AssessmentScoreInitial
		, tbasPM.TreatmentBMPAssessmentID as TreatmentBMPAssessmentIDPM, isnull(tbasPM.IsAssessmentComplete, 0) as IsAssessmentCompletePM, tbasPM.AssessmentScore as AssessmentScorePM
		, mr.MaintenanceRecordID
		, isnull(wqmp.WaterQualityManagementPlanID, 0) as WaterQualityManagementPlanID, isnull(wqmp.WaterQualityManagementPlanName, '') as WaterQualityManagementPlanName
from dbo.FieldVisit fv
join dbo.TreatmentBMP tb on fv.TreatmentBMPID = tb.TreatmentBMPID
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.StormwaterJurisdiction sj on tb.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
join dbo.Organization o on sj.OrganizationID = o.OrganizationID
join dbo.FieldVisitStatus fvs on fv.FieldVisitStatusID = fvs.FieldVisitStatusID
join dbo.FieldVisitType fvt on fv.FieldVisitTypeID = fvt.FieldVisitTypeID
join dbo.Person p on fv.PerformedByPersonID = p.PersonID
left join dbo.WaterQualityManagementPlan wqmp on tb.WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID
left join 
(
	select tbtcat.TreatmentBMPTypeID, count(cat.CustomAttributeTypeID) as NumberOfRequiredAttributes
	from dbo.TreatmentBMPTypeCustomAttributeType tbtcat
	join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID and cat.IsRequired = 1 and cat.CustomAttributeTypePurposeID != 3
	group by tbtcat.TreatmentBMPTypeID
) reqAtt on tbt.TreatmentBMPTypeID = reqAtt.TreatmentBMPTypeID
left join 
(
	select ca.TreatmentBMPID, ca.TreatmentBMPTypeID, count(distinct cat.CustomAttributeTypeID) as NumberRequiredAttributesEntered
	from dbo.CustomAttribute ca
	join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
	join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID 
	where len(cav.AttributeValue) > 0 and cat.CustomAttributeTypePurposeID != 3 and cat.IsRequired = 1
	group by ca.TreatmentBMPID, ca.TreatmentBMPTypeID
) reqAttEnt on tb.TreatmentBMPID = reqAttEnt.TreatmentBMPID
left join dbo.TreatmentBMPAssessment tbasInit on fv.FieldVisitID = tbasInit.FieldVisitID and tbasInit.TreatmentBMPAssessmentTypeID = 1 -- Initial
left join dbo.TreatmentBMPAssessment tbasPM on fv.FieldVisitID = tbasPM.FieldVisitID and tbasPM.TreatmentBMPAssessmentTypeID = 2 -- Post-Maintenance
left join dbo.MaintenanceRecord mr on fv.FieldVisitID = mr.FieldVisitID


GO