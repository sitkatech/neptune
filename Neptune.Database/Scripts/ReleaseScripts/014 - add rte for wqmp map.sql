DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '014 - add rte for wqmp map'
IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    insert into dbo.NeptunePage (NeptunePageTypeID, NeptunePageContent)
    values (91, 'A Water Quality Management Plan (WQMP) is required for certain development and redevelopment projects known as Priority Projects or Priority Development Projects. These WQMP sites often include both structural treatment BMPs and source control BMPs. Hydromodification control BMPs also apply to some sites. Use the map below to see all currently inventoried WQMPs. You can click the WQMP name to see additional details, including the associated parcels and BMPs. Note that all inventoried WQMPs are shown, however, some parts of the inventory may not be verified. This is not intended to be a complete auditable record at this time.  Learn more about WQMP Modeling Options.')

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Liz Arikawa', @MigrationName, '014 - add rte for wqmp map'
END