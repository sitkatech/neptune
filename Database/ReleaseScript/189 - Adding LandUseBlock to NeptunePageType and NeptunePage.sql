  insert into dbo.NeptunePageType (NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
  values (42, 'LandUseBlcok', 'Land Use Block', 2)

go

insert into dbo.NeptunePage (NeptunePageTypeID, NeptunePageContent)
  values (42, '')