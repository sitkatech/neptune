Drop View If Exists dbo.vTrashGeneratingUnitLoadStatistic
GO

/*
Everything needed to calculate the color-ramp symbology for the load-based maps
*/
Create view dbo.vTrashGeneratingUnitLoadStatistic
as

Select
	PrimaryKey,
	TrashGeneratingUnitID,
	TreatmentBMPID,
	TrashGeneratingUnitGeometry,
	StormwaterJurisdictionID,
	BaselineLoadingRate,
	IsFullTrashCapture,
	PartialTrashCaptureEffectivenessPercentage,
	Case
		When DelineationIsVerified = 0 then BaselineLoadingRate
		When IsFullTrashCapture = 1 then 2.5
		When BaselineLoadingRate * (1-PartialTrashCaptureEffectivenessPercentage/100.0) > 2.5 then BaselineLoadingRate * (1 - PartialTrashCaptureEffectivenessPercentage/100.0)
		Else 2.5
	end as CurrentLoadingRate,
	ProgressLoadingRate - BaselineLoadingRate as LoadingRateDelta
From (

	Select
		TrashGeneratingUnitID as PrimaryKey,
		TrashGeneratingUnitID,
		tgu.TreatmentBMPID,
		tgu.TrashGeneratingUnitGeometry as TrashGeneratingUnitGeometry,
		tgu.StormwaterJurisdictionID,
		IsNull(
			Case
				when score.TrashGenerationRate is null then lub.TrashGenerationRate
				else score.TrashGenerationRate
			end, 0
		) as BaselineLoadingRate,
		case
			when tbmp.TrashCaptureStatusTypeID = 1 then 1
			else 0
		end as IsFullTrashCapture, 
		IsNull(tbmp.TrashCaptureEffectiveness, 0) as PartialTrashCaptureEffectivenessPercentage,
		IsNull(d.IsVerified, 0) as DelineationIsVerified,
		IsNull(
			Case
				when scoreProgress.TrashGenerationRate is null then lub.TrashGenerationRate
				else scoreProgress.TrashGenerationRate
			end, 0
		) as ProgressLoadingRate
	From
		dbo.TrashGeneratingUnit tgu
		left join dbo.LandUseBlock lub
			on tgu.LandUseBlockID = lub.LandUseBlockID
		left join dbo.OnlandVisualTrashAssessmentArea area
			on tgu.OnlandVisualTrashAssessmentAreaID = area.OnlandVisualTrashAssessmentAreaID
		left join dbo.OnlandVisualTrashAssessmentScore score
			on area.OnlandVisualTrashAssessmentBaselineScoreID = score.OnlandVisualTrashAssessmentScoreID
		left join dbo.vOnlandVisualTrashAssessmentAreaProgress areaProgress
			on tgu.OnlandVisualTrashAssessmentAreaID = areaProgress.OnlandVisualTrashAssessmentAreaID
		left join dbo.OnlandVisualTrashAssessmentScore scoreProgress
			on areaProgress.OnlandVisualTrashAssessmentScoreID = scoreProgress.OnlandVisualTrashAssessmentScoreID
		left join dbo.TreatmentBMP tbmp
			on tgu.TreatmentBMPID = tbmp.TreatmentBMPID
		left join dbo.Delineation d
			on tbmp.DelineationID = d.DelineationID
		left join dbo.TrashCaptureStatusType tcs
			on tcs.TrashCaptureStatusTypeID = tbmp.TrashCaptureStatusTypeID
		left join dbo.PriorityLandUseType plut
			on lub.PriorityLandUseTypeID = plut.PriorityLandUseTypeID
		left join dbo.StormwaterJurisdiction sj
			on tgu.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
		left join dbo.Organization o
			on o.OrganizationID = sj.OrganizationID
	Where 
		tgu.LandUseBlockID is not null
) subq

GO