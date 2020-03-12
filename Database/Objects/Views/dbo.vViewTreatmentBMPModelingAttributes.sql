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
	   tma.WaterQualityDetentionVolume, tma.WettedFootprint, tma.WinterHarvestedWaterDemand, tms.OperationMonths, pz.DesignStormwaterDepthInInches, ws.WatershedName
from dbo.TreatmentBMP tb
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID and tbt.IsAnalyzedInModelingModule = 1
join dbo.StormwaterJurisdiction sj on tb.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
join dbo.Organization o on sj.OrganizationID = o.OrganizationID
left join dbo.TreatmentBMPModelingAttribute tma on tb.TreatmentBMPID = tma.TreatmentBMPID
left join (select TreatmentBMPID, 
	              STRING_AGG(case when OperationMonth = 1 then 'Jan' 
								  when OperationMonth = 2 then 'Feb' 
								  when OperationMonth = 3 then 'Mar' 
								  when OperationMonth = 4 then 'Apr' 
								  when OperationMonth = 5 then 'May' 
								  when OperationMonth = 6 then 'Jun' 
								  when OperationMonth = 7 then 'Jul' 
								  when OperationMonth = 8 then 'Aug' 
								  when OperationMonth = 9 then 'Sep' 
								  when OperationMonth = 10 then 'Oct' 
								  when OperationMonth = 11 then 'Nov' 
								  when OperationMonth = 12 then 'Dec' end, ', ') OperationMonths
			from dbo.TreatmentBMPOperationMonth
			group by TreatmentBMPID) tms on tms.TreatmentBMPID = tb.TreatmentBMPID
left join dbo.PrecipitationZone pz on tb.PrecipitationZoneID = pz.PrecipitationZoneID
left join dbo.Watershed ws on tb.WatershedID = ws.WatershedID
GO



