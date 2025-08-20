DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '0001 - Store LoadGeneratingUnit4326Geometry into temp'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN

	PRINT @MigrationName;


	SELECT * INTO #tempLoadGeneratingUnit4326
	FROM dbo.LoadGeneratingUnit4326


    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
SELECT 'Mack Peters', @MigrationName, @MigrationName
END