create table dbo.TreatmentBMPAssessmentPhoto (
	-- PK
	TreatmentBMPAssessmentPhotoID int not null identity(1, 1) constraint PK_TreatmentBMPAssessmentPhoto_TreatmentBMPAssessmentPhotoID primary key,

	-- FKs
	TenantID int not null constraint FK_TreatmentBMPAssessmentPhoto_Tenant_TenantID foreign key references dbo.Tenant(TenantID),
	FileResourceID int not null constraint FK_TreatmentBMPAssessmentPhoto_FileResource_FileResourceID foreign key references dbo.FileResource(FileResourceID),
	TreatmentBMPAssessmentID int not null constraint FK_TreatmentBMPAssessmentPhoto_TreatmentBMPAssessment_TreatmentBMPAssessmentID foreign key references dbo.TreatmentBMPAssessment(TreatmentBMPAssessmentID),

	-- Other attributes
	Caption varchar(1000) not null,

	-- Double FKs
	constraint FK_TreatmentBMPAssessmentPhoto_FileResource_FileResourceID_TenantID foreign key (FileResourceID, TenantID) references dbo.FileResource(FileResourceID, TenantID),
	constraint FK_TreatmentBMPAssessmentPhoto_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TenantID foreign key (TreatmentBMPAssessmentID, TenantID) references dbo.TreatmentBMPAssessment(TreatmentBMPAssessmentID, TenantID)
)

