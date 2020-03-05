Drop view if exists dbo.vViewTreatmentBMPModelingAttributes
GO

Create View dbo.vViewTreatmentBMPModelingAttributes
as

select tb.TreatmentBMPID as PrimaryKey, tb.TreatmentBMPName, tbt.TreatmentBMPTypeID, tbt.TreatmentBMPTypeName, sj.StormwaterJurisdictionID, 
	   tma.UpstreamTreatmentBMPID, tma.AverageDivertedFlowrate, tma.AverageTreatmentFlowrate, tma.DesignDryWeatherTreatmentCapacity, tma.DesignLowFlowDiversionCapacity,
	   tma.DesignMediaFiltrationRate, tma.DesignResidenceTimeforPermanentPool, tma.DiversionRate, tma.DrawdownTimeforWQDetentionVolume, tma.EffectiveFootprint,
	   tma.EffectiveRetentionDepth, tma.InfiltrationDischargeRate, tma.InfiltrationSurfaceArea, tma.MediaBedFootprint, tma.PermanentPoolorWetlandVolume,
	   tma.RoutingConfigurationID, tma.StorageVolumeBelowLowestOutletElevation, tma.SummerHarvestedWaterDemand, tma.TimeOfConcentrationID, tma.TotalDrawdownTime,
	   tma.TotalEffectiveBMPVolume, tma.TotalEffectiveDrywellBMPVolume, tma.TreatmentRate, tma.UnderlyingHydrologicSoilGroupID, tma.UnderlyingInfiltrationRate,
	   tma.WaterQualityDetentionVolume, tma.WettedFootprint, tma.WinterHarvestedWaterDemand
from dbo.TreatmentBMP tb
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID and tbt.IsAnalyzedInModelingModule = 1
join dbo.StormwaterJurisdiction sj on tb.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
left join dbo.TreatmentBMPModelingAttribute tma on tb.TreatmentBMPID = tma.TreatmentBMPID

GO


