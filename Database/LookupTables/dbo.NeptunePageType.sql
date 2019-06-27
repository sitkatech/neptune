delete from dbo.NeptunePageType
go

insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(1, 'HomePage', 'Home Page', 2),
(2, 'About', 'About', 2),
(3, 'OrganizationsList', 'Organizations List', 1),
(4, 'HomeMapInfo', 'Home Page Map Info', 2),
(5, 'HomeAdditionalInfo', 'Home Page Additional Info', 2),
(6, 'TreatmentBMP', 'Treatment BMP', 2),
(7, 'TreatmentBMPType', 'Treatment BMP Type', 2),
(9, 'Jurisdiction', 'Jurisdiction', 2),
(10, 'Assessment', 'Assessment', 2),
(11, 'ManageObservationTypesList', 'Manage Observation Types List', 2),
(12, 'ManageTreatmentBMPTypesList', 'Manage Treatment BMP Types List', 2),
(13, 'ManageObservationTypeInstructions', 'Manage Observation Type Instructions', 2),
(14, 'ManageObservationTypeObservationInstructions', 'Manage Observation Type Instructions for Observation Instructions', 2),
(15, 'ManageObservationTypeLabelsAndUnitsInstructions', 'Manage Observation Type Labels and Units Instructions', 2),
(16, 'ManageTreatmentBMPTypeInstructions', 'Manage Treatment BMP Type Instructions', 2),
(17, 'ManageCustomAttributeTypeInstructions', 'Manage Custom Attribute Type Instructions', 2),
(18, 'ManageCustomAttributeInstructions', 'Manage Custom Attribute Instructions', 2),
(19, 'ManageCustomAttributeTypesList', 'Manage Custom Attribute Types List', 2),
(20, 'Legal', 'Legal', 2),
(21, 'FundingSourcesList', 'Funding Sources List', 2),
(22, 'FindABMP', 'Find a BMP', 2),
(23, 'LaunchPad', 'Launch Pad', 2),
(24, 'FieldRecords', 'Field Records', 2),
(25, 'RequestSupport', 'Request Support', 2),
(26, 'InviteUser', 'Invite User', 2),
(27, 'WaterQualityMaintenancePlan', 'Water Quality Maintenance Plan', 2),
(28, 'ParcelList', 'Parcel List', 2),
(29, 'Training', 'Training', 2),
(30, 'ManagerDashboard', 'Manager Dashboard', 2),
(31, 'WaterQualityMaintenancePlanOandMVerifications', 'Water Quality Maintenance Plan O&M Verifications', 2),
(32, 'ModelingHomePage', 'Modeling Home Page', 2),
(33, 'TrashHomePage', 'Trash Module Home Page', 2),
(34, 'OVTAInstructions', 'OVTA Instructions', 2),
(35, 'OVTAIndex', 'OVTA Index', 2),
(36, 'TrashModuleProgramOverview', 'Trash Module Program Overview', 2),
(37, 'DelineationMap', 'Delineation Map', 2),
(38, 'BulkUploadRequest', 'Bulk Upload Request', 2),
(39, 'DroolToolHomePage', 'Drool Tool Home Page', 2),
(40, 'DroolToolAboutPage', 'Drool Tool About Page', 2),
(41, 'TreatmentBMPAssessment', 'Treatment BMP Assessment', 2),
(42, 'EditOVTAArea', 'Edit OVTA Area', 2),
(43, 'LandUseBlock', 'Land Use Block', 2),
(44, 'ExportAssessmentGeospatialData', 'Export Assessment Geospatial Data', 2)
