insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(44, 'ExportAssessmentGeospatialData', 'Export Assessment Geospatial Data', 2)


Insert into dbo.NeptunePage(NeptunePageTypeID)
Values
(44)