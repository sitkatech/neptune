create table dbo.TreatmentBMPModelingType
(
	TreatmentBMPModelingTypeID int not null constraint PK_TreatmentBMPModelingType_TreatmentBMPModelingTypeID primary key,
	TreatmentBMPModelingTypeName varchar(100) not null  constraint PK_TreatmentBMPModelingType_TreatmentBMPModelingTypeName unique,
	TreatmentBMPModelingTypeDisplayName varchar(100) not null  constraint PK_TreatmentBMPModelingType_TreatmentBMPModelingTypeDisplayName unique
)

create table dbo.RoutingConfiguration
(
	RoutingConfigurationID int not null constraint PK_RoutingConfiguration_RoutingConfigurationID primary key,
	RoutingConfigurationName varchar(100) not null constraint AK_RoutingConfiguration_RoutingConfigurationName unique,
	RoutingConfigurationDisplayName varchar(100) not null constraint AK_RoutingConfiguration_RoutingConfigurationDisplayName unique
)


create table dbo.TimeOfConcentration
(
	TimeOfConcentrationID int not null constraint PK_TimeOfConcentration_TimeOfConcentrationID primary key,
	TimeOfConcentrationName varchar(100) not null constraint AK_TimeOfConcentration_TimeOfConcentrationName unique,
	TimeOfConcentrationDisplayName varchar(100) not null constraint AK_TimeOfConcentration_TimeOfConcentrationDisplayName unique
)

create table dbo.UnderlyingHydrologicSoilGroup
(
	UnderlyingHydrologicSoilGroupID int not null constraint PK_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupID primary key,
	UnderlyingHydrologicSoilGroupName varchar(100) not null constraint AK_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupName unique,
	UnderlyingHydrologicSoilGroupDisplayName varchar(100) not null constraint AK_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupDisplayName unique
)

create table dbo.TreatmentBMPOperationMonth
(
	TreatmentBMPOperationMonthID int not null identity(1,1) constraint PK_TreatmentBMPOperationMonth_TreatmentBMPOperationMonthID primary key,
	TreatmentBMPID int not null constraint FK_TreatmentBMPOperationMonth_TreatmentBMP_TreatmentBMPID foreign key references dbo.TreatmentBMP(TreatmentBMPID),
	OperationMonth int not null constraint CK_TreatmentBMPOperationMonth_Between1and12 check (OperationMonth between 1 and 12)
)

create table dbo.TreatmentBMPModelingAttribute
(
	TreatmentBMPModelingAttributeID int not null identity(1,1) constraint PK_TreatmentBMPModelingAttribute_TreatmentBMPModelingAttributeID primary key,
	TreatmentBMPID int not null constraint AK_TreatmentBMPModelingAttribute_TreatmentBMPID unique constraint FK_TreatmentBMPModelingAttribute_TreatmentBMP_TreatmentBMPID foreign key references dbo.TreatmentBMP(TreatmentBMPID),
	UpstreamTreatmentBMPID int null constraint FK_TreatmentBMPModelingAttribute_TreatmentBMP_UpstreamTreatmentBMPID_TreatmentBMPID foreign key references dbo.TreatmentBMP(TreatmentBMPID),
	AverageDivertedFlowrate float null,
	AverageTreatmentFlowrate float null,
	DesignDryWeatherTreatmentCapacity float null,
	DesignLowFlowDiversionCapacity float null,
	DesignMediaFiltrationRate float null,
	DesignResidenceTimeforPermanentPool float null,
	DiversionRate float null,
	DrawdownTimeforWQDetentionVolume float null,
	EffectiveFootprint float null,
	EffectiveRetentionDepth float null,
	InfiltrationDischargeRate float null,
	InfiltrationSurfaceArea float null,
	MediaBedFootprint float null,
	PermanentPoolorWetlandVolume float null,
	RoutingConfigurationID int null constraint FK_TreatmentBMPModelingAttribute_RoutingConfiguration_RoutingConfigurationID foreign key references dbo.RoutingConfiguration(RoutingConfigurationID),
	StorageVolumeBelowLowestOutletElevation float null,
	SummerHarvestedWaterDemand float null,
	TimeOfConcentrationID int null constraint FK_TreatmentBMPModelingAttribute_TimeOfConcentration_TimeOfConcentrationID foreign key references dbo.TimeOfConcentration(TimeOfConcentrationID),
	TotalDrawdownTime float null,
	TotalEffectiveBMPVolume float null,
	TotalEffectiveDrywellBMPVolume float null,
	TreatmentRate float null,
	UnderlyingHydrologicSoilGroupID int null constraint FK_TreatmentBMPModelingAttribute_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupID foreign key references dbo.UnderlyingHydrologicSoilGroup(UnderlyingHydrologicSoilGroupID),
	UnderlyingInfiltrationRate float null,
	WaterQualityDetentionVolume float null,
	WettedFootprint float null,
	WinterHarvestedWaterDemand float null,
	-- these we add temporarily and then we remove after applying a formula
	AverageOutletDischargeRate float null,
	AverageSurfaceOutletDischargeRate float null,
	InfiltrationStorageAsPercentOfTotalVolume float null,
	TotalDepthOfWell float null,
	Diameter float null,
	[Length] float null,
	[Width] float null,
	TotalStorageVolume float null,
	PermanentPoolVolumeAsPercentOfTotal float null,
	DesignCaptureVolumeOfTributaryArea float null
)


