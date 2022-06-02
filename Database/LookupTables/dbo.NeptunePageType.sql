delete from dbo.NeptunePageType
go

insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName)
values
(1, 'HomePage', 'Home Page'),
(2, 'About', 'About'),
(3, 'OrganizationsList', 'Organizations List'),
(4, 'HomeMapInfo', 'Home Page Map Info'),
(5, 'HomeAdditionalInfo', 'Home Page Additional Info'),
(6, 'TreatmentBMP', 'Treatment BMP'),
(7, 'TreatmentBMPType', 'Treatment BMP Type'),
(9, 'Jurisdiction', 'Jurisdiction'),
(10, 'Assessment', 'Assessment'),
(11, 'ManageObservationTypesList', 'Manage Observation Types List'),
(12, 'ManageTreatmentBMPTypesList', 'Manage Treatment BMP Types List'),
(13, 'ManageObservationTypeInstructions', 'Manage Observation Type Instructions'),
(14, 'ManageObservationTypeObservationInstructions', 'Manage Observation Type Instructions for Observation Instructions'),
(15, 'ManageObservationTypeLabelsAndUnitsInstructions', 'Manage Observation Type Labels and Units Instructions'),
(16, 'ManageTreatmentBMPTypeInstructions', 'Manage Treatment BMP Type Instructions'),
(17, 'ManageCustomAttributeTypeInstructions', 'Manage Custom Attribute Type Instructions'),
(18, 'ManageCustomAttributeInstructions', 'Manage Custom Attribute Instructions'),
(19, 'ManageCustomAttributeTypesList', 'Manage Custom Attribute Types List'),
(20, 'Legal', 'Legal'),
(21, 'FundingSourcesList', 'Funding Sources List'),
(22, 'FindABMP', 'Find a BMP'),
(23, 'LaunchPad', 'Launch Pad'),
(24, 'FieldRecords', 'Field Records'),
(25, 'RequestSupport', 'Request Support'),
(26, 'InviteUser', 'Invite User'),
(27, 'WaterQualityMaintenancePlan', 'Water Quality Maintenance Plan'),
(28, 'ParcelList', 'Parcel List'),
(29, 'Training', 'Training'),
(30, 'ManagerDashboard', 'Manager Dashboard'),
(31, 'WaterQualityMaintenancePlanOandMVerifications', 'Water Quality Maintenance Plan O&M Verifications'),
(32, 'ModelingHomePage', 'Modeling Home Page'),
(33, 'TrashHomePage', 'Trash Module Home Page'),
(34, 'OVTAInstructions', 'OVTA Instructions'),
(35, 'OVTAIndex', 'OVTA Index'),
(36, 'TrashModuleProgramOverview', 'Trash Module Program Overview'),
(37, 'DelineationMap', 'Delineation Map'),
(38, 'BulkUploadRequest', 'Bulk Upload Request'),
(41, 'TreatmentBMPAssessment', 'Treatment BMP Assessment'),
(42, 'EditOVTAArea', 'Edit OVTA Area'),
(43, 'LandUseBlock', 'Land Use Block'),
(44, 'ExportAssessmentGeospatialData', 'Export Assessment Geospatial Data'),
(45, 'HRUCharacteristics', 'HRU Characteristics'),
(46, 'RegionalSubbasins', 'Regional Subbasins'),
(47, 'DelineationReconciliationReport', 'Delineation Reconciliation Report'),
(48, 'ViewTreatmentBMPModelingAttributes', 'View Treatment BMP Modeling Attributes'),
(49, 'UploadTreatmentBMPs', 'Upload Treatment BMPs'),
(50, 'AboutModelingBMPPerformance', 'About Modeling BMP Performance'),
(51, 'BulkUploadFieldVisits', 'Bulk Upload Field Visits'),
(52, 'HippocampHomePage', 'Hippocamp Home Page'),
(53, 'HippocampTraining', 'Hippocamp Training'),
(54, 'HippocampLabelsAndDefinitionsList', 'Hippocamp Labels and Definitions List'),
(55, 'HippocampAbout', 'Hippocamp About Page'),
(56, 'HippocampProjectsList', 'Hippocamp Projects List'),
(57, 'HippocampProjectInstructions', 'Hippocamp Project Instructions Page'),
(58, 'HippocampProjectBasics', 'Hippocamp Project Basics'),
(59, 'HippocampProjectAttachments', 'Hippocamp Project Attachments'),
(60, 'HippocampTreatmentBMPs', 'Hippocamp Treatment BMPs'),
(61, 'HippocampDelineations', 'Hippocamp Delineations'),
(62, 'HippocampModeledPerformance', 'Hippocamp Modeled Performance'),
(63, 'HippocampReview', 'Hippocamp Review'),
(64, 'HippocampPlanningMap', 'Hippocamp Planning Map'),
(65, 'OCTAM2Tier2GrantProgramMetrics', 'OCTA M2 Tier 2 Grant Program Metrics'),
(66, 'OCTAM2Tier2GrantProgramDashboard', 'OCTA M2 Tier 2 Grant Program Dashboard')
