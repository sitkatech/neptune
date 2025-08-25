DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '017 - Recalculate Baseline and Progress OVTA Scores'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    --Baseline
    update ovta
    set ovta.OnlandVisualTrashAssessmentBaselineScoreID = ovtas.OnlandVisualTrashAssessmentScoreID
        from dbo.OnlandVisualTrashAssessmentArea ovta
    left join (
	    select OnlandVisualTrashAssessmentAreaID, avg(NumericValue) AverageNumericValue
	    from dbo.OnlandVisualTrashAssessment ovta
	    join dbo.OnlandVisualTrashAssessmentScore ovtas on ovta.OnlandVisualTrashAssessmentScoreID = ovtas.OnlandVisualTrashAssessmentScoreID
		      --OnlandVisualTrashAssessmentStatusName = Complete
	    where OnlandVisualTrashAssessmentStatusID = 2 and IsProgressAssessment = 0
	    group by OnlandVisualTrashAssessmentAreaID
	    having count(*) >= 2) ovtaa on ovta.OnlandVisualTrashAssessmentAreaID = ovtaa.OnlandVisualTrashAssessmentAreaID
        left join dbo.OnlandVisualTrashAssessmentScore ovtas on ovtaa.AverageNumericValue = ovtas.NumericValue

    --Progress
    UPDATE ovta
    SET ovta.OnlandVisualTrashAssessmentProgressScoreID = ovtas.OnlandVisualTrashAssessmentScoreID
        FROM dbo.OnlandVisualTrashAssessmentArea ovta
    LEFT JOIN (
        SELECT
            OnlandVisualTrashAssessmentAreaID,
            AVG(NumericValue) AS Top3AverageNumericValue
        FROM (
            SELECT
                ovta2.OnlandVisualTrashAssessmentAreaID,
                ovtas2.NumericValue,
                ROW_NUMBER() OVER (
                    PARTITION BY ovta2.OnlandVisualTrashAssessmentAreaID
                    ORDER BY ovta2.CompletedDate DESC
                ) AS rn
            FROM dbo.OnlandVisualTrashAssessment ovta2
            JOIN dbo.OnlandVisualTrashAssessmentScore ovtas2
                ON ovta2.OnlandVisualTrashAssessmentScoreID = ovtas2.OnlandVisualTrashAssessmentScoreID
            WHERE ovta2.OnlandVisualTrashAssessmentStatusID = 2
              AND ovta2.IsProgressAssessment = 1
              AND ovta2.CompletedDate < DATEADD(year, -4, GETDATE())
        ) ranked
        WHERE rn <= 3
        GROUP BY OnlandVisualTrashAssessmentAreaID
        HAVING COUNT(*) >= 2
    ) ovtaa ON ovta.OnlandVisualTrashAssessmentAreaID = ovtaa.OnlandVisualTrashAssessmentAreaID
        LEFT JOIN dbo.OnlandVisualTrashAssessmentScore ovtas
        ON ovtaa.Top3AverageNumericValue = ovtas.NumericValue

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Mack Peters', @MigrationName, '017 - Recalculate Baseline and Progress OVTA Scores'
END