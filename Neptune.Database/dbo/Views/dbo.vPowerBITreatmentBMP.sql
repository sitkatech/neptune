create view dbo.vPowerBITreatmentBMP
as
select 
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
--    att.UpstreamTreatmentBMPID, 
    att.AverageDivertedFlowrate, att.AverageTreatmentFlowrate, 
    att.DesignDryWeatherTreatmentCapacity, att.DesignLowFlowDiversionCapacity, att.DesignMediaFiltrationRate, att.DiversionRate, 
    att.DrawdownTimeForWQDetentionVolume, att.EffectiveFootprint, att.EffectiveRetentionDepth, att.ExtendedDetentionSurchargeVolume,
    att.InfiltrationDischargeRate, att.InfiltrationSurfaceArea, att.MediaBedFootprint, att.PermanentPoolOrWetlandVolume, 
    att.RoutingConfiguration, att.StorageVolumeBelowLowestOutletElevation, att.SummerHarvestedWaterDemand, 
    att.TimeOfConcentration, att.DrawdownTimeForDetentionVolume, att.TotalEffectiveBMPVolume, att.TotalEffectiveDrywellBMPVolume, att.TreatmentRate, 
    att.UnderlyingHydrologicSoilGroup, att.UnderlyingInfiltrationRate, 
    att.WettedFootprint, att.WinterHarvestedWaterDemand, att.MonthsOperational, att.DryWeatherFlowOverride

from
	dbo.TreatmentBMP bmp 
	left join dbo.Delineation d
		on bmp.TreatmentBMPID = d.TreatmentBMPID
	left join dbo.Watershed w
		on bmp.WatershedID = w.WatershedID
	left join dbo.DelineationType dt
		on d.DelineationTypeID = dt.DelineationTypeID
	left join dbo.vTreatmentBMPModelingAttribute att
		on bmp.TreatmentBMPID = att.TreatmentBMPID
	join dbo.TreatmentBMPType ty
		on bmp.TreatmentBMPTypeID = ty.TreatmentBMPTypeID
	join dbo.StormwaterJurisdiction sj
		on bmp.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
	join dbo.Organization o
		on sj.OrganizationID = o.OrganizationID
where ty.TreatmentBMPModelingTypeID is not null and bmp.ProjectID is null