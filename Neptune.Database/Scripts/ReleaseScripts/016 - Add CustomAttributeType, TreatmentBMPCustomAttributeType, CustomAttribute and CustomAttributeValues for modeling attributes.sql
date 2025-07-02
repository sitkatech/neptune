DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '016 - Add CustomAttributeType, TreatmentBMPCustomAttributeType, CustomAttribute and CustomAttributeValues for modeling attributes'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    declare @currentCustomAttributeTypeIDMax int = (select max(CustomAttributeTypeID) from dbo.CustomAttributeType)
	
insert into dbo.CustomAttributeType (CustomAttributeTypeName, CustomAttributeDataTypeID, MeasurementUnitTypeID, IsRequired, CustomAttributeTypeDescription, CustomAttributeTypePurposeID, CustomAttributeTypeOptionsSchema)
values ('Average Diverted Flowrate',3, 19, 1, 'Average actual diverted flowrate over the months of operation.', 1, null),
('Average Treatment Flowrate',3, 18, 1, 'Average actual treated flowrate over the months of operation.', 1, null),
('Design Dry Weather Treatment Capacity',3,18, 1, 'Flow treatment capacity of the BMP.', 1, null),
('Design Low Flow Diversion Capacity',3, 19, 1, 'The physical capacity of the low flow diversion or the maximum permitted flow.', 1, null),
('Design Media Filtration Rate',3, 10,0, 'Design filtration rate through the media bed. This may be controlled by the media permeability or by an outlet control on the underdrain system.', 1, null),
('Diversion Rate', 3, 18, 1, 'Flowrate diverted into the BMP.', 1, null),
('Drawdown Time For Detention Volume',3,23, 1, 'Time for the basin to fully draw own after the end of a storm if there is no further inflow.', 1, null),
('Drawdown Time For WQ Detention Volume',3,23, 1, 'Time for water quality surcharge volume to draw down after the end of a storm if there is no further inflow.', 1, null),
('Dry Weather Flow Override',5, null, 0, 'Indicates if the modeled values for Dry Weather Flow have been overridden', 1, '["No - As Modeled", "Yes - DWF Effectively Eliminated"]'),
('Effective Footprint',3,2, 1, 'Average actual diverted flowrate over the months of operation.', 1, null),
('Effective Retention Depth',3,8, 1, 'Depth of water stored in shallow surface depression or media/rock sump for infiltration to occur.', 1, null),
('Infiltration Discharge Rate',3,18, 1, 'Design or tested infiltration flowrate of the drywell. This is specified in cubic feet per section, rather than inches per hour.', 1, null),
('Infiltration Surface Area',3,2, 1, 'Surface area through which infiltration can occur in the system. If infiltration will occur into the sidewalls of a BMP, it is appropriate to include half of the sidewall area as as part of the infiltration surface area.', 1, null),
('Media Bed Footprint',3,2, 1, 'Surface area of the media bed of the BMP.', 1, null),
('Modeled Months Of Operation',5, null, 1, 'This defines the months that the facility is operational. For modeling purposes, this can be "Summer," "Winter," or "Both."', 1, '["Both", "Summer", "Winter"]'),
('Permanent Pool Or Wetland Volume',3, 15, 1, 'Constructed wetland or permanent pool volume below discharge elevation.', 1, null),
('Routing Configuration', 5, null, 0, 'This specifies whether the BMP receives all flow from the drainage area (online), or if there is a diversion structure that limits the flow into the BMP (offline).', 1,'["Offline", "Online"]'),
('Storage Volume Below Lowest Outlet Elevation',3, 15, 1, 'The volume of water stored below the lowest outlet (e.g., underdrain, orifice) of the system.', 1, null),
('Summer Harvested Water Demand',3, 19, 1, 'Average daily harvested water demand from May through October.', 1, null),
('Time Of Concentration',5,17, 0, 'The time required for the entire drainage to begin contributing runoff to the BMP. This value must be less than 60 minutes. See TGD guidance.', 1, '[ "5", "10", "15", "20", "30", "45", "60"]'),
('Total Effective BMP Volume',3, 15, 1, 'The volume of the BMP available for water quality purposes. This includes ponding volume and the available pore volume in media layers and/or in gravel storage layers. It does not include flow control volumes or other volume that is not designed for water quality purposes. ', 1, null),
('Total Effective Drywell BMP Volume',3, 15, 1, 'The volume of the BMP available for water quality purposes. This includes the volume in any pre-treatment chamber as well as the volume in the well itself.', 1, null),
('Treatment Rate',3,18, 1, 'The flowrate at which the BMP can provide treatment of runoff.', 1, null),
('Underlying Hydrologic Soil Group',5, null, 0, 'Choose the soil group that best represents the soils underlying the BMP. This is used to estimate a default infiltration rate (A = XX, B = XX, C=XX, D=XX)', 1,'["A", "B", "C", "D", "Liner"]'),
('Underlying Infiltration Rate',3, 10, 1, 'The underlying infiltration rate below the BMP. This refers to the underlying soil, not engineered media.', 1, null),
('Water Quality Detention Volume',3, 15, 1, 'Extended detention surcharge storage above permanent pool volume. Extended detention is > 24-hour drawdown time.', 1, null),
('Wetted Footprint',3,2, 1, 'Wetted footprint when BMP is half full.', 1, null),
('Winter Harvested Water Demand',3, 19, 1, 'Average daily harvested water demand from November through April. This should be averaged to account for any shutdowns during wet weather and reduction in demand during the winter season.', 1, null)

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
(34, @currentCustomAttributeTypeIDMax+9, 30),
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
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +1 and tbmpma.AverageDivertedFlowrate is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(AverageDivertedFlowrate, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +1 and tbmpma.AverageDivertedFlowrate is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +2 and tbmpma.AverageTreatmentFlowrate is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(AverageTreatmentFlowrate, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +2 and tbmpma.AverageTreatmentFlowrate is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +3 and tbmpma.DesignDryWeatherTreatmentCapacity is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(DesignDryWeatherTreatmentCapacity, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +3 and tbmpma.DesignDryWeatherTreatmentCapacity is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +4 and tbmpma.DesignLowFlowDiversionCapacity is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(DesignLowFlowDiversionCapacity, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +4 and tbmpma.DesignLowFlowDiversionCapacity is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +5 and tbmpma.DesignMediaFiltrationRate is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(DesignMediaFiltrationRate, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +5 and tbmpma.DesignMediaFiltrationRate is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +6 and tbmpma.DiversionRate is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(DiversionRate, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +6 and tbmpma.DiversionRate is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +7 and tbmpma.DrawdownTimeForDetentionVolume is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(DrawdownTimeForDetentionVolume, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +7 and tbmpma.DrawdownTimeForDetentionVolume is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +8 and tbmpma.DrawdownTimeForWQDetentionVolume is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(DrawdownTimeForWQDetentionVolume, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +8 and tbmpma.DrawdownTimeForWQDetentionVolume is not null		

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +9 and tbmpma.DryWeatherFlowOverrideID is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, DryWeatherFlowOverrideDisplayName
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
join dbo.DryWeatherFlowOverride dwfo on tbmpma.DryWeatherFlowOverrideID = dwfo.DryWeatherFlowOverrideID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +9 and tbmpma.DryWeatherFlowOverrideID is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +10 and tbmpma.EffectiveFootprint is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(EffectiveFootprint, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +10 and tbmpma.EffectiveFootprint is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +11 and tbmpma.EffectiveRetentionDepth is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(EffectiveRetentionDepth, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +11 and tbmpma.EffectiveRetentionDepth is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +12 and tbmpma.InfiltrationDischargeRate is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(InfiltrationDischargeRate, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +12 and tbmpma.InfiltrationDischargeRate is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +13 and tbmpma.InfiltrationSurfaceArea is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(InfiltrationSurfaceArea, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +13 and tbmpma.InfiltrationSurfaceArea is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +14 and tbmpma.MediaBedFootprint is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(MediaBedFootprint, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +14 and tbmpma.MediaBedFootprint is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +15 and tbmpma.MonthsOfOperationID is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, MonthsOfOperationDisplayName
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
join dbo.MonthsOfOperation mo on tbmpma.MonthsOfOperationID = mo.MonthsOfOperationID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +15 and tbmpma.MonthsOfOperationID is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +16 and tbmpma.PermanentPoolOrWetlandVolume is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(PermanentPoolOrWetlandVolume, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +16 and tbmpma.PermanentPoolOrWetlandVolume is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +17 and tbmpma.RoutingConfigurationID is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, RoutingConfigurationDisplayName
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
join dbo.RoutingConfiguration rc on tbmpma.RoutingConfigurationID = rc.RoutingConfigurationID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +17 and tbmpma.RoutingConfigurationID is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +18 and tbmpma.StorageVolumeBelowLowestOutletElevation is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(StorageVolumeBelowLowestOutletElevation, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +18 and tbmpma.StorageVolumeBelowLowestOutletElevation is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +19 and tbmpma.SummerHarvestedWaterDemand is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(SummerHarvestedWaterDemand, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +19 and tbmpma.SummerHarvestedWaterDemand is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +20 and tbmpma.TimeOfConcentrationID is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, TimeOfConcentrationDisplayName
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
join dbo.TimeOfConcentration toc on tbmpma.TimeOfConcentrationID = toc.TimeOfConcentrationID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +20 and tbmpma.TimeOfConcentrationID is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +21 and tbmpma.TotalEffectiveBMPVolume is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(TotalEffectiveBMPVolume, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +21 and tbmpma.TotalEffectiveBMPVolume is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +22 and tbmpma.TotalEffectiveDrywellBMPVolume is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(TotalEffectiveDrywellBMPVolume, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +22 and tbmpma.TotalEffectiveDrywellBMPVolume is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +23 and tbmpma.TreatmentRate is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(TreatmentRate, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +23 and tbmpma.TreatmentRate is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +24 and tbmpma.UnderlyingHydrologicSoilGroupID is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, UnderlyingHydrologicSoilGroupDisplayName
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
join dbo.UnderlyingHydrologicSoilGroup uhs on tbmpma.UnderlyingHydrologicSoilGroupID = uhs.UnderlyingHydrologicSoilGroupID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +24 and tbmpma.UnderlyingHydrologicSoilGroupID is not null
		
insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +25 and tbmpma.UnderlyingInfiltrationRate is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(UnderlyingInfiltrationRate, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +25 and tbmpma.UnderlyingInfiltrationRate is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +26 and tbmpma.WaterQualityDetentionVolume is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(WaterQualityDetentionVolume, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +26 and tbmpma.WaterQualityDetentionVolume is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +27 and tbmpma.WettedFootprint is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(WettedFootprint, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +27 and tbmpma.WettedFootprint is not null

insert into dbo.CustomAttribute
select tbmp.TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmpt.TreatmentBMPTypeID, cat.CustomAttributeTypeID
from dbo.TreatmentBMPModelingAttribute tbmpma
join dbo.TreatmentBMP tbmp on tbmpma.TreatmentBMPID = tbmp.TreatmentBMPID
join dbo.TreatmentBMPType tbmpt on tbmp.TreatmentBMPTypeID = tbmpt.TreatmentBMPTypeID
join dbo.TreatmentBMPTypeCustomAttributeType tbtcat on tbmpt.TreatmentBMPTypeID = tbtcat.TreatmentBMPTypeID
join dbo.CustomAttributeType cat on tbtcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
where cat.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +28 and tbmpma.WinterHarvestedWaterDemand is not null

insert into dbo.CustomAttributeValue
select CustomAttributeID, format(WinterHarvestedWaterDemand, '0.#####')
from dbo.CustomAttribute ca
join dbo.TreatmentBMPModelingAttribute tbmpma on ca.TreatmentBMPID = tbmpma.TreatmentBMPID
where ca.CustomAttributeTypeID = @currentCustomAttributeTypeIDMax +28 and tbmpma.WinterHarvestedWaterDemand is not null

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Mack Peters', @MigrationName, '016 - Add CustomAttributeType, TreatmentBMPCustomAttributeType, CustomAttribute and CustomAttributeValues for modeling attributes'
END