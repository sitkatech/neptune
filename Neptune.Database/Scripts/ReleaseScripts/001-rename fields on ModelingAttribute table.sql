DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '001-rename fields on ModelingAttribute table'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    exec sp_rename 'dbo.TreatmentBMPModelingAttribute.DrawdownTimeForWQDetentionVolume', 'DrawdownTimeForWQDetentionVolume', 'COLUMN'
    exec sp_rename 'dbo.TreatmentBMPModelingAttribute.PermanentPoolOrWetlandVolume', 'PermanentPoolOrWetlandVolume', 'COLUMN'

	
    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Ray Lee', @MigrationName, '001-rename fields on ModelingAttribute table'
END