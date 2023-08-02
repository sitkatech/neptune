/*
Pre-Deployment Script
--------------------------------------------------------------------------------------
This file is generated on every build, DO NOT modify.
--------------------------------------------------------------------------------------
*/

PRINT N'Qanat.Database - Script.PreDeployment.ReleaseScripts.sql';
GO

:r ".\0001 - store WaterAccount address in temp table.sql"
GO
:r ".\0002 - copy out zone groups.sql"
GO

