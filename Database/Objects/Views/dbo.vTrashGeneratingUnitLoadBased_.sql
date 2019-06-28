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

/*
load reduction via partial capture
Also fairly straightforward, as above, but with a trash capture effectiveness to measure how "partial" the partials are
*/
Create view dbo.vTrashGeneratingUnitLoadBasedPartialCapture
as
Select
	PrimaryKey,
	TrashGeneratingUnitID,
	StormwaterJurisdictionID,
	Area,
	cast (BaselineLoadingRate as decimal) as BaselineLoadingRate,
	Case
		when ActualLoadingAfterTrashCapture < 2.5 then 2.5
		else ActualLoadingAfterTrashCapture
	end as ActualLoadingAfterTrashCapture
from
(Select
	TrashGeneratingUnitID as PrimaryKey,
	TrashGeneratingUnitID,
	tgu.StormwaterJurisdictionID,
	IsNull(TrashGeneratingUnitGeometry.STArea(),0) as Area,
	IsNull(Case
		when score.TrashGenerationRate is null then lub.TrashGenerationRate
		else score.TrashGenerationRate
	end,0) as BaselineLoadingRate,
	IsNull(Case
		when score.TrashGenerationRate is null then lub.TrashGenerationRate
		else score.TrashGenerationRate
	end,0) * (1 - 
	IsNull(Case 
		when tbmp.TrashCaptureEffectiveness is null then 0.0
		else cast(tbmp.TrashCaptureEffectiveness as decimal)/100
	end,0)) as ActualLoadingAfterTrashCapture
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
	left join dbo.PriorityLandUseType plut
		on lub.PriorityLandUseTypeID = plut.PriorityLandUseTypeID
	left join dbo.StormwaterJurisdiction sj
		on tgu.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
	left join dbo.Organization o
		on o.OrganizationID = sj.OrganizationID
Where tbmp.TrashCaptureStatusTypeID = 2
	and tgu.LandUseBlockID is not null
	and d.IsVerified = 1
	and (lub.TrashGenerationRate is not null or score.TrashGenerationRate is not null)
	) sub
GO

/*
 load reduction via OVTAs
 Difference betwen baseline and progress scores
 Do we need to exclude the fully-and-partially-captured areas?
 */
Create view dbo.vTrashGeneratingUnitLoadBasedTrashAssessment
as
Select
	TrashGeneratingUnitID as PrimaryKey,
	TrashGeneratingUnitID,
	tgu.StormwaterJurisdictionID,
	IsNull(TrashGeneratingUnitGeometry.STArea(), 0) as Area,
	IsNull(Case
		when pscore.TrashGenerationRate is null then lub.TrashGenerationRate
		else pscore.TrashGenerationRate
	end,0) as ProgressLoadingRate,
	IsNull(Case
		when bscore.TrashGenerationRate is null then lub.TrashGenerationRate
		else bscore.TrashGenerationRate
	end,0) as BaselineLoadingRate
From
	dbo.TrashGeneratingUnit tgu left join LandUseBlock lub
		on tgu.LandUseBlockID = lub.LandUseBlockID
	left join dbo.vOnlandVisualTrashAssessmentAreaProgress ovta
		on tgu.OnlandVisualTrashAssessmentAreaID = ovta.OnlandVisualTrashAssessmentAreaID
	left join OnlandVisualTrashAssessmentArea area
		on tgu.OnlandVisualTrashAssessmentAreaID = area.OnlandVisualTrashAssessmentAreaID
	left join OnlandVisualTrashAssessmentScore bscore
		on area.OnlandVisualTrashAssessmentBaselineScoreID = bscore.OnlandVisualTrashAssessmentScoreID
	left join OnlandVisualTrashAssessmentScore pscore
		on ovta.OnlandVisualTrashAssessmentScoreID = pscore.OnlandVisualTrashAssessmentScoreID
	left join dbo.Delineation d
		on tgu.DelineationID = d.DelineationID
	left join dbo.TreatmentBMP tbmp
		on d.TreatmentBMPID = tbmp.TreatmentBMPID
	left join dbo.TrashCaptureStatusType tcs
		on tcs.TrashCaptureStatusTypeID = tbmp.TrashCaptureStatusTypeID
	left join dbo.PriorityLandUseType plut
		on lub.PriorityLandUseTypeID = plut.PriorityLandUseTypeID
	left join dbo.StormwaterJurisdiction sj
		on tgu.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
	left join dbo.Organization o
		on o.OrganizationID = sj.OrganizationID
Where tgu.LandUseBlockID is not null
	and area.OnlandVisualTrashAssessmentBaselineScoreID is not null
	and pscore.TrashGenerationRate is not null
	and (lub.TrashGenerationRate is not null or bscore.TrashGenerationRate is not null)
GO

/*
target load reduction
This is the difference between the actual loading and what the loading would be if every PLU was full capture
*/
Create view dbo.vTrashGeneratingUnitLoadBasedTargetReduction
as
Select
	TrashGeneratingUnitID as PrimaryKey,
	TrashGeneratingUnitID,
	tgu.StormwaterJurisdictionID,
	IsNull(TrashGeneratingUnitGeometry.STArea(), 0) as Area,
	IsNull(Case
		when score.TrashGenerationRate is null then lub.TrashGenerationRate
		else score.TrashGenerationRate
	end,0) as BaselineLoadingRate

From dbo.TrashGeneratingUnit tgu left join LandUseBlock lub
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
	left join dbo.PriorityLandUseType plut
		on lub.PriorityLandUseTypeID = plut.PriorityLandUseTypeID
	left join dbo.StormwaterJurisdiction sj
		on tgu.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
	left join dbo.Organization o
		on o.OrganizationID = sj.OrganizationID
Where
	lub.LandUseBlockID is not null
	and lub.PriorityLandUseTypeID <> 7
	and (lub.TrashGenerationRate is not null or score.TrashGenerationRate is not null)
GO