/*
Post-Deployment Script
--------------------------------------------------------------------------------------
This file is generated on every build, DO NOT modify.
--------------------------------------------------------------------------------------
*/

PRINT N'Neptune.Database - Script.PostDeployment.ReleaseScripts.sql';
GO

:r ".\001-rename fields on ModelingAttribute table.sql"
GO
:r ".\002- remove excess white space from TreatmentBMPType names.sql"
GO
:r ".\003- Add neptune page for upload simplified bmps.sql"
GO
:r ".\004 - Add neptune page for upload ovtas.sql"
GO
:r ".\005 - Add neptune page for WQMP APN upload.sql"
GO
:r ".\006 - Add neptune pages for data hub.sql"
GO
:r ".\007 - Add neptune page Export BMP Inventory to GIS.sql"
GO
:r ".\008 - Add neptune page for new home page.sql"
GO
:r ".\009 - add neptune page for wqmp modeling options.sql"
GO
:r ".\010 - add field definitions for trash home page.sql"
GO

