DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '016 - Add CustomAttributeType, TreatmentBMPCustomAttributeType, CustomAttribute and CustomAttributeValues for modeling attributes'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    declare @currentCustomAttributeTypeIDMax int = (select max(CustomAttributeTypeID) from dbo.CustomAttributeType)
	
insert into dbo.CustomAttributeType (CustomAttributeTypeName, CustomAttributeDataTypeID, MeasurementUnitTypeID, IsRequired, CustomAttributeTypeDescription, CustomAttributeTypePurposeID, CustomAttributeTypeOptionsSchema)
values ('Average Diverted Flowrate',3, 19, 0, 'Average actual diverted flowrate over the months of operation.', 1, null),
('Average Treatment Flowrate',3, 18, 0, 'Average actual treated flowrate over the months of operation.', 1, null),
('Design Dry Weather Treatment Capacity',3,18, 0, 'Flow treatment capacity of the BMP.', 1, null),
('Design Low Flow Diversion Capacity',3, 19, 0, 'The physical capacity of the low flow diversion or the maximum permitted flow.', 1, null),
('Design Media Filtration Rate',3, 10,0, 'Design filtration rate through the media bed. This may be controlled by the media permeability or by an outlet control on the underdrain system.', 1, null),
('Diversion Rate', 3, 18, 0, 'Flowrate diverted into the BMP.', 1, null),
('Drawdown Time For Detention Volume',3,23, 0, 'Time for the basin to fully draw own after the end of a storm if there is no further inflow.', 1, null),
('Drawdown Time For WQ Detention Volume',3,23, 0, 'Time for water quality surcharge volume to draw down after the end of a storm if there is no further inflow.', 1, null),
('Dry Weather Flow Override',5, null, 0, 'Indicates if the modeled values for Dry Weather Flow have been overridden', 1, '["No - As Modeled", "Yes - DWF Effectively Eliminated"]'),
('Effective Footprint',3,2, 0, 'Average actual diverted flowrate over the months of operation.', 1, null),
('Effective Retention Depth',3,8, 0, 'Depth of water stored in shallow surface depression or media/rock sump for infiltration to occur.', 1, null),
('Infiltration Discharge Rate',3,18, 0, 'Design or tested infiltration flowrate of the drywell. This is specified in cubic feet per section, rather than inches per hour.', 1, null),
('Infiltration Surface Area',3,2, 0, 'Surface area through which infiltration can occur in the system. If infiltration will occur into the sidewalls of a BMP, it is appropriate to include half of the sidewall area as as part of the infiltration surface area.', 1, null),
('Media Bed Footprint',3,2, 0, 'Surface area of the media bed of the BMP.', 1, null),
('Modeled Months Of Operation',5, null, 0, 'This defines the months that the facility is operational. For modeling purposes, this can be "Summer," "Winter," or "Both."', 1, '["Both", "Summer", "Winter"]'),
('Permanent Pool Or Wetland Volume',3, 15, 0, 'Constructed wetland or permanent pool volume below discharge elevation.', 1, null),
('Routing Configuration', 5, null, 0, 'This specifies whether the BMP receives all flow from the drainage area (online), or if there is a diversion structure that limits the flow into the BMP (offline).', 1,'["Offline", "Online"]'),
('Storage Volume Below Lowest Outlet Elevation',3, 15, 0, 'The volume of water stored below the lowest outlet (e.g., underdrain, orifice) of the system.', 1, null),
('Summer Harvested Water Demand',3, 19, 0, 'Average daily harvested water demand from May through October.', 1, null),
('Time Of Concentration',5,17, 0, 'The time required for the entire drainage to begin contributing runoff to the BMP. This value must be less than 60 minutes. See TGD guidance.', 1, '[ "5", "10", "15", "20", "30", "45", "60"]'),
('Total Effective BMP Volume',3, 15, 0, 'The volume of the BMP available for water quality purposes. This includes ponding volume and the available pore volume in media layers and/or in gravel storage layers. It does not include flow control volumes or other volume that is not designed for water quality purposes. ', 1, null),
('Total Effective Drywell BMP Volume',3, 15, 0, 'The volume of the BMP available for water quality purposes. This includes the volume in any pre-treatment chamber as well as the volume in the well itself.', 1, null),
('Treatment Rate',3,18, 0, 'The flowrate at which the BMP can provide treatment of runoff.', 1, null),
('Underlying Hydrologic Soil Group',5, null, 0, 'Choose the soil group that best represents the soils underlying the BMP. This is used to estimate a default infiltration rate (A = XX, B = XX, C=XX, D=XX)', 1,'["A", "B", "C", "D", "Liner"]'),
('Underlying Infiltration Rate',3, 10, 0, 'The underlying infiltration rate below the BMP. This refers to the underlying soil, not engineered media.', 1, null),
('Water Quality Detention Volume',3, 15, 0, 'Extended detention surcharge storage above permanent pool volume. Extended detention is > 24-hour drawdown time.', 1, null),
('Wetted Footprint',3,2, 0, 'Wetted footprint when BMP is half full.', 1, null),
('Winter Harvested Water Demand',3, 19, 0, 'Average daily harvested water demand from November through April. This should be averaged to account for any shutdowns during wet weather and reduction in demand during the winter season.', 1, null)

