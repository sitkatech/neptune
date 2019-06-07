Drop View If Exists dbo.vMostRecentTreatmentBMPAssessment
Go

Create View dbo.vMostRecentTreatmentBMPAssessment
as
select
	tb.TreatmentBMPID as PrimaryKey,
	tb.TreatmentBMPID,
	TreatmentBMPName,
	ojur.OrganizationName as StormwaterJurisdictionName,
	sj.StormwaterJurisdictionID as StormwaterJurisdictionID,
	oown.OrganizationName as OwnerOrganizationName,
	OwnerOrganizationID as OwnerOrganizationID,
	RequiredFieldVisitsPerYear,
	NumberOfAssessments,
	LastAssessmentDate,
	tbarecent.TreatmentBMPAssessmentID as LastAssessmentID,
	tbarecent.AssessmentScore
	--,	tbo.ObservationData
From
	dbo.TreatmentBMP tb
	join (
		select 
			tbasub.TreatmentBMPID,
			AssessmentScore,
			TreatmentBMPAssessmentID,
			ROW_NUMBER() over (partition by tbasub.TreatmentBMPID order by fvsub.VisitDate desc)  as TbaRecentRowNumber
		from
			dbo.TreatmentBMPAssessment tbasub
			join dbo.FieldVisit fvsub
				on tbasub.FieldVisitID = fvsub.FieldVisitID
	) tbarecent
		on tb.TreatmentBMPID = tbarecent.TreatmentBMPID 
	join dbo.StormwaterJurisdiction sj
		on tb.StormwaterJurisdictionID = sj.StormwaterJurisdictionID
	join dbo.Organization ojur
		on sj.OrganizationID = ojur.OrganizationID
	join dbo.Organization oown
		on tb.OwnerOrganizationID = oown.OrganizationID
	join (
		select
			tbasub.TreatmentBMPID, count(*) as NumberOfAssessments, Max(fvsub.VisitDate) as LastAssessmentDate
		from
			dbo.TreatmentBMPAssessment tbasub join dbo.FieldVisit fvsub
				on tbasub.FieldVisitID = fvsub.FieldVisitID
		group by tbasub.TreatmentBMPID
	) tbacount
		on tb.TreatmentBMPID = tbacount.TreatmentBMPID
where TbaRecentRowNumber = 1
GO