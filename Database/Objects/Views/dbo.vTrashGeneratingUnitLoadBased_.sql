drop view if exists vTrashGeneratingUnitLoadBasedTargetReduction
GO

drop view if exists vTrashGeneratingUnitLoadBasedTrashAssessment
GO

drop view if exists vTrashGeneratingUnitLoadBasedPartialCapture
GO

drop view if exists vTrashGeneratingUnitLoadBasedFullCapture
GO


/*
"Load Reduction Via Full Trash Capture"
Fairly straightforward
*/
Create view dbo.vTrashGeneratingUnitLoadBasedFullCapture
as
Select
	TrashGeneratingUnitID as PrimaryKey,
	TrashGeneratingUnitID,
	tgu.StormwaterJurisdictionID,
	IsNull(TrashGeneratingUnitGeometry.STArea(), 0) as Area,
	IsNull(Case
		when score.TrashGenerationRate is null then lub.TrashGenerationRate
		else score.TrashGenerationRate
	end, 0) as BaselineLoadingRate
From
	dbo.TrashGeneratingUnit tgu left join LandUseBlock lub
		on tgu.LandUseBlockID = lub.LandUseBlockID
	left join OnlandVisualTrashAssessmentArea area
		on tgu.OnlandVisualTrashAssessmentAreaID = area.OnlandVisualTrashAssessmentAreaID
	left join OnlandVisualTrashAssessmentScore score
		on area.OnlandVisualTrashAssessmentBaselineScoreID = score.OnlandVisualTrashAssessmentScoreID
	left join dbo.Delineation d
		on tgu.DelineationID = d.DelineationID
	left join dbo.TreatmentBMP tbmp
		on d.TreatmentBMPID = tbmp.TreatmentBMPID
	left join dbo.TrashCaptureStatusType tcs
		on tcs.TrashCaptureStatusTypeID = tbmp.TrashCaptureStatusTypeID
Where tbmp.TrashCaptureStatusTypeID = 1
	and tgu.LandUseBlockID is not null
	and d.IsVerified = 1
	and (lub.TrashGenerationRate is not null or score.TrashGenerationRate is not null)
GO