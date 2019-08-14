Drop View If Exists dbo.vTrashGeneratingUnitLoadStatistic
GO

/*
Everything needed to compute load-based results, including the choropleth symbology for the load-based maps
*/
Create view dbo.vTrashGeneratingUnitLoadStatistic
as
Select
	*,
	IsNull(TrashGeneratingUnitGeometry.STArea(), 0) as Area,
	case
		when CurrentLoadingRate < ProgressLoadingRate then CurrentLoadingRate - BaselineLoadingRate
		else ProgressLoadingRate - BaselineLoadingRate
	end as LoadingRateDelta
from
(
Select
	PrimaryKey,
	TrashGeneratingUnitID,
	TreatmentBMPID,
	TreatmentBMPName,
	TrashGeneratingUnitGeometry,
	TrashGeneratingUnitGeometry.STArea() as TrashGeneratingUnitArea,
	StormwaterJurisdictionID,
	OrganizationID,
	OrganizationName,
	BaselineLoadingRate,
	IsNull(Cast(IsFullTrashCapture as bit), 0) as IsFullTrashCapture,
	PartialTrashCaptureEffectivenessPercentage,
	PriorityLandUseTypeDisplayName as LandUseType,
	PriorityLandUseTypeID,
	Cast(HasBaselineScore as bit) as HasBaselineScore,
	Cast(HasProgressScore as bit) as HasProgressScore,
	Case
		When DelineationIsVerified = 0 then BaselineLoadingRate
		When IsFullTrashCapture = 1 then 2.5
		When BaselineLoadingRate * (1 - PartialTrashCaptureEffectivenessPercentage/100.0) > 2.5 then BaselineLoadingRate * (1 - PartialTrashCaptureEffectivenessPercentage/100.0)
		Else 2.5
	end as CurrentLoadingRate,
	ProgressLoadingRate,
	DelineationIsVerified,
	LastUpdateDate as LastCalculatedDate,
	PriorityLandUseTypeDisplayName,
	OnlandVisualTrashAssessmentAreaID,
	WaterQualityManagementPlanID,
	LandUseBlockID,
	LastUpdateDate
From (
	Select
		TrashGeneratingUnitID as PrimaryKey,
		TrashGeneratingUnitID,
		tbmp.TreatmentBMPID,
		tbmp.TreatmentBMPName,
		tgu.TrashGeneratingUnitGeometry as TrashGeneratingUnitGeometry,
		tgu.StormwaterJurisdictionID,
		o.OrganizationID,
		o.OrganizationName,
		plut.PriorityLandUseTypeDisplayName,
		plut.PriorityLandUseTypeID,
		tgu.OnlandVisualTrashAssessmentAreaID,
		wqmp.WaterQualityManagementPlanID,
		lub.LandUseBlockID,
		Case
			when area.OnlandVisualTrashAssessmentBaselineScoreID is null then 0
			else 1
		end as HasBaselineScore,
		Case
			when area.OnlandVisualTrashAssessmentProgressScoreID is null then 0
			else 1
		end as HasProgressScore,
		IsNull(
			Case
				when scoreBaseline.TrashGenerationRate is null then lub.TrashGenerationRate
				else scoreBaseline.TrashGenerationRate
			end, 0
		) as BaselineLoadingRate,
		case
			when tbmp.TrashCaptureStatusTypeID = 1 or wqmp.TrashCaptureStatusTypeID = 1 then 1
			else 0
		end as IsFullTrashCapture, 
		IsNull(
			case
				when wqmp.TrashCaptureEffectiveness is not null then wqmp.TrashCaptureEffectiveness
				else tbmp.TrashCaptureEffectiveness
			end
		, 0) as PartialTrashCaptureEffectivenessPercentage,
		IsNull(d.IsVerified, 0) as DelineationIsVerified,
		IsNull(
			Case
				when scoreProgress.TrashGenerationRate is null then 
					case
						when scoreBaseline.TrashGenerationRate is null then lub.TrashGenerationRate
						else scoreBaseline.TrashGenerationRate
					end
				else scoreProgress.TrashGenerationRate
			end, 0
		) as ProgressLoadingRate,
		tgu.LastUpdateDate
	From
		dbo.TrashGeneratingUnit tgu
		left join dbo.LandUseBlock lub
			on tgu.LandUseBlockID = lub.LandUseBlockID
		left join dbo.OnlandVisualTrashAssessmentArea area
			on tgu.OnlandVisualTrashAssessmentAreaID = area.OnlandVisualTrashAssessmentAreaID
		left join dbo.OnlandVisualTrashAssessmentScore scoreBaseline
			on area.OnlandVisualTrashAssessmentBaselineScoreID = scoreBaseline.OnlandVisualTrashAssessmentScoreID
		left join dbo.vOnlandVisualTrashAssessmentAreaProgress areaProgress
			on tgu.OnlandVisualTrashAssessmentAreaID = areaProgress.OnlandVisualTrashAssessmentAreaID
		left join dbo.OnlandVisualTrashAssessmentScore scoreProgress
			on areaProgress.OnlandVisualTrashAssessmentScoreID = scoreProgress.OnlandVisualTrashAssessmentScoreID
		left join dbo.Delineation d
			on tgu.DelineationID = d.DelineationID
		left join dbo.TreatmentBMP tbmp
			on d.TreatmentBMPID = tbmp.TreatmentBMPID
		left join dbo.WaterQualityManagementPlan wqmp
			on tgu.WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID
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
		and (lub.TrashGenerationRate is not null or scoreBaseline.TrashGenerationRate is not null)
		and lub.PermitTypeID = 1
) subq
) subq2
GO

Drop View If Exists dbo.vGeoServerTrashGeneratingUnitLoad
GO

Create View dbo.vGeoServerTrashGeneratingUnitLoad
as
Select
	*
from dbo.vTrashGeneratingUnitLoadStatistic