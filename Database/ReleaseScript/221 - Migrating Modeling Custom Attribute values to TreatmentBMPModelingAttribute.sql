insert into dbo.TreatmentBMPModelingAttribute(TreatmentBMPID)
select distinct ca.TreatmentBMPID
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where cat.CustomAttributeTypeName in
(
'Average Outlet Discharge Rate'
, 'Average Surface Outlet Discharge Rate'
, 'Design Capture Volume of Tributary Area'
, 'Design Infiltration Flowrate'
, 'Diameter'
, 'Diversion Capacity'
, 'Effective Footprint'
, 'Effective Retention Depth (surface and pores)'
, 'Estimated Diverted Flow'
, 'Infiltration Storage as Percent of Total Volume'
, 'Length'
, 'Media Filtration Rate'
, 'Months of Operation'
, 'Online/Offline Configuration'
, 'Permanent Pool Volume as Percent of Total'
, 'Summer Harvested Water Demand Rate'
, 'Total Depth of Well'
, 'Total Effective Storage Volume'
, 'Total Storage Volume'
, 'Treatment Flowrate'
, 'Underlying Infiltration Rate'
, 'Width'
, 'Winter Harvested Water Demand Rate'
)
and len(cav.AttributeValue) > 0
order by ca.TreatmentBMPID

-- Effective Footprint
update tma
set tma.InfiltrationSurfaceArea = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Effective Footprint'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Infiltration Basin',
'Infiltration Trench',
'Bioretention with no Underdrain',
'Permeable Pavement',
'Underground Infiltration'
)

update tma
set tma.MediaBedFootprint = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Effective Footprint'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Bioinfiltration (bioretention with raised underdrain)',
'Bioretention with Underdrain and Impervious Liner',
'Sand Filters'
)

update tma
set tma.EffectiveFootprint = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Effective Footprint'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Dry Extended Detention Basin',
'Flow Duration Control Basin',
'Flow Duration Control Tank'
)


--Online/Offline Configuration
update tma
set tma.RoutingConfigurationID = case when cav.AttributeValue = 'Online - receives all flow' then 1 when cav.AttributeValue = 'Offline - only design flow is diverted to system' then 2 else null end
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Online/Offline Configuration'
)
and tbmt.TreatmentBMPModelingTypeDisplayName not in
(
'Hydrodynamic Separator',
'Proprietary Biotreatment',
'Proprietary Treatment Control',
'Low Flow Diversions',
'Dry Weather Treatment Systems'
)


-- Total Effective Storage Volume
update tma
set tma.TotalEffectiveBMPVolume = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Total Effective Storage Volume'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Infiltration Basin'
, 'Infiltration Trench'
, 'Bioretention with no Underdrain'
, 'Permeable Pavement'
, 'Underground Infiltration'
, 'Bioinfiltration (bioretention with raised underdrain)'
, 'Dry Extended Detention Basin'
, 'Flow Duration Control Basin'
, 'Flow Duration Control Tank'
, 'Bioretention with Underdrain and Impervious Liner'
, 'Sand Filters'
)


-- Total Storage Volume
update tma
set tma.TotalEffectiveBMPVolume = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Total Storage Volume'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Cisterns for Harvest and Use'
)


-- Underlying Infiltration Rate
update tma
set tma.UnderlyingInfiltrationRate = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Underlying Infiltration Rate'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Infiltration Basin'
, 'Infiltration Trench'
, 'Bioretention with no Underdrain'
, 'Permeable Pavement'
, 'Underground Infiltration'
)

-- Total Depth of Well
update tma
set tma.TotalDepthOfWell = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Total Depth of Well'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Drywell'
)

-- Diameter
update tma
set tma.Diameter = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Diameter'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Drywell'
)