--select SCOPE_IDENTITY()

insert into dbo.TreatmentBMPTypeCustomAttributeType (TreatmentBMPTypeID, CustomAttributeTypeID, SortOrder)
values 
(21, @currentCustomAttributeTypeIDMax+21, 0),
(21, @currentCustomAttributeTypeIDMax+18, 10),
(21, @currentCustomAttributeTypeIDMax+14, 20),
(21, @currentCustomAttributeTypeIDMax+5, 30),
(21, @currentCustomAttributeTypeIDMax+24, 40),
(21, @currentCustomAttributeTypeIDMax+9, 50),
--
(16, @currentCustomAttributeTypeIDMax+21, 0),
(16, @currentCustomAttributeTypeIDMax+13, 10),
(16, @currentCustomAttributeTypeIDMax+25, 20),
(16, @currentCustomAttributeTypeIDMax+9, 30),
(14, @currentCustomAttributeTypeIDMax+21, 0),
(14, @currentCustomAttributeTypeIDMax+13, 10),
(14, @currentCustomAttributeTypeIDMax+25, 20),
(14, @currentCustomAttributeTypeIDMax+9, 30),
(15, @currentCustomAttributeTypeIDMax+21, 0),
(15, @currentCustomAttributeTypeIDMax+13, 10),
(15, @currentCustomAttributeTypeIDMax+25, 20),
(15, @currentCustomAttributeTypeIDMax+9, 30),
(18, @currentCustomAttributeTypeIDMax+21, 0),
(18, @currentCustomAttributeTypeIDMax+13, 10),
(18, @currentCustomAttributeTypeIDMax+25, 20),
(18, @currentCustomAttributeTypeIDMax+9, 30),
(19, @currentCustomAttributeTypeIDMax+21, 0),
(19, @currentCustomAttributeTypeIDMax+13, 10),
(19, @currentCustomAttributeTypeIDMax+25, 20),
(19, @currentCustomAttributeTypeIDMax+9, 30),
--
(25, @currentCustomAttributeTypeIDMax+21, 0),
(25, @currentCustomAttributeTypeIDMax+14, 10),
(25, @currentCustomAttributeTypeIDMax+5, 20),
(25, @currentCustomAttributeTypeIDMax+9, 30),
(34, @currentCustomAttributeTypeIDMax+21, 0),
(34, @currentCustomAttributeTypeIDMax+14, 10),
(34, @currentCustomAttributeTypeIDMax+5, 20),
(35, @currentCustomAttributeTypeIDMax+9, 30),
--
(20, @currentCustomAttributeTypeIDMax+21, 0),
(20, @currentCustomAttributeTypeIDMax+28, 10),
(20, @currentCustomAttributeTypeIDMax+19, 20),
(20, @currentCustomAttributeTypeIDMax+9, 30),
--
(28, @currentCustomAttributeTypeIDMax+16, 0),
(28, @currentCustomAttributeTypeIDMax+26, 10),
(28, @currentCustomAttributeTypeIDMax+9, 20),
(27, @currentCustomAttributeTypeIDMax+16, 0),
(27, @currentCustomAttributeTypeIDMax+26, 10),
(27, @currentCustomAttributeTypeIDMax+9, 20),
(43, @currentCustomAttributeTypeIDMax+16, 0),
(43, @currentCustomAttributeTypeIDMax+26, 10),
(43, @currentCustomAttributeTypeIDMax+9, 20),
--
(24, @currentCustomAttributeTypeIDMax+21, 0),
(24, @currentCustomAttributeTypeIDMax+18, 10),
(24, @currentCustomAttributeTypeIDMax+10, 20),
(24, @currentCustomAttributeTypeIDMax+8, 30),
(24, @currentCustomAttributeTypeIDMax+24, 40),
(24, @currentCustomAttributeTypeIDMax+9, 50),
(29, @currentCustomAttributeTypeIDMax+21, 0),
(29, @currentCustomAttributeTypeIDMax+18, 10),
(29, @currentCustomAttributeTypeIDMax+10, 20),
(29, @currentCustomAttributeTypeIDMax+8, 30),
(29, @currentCustomAttributeTypeIDMax+24, 40),
(29, @currentCustomAttributeTypeIDMax+9, 50),
(30, @currentCustomAttributeTypeIDMax+21, 0),
(30, @currentCustomAttributeTypeIDMax+18, 10),
(30, @currentCustomAttributeTypeIDMax+10, 20),
(30, @currentCustomAttributeTypeIDMax+8, 30),
(30, @currentCustomAttributeTypeIDMax+24, 40),
(30, @currentCustomAttributeTypeIDMax+9, 50),
--
(39, @currentCustomAttributeTypeIDMax+3, 0),
(39, @currentCustomAttributeTypeIDMax+2, 10),
(39, @currentCustomAttributeTypeIDMax+15, 20),
(39, @currentCustomAttributeTypeIDMax+9, 30),
--
(17, @currentCustomAttributeTypeIDMax+22, 0),
(17, @currentCustomAttributeTypeIDMax+12, 10),
(17, @currentCustomAttributeTypeIDMax+20, 20),
(17, @currentCustomAttributeTypeIDMax+9, 30),
--
(36, @currentCustomAttributeTypeIDMax+23, 0),
(36, @currentCustomAttributeTypeIDMax+20, 10),
(36, @currentCustomAttributeTypeIDMax+9, 20),
(26, @currentCustomAttributeTypeIDMax+23, 0),
(26, @currentCustomAttributeTypeIDMax+20, 10),
(26, @currentCustomAttributeTypeIDMax+9, 20),
(37, @currentCustomAttributeTypeIDMax+23, 0),
(37, @currentCustomAttributeTypeIDMax+20, 10),
(37, @currentCustomAttributeTypeIDMax+9, 20),
--
(38, @currentCustomAttributeTypeIDMax+4, 0),
(38, @currentCustomAttributeTypeIDMax+1, 10),
(38, @currentCustomAttributeTypeIDMax+15, 20),
(38, @currentCustomAttributeTypeIDMax+9, 30),
--
(40, @currentCustomAttributeTypeIDMax+20, 0),
(40, @currentCustomAttributeTypeIDMax+23, 10),
(40, @currentCustomAttributeTypeIDMax+27, 20),
(40, @currentCustomAttributeTypeIDMax+11, 30),
(40, @currentCustomAttributeTypeIDMax+24, 40),
(40, @currentCustomAttributeTypeIDMax+9, 50),
(22, @currentCustomAttributeTypeIDMax+20, 0),
(22, @currentCustomAttributeTypeIDMax+23, 10),
(22, @currentCustomAttributeTypeIDMax+27, 20),
(22, @currentCustomAttributeTypeIDMax+11, 30),
(22, @currentCustomAttributeTypeIDMax+24, 40),
(22, @currentCustomAttributeTypeIDMax+9, 50)

