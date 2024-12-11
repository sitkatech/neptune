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