update tma
set tma.TotalEffectiveDrywellBMPVolume = tma.TotalDepthOfWell * (tma.Diameter / 2) * (tma.Diameter / 2) * 3.14159265359
from dbo.TreatmentBMPModelingAttribute tma
join dbo.TreatmentBMP tb on tma.TreatmentBMPID = tb.TreatmentBMPID
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
where tma.TotalDepthOfWell is not null and tma.Diameter is not null

alter table dbo.TreatmentBMPModelingAttribute drop column TotalDepthOfWell
alter table dbo.TreatmentBMPModelingAttribute drop column Diameter
GO

-- Design Infiltration Flowrate
update tma
set tma.InfiltrationDischargeRate = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Design Infiltration Flowrate'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Drywell'
)


-- Infiltration Storage as Percent of Total Volume
update tma
set tma.InfiltrationStorageAsPercentOfTotalVolume = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Infiltration Storage as Percent of Total Volume'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Bioinfiltration (bioretention with raised underdrain)'
, 'Dry Extended Detention Basin'
, 'Flow Duration Control Basin'
, 'Flow Duration Control Tank'
)

update tma
set tma.StorageVolumeBelowLowestOutletElevation = tma.TotalEffectiveBMPVolume * tma.InfiltrationStorageAsPercentOfTotalVolume
from dbo.TreatmentBMPModelingAttribute tma
join dbo.TreatmentBMP tb on tma.TreatmentBMPID = tb.TreatmentBMPID
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
where tma.InfiltrationStorageAsPercentOfTotalVolume is not null

alter table dbo.TreatmentBMPModelingAttribute drop column InfiltrationStorageAsPercentOfTotalVolume
GO

-- Media Filtration Rate
update tma
set tma.DesignMediaFiltrationRate = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Media Filtration Rate'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Bioinfiltration (bioretention with raised underdrain)'
, 'Bioretention with Underdrain and Impervious Liner'
, 'Sand Filters'
)


-- Average Outlet Discharge Rate
update tma
set tma.AverageOutletDischargeRate = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Average Outlet Discharge Rate'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Dry Extended Detention Basin'
, 'Flow Duration Control Basin'
, 'Flow Duration Control Tank'
)

update tma
set tma.TotalDrawdownTime = tma.TotalEffectiveBMPVolume / tma.AverageOutletDischargeRate
from dbo.TreatmentBMPModelingAttribute tma
join dbo.TreatmentBMP tb on tma.TreatmentBMPID = tb.TreatmentBMPID
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
where tma.AverageOutletDischargeRate > 0

alter table dbo.TreatmentBMPModelingAttribute drop column AverageOutletDischargeRate
GO


-- Treatment Flowrate
update tma
set tma.TreatmentRate = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Treatment Flowrate'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Hydrodynamic Separator'
, 'Proprietary Biotreatment'
, 'Proprietary Treatment Control'
, 'Vegetated Swale'
, 'Vegetated Filter Strip'
)


-- Diversion Capacity
update tma
set tma.DesignLowFlowDiversionCapacity = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Diversion Capacity'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Low Flow Diversions'
)

-- Months of Operation
insert into dbo.TreatmentBMPOperationMonth(TreatmentBMPID, OperationMonth)
select ca.TreatmentBMPID, 
datepart(mm, cav.AttributeValue + ' 1, 1977') as OperationMonth
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 and cav.AttributeValue != 'Year Round'
and cat.CustomAttributeTypeName in
(
'Months of Operation'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Low Flow Diversions'
, 'Dry Weather Treatment Systems'
)

insert into dbo.TreatmentBMPOperationMonth(TreatmentBMPID, OperationMonth)
select ca.TreatmentBMPID, om.OperationMonth
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
cross join
(
	select 1 as OperationMonth
	union all
	select 2 as OperationMonth
	union all
	select 3 as OperationMonth
	union all
	select 4 as OperationMonth
	union all
	select 5 as OperationMonth
	union all
	select 6 as OperationMonth
	union all
	select 7 as OperationMonth
	union all
	select 8 as OperationMonth
	union all
	select 9 as OperationMonth
	union all
	select 10 as OperationMonth
	union all
	select 11 as OperationMonth
	union all
	select 12 as OperationMonth
) om
where cav.AttributeValue = 'Year Round'
and cat.CustomAttributeTypeName in
(
'Months of Operation'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Low Flow Diversions'
, 'Dry Weather Treatment Systems'
)


