DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '007 - Add neptune page for Export BMP Inventory to GIS'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
	values
		(86, 'This will download the BMP Inventory to your computer as a file geodatabase. This may take several minutes. Please do not close your browser until the download is complete.<')

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Shannon Bulloch', @MigrationName, 'Add neptune page for Export BMP Inventory to GIS'
END