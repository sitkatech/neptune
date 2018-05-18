insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(22, 'FindABMP', 'Find a BMP', 2)

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent, TenantID)
values
(22, '', 2)