declare @currentCustomAttributeIDMax int = (select max(CustomAttributeID) from dbo.CustomAttribute)

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbma
left join dbo.TreatmentBMP tbmp on tbma.TreatmentBMPID = tbmp.TreatmentBMPID
left join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
left join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
left join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID > @currentCustomAttributeTypeIDMax

insert into dbo.CustomAttributeValue
select CustomAttributeID,
	case
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +1 then format(AverageDivertedFlowrate, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +2 then format(AverageTreatmentFlowrate, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +3 then format(DesignDryWeatherTreatmentCapacity, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +4 then format(DesignLowFlowDiversionCapacity, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +5 then format(DesignMediaFiltrationRate, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +6 then format(DiversionRate, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +7 then format(DrawdownTimeForDetentionVolume, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +8 then format(DrawdownTimeForWQDetentionVolume, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +9 then 
			case 
				when DryWeatherFlowOverrideID = 1 then 'No - As Modeled'
				when DryWeatherFlowOverrideID = 2 then 'Yes - DWF Effectively Eliminated'
				else null
			end
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +10 then format(EffectiveFootprint, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +11 then format(EffectiveRetentionDepth, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +12 then format(InfiltrationDischargeRate, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +13 then format(InfiltrationSurfaceArea, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +14 then format(MediaBedFootprint, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +15 then 
			case 
				when MonthsOfOperationID = 1 then 'Summer'
				when MonthsOfOperationID = 2 then 'Winter'
				when MonthsOfOperationID = 3 then 'Both'
				else null
			end
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +16 then format(PermanentPoolOrWetlandVolume, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +17 then
			case 
				when RoutingConfigurationID = 1 then 'Online'
				when RoutingConfigurationID = 2 then 'Offline'
				else null
			end
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +18 then format(StorageVolumeBelowLowestOutletElevation, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +19 then format(SummerHarvestedWaterDemand, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +20 then 
			case
				when TimeOfConcentrationID = 1 then '5'
				when TimeOfConcentrationID = 2 then '10'
				when TimeOfConcentrationID = 3 then '15'
				when TimeOfConcentrationID = 4 then '20'
				when TimeOfConcentrationID = 5 then '30'
				when TimeOfConcentrationID = 6 then '45'
				when TimeOfConcentrationID = 7 then '60'
				else null
			end
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +21 then format(TotalEffectiveBMPVolume, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +22 then format(TotalEffectiveDrywellBMPVolume, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +23 then format(TreatmentRate, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +24 then 
			case
				when UnderlyingHydrologicSoilGroupID = 1 then 'A'
				when UnderlyingHydrologicSoilGroupID = 2 then 'B'
				when UnderlyingHydrologicSoilGroupID = 3 then 'C'
				when UnderlyingHydrologicSoilGroupID = 4 then 'D'
				when UnderlyingHydrologicSoilGroupID = 5 then 'Liner'
				else null
			end
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +25 then format(UnderlyingInfiltrationRate, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +26 then format(WaterQualityDetentionVolume, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +27 then format(WettedFootprint, '0.#####')
		when CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +28 then format(WinterHarvestedWaterDemand, '0.#####')
		else null
	end as AttributeValue
from dbo.CustomAttribute ca
left join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where CustomAttributeID > @currentCustomAttributeTypeIDMax

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Mack Peters', @MigrationName, '016 - Add CustomAttributeType, TreatmentBMPCustomAttributeType, CustomAttribute and CustomAttributeValues for modeling attributes'
END