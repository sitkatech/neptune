Drop view if exists dbo.vPowerBIWaterQualityManagementPlan
GO

Create view dbo.vPowerBIWaterQualityManagementPlan
as
select 
wqmp.WaterQualityManagementPlanID as PrimaryKey,
wqmp.WaterQualityManagementPlanID,
wqmp.WaterQualityManagementPlanName,
o.OrganizationName,
stat.WaterQualityManagementPlanStatusDisplayName,
devel.WaterQualityManagementPlanDevelopmentTypeDisplayName,
land.WaterQualityManagementPlanLandUseDisplayName,
term.WaterQualityManagementPlanPermitTermDisplayName,
wqmp.ApprovalDate,
wqmp.DateOfContruction,
hydromod.HydromodificationAppliesDisplayName,
hydrosub.HydrologicSubareaName,
wqmp.RecordedWQMPAreaInAcres,
trash.TrashCaptureStatusTypeDisplayName,
wqmp.TrashCaptureEffectiveness

 from
dbo.WaterQualityManagementPlan wqmp
join dbo.StormwaterJurisdiction sj on wqmp.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
join dbo.Organization o on sj.OrganizationID = o.OrganizationID
join dbo.WaterQualityManagementPlanStatus stat on wqmp.WaterQualityManagementPlanStatusID = stat.WaterQualityManagementPlanStatusID
join dbo.WaterQualityManagementPlanDevelopmentType devel on wqmp.WaterQualityManagementPlanDevelopmentTypeID = devel.WaterQualityManagementPlanDevelopmentTypeID
join dbo.WaterQualityManagementPlanLandUse land on wqmp.WaterQualityManagementPlanLandUseID = land.WaterQualityManagementPlanLandUseID
join dbo.WaterQualityManagementPlanPermitTerm term on wqmp.WaterQualityManagementPlanPermitTermID = term.WaterQualityManagementPlanPermitTermID
join dbo.HydromodificationApplies hydromod on wqmp.HydromodificationAppliesID = hydromod.HydromodificationAppliesID
join dbo.HydrologicSubarea hydrosub on wqmp.HydrologicSubareaID = hydrosub.HydrologicSubareaID
join dbo.TrashCaptureStatusType trash on wqmp.TrashCaptureStatusTypeID = trash.TrashCaptureStatusTypeID