-- underlying hydrological soil group to D as a default for those that do not have months of operation specified
insert into dbo.TreatmentBMPOperationMonth(TreatmentBMPID, OperationMonth)
select a.TreatmentBMPID, m.OperationMonth
from
(
	select tbma.TreatmentBMPID
	from dbo.TreatmentBMPModelingAttribute tbma
	join dbo.TreatmentBMP tb on tbma.TreatmentBMPID = tb.TreatmentBMPID
	join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
	where tbt.TreatmentBMPModelingTypeID in (14, 7)
	and tbma.TreatmentBMPID not in
	(
		select distinct TreatmentBMPID
		from dbo.TreatmentBMPOperationMonth
	)
) a
cross join
( 
select 4 as OperationMonth
union all 
select 5 as OperationMonth
union all 
select 6 as OperationMonth
union all 
select 7 as OperationMonth
union all 
select 8 as OperationMonth
union all 
select 9 as OperationMonth
union all 
select 10 as OperationMonth
) m


-- Estimated Diverted Flow
update tma
set tma.AverageTreatmentFlowrate = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Estimated Diverted Flow'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Dry Weather Treatment Systems'
)


-- Length
update tma
set tma.Length = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Length'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Vegetated Swale'
, 'Vegetated Filter Strip'
)

-- Width
update tma
set tma.Width = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Width'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Vegetated Swale'
, 'Vegetated Filter Strip'
)


update tma
set tma.WettedFootprint = tma.Length * tma.Width
from dbo.TreatmentBMPModelingAttribute tma
join dbo.TreatmentBMP tb on tma.TreatmentBMPID = tb.TreatmentBMPID
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
where tma.Length > 0 and tma.Width > 0

alter table dbo.TreatmentBMPModelingAttribute drop column [Length]
alter table dbo.TreatmentBMPModelingAttribute drop column [Width]
GO


-- Effective Retention Depth (surface and pores)
update tma
set tma.EffectiveRetentionDepth = case when replace(cav.AttributeValue, ',', '') is not null then replace(cav.AttributeValue, ',', '') / 12.0 else null end
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Effective Retention Depth (surface and pores)'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Vegetated Swale'
, 'Vegetated Filter Strip'
)


-- Winter Harvested Water Demand
update tma
set tma.WinterHarvestedWaterDemand = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Winter Harvested Water Demand Rate'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Cisterns for Harvest and Use'
, 'Wet Detention Basin'
, 'Constructed Wetland'
)

-- Summer Harvested Water Demand
update tma
set tma.SummerHarvestedWaterDemand = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Summer Harvested Water Demand Rate'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Cisterns for Harvest and Use'
, 'Wet Detention Basin'
, 'Constructed Wetland'
)

-- Total Storage Volume
update tma
set tma.TotalStorageVolume = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Total Storage Volume'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Wet Detention Basin'
, 'Constructed Wetland'
)

-- Permanent Pool Volume as Percent of Total
update tma
set tma.PermanentPoolVolumeAsPercentOfTotal = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Permanent Pool Volume as Percent of Total'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Wet Detention Basin'
, 'Constructed Wetland'
)

update tma
set tma.WaterQualityDetentionVolume = tma.TotalStorageVolume * (1 - tma.PermanentPoolVolumeAsPercentOfTotal)
from dbo.TreatmentBMPModelingAttribute tma
join dbo.TreatmentBMP tb on tma.TreatmentBMPID = tb.TreatmentBMPID
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
where tma.DesignCaptureVolumeOfTributaryArea > 0 and tma.AverageSurfaceOutletDischargeRate > 0

