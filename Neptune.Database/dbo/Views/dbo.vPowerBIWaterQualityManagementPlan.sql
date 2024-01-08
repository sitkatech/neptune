Create view dbo.vPowerBIWaterQualityManagementPlan
as
select 
wqmp.WaterQualityManagementPlanID,
wqmp.WaterQualityManagementPlanName,
o.OrganizationName,
stat.WaterQualityManagementPlanStatusDisplayName,
devel.WaterQualityManagementPlanDevelopmentTypeDisplayName,
land.WaterQualityManagementPlanLandUseDisplayName,
term.WaterQualityManagementPlanPermitTermDisplayName,
DATEDIFF(second, {d '1970-01-01'}, wqmp.ApprovalDate) as ApprovalDate,
DATEDIFF(second, {d '1970-01-01'}, wqmp.DateOfConstruction) as DateOfConstruction,
hydromod.HydromodificationAppliesTypeDisplayName as HydromodificationAppliesDisplayName,
hydrosub.HydrologicSubareaName,
wqmp.RecordedWQMPAreaInAcres,
trash.TrashCaptureStatusTypeDisplayName,
wqmp.TrashCaptureEffectiveness,
wqmpma.WaterQualityManagementPlanModelingApproachName as ModelingApproach
 from
dbo.WaterQualityManagementPlan wqmp
join dbo.StormwaterJurisdiction sj on wqmp.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
join dbo.Organization o on sj.OrganizationID = o.OrganizationID
left join dbo.WaterQualityManagementPlanStatus stat on wqmp.WaterQualityManagementPlanStatusID = stat.WaterQualityManagementPlanStatusID
join dbo.WaterQualityManagementPlanDevelopmentType devel on wqmp.WaterQualityManagementPlanDevelopmentTypeID = devel.WaterQualityManagementPlanDevelopmentTypeID
join dbo.WaterQualityManagementPlanLandUse land on wqmp.WaterQualityManagementPlanLandUseID = land.WaterQualityManagementPlanLandUseID
left join dbo.WaterQualityManagementPlanPermitTerm term on wqmp.WaterQualityManagementPlanPermitTermID = term.WaterQualityManagementPlanPermitTermID
left join dbo.HydromodificationAppliesType hydromod on wqmp.HydromodificationAppliesTypeID = hydromod.HydromodificationAppliesTypeID
left join dbo.HydrologicSubarea hydrosub on wqmp.HydrologicSubareaID = hydrosub.HydrologicSubareaID
left join dbo.WaterQualityManagementPlanModelingApproach wqmpma on wqmp.WaterQualityManagementPlanModelingApproachID = wqmpma.WaterQualityManagementPlanModelingApproachID
join dbo.TrashCaptureStatusType trash on wqmp.TrashCaptureStatusTypeID = trash.TrashCaptureStatusTypeID