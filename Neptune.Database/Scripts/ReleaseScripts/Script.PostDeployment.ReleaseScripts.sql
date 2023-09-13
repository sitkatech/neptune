/*
Post-Deployment Script
--------------------------------------------------------------------------------------
This file is generated on every build, DO NOT modify.
--------------------------------------------------------------------------------------
*/

PRINT N'Neptune.Database - Script.PostDeployment.ReleaseScripts.sql';
GO

:r ".\0001 - Add DataLength value to FileResource table.sql"
GO

