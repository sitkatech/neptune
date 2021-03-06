Drop view if exists dbo.vViewTreatmentBMPModelingAttributes
GO

Create View dbo.vViewTreatmentBMPModelingAttributes
as

select tb.TreatmentBMPID as PrimaryKey, tb.TreatmentBMPName, tb.UpstreamBMPID, 
		case
			when tb.UpstreamBMPID is not null then (select TreatmentBMPName from dbo.TreatmentBMP where TreatmentBMPID = tb.UpstreamBMPID)
			else ''
		end as UpstreamBMPName, tbt.TreatmentBMPTypeID, tbt.TreatmentBMPTypeName, sj.StormwaterJurisdictionID, o.OrganizationName,
	   tma.UpstreamTreatmentBMPID, tma.AverageDivertedFlowrate, tma.AverageTreatmentFlowrate, tma.DesignDryWeatherTreatmentCapacity, tma.DesignLowFlowDiversionCapacity,
	   tma.DesignMediaFiltrationRate, tma.DesignResidenceTimeforPermanentPool, tma.DiversionRate, tma.DrawdownTimeforWQDetentionVolume, tma.EffectiveFootprint,
	   tma.EffectiveRetentionDepth, tma.InfiltrationDischargeRate, tma.InfiltrationSurfaceArea, tma.MediaBedFootprint, tma.PermanentPoolorWetlandVolume,
	   tma.RoutingConfigurationID, tma.StorageVolumeBelowLowestOutletElevation, tma.SummerHarvestedWaterDemand, tma.TimeOfConcentrationID, tma.DrawdownTimeForDetentionVolume,
	   tma.TotalEffectiveBMPVolume, tma.TotalEffectiveDrywellBMPVolume, tma.TreatmentRate, tma.UnderlyingHydrologicSoilGroupID, tma.UnderlyingInfiltrationRate,
	   tma.WaterQualityDetentionVolume, tma.WettedFootprint, tma.WinterHarvestedWaterDemand,
	   om.MonthsOfOperationDisplayName as OperationMonths,
	   pz.DesignStormwaterDepthInInches, ws.WatershedName,
	   dt.DelineationTypeDisplayName as DelineationType,
	   case
		When d.IsVerified = 1 then 'Verified'
		when d.IsVerified = 0 then 'Provisional'
		else null
	   end as DelineationStatus,
	   tma.DryWeatherFlowOverrideID as DryWeatherFlowOverrideID
from dbo.TreatmentBMP tb
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID and tbt.IsAnalyzedInModelingModule = 1
join dbo.StormwaterJurisdiction sj on tb.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
join dbo.Organization o on sj.OrganizationID = o.OrganizationID
left join dbo.TreatmentBMPModelingAttribute tma on tb.TreatmentBMPID = tma.TreatmentBMPID
left join dbo.MonthsOfOperation om on tma.MonthsOfOperationID = om.MonthsOfOperationID
left join dbo.PrecipitationZone pz on tb.PrecipitationZoneID = pz.PrecipitationZoneID
left join dbo.Watershed ws on tb.WatershedID = ws.WatershedID
left join dbo.Delineation d on d.TreatmentBMPID = tb.TreatmentBMPID
left join dbo.DelineationType dt on dt.DelineationTypeID = d.DelineationTypeID
GO