alter table dbo.TreatmentBMPType add TreatmentBMPModelingTypeID int null constraint FK_TreatmentBMPType_TreatmentBMPModelingType_TreatmentBMPModelingTypeID foreign key references dbo.TreatmentBMPModelingType(TreatmentBMPModelingTypeID)

GO

insert into dbo.RoutingConfiguration(RoutingConfigurationID, RoutingConfigurationName, RoutingConfigurationDisplayName)
values
(1, 'Online', 'Online'),
(2, 'Offline', 'Offline')

insert into dbo.TimeOfConcentration(TimeOfConcentrationID, TimeOfConcentrationName, TimeOfConcentrationDisplayName)
values
(1, 'FiveMinutes', '5'),
(2, 'TenMinutes', '10'),
(3, 'FifteenMinutes', '15'),
(4, 'TwentyMinutes', '20'),
(5, 'ThirtyMinutes', '30'),
(6, 'FortyFiveMinutes', '45'),
(7, 'SixtyMinutes', '60')

insert into dbo.UnderlyingHydrologicSoilGroup(UnderlyingHydrologicSoilGroupID, UnderlyingHydrologicSoilGroupName, UnderlyingHydrologicSoilGroupDisplayName)
values
(1, 'A', 'A'),
(2, 'B', 'B'),
(3, 'C', 'C'),
(4, 'D', 'D')


insert into dbo.TreatmentBMPModelingType(TreatmentBMPModelingTypeID, TreatmentBMPModelingTypeName, TreatmentBMPModelingTypeDisplayName)
values
(1, 'Bioinfiltration (bioretention with raised underdrain)', 'Bioinfiltration (bioretention with raised underdrain)'),
(2, 'Bioretention with no Underdrain', 'Bioretention with no Underdrain'),
(3, 'Bioretention with Underdrain and Impervious Liner', 'Bioretention with Underdrain and Impervious Liner'),
(4, 'Cisterns for Harvest and Use', 'Cisterns for Harvest and Use'),
(5, 'Constructed Wetland', 'Constructed Wetland'),
(6, 'Dry Extended Detention Basin', 'Dry Extended Detention Basin'),
(7, 'Dry Weather Treatment Systems', 'Dry Weather Treatment Systems'),
(8, 'Drywell', 'Drywell'),
(9, 'Flow Duration Control Basin', 'Flow Duration Control Basin'),
(10, 'Flow Duration Control Tank', 'Flow Duration Control Tank'),
(11, 'Hydrodynamic Separator', 'Hydrodynamic Separator'),
(12, 'Infiltration Basin', 'Infiltration Basin'),
(13, 'Infiltration Trench', 'Infiltration Trench'),
(14, 'Low Flow Diversions', 'Low Flow Diversions'),
(15, 'Permeable Pavement', 'Permeable Pavement'),
(16, 'Proprietary Biotreatment', 'Proprietary Biotreatment'),
(17, 'Proprietary Treatment Control', 'Proprietary Treatment Control'),
(18, 'Sand Filters', 'Sand Filters'),
(19, 'Underground Infiltration', 'Underground Infiltration'),
(20, 'Vegetated Filter Strip', 'Vegetated Filter Strip'),
(21, 'Vegetated Swale', 'Vegetated Swale'),
(22, 'Wet Detention Basin', 'Wet Detention Basin')



update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 1
where TreatmentBMPTypeID in (21)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 2
where TreatmentBMPTypeID in (16)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 3
where TreatmentBMPTypeID in (25)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 4
where TreatmentBMPTypeID in (20)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 5
where TreatmentBMPTypeID in (28)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 6
where TreatmentBMPTypeID in (24)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 7
where TreatmentBMPTypeID in (39)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 8
where TreatmentBMPTypeID in (17)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 9
where TreatmentBMPTypeID in (29)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 10
where TreatmentBMPTypeID in (30)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 11
where TreatmentBMPTypeID in (36)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 12
where TreatmentBMPTypeID in (14)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 13
where TreatmentBMPTypeID in (15)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 14
where TreatmentBMPTypeID in (38)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 15
where TreatmentBMPTypeID in (18)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 16
where TreatmentBMPTypeID in (26)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 17
where TreatmentBMPTypeID in (37)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 18
where TreatmentBMPTypeID in (34)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 19
where TreatmentBMPTypeID in (19)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 20
where TreatmentBMPTypeID in (40)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 21
where TreatmentBMPTypeID in (22)

update dbo.TreatmentBMPType
set TreatmentBMPModelingTypeID = 22
where TreatmentBMPTypeID in (27)

