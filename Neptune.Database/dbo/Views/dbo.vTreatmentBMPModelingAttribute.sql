Create View dbo.vTreatmentBMPModelingAttribute
as

select tbmp.TreatmentBMPID, UpstreamBMPID,
	cast(max(case when cat.CustomAttributeTypeName = 'Average Diverted Flowrate' then cav.AttributeValue else null end) as float) as AverageDivertedFlowrate,
	cast(max(case when cat.CustomAttributeTypeName = 'Average Treatment Flowrate' then cav.AttributeValue else null end) as float) as AverageTreatmentFlowrate,
	cast(max(case when cat.CustomAttributeTypeName = 'Design Dry Weather Treatment Capacity' then cav.AttributeValue else null end) as float) as DesignDryWeatherTreatmentCapacity,
	cast(max(case when cat.CustomAttributeTypeName = 'Design Low Flow Diversion Capacity' then cav.AttributeValue else null end) as float) as DesignLowFlowDiversionCapacity,
	cast(max(case when cat.CustomAttributeTypeName = 'Design Media Filtration Rate' then cav.AttributeValue else null end) as float) as DesignMediaFiltrationRate,
	--MP 8/21/25 We haven't been allowing people to change this prior to the update back to CustomAttributes, but it's worth keeping to feed Nereid and I suppose display in our ModelingAttributes table
	--For now, always null
	null as DiversionRate,
	cast(max(case when cat.CustomAttributeTypeName = 'Drawdown Time For Detention Volume' then cav.AttributeValue else null end) as float) as DrawdownTimeForDetentionVolume,
	cast(max(case when cat.CustomAttributeTypeName = 'Drawdown Time For WQ Detention Volume' then cav.AttributeValue else null end) as float) as DrawdownTimeForWQDetentionVolume,
	max(case when cat.CustomAttributeTypeName = 'Dry Weather Flow Override' then cav.AttributeValue else null end) as DryWeatherFlowOverride,
	cast(max(case when cat.CustomAttributeTypeName = 'Effective Footprint' then cav.AttributeValue else null end) as float) as EffectiveFootprint,
	cast(max(case when cat.CustomAttributeTypeName = 'Effective Retention Depth' then cav.AttributeValue else null end) as float) as EffectiveRetentionDepth,
	cast(max(case when cat.CustomAttributeTypeName = 'Infiltration Discharge Rate' then cav.AttributeValue else null end) as float) as InfiltrationDischargeRate,
	cast(max(case when cat.CustomAttributeTypeName = 'Infiltration Surface Area' then cav.AttributeValue else null end) as float) as InfiltrationSurfaceArea,
	cast(max(case when cat.CustomAttributeTypeName = 'Media Bed Footprint' then cav.AttributeValue else null end) as float) as MediaBedFootprint,
	max(case when cat.CustomAttributeTypeName = 'Modeled Months Of Operation' then cav.AttributeValue else null end) as ModeledMonthsOfOperation,
	cast(max(case when cat.CustomAttributeTypeName = 'Permanent Pool Or Wetland Volume' then cav.AttributeValue else null end) as float) as PermanentPoolOrWetlandVolume,
	--MP 8/21/25 We haven't been allowing people to change this prior to the update back to CustomAttributes, but it's worth keeping to feed Nereid and I suppose display in our ModelingAttributes table
	--For now, always Online
	'Online' as RoutingConfiguration,
	cast(max(case when cat.CustomAttributeTypeName = 'Storage Volume Below Lowest Outlet Elevation' then cav.AttributeValue else null end) as float) as StorageVolumeBelowLowestOutletElevation,
	cast(max(case when cat.CustomAttributeTypeName = 'Summer Harvested Water Demand' then cav.AttributeValue else null end) as float) as SummerHarvestedWaterDemand,
	max(case when cat.CustomAttributeTypeName = 'Time Of Concentration' then cav.AttributeValue else null end) as TimeOfConcentration,
	cast(max(case when cat.CustomAttributeTypeName = 'Total Effective BMP Volume' then cav.AttributeValue else null end) as float) as TotalEffectiveBMPVolume,
	cast(max(case when cat.CustomAttributeTypeName = 'Total Effective Drywell BMP Volume' then cav.AttributeValue else null end) as float) as TotalEffectiveDrywellBMPVolume,
	cast(max(case when cat.CustomAttributeTypeName = 'Treatment Rate' then cav.AttributeValue else null end) as float) as TreatmentRate,
	max(case when cat.CustomAttributeTypeName = 'Underlying Hydrologic Soil Group' then cav.AttributeValue else null end) as UnderlyingHydrologicSoilGroup,
	cast(max(case when cat.CustomAttributeTypeName = 'Underlying Infiltration Rate' then cav.AttributeValue else null end) as float) as UnderlyingInfiltrationRate,
	cast(max(case when cat.CustomAttributeTypeName = 'Water Quality Detention Volume' then cav.AttributeValue else null end) as float) as WaterQualityDetentionVolume,
	cast(max(case when cat.CustomAttributeTypeName = 'Wetted Footprint' then cav.AttributeValue else null end) as float) as WettedFootprint,
	cast(max(case when cat.CustomAttributeTypeName = 'Winter Harvested Water Demand' then cav.AttributeValue else null end) as float) as WinterHarvestedWaterDemand
from dbo.CustomAttributeValue cav
join dbo.CustomAttribute ca on cav.CustomAttributeID = ca.CustomAttributeID
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.CustomAttributeTypePurpose catp on cat.CustomAttributeTypePurposeID = catp.CustomAttributeTypePurposeID
join dbo.TreatmentBMP tbmp on ca.TreatmentBMPID = tbmp.TreatmentBMPID
where catp.CustomAttributeTypePurposeID = 1
group by tbmp.TreatmentBMPID, UpstreamBMPID


GO