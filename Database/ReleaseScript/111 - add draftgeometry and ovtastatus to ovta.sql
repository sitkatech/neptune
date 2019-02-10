Create Table dbo.OnlandVisualTrashAssessmentStatus(
OnlandVisualTrashAssessmentStatusID int not null constraint PK_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusID primary key,
OnlandVisualTrashAssessmentStatusName varchar(20) not null constraint AK_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusName unique,
OnlandVisualTrashAssessmentStatusDisplayName varchar(20) not null constraint AK_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusDisplayName unique
)

Alter Table dbo.OnlandVisualTrashAssessment
Add OnlandVisualTrashAssessmentStatusID int not null constraint FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusID
	Foreign Key references dbo.OnlandVisualTrashAssessmentStatus(OnlandVisualTrashAssessmentStatusID),
	DraftGeometry geometry null,
	IsDraftGeometryManuallyRefined bit null
go

Alter Table dbo.OnlandVisualTrashAssessment
Add Constraint CK_OnlandVisualTrashAssessment_AssessmentCannotHaveDraftGeometryWhenComplete
check (not (DraftGeometry is not null and OnlandVisualTrashAssessmentStatusID = 2)) 

Alter Table dbo.OnlandVisualTrashAssessment
Add Constraint CK_OnlandVisualTrashAssessment_AssessmentCannotHaveDraftGeometryAndOfficialArea
check (not (DraftGeometry is not null and OnlandVisualTrashAssessmentAreaID is not null)) 

Alter Table dbo.OnlandVisualTrashAssessment
Add Constraint CK_OnlandVisualTrashAssessment_NewAssessmentCannotHaveOfficialAreaUnlessComplete
check ((OnlandVisualTrashAssessmentAreaID is not null and (AssessingNewArea = 0 or OnlandVisualTrashAssessmentStatusID = 2)) 
	or (OnlandVisualTrashAssessmentAreaID is null and AssessingNewArea = 1))