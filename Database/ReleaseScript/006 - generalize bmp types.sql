alter table dbo.TreatmentBMPType add TreatmentBMPTypeDescription varchar(1000) not null

alter table dbo.TreatmentBMPTypeObservationType add OverrideAssessmentScoreIfFailing bit not null
alter table dbo.TreatmentBMPTypeObservationType alter column AssessmentScoreWeight decimal(9,6) null

go

alter table dbo.TreatmentBMPTypeObservationType add constraint CK_OverrideNotNullIfAssessmentScoreWeightNull CHECK ((AssessmentScoreWeight IS NULL AND OverrideAssessmentScoreIfFailing IS NOT NULL) or (AssessmentScoreWeight IS NOT NULL AND OverrideAssessmentScoreIfFailing IS NOT NULL))

insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(12, 'ManageTreatmentBMPTypesList', 'Manage Treatment BMP Types List', 2),
(13, 'ManageObservationTypeInstructions', 'Manage Observation Type Instructions', 2),
(14, 'ManageObservationTypeObservationInstructions', 'Manage Observation Type Instructions for Observation Instructions', 2),
(15, 'ManageObservationTypeLabelsAndUnitsInstructions', 'Manage Observation Type Labels and Units Instructions', 2),
(16, 'ManageTreatmentBMPTypeInstructions', 'Manage Treatment BMP Type Instructions', 2)

insert into dbo.neptunepage(TenantID, NeptunePageTypeID)
select 
	t.tenantid,
	npt.NeptunePageTypeID
from dbo.neptunepagetype npt
cross join dbo.tenant t
where npt.NeptunePageTypeID in (12, 13, 14, 15, 16)