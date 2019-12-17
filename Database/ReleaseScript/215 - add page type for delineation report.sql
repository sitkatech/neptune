insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(47, 'DelineationReconciliationReport', 'Delineation Reconciliation Report', 2)

insert into dbo.NeptunePage(NeptunePageTypeID) values(47)