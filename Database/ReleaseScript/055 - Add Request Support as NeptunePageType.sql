insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(25, 'RequestSupport', 'Request Support', 2),
(26, 'InviteUser', 'Invite User', 2)

insert into dbo.NeptunePage(NeptunePageTypeID, TenantID)
values 
(25, 2),
(26, 2)