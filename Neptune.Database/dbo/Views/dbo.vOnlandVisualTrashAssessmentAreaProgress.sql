Create View dbo.vOnlandVisualTrashAssessmentAreaProgress
as
Select
	subq.OnlandVisualTrashAssessmentAreaID as PrimaryKey,
	subq.OnlandVisualTrashAssessmentAreaID,
	score.OnlandVisualTrashAssessmentScoreDisplayName,
	score.OnlandVisualTrashAssessmentScoreID
From (
	-- aggregate over areas
	Select
		OnlandVisualTrashAssessmentAreaID,
		-- NPT-1136: midpoint-up is deliberate and must match OnlandVisualTrashAssessmentScore.FromAverageNumericValue.
		-- NumericValue is an int, so a plain AVG truncates (A, A, B = 3.67 -> 3 = B); average as a decimal and ROUND,
		-- which rounds half away from zero on decimals (B + C = 2.5 -> 3 = B).
		cast(round(avg(cast(NumericValue as decimal(5,2))), 0) as int) as NumericValue,
		-- we only care about areas with one or more progress assessments within the last 5 years, so we need this additional agg
		Count(*) as NumberOfProgressAssessments
	From (
		-- get the progress assessments
		select
			OnlandVisualTrashAssessmentAreaID,
			NumericValue,
			-- we only care about the most recent assessments, so we need to rank them on date
			-- (CompletedDate has no time, so break same-day ties on ID to match CalculateProgressScore)
			ROW_NUMBER() over (partition by OnlandVisualTrashAssessmentAreaID order by CompletedDate desc, OnlandVisualTrashAssessmentID desc) as RowNumber
		from dbo.OnlandVisualTrashAssessment ovta
			inner join dbo.OnlandVisualTrashAssessmentScore score
				on ovta.OnlandVisualTrashAssessmentScoreID = score.OnlandVisualTrashAssessmentScoreID
		where ovta.OnlandVisualTrashAssessmentStatusID = 2 -- Complete
			and IsProgressAssessment = 1 and CompletedDate >= DATEADD(year, -5, CAST(CURRENT_TIMESTAMP AS date))
		) subq
	where RoWNumber <=3
	group by (OnlandVisualTrashAssessmentAreaID)
	having count(*) >= 1
) subq inner join dbo.OnlandVisualTrashAssessmentScore score
	on subq.NumericValue = score.NumericValue
Go

/* select * from dbo.vOnlandVisualTrashAssessmentAreaProgress */