alter table dbo.TreatmentBMPModelingAttribute drop column TotalStorageVolume
alter table dbo.TreatmentBMPModelingAttribute drop column PermanentPoolVolumeAsPercentOfTotal
GO


-- Design Capture Volume of Tributary Area
update tma
set tma.DesignCaptureVolumeOfTributaryArea = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Design Capture Volume of Tributary Area'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Wet Detention Basin'
, 'Constructed Wetland'
)

-- Average Surface Outlet Discharge Rate
update tma
set tma.AverageSurfaceOutletDischargeRate = replace(cav.AttributeValue, ',', '')
from dbo.CustomAttribute ca
join dbo.CustomAttributeType cat on ca.CustomAttributeTypeID = cat.CustomAttributeTypeID
join dbo.TreatmentBMPType tbt on ca.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
join dbo.TreatmentBMPModelingAttribute tma on ca.TreatmentBMPID = tma.TreatmentBMPID
join dbo.CustomAttributeValue cav on ca.CustomAttributeID = cav.CustomAttributeID
where len(cav.AttributeValue) > 0 
and cat.CustomAttributeTypeName in
(
'Average Surface Outlet Discharge Rate'
)
and tbmt.TreatmentBMPModelingTypeDisplayName in
(
'Wet Detention Basin'
, 'Constructed Wetland'
)

update tma
set tma.DrawdownTimeForWQDetentionVolume = tma.DesignCaptureVolumeOfTributaryArea / tma.AverageSurfaceOutletDischargeRate
from dbo.TreatmentBMPModelingAttribute tma
join dbo.TreatmentBMP tb on tma.TreatmentBMPID = tb.TreatmentBMPID
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
join dbo.TreatmentBMPModelingType tbmt on tbt.TreatmentBMPModelingTypeID = tbmt.TreatmentBMPModelingTypeID
where tma.DesignCaptureVolumeOfTributaryArea > 0 and tma.AverageSurfaceOutletDischargeRate > 0

alter table dbo.TreatmentBMPModelingAttribute drop column DesignCaptureVolumeOfTributaryArea
alter table dbo.TreatmentBMPModelingAttribute drop column AverageSurfaceOutletDischargeRate


update dbo.CustomAttributeType
set CustomAttributeTypePurposeID = 2
where CustomAttributeTypePurposeID = 1

-- defaulting to Online and 5 minutes
update tbma
set RoutingConfigurationID = 1, TimeOfConcentrationID = 1
from dbo.TreatmentBMPModelingAttribute tbma
join dbo.TreatmentBMP tb on tbma.TreatmentBMPID = tb.TreatmentBMPID
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
where tbt.TreatmentBMPModelingTypeID in (
1
, 2
, 3
, 4
, 5
, 6
, 8
, 9
, 10
, 12
, 13
, 15
, 18
, 19
, 20
, 21
, 22
)

-- time of concentration default to 5 mins
update tbma
set TimeOfConcentrationID = 1
from dbo.TreatmentBMPModelingAttribute tbma
join dbo.TreatmentBMP tb on tbma.TreatmentBMPID = tb.TreatmentBMPID
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
where tbt.TreatmentBMPModelingTypeID in (
11
, 16
, 17
)

-- underlying hydrological soil group to D
update tbma
set UnderlyingHydrologicSoilGroupID = 4
from dbo.TreatmentBMPModelingAttribute tbma
join dbo.TreatmentBMP tb on tbma.TreatmentBMPID = tb.TreatmentBMPID
join dbo.TreatmentBMPType tbt on tb.TreatmentBMPTypeID = tbt.TreatmentBMPTypeID
where tbt.TreatmentBMPModelingTypeID in (
1
, 6
, 9
, 10
, 20
, 21
)
