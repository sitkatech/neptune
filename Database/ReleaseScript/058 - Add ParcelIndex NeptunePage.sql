insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(28, 'ParcelList', 'Parcel List', 2)

insert into dbo.NeptunePage(NeptunePageTypeID, TenantID)
values(28, 2)