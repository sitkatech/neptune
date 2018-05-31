insert into NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(24, 'FieldRecords', 'Field Records', 2)

insert into NeptunePage (TenantID, NeptunePageTypeID, NeptunePageContent)
values
(2, 24, '')