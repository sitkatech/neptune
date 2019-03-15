insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(38, 'BulkUploadRequest', 'Bulk Upload Request', 2)

insert into dbo.NeptunePage(NeptunePageTypeID)
values (38)