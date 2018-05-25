insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(23, 'LaunchPad', 'Launch Pad', 2)

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent, TenantID)
select
	NeptunePageTypeID = 23,
	NeptunePageContent = '',
	TenantID = t.TenantID
from dbo.Tenant t
