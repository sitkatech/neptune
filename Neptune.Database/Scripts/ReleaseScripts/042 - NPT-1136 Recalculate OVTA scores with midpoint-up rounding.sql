DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '042 - NPT-1136 Recalculate OVTA scores with midpoint-up rounding';

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN

	PRINT @MigrationName;

	-- NPT-1136: OVTA Area scores are the average of assessment NumericValues (A=4, B=3, C=2, D=1) rounded
	-- to the nearest score, with ties rounding UP (B + C = 2.5 -> B). The C# previously used banker's rounding
	-- (2.5 -> C), and scripts 017/036 averaged the int NumericValue directly, which truncates (A, A, B = 3.67 -> B).
	-- The stored scores are only recalculated when an area's OVTAs change, so this one-time backfill applies the
	-- midpoint-up rule to every area. Must match OnlandVisualTrashAssessmentAreas.CalculateBaselineScoreFromBackingData,
	-- OnlandVisualTrashAssessments.CalculateProgressScore and dbo.vOnlandVisualTrashAssessmentAreaProgress.
	-- SQL Server's ROUND on a decimal rounds half away from zero, which is "up" for these positive values.

	--Baseline: all completed baseline assessments, at least 2 required
	UPDATE ovta
	SET ovta.OnlandVisualTrashAssessmentBaselineScoreID = ovtas.OnlandVisualTrashAssessmentScoreID
		FROM dbo.OnlandVisualTrashAssessmentArea ovta
	LEFT JOIN (
		SELECT
			ovta2.OnlandVisualTrashAssessmentAreaID,
			CAST(ROUND(AVG(CAST(ovtas2.NumericValue AS decimal(5,2))), 0) AS int) AS RoundedAverageNumericValue
		FROM dbo.OnlandVisualTrashAssessment ovta2
		JOIN dbo.OnlandVisualTrashAssessmentScore ovtas2
			ON ovta2.OnlandVisualTrashAssessmentScoreID = ovtas2.OnlandVisualTrashAssessmentScoreID
			--OnlandVisualTrashAssessmentStatusName = Complete
		WHERE ovta2.OnlandVisualTrashAssessmentStatusID = 2
		  AND ovta2.IsProgressAssessment = 0
		GROUP BY ovta2.OnlandVisualTrashAssessmentAreaID
		HAVING COUNT(*) >= 2
	) ovtaa ON ovta.OnlandVisualTrashAssessmentAreaID = ovtaa.OnlandVisualTrashAssessmentAreaID
		LEFT JOIN dbo.OnlandVisualTrashAssessmentScore ovtas
		ON ovtaa.RoundedAverageNumericValue = ovtas.NumericValue

	--Progress: the 3 most recent completed progress assessments from the last 5 years, at least 1 required
	UPDATE ovta
	SET ovta.OnlandVisualTrashAssessmentProgressScoreID = ovtas.OnlandVisualTrashAssessmentScoreID
		FROM dbo.OnlandVisualTrashAssessmentArea ovta
	LEFT JOIN (
		SELECT
			OnlandVisualTrashAssessmentAreaID,
			CAST(ROUND(AVG(CAST(NumericValue AS decimal(5,2))), 0) AS int) AS RoundedTop3AverageNumericValue
		FROM (
			SELECT
				ovta2.OnlandVisualTrashAssessmentAreaID,
				ovtas2.NumericValue,
				ROW_NUMBER() OVER (
					PARTITION BY ovta2.OnlandVisualTrashAssessmentAreaID
					-- CompletedDate has no time, so break same-day ties on ID to match CalculateProgressScore
					ORDER BY ovta2.CompletedDate DESC, ovta2.OnlandVisualTrashAssessmentID DESC
				) AS rn
			FROM dbo.OnlandVisualTrashAssessment ovta2
			JOIN dbo.OnlandVisualTrashAssessmentScore ovtas2
				ON ovta2.OnlandVisualTrashAssessmentScoreID = ovtas2.OnlandVisualTrashAssessmentScoreID
				--OnlandVisualTrashAssessmentStatusName = Complete
			WHERE ovta2.OnlandVisualTrashAssessmentStatusID = 2
			  AND ovta2.IsProgressAssessment = 1
			  AND ovta2.CompletedDate >= DATEADD(year, -5, CAST(GETDATE() AS date))
		) ranked
		WHERE rn <= 3
		GROUP BY OnlandVisualTrashAssessmentAreaID
		HAVING COUNT(*) >= 1
	) ovtaa ON ovta.OnlandVisualTrashAssessmentAreaID = ovtaa.OnlandVisualTrashAssessmentAreaID
		LEFT JOIN dbo.OnlandVisualTrashAssessmentScore ovtas
		ON ovtaa.RoundedTop3AverageNumericValue = ovtas.NumericValue

	INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
	SELECT 'Shannon Bulloch', @MigrationName, 'NPT-1136 - Recalculate OVTA baseline and progress scores with midpoint-up rounding'
END
