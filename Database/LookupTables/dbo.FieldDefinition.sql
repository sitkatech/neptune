delete from dbo.FieldDefinition
go

INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel) 
VALUES 
(1, N'IsPrimaryContactOrganization', N'Is Primary Contact Organization', N'<p>The entity with primary responsibility for organizing, planning, and executing implementation activities for a project or program. This is usually the lead implementer.</p>', 1),
(2, N'Organization', N'Organization', N'<p>A partner entity that is directly involved with implementation or funding a project.&nbsp;</p>', 1),
(3, N'Password', N'Password', N'<p>Password required to log into the Orange County Stormwater Tools in order to access and edit project and program information.</p>', 0),
(4, N'MeasurementUnit', N'Measurement Unit', N'<p>The unit of measure used by an Indicator (aka&nbsp;Performance Measure) to track the extent of implementation.</p>', 1),
(5, N'PhotoCaption', N'Photo Caption', N'<p>A concise yet descriptive explanation of an uploaded photo. Photo captions are displayed in the lower right-hand corner of the image as it appears on the webpage.</p>', 1),
(6, N'PhotoCredit', N'Photo Credit', N'<p>If needed, credit is given to the photographer or owner of an image on the website. Photo credits are displayed in the lower right-hand corner of the image as it appears on the webpage.</p>', 1),
(7, N'PhotoTiming', N'Photo Timing', N'<p>The phase in a project timeline during which the photograph was taken. Photo timing can be before, during or after project implementation.&nbsp;</p>', 1),
(8, N'PrimaryContact', N'Primary Contact', N'<p>An individual at the listed organization responsible for reporting accomplishments and expenditures achieved by the project or program, and who should be contacted when there are questions related to any project associated to the organization.</p>', 1),
(9, N'OrganizationType', N'Organization Type', N'<p>A categorization of an organization, e.g. Local, State, Federal or Private.</p>', 1),
(10, N'Username', N'User name', N'<p>Password required to log into the system&nbsp;order to access and edit project and program information that is not allowed by public users.</p>', 1),
(11, N'ExternalLinks', N'External Links', N'<p>Links to external web pages where you might find additional information.</p>', 1),
(12, N'RoleName', N'Role Name', N'<p>The name or title describing&nbsp;function or set of permissions that can be assigned to a user.</p>', 1),
(13, N'Chart Last Updated Date', 'ChartLastUpdatedDate','<p>The date this chart was last updated with current information.</p>', 1),
(14, N'TreatmentBMPType', N'Treatment BMP Type', '', 1),
(16, N'ConveyanceFunctionsAsIntended', N'Conveyance Functions as Intended', '', 1),
(17, N'AssessmentScoreWeight', N'Assessment Score Weight', '', 1),
(18, N'ObservationScore', N'Observation Score', '', 1),
(19, N'AlternativeScore', N'Alternative Score', '', 1),
(20, N'AssessmentForInternalUseOnly', N'Assessment for Internal Use Only', '', 1),
(21, N'TreatmentBMPDesignDepth', N'Treatment BMP Design Depth', '', 1),
(22, N'ReceivesSystemCommunications', N'Receives System Communications', '', 1),
(23, N'Jurisdiction', N'Jurisdiction', '', 1),
(24, N'Delineation', N'Delineation', '', 1),
(25, N'TreatmentBMP', N'Treatment BMP', '', 1),
(26, N'TreatmentBMPAssessmentObservationType', N'Observation Name', '', 1),
(27, N'ObservationCollectionMethod', N'Collection Method', '', 1),
(28, N'ObservationThresholdType', N'Threshold Type', '', 1),
(29, N'ObservationTargetType', N'Target Type', '', 1),
(30, N'MeasurementUnitLabel', N'Measurement Unit Label', '', 1),
(31, N'PropertiesToObserve', N'Properties To Observe', '', 1),
(32, N'MinimumNumberOfObservations', N'Minimum Number of Observations', '', 1),
(33, N'MaximumNumberOfObservations', N'Maximum Number of Observations', '', 1),
(34, N'MinimumValueOfEachObservation', N'Minimum Value of Each Observation', '', 1),
(35, N'MaximumValueOfEachObservation', N'Maximum Value of Each Observation', '', 1),
(36, N'DefaultThresholdValue', N'Default Threshold Value', '', 1),
(37, N'DefaultBenchmarkValue', N'Default Benchmark Value', '', 1),
(38, N'AssessmentFailsIfObservationFails', N'Assessment Fails if Observation Fails', '', 1),
(39, N'CustomAttributeType', N'Attribute Name', '', 1),
(40, N'CustomAttributeDataType', N'Data Type', '', 1),
(41, N'MaintenanceRecordType', N'Maintenance Type', 'Whether the maintenance performed was Preventative or Corrective maintenance', 1),
(42, N'MaintenanceRecord', N'Maintenance Record', 'A record of a maintenance activity performed on a Treatment BMP', 1),
(43, N'AttributeTypePurpose', N'Purpose', 'How the attribute type will be used for analysis and reporting', 1),
(44, N'FundingSource', N'Funding Source', '', 1),
(45, N'IsPostMaintenanceAssessment', N'Post Maintenance Assessment?', 'Whether the assessment was conducted as a follow-up to a maintenance activity', 1),
(46, N'FundingEvent', N'Funding Event', 'A discrete activity (e.g. Project planning, major maintenance, capital construction) that invests in a BMP. A funding event consists of one or funding sources and associated expenditures', 1),
(47, N'FieldVisit', N'Field Visit', 'A visit to a Treatment BMP which can consist of an Assessment, a Maintenance Record, and a Post-Maintenance Assessment', 1),
(48, N'FieldVisitStatus', N'Field Visit Status', 'Completion status of the Field Visit. A Field Visit can be In Progress, Complete, or left Unresolved. A Treatment BMP may only have one In Progress Field Visit at any given time.', 1),
(49, N'WaterQualityManagementPlan', N'Water Quality Management Plan', N'', 1),
(50, N'Parcel', N'Parcel', N'', 1),
(51, N'RequiredLifespanOfInstallation', N'Required Lifespan of Installation', N'Specifies when or whether a BMP can be removed', 1),
(52, N'RequiredFieldVisitsPerYear', N'Required Field Visits Per Year', N'Number of Field Visists that must be conducted for a given BMP each year', 1),
(53, N'RequiredPostStormFieldVisitsPerYear', N'Required Post-Storm Field Visits Per Year', N'Number of Post-Storm Field Visists that must be conducted for a given BMP each year', 1),
(54, N'WaterQualityManagementPlanDocumentType', N'WQMP Document Type', N'Specifies what type of supporting document this is. Some document types are required for a WQMP to be considered complete', 1),
(55, N'HasAllRequiredDocuments', N'Has All Required Documents?', N'Indicates whether all required supporting documents are present for a WQMP', 1),
(56, N'DateOfLastInventoryChange', N'Date of Last Inventory Change', N'', 1),
(57, N'TrashCaptureStatus', N'Trash Capture Status', N'Indicates the ability of this BMP to capture trash.', 1),
(58, N'OnlandVisualTrashAssessment', N'On-land Visual Trash Assessment', N'The assessing, visually, of trash on land.', 1),
(59, N'OnlandVisualTrashAssessmentNotes', N'Comments and Additional Information', 'Enter the name of all assessors and any other notes about the assessment.', 1),
(60, N'DelineationType', N'Delineation Type', N'Indicates whether the delineation is distributed or centralized.', 1),
(61, N'BaselineScore', N'Baseline Score', N'For an OVTA, scores range from A to D and indicate the condition of the assessed area at the time of the assessment. For an OVTA Area, the score is an aggregate of all of its Assessments'' scores.', 1),
(62, N'SizingBasis', N'Sizing Basis', N'Indicates whether this BMP is sized for full trash capture, water quality improvement, or otherwise.', 1),
(63, N'ProgressScore', N'Progress Score', N'', 1),
(64, N'AssessmentScore', N'Assessment Score', N'', 1),
(65, N'ViaFullCapture', N'Via Full Capture', N'', 1),
(66, N'ViaPartialCapture', N'Via Partial Capture', N'', 1),
(67, N'ViaOVTAScore', N'Via OVTA Score', N'', 1),
(68, N'TotalAchieved', N'Total Achieved', N'', 1),
(69, N'TargetLoadReduction', N'Target Load Reduction', N'', 1),
(70, N'LoadingRate', N'Loading Rate', N'', 1),
(71, N'LandUse', N'Land Use', N'', 1),
(72, N'Area', N'Area', N'', 1),
(73, N'ImperviousArea', N'Impervious Area', N'', 1),
(74, N'GrossArea', N'Gross Area', N'', 1),
(75, N'LandUseStatistics', N'Land Use Statistics', N'', 1),
(76, N'RegionalSubbasin', N'Regional Subbasin', N'', 1),
(77, 'AverageDivertedFlowrate', 'Average Diverted Flowrate', 'Average actual diverted flowrate over the months of operation.', 1),
(78, 'AverageTreatmentFlowrate', 'Average Treatment Flowrate', 'Average actual treated flowrate over the months of operation.', 1),
(79, 'DesignDryWeatherTreatmentCapacity', 'Design Dry Weather Treatment Capacity', 'Flow treatment capacity of the BMP.', 1),
(80, 'DesignLowFlowDiversionCapacity', 'Design Low Flow Diversion Capacity', 'The physical capacity of the low flow diversion or the maximum permitted flow.', 1),
(81, 'DesignMediaFiltrationRate', 'Design Media Filtration Rate', 'Design filtration rate through the media bed. This may be controlled by the media permeability or by an outlet control on the underdrain system.', 1),
(82, 'DesignResidenceTimeForPermanentPool', 'Design Residence Time for Permanent Pool', 'Amount of residence time needed to meet full level of treatment for water that is stored in the permanent pool.', 1),
(83, 'DiversionRate', 'Diversion Rate', 'Flowrate diverted into the BMP.', 1),
(84, 'DrawdownTimeForWQDetentionVolume', 'Drawdown Time for WQ Detention Volume', 'Time for water quality surcharge volume to draw down after the end of a storm if there is no further inflow.', 1),
(85, 'EffectiveFootprint', 'Effective Footprint', 'The footprint of the BMP that is effective for filtration or infiltration. Unless other information is available, this can be estimated as the wetted footprint when BMP is half full.', 1),
(86, 'EffectiveRetentionDepth', 'Effective Retention Depth', 'Depth of water stored in shallow surface depression or media/rock sump for infiltration to occur.', 1),
(87, 'InfiltrationDischargeRate', 'Infiltration Discharge Rate', 'Design or tested infiltration flowrate of the drywell. This is specified in cubic feet per section, rather than inches per hour.', 1),
(88, 'InfiltrationSurfaceArea', 'Infiltration Surface Area', 'Surface area through which infiltration can occur in the system. If infiltration will occur into the sidewalls of a BMP, it is appropriate to include half of the sidewall area as as part of the infiltration surface area.', 1),
(89, 'MediaBedFootprint', 'Media Bed Footprint', 'Surface area of the media bed of the BMP.', 1),
(90, 'MonthsOfOperation', 'Months of Operation', 'This defines the months that the facility is operational.', 1),
(91, 'PermanentPoolOrWetlandVolume', 'Permanent Pool or Wetland Volume', 'Volume of water below the lowest surface outlet. Serves as a wetland or permanent pool. Water may be harvested from this pool. ', 1),
(92, 'RoutingConfiguration', 'Routing Configuration', 'This specifies whether the BMP receives all flow from the drainage area (online), or if there is a diversion structure that limits the flow into the BMP (offline).', 1),
(93, 'StorageVolumeBelowLowestOutletElevation', 'Storage Volume Below Lowest Outlet Elevation', 'The volume of water stored below the lowest outlet (e.g., underdrain, orifice) of the system.', 1),
(94, 'SummerHarvestedWaterDemand', 'Summer Harvested Water Demand', 'Average daily harvested water demand from May through October.', 1),
(95, 'TimeOfConcentration', 'Time of Concentration', 'The time required for the entire drainage to begin contributing runoff to the BMP. This value must be less than 60 minutes. See TGD guidance.', 1),
(96, 'TotalDrawdownTime', 'Total Drawdown Time', 'Time for the basin to fully draw own after the end of a storm if there is no further inflow.', 1),
(97, 'TotalEffectiveBMPVolume', 'Total Effective BMP Volume', 'The volume of the BMP available for water quality purposes. This includes ponding volume and the available pore volume in media layers and/or in gravel storage layers. It does not include flow control volumes or other volume that is not designed for water quality purposes. ', 1),
(98, 'TotalEffectiveDrywellBMPVolume', 'Total Effective Drywell BMP Volume', 'The volume of the BMP available for water quality purposes. This includes the volume in any pre-treatment chamber as well as the volume in the well itself.', 1),
(99, 'TreatmentRate', 'Treatment Rate', 'The flowrate at which the BMP can provide treatment of runoff.', 1),
(100, 'UnderlyingHydrologicSoilGroupHSG', 'Underlying Hydrologic Soil Group (HSG)', 'Choose the soil group that best represents the soils underlying the BMP. This is used to estimate a default infiltration rate (A = XX, B = XX, C=XX, D=XX)', 1),
(101, 'UnderlyingInfiltrationRate', 'Underlying Infiltration Rate', 'The underlying infiltration rate below the BMP. This refers to the underlying soil, not engineered media.', 1),
(102, 'UpstreamBMP', 'Upstream BMP', 'Assign a delineation to the BMP through the normal delineation options.<br /><br />OR<br /><br />Indicate that the BMP receives flow from an upstream BMP.', 1),
(103, 'WaterQualityDetentionVolume', 'Water Quality Detention Volume', 'Volume of water above the surface outlet that provides a water quality detention function; surcharges during storms and drains after storms.  Do not include volume intended for peak flow control.', 1),
(104, 'WettedFootprint', 'Wetted Footprint', 'Wetted footprint when BMP is half full.', 1),
(105, 'WinterHarvestedWaterDemand', 'Winter Harvested Water Demand', 'Average daily harvested water demand from November through April. This should be averaged to account for any shutdowns during wet weather and reduction in demand during the winter season.', 1),
(106, 'PercentOfSiteTreated', 'Percent of Site Treated', '', 1),
(107, 'PercentCaptured', 'Percent Captured', '', 1),
(108, 'PercentRetained', 'Percent Retained', '', 1),
(109, 'AreaWithinWQMP', 'Area within WQMP', '', 1),
(110, 'Watershed', 'Watershed', '', 1),
(111, 'DesignStormwaterDepth', 'Design Stormwater Depth', '', 1)
