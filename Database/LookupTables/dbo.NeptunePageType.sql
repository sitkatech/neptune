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
(11, 'ObservationTypes', 'Observation Types', 2)