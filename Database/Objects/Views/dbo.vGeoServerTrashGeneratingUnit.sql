Drop view if exists dbo.vGeoServerTrashGeneratingUnit
Go

Create view dbo.vGeoServerTrashGeneratingUnit as
select
	TrashGeneratingUnit4326ID,
	tgu.StormwaterJurisdictionID,
	o.OrganizationID,
	o.OrganizationName,
	case
		when tbmp.TrashCaptureStatusTypeID = 1 or wqmp.TrashCaptureStatusTypeID = 1 then 'Full'
		when tbmp.TrashCaptureStatusTypeID = 2 or wqmp.TrashCaptureStatusTypeID = 2 then 'Partial'
		when tbmp.TrashCaptureStatusTypeID = 3 or wqmp.TrashCaptureStatusTypeID = 3 then 'None'
		else 'NotProvided'
	end as TrashCaptureStatus,
	case when ovtaad.MostRecentAssessmentScore is null then 'NotProvided' else ovtaad.MostRecentAssessmentScore end as AssessmentScore,
	Case when tgu.LandUseBlockID is null then 0 when plut.PriorityLandUseTypeName = 'ALU' then 0 else 1 end as IsPriorityLandUse, -- ALUs are not PLUs
	Case when tgu.LandUseBlockID is null then 1 else 0 end as NoDataProvided,
	TrashGeneratingUnit4326Geometry as TrashGeneratingUnitGeometry,
	ovtaad.OnlandVisualTrashAssessmentAreaID,
	tbmp.TreatmentBMPID,
	tbmp.TreatmentBMPName,
	wqmp.WaterQualityManagementPlanID,
	wqmp.WaterQualityManagementPlanName,
	plut.PriorityLandUseTypeDisplayName as LandUseType,
	tgu.LastUpdateDate as LastCalculatedDate
from dbo.TrashGeneratingUnit4326 tgu
	left join 
    (
        Select
	        a.OnlandVisualTrashAssessmentAreaID,
	        q.CompletedDate as MostRecentAssessmentDate,
	        Score.OnlandVisualTrashAssessmentScoreDisplayName as MostRecentAssessmentScore
        from dbo.OnlandVisualTrashAssessmentArea a
	        inner join (
		        Select
			        ovta.OnlandVisualTrashAssessmentID,
			        ovta.OnlandVisualTrashAssessmentAreaID,
			        ovta.OnlandVisualTrashAssessmentScoreID,
			        ovta.CompletedDate,
			        rownumber = Row_Number() over (partition by ovta.OnlandVisualTrashAssessmentAreaID order by ovta.CompletedDate desc)
		        from dbo.OnlandVisualTrashAssessment ovta
		        where CompletedDate is not null
	        ) q 
		        on a.OnlandVisualTrashAssessmentAreaID = q.OnlandVisualTrashAssessmentAreaID
	        join  dbo.OnlandVisualTrashAssessmentScore score
		        on score.OnlandVisualTrashAssessmentScoreID = q.OnlandVisualTrashAssessmentScoreID
	        where rownumber = 1
    ) ovtaad
		on tgu.OnlandVisualTrashAssessmentAreaID = ovtaad.OnlandVisualTrashAssessmentAreaID
	left join dbo.Delineation d
		on tgu.DelineationID = d.DelineationID
	left join dbo.TreatmentBMP tbmp
		on d.TreatmentBMPID = tbmp.TreatmentBMPID
	left join dbo.TrashCaptureStatusType tcs
		on tcs.TrashCaptureStatusTypeID = tbmp.TrashCaptureStatusTypeID
	left join dbo.LandUseBlock lub
		on lub.LandUseBlockID = tgu.LandUseBlockID
	left join dbo.PriorityLandUseType plut
		on lub.PriorityLandUseTypeID = plut.PriorityLandUseTypeID
	left join dbo.StormwaterJurisdiction sj
		on tgu.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
	left join dbo.Organization o
		on o.OrganizationID = sj.OrganizationID
	left join dbo.WaterQualityManagementPlan wqmp
		on tgu.WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID
--WHERE tgu.TrashGeneratingUnit4326Geometry.STGeometryType() in ('POLYGON', 'MULTIPOLYGON')
Go