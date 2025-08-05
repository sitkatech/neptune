DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '016 - Load 4326 Geometries into LoadGeneratingUnit table'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

UPDATE dbo.LoadGeneratingUnit
SET LoadGeneratingUnitGeometry4326 = lgu4326.LoadGeneratingUnit4326Geometry
    FROM #tempLoadGeneratingUnit4326 lgu4326
 JOIN dbo.LoadGeneratingUnit lgu
ON lgu4326.LoadGeneratingUnit4326ID = lgu.LoadGeneratingUnitID

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Mack Peters', @MigrationName, '016 - Load 4326 Geometries into LoadGeneratingUnit table'
END