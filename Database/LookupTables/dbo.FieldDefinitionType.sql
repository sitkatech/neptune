delete from dbo.FieldDefinitionType
go

INSERT dbo.FieldDefinitionType (FieldDefinitionTypeID, FieldDefinitionTypeName, FieldDefinitionTypeDisplayName) 
VALUES 
(1, N'IsPrimaryContactOrganization', N'Is Primary Contact Organization'),
(2, N'Organization', N'Organization'),
(3, N'Password', N'Password'),
(4, N'MeasurementUnit', N'Measurement Unit'),
(5, N'PhotoCaption', N'Photo Caption'),
(6, N'PhotoCredit', N'Photo Credit'),
(7, N'PhotoTiming', N'Photo Timing'),
(8, N'PrimaryContact', N'Primary Contact'),
(9, N'OrganizationType', N'Organization Type'),
(10, N'Username', N'User name'),
(11, N'ExternalLinks', N'External Links'),
(12, N'RoleName', N'Role Name'),
(13, N'Chart Last Updated Date', N'Chart Last Updated Date'),
(14, N'TreatmentBMPType', N'Treatment BMP Type'),
(16, N'ConveyanceFunctionsAsIntended', N'Conveyance Functions as Intended'),
(17, N'AssessmentScoreWeight', N'Assessment Score Weight'),
(18, N'ObservationScore', N'Observation Score'),
(19, N'AlternativeScore', N'Alternative Score'),
(20, N'AssessmentForInternalUseOnly', N'Assessment for Internal Use Only'),
(21, N'TreatmentBMPDesignDepth', N'Treatment BMP Design Depth'),
(22, N'ReceivesSystemCommunications', N'Receives System Communications'),
(23, N'Jurisdiction', N'Jurisdiction'),
(24, N'Delineation', N'Delineation'),
(25, N'TreatmentBMP', N'Treatment BMP'),
(26, N'TreatmentBMPAssessmentObservationType', N'Observation Name'),
(27, N'ObservationCollectionMethod', N'Collection Method'),
(28, N'ObservationThresholdType', N'Threshold Type'),
(29, N'ObservationTargetType', N'Target Type'),
(30, N'MeasurementUnitLabel', N'Measurement Unit Label'),
(31, N'PropertiesToObserve', N'Properties To Observe'),
(32, N'MinimumNumberOfObservations', N'Minimum Number of Observations'),
(33, N'MaximumNumberOfObservations', N'Maximum Number of Observations'),
(34, N'MinimumValueOfEachObservation', N'Minimum Value of Each Observation'),
(35, N'MaximumValueOfEachObservation', N'Maximum Value of Each Observation'),
(36, N'DefaultThresholdValue', N'Default Threshold Value'),
(37, N'DefaultBenchmarkValue', N'Default Benchmark Value'),
(38, N'AssessmentFailsIfObservationFails', N'Assessment Fails if Observation Fails'),
(39, N'CustomAttributeType', N'Attribute Name'),
(40, N'CustomAttributeDataType', N'Data Type'),
(41, N'MaintenanceRecordType', N'Maintenance Type'),
(42, N'MaintenanceRecord', N'Maintenance Record'),
(43, N'AttributeTypePurpose', N'Purpose'),
(44, N'FundingSource', N'Funding Source'),
(45, N'IsPostMaintenanceAssessment', N'Post Maintenance Assessment?'),
(46, N'FundingEvent', N'Funding Event'),
(47, N'FieldVisit', N'Field Visit'),
(48, N'FieldVisitStatus', N'Field Visit Status'),
(49, N'WaterQualityManagementPlan', N'Water Quality Management Plan'),
(50, N'Parcel', N'Parcel'),
(51, N'RequiredLifespanOfInstallation', N'Required Lifespan of Installation'),
(52, N'RequiredFieldVisitsPerYear', N'Required Field Visits Per Year'),
(53, N'RequiredPostStormFieldVisitsPerYear', N'Required Post-Storm Field Visits Per Year'),
(54, N'WaterQualityManagementPlanDocumentType', N'WQMP Document Type'),
(55, N'HasAllRequiredDocuments', N'Has All Required Documents?'),
(56, N'DateOfLastInventoryChange', N'Date of Last Inventory Change'),
(57, N'TrashCaptureStatus', N'Trash Capture Status'),
(58, N'OnlandVisualTrashAssessment', N'On-land Visual Trash Assessment'),
(59, N'OnlandVisualTrashAssessmentNotes', N'Comments and Additional Information'),
(60, N'DelineationType', N'Delineation Type'),
(61, N'BaselineScore', N'Baseline Score'),
(62, N'SizingBasis', N'Sizing Basis'),
(63, N'ProgressScore', N'Progress Score'),
(64, N'AssessmentScore', N'Assessment Score'),
(65, N'ViaFullCapture', N'Via Full Capture'),
(66, N'ViaPartialCapture', N'Via Partial Capture'),
(67, N'ViaOVTAScore', N'Via OVTA Score'),
(68, N'TotalAchieved', N'Total Achieved'),
(69, N'TargetLoadReduction', N'Target Load Reduction'),
(70, N'LoadingRate', N'Loading Rate'),
(71, N'LandUse', N'Land Use'),
(72, N'Area', N'Area'),
(73, N'ImperviousArea', N'Impervious Area'),
(74, N'GrossArea', N'Gross Area'),
(75, N'LandUseStatistics', N'Land Use Statistics'),
(76, N'RegionalSubbasin', N'Regional Subbasin'),
(77, 'AverageDivertedFlowrate', 'Average Diverted Flowrate'),
(78, 'AverageTreatmentFlowrate', 'Average Treatment Flowrate'),
(79, 'DesignDryWeatherTreatmentCapacity', 'Design Dry Weather Treatment Capacity'),
(80, 'DesignLowFlowDiversionCapacity', 'Design Low Flow Diversion Capacity'),
(81, 'DesignMediaFiltrationRate', 'Design Media Filtration Rate'),
(82, 'DesignResidenceTimeForPermanentPool', 'Design Residence Time for Permanent Pool'),
(83, 'DiversionRate', 'Diversion Rate'),
(84, 'DrawdownTimeForWQDetentionVolume', 'Drawdown Time for WQ Detention Volume'),
(85, 'EffectiveFootprint', 'Effective Footprint'),
(86, 'EffectiveRetentionDepth', 'Effective Retention Depth'),
(87, 'InfiltrationDischargeRate', 'Infiltration Discharge Rate'),
(88, 'InfiltrationSurfaceArea', 'Infiltration Surface Area'),
(89, 'MediaBedFootprint', 'Media Bed Footprint'),
(90, 'MonthsOperational', 'Months Operational'),
(91, 'PermanentPoolOrWetlandVolume', 'Permanent Pool or Wetland Volume'),
(92, 'RoutingConfiguration', 'Routing Configuration'),
(93, 'StorageVolumeBelowLowestOutletElevation', 'Storage Volume Below Lowest Outlet Elevation'),
(94, 'SummerHarvestedWaterDemand', 'Summer Harvested Water Demand'),
(95, 'TimeOfConcentration', 'Time of Concentration'),
(96, 'DrawdownTimeForDetentionVolume', 'Drawdown Time For Detention Volume'),
(97, 'TotalEffectiveBMPVolume', 'Total Effective BMP Volume'),
(98, 'TotalEffectiveDrywellBMPVolume', 'Total Effective Drywell BMP Volume'),
(99, 'TreatmentRate', 'Treatment Rate'),
(100, 'UnderlyingHydrologicSoilGroupHSG', 'Underlying Hydrologic Soil Group (HSG)'),
(101, 'UnderlyingInfiltrationRate', 'Underlying Infiltration Rate'),
(102, 'UpstreamBMP', 'Upstream BMP'),
(103, 'WaterQualityDetentionVolume', 'Water Quality Detention Volume'),
(104, 'WettedFootprint', 'Wetted Footprint'),
(105, 'WinterHarvestedWaterDemand', 'Winter Harvested Water Demand'),
(106, 'PercentOfSiteTreated', '% of Site Treated'),
(107, 'PercentCaptured', 'Wet Weather % Captured'),
(108, 'PercentRetained', 'Wet Weather % Retained'),
(109, 'AreaWithinWQMP', 'Area within WQMP'),
(110, 'Watershed', 'Watershed'),
(111, 'DesignStormwaterDepth', 'Design Stormwater Depth'),
(112, 'FullyParameterized', 'Fully Parameterized?'),
(113, 'HydromodificationApplies', 'Hydromodification Controls Apply'),
(114, 'DelineationStatus', N'Delineation Status'),
(115, 'DryWeatherFlowOverride', 'Dry Weather Flow Override?'),
(116, 'ModeledPerformance', N'Modeled Performance'),
(117, 'OCTA M2 Tier 2 Grant Program', 'OCTA M2 Tier 2 Grant Program'),
(118, 'SEA Score', 'Strategically Effective Area Score'),
(119, 'TPI Score', 'Transportation Nexus Score'),
(120, 'WQLRI', 'Water Quality Load Reduction Index'),
(121, 'Pollutant Contribution to SEA', 'Pollutant Contribution to Strategically Effective Area'),
(122, 'SiteRunoff', 'Site Runoff'),
(123, 'TreatedAndDischarged', 'Treated and Discharged'),
(124, 'RetainedOrRecycled', 'Retained or Recycled'),
(125, 'Untreated(BypassOrOverflow)', 'Untreated (Bypass or Overflow)'),
(126, 'TotalSuspendedSolids', 'Total Suspended Solids'),
(127, 'TotalNitrogen', 'Total Nitrogen'),
(128, 'TotalPhosphorous', 'Total Phosphorous'),
(129, 'FecalColiform', 'Fecal Coliform'),
(130, 'TotalCopper', 'Total Copper'),
(131, 'TotalLead', 'Total Lead'),
(132, 'TotalZinc', 'Total Zinc'),
(133, 'OCTAWatershed', 'OCTA Watershed'),
(134, 'EffectiveAreaAcres', 'Effective Area (acres)'),
(135, 'DesignStormDepth85thPercentile', '85th Percentile Design Storm Depth (inches)'),
(136, 'DesignVolume85thPercentile', '85th Percentile Design Volume (cuft)')

