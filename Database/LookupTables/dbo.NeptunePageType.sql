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
(8, 'ModeledCatchment', 'Modeled Catchment', 2),
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
(21, 'FundingSourcesList', 'Funding Sources List', 2)
