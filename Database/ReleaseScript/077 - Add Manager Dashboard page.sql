/****** Script for SelectTopNRows command from SSMS  ******/
SELECT NeptunePageID, TenantID, NeptunePageTypeID, NeptunePageContent
  FROM Neptune.dbo.NeptunePage




INSERT INTO dbo.NeptunePageType (NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
VALUES (30, 'ManagerDashboard', 'Manager Dashboard', 2);



INSERT INTO dbo.NeptunePage (TenantID, NeptunePageTypeID, NeptunePageContent)
VALUES (2, 30, NULL);