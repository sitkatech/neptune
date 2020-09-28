insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(51, 'BulkUploadFieldVisits', 'Bulk Upload Field Visits', 2)

insert into dbo.NeptunePage(NeptunePageTypeID)
values
(51)