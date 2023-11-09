create view dbo.vPowerBITreatmentBMP
as
select 
	bmp.TreatmentBMPID as PrimaryKey,
    bmp.TreatmentBMPID,
	bmp.TreatmentBMPName,
	ty.TreatmentBMPTypeName,
	o.OrganizationName as Jurisdiction,
	bmp.LocationPoint4326.STX as LocationLon,
	bmp.LocationPoint4326.STY as LocationLat,
	w.WatershedName as Watershed,
	bmp.UpstreamBMPID as UpstreamTreatmentBMPID,
	dt.DelineationTypeDisplayName as DelineationType,
	bmp.WaterQualityManagementPlanID,
	att.TreatmentBMPModelingAttributeID, 
--    att.UpstreamTreatmentBMPID, 
    att.AverageDivertedFlowrate, att.AverageTreatmentFlowrate, 
    att.DesignDryWeatherTreatmentCapacity, att.DesignLowFlowDiversionCapacity, att.DesignMediaFiltrationRate, att.DiversionRate, 
    att.DrawdownTimeforWQDetentionVolume, att.EffectiveFootprint, att.EffectiveRetentionDepth, 
    att.InfiltrationDischargeRate, att.InfiltrationSurfaceArea, att.MediaBedFootprint, att.PermanentPoolorWetlandVolume, 
    att.RoutingConfigurationID, att.StorageVolumeBelowLowestOutletElevation, att.SummerHarvestedWaterDemand, 
    att.TimeOfConcentrationID, att.DrawdownTimeForDetentionVolume, att.TotalEffectiveBMPVolume, att.TotalEffectiveDrywellBMPVolume, att.TreatmentRate, 
    att.UnderlyingHydrologicSoilGroupID, att.UnderlyingInfiltrationRate, att.WaterQualityDetentionVolume, 
    att.WettedFootprint, att.WinterHarvestedWaterDemand, att.MonthsOfOperationID, att.DryWeatherFlowOverrideID

from
	dbo.TreatmentBMP bmp 
	left join dbo.Delineation d
		on bmp.TreatmentBMPID = d.TreatmentBMPID
	left join dbo.Watershed w
		on bmp.WatershedID = w.WatershedID
	left join dbo.DelineationType dt
		on d.DelineationTypeID = dt.DelineationTypeID
	left join dbo.TreatmentBMPModelingAttribute att
		on bmp.TreatmentBMPID = att.TreatmentBMPID
	join dbo.TreatmentBMPType ty
		on bmp.TreatmentBMPTypeID = ty.TreatmentBMPTypeID
	join dbo.StormwaterJurisdiction sj
		on bmp.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
	join dbo.Organization o
		on sj.OrganizationID = o.OrganizationID
where ty.TreatmentBMPModelingTypeID is not null and bmp.ProjectID is null