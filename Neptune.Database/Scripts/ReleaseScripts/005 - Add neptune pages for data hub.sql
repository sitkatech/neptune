DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '005 - Add neptune pages for data hub'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
	values
		(71, 'Data Hub'),
		(72, 'Data Hub'),
		(73, 'Data Hub'),
		(74, 'Data Hub'),
		(75, 'Data Hub'),
		(76, 'Data Hub'),
		(77, 'Data Hub'),
		(78, 'Data Hub'),
		(79, 'Data Hub')

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Liz Arikawa', @MigrationName, '005 - Add neptune pages for data hub'
END