

DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '012 - recalculate OVTA Area Baseline Score and Progress Score'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

   UPDATE
		area
	SET
		area.OnlandVisualTrashAssessmentBaselineScoreID = baselineScore.OnlandVisualTrashAssessmentScoreID
	FROM
		dbo.OnlandVisualTrashAssessmentArea AS area
		left JOIN 
	(  Select
		subq.OnlandVisualTrashAssessmentAreaID,
		score.OnlandVisualTrashAssessmentScoreID
	From (
		-- aggregate over areas
		Select
			OnlandVisualTrashAssessmentAreaID,
			Avg(NumericValue) as NumericValue,
			-- we only care about areas with two or more progress assessments, so we need this additional agg
			Count(*) as NumberOfBaselineAssessments
		From (
			-- get the baseline assessments
			select
				OnlandVisualTrashAssessmentAreaID,
				NumericValue,
				-- we only care about the most recent assessments, so we need to rank them on date
				ROW_NUMBER() over (partition by OnlandVisualTrashAssessmentAreaID order by CompletedDate desc) as RowNumber
			from dbo.OnlandVisualTrashAssessment ovta
				inner join dbo.OnlandVisualTrashAssessmentScore score
					on ovta.OnlandVisualTrashAssessmentScoreID = score.OnlandVisualTrashAssessmentScoreID
			where IsProgressAssessment = 0 and CompletedDate is not null
			) subq
	
		group by (OnlandVisualTrashAssessmentAreaID)
		having count(*) > 1
	) subq inner join dbo.OnlandVisualTrashAssessmentScore score
		on subq.NumericValue = score.NumericValue)
		AS baselineScore  ON area.OnlandVisualTrashAssessmentAreaID = baselineScore.OnlandVisualTrashAssessmentAreaID



		-- update progress scores.  area needs minimum of 2 assessments within the last 4 years.  take the average (same as baseline)
	UPDATE
    area
	SET
		area.OnlandVisualTrashAssessmentProgressScoreID = progressScore.OnlandVisualTrashAssessmentScoreID
	FROM
		dbo.OnlandVisualTrashAssessmentArea AS area
		left JOIN 
	(  Select
		subq.OnlandVisualTrashAssessmentAreaID,
		score.OnlandVisualTrashAssessmentScoreID
	From (
		-- aggregate over areas
		Select
			OnlandVisualTrashAssessmentAreaID,
			Avg(NumericValue) as NumericValue,
			-- we only care about areas with two or more progress assessments, so we need this additional agg
			Count(*) as NumberOfProgressAssessments
		From (
			-- get the progress assessments
			select
				OnlandVisualTrashAssessmentAreaID,
				NumericValue,
				-- we only care about the most recent assessments, so we need to rank them on date
				ROW_NUMBER() over (partition by OnlandVisualTrashAssessmentAreaID order by CompletedDate desc) as RowNumber
			from dbo.OnlandVisualTrashAssessment ovta
				inner join dbo.OnlandVisualTrashAssessmentScore score
					on ovta.OnlandVisualTrashAssessmentScoreID = score.OnlandVisualTrashAssessmentScoreID
			where IsProgressAssessment = 1 and CompletedDate >= DATEADD(year, -4, CURRENT_TIMESTAMP)
			) subq
	
		group by (OnlandVisualTrashAssessmentAreaID)
		having count(*) > 1
	) subq inner join dbo.OnlandVisualTrashAssessmentScore score
		on subq.NumericValue = score.NumericValue)
		AS progressScore  ON area.OnlandVisualTrashAssessmentAreaID = progressScore.OnlandVisualTrashAssessmentAreaID


    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Shannon Bulloch', @MigrationName, '012 -  recalculate OVTA Area Baseline Score and Progress Score'
END