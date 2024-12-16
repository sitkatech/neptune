DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '002- remove excess white space from TreatmentBMPType names'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    update dbo.TreatmentBMPType
	set TreatmentBMPTypeName = 'Bioretention with no Underdrain'
	where TreatmentBMPTypeID = 16

  
	update dbo.TreatmentBMPType
	set TreatmentBMPTypeName = 'Bioretention with Underdrain and Impervious Liner'
	where TreatmentBMPTypeID = 25
	
    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Liz Arikawa', @MigrationName, '002- remove excess white space from TreatmentBMPType names'
END