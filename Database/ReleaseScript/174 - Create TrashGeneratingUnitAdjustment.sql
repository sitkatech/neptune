create table dbo.TrashGeneratingUnitAdjustment(
	TrashGeneratingUnitAdjustmentID int not null identity(1,1) constraint PK_TrashGeneratingUnitAdjustment_TrashGeneratingUnitAdjustmentID primary key,
	AdjustedDelineationID int null
		constraint FK_TrashGeneratingUnitAdjustment_Delineation_AdjustedDelineationID_DelineationID
		foreign key references dbo.Delineation(DelineationID),
	AdjustedOnlandVisualTrashAssessmentAreaID int null
		constraint FK_TrashGeneratingUnitAdjustment_OnlandVisualTrashAssessmentArea_AdjustedOnlandVisualTrashAssessmentAreaID_OnlandVisualTrashAsse
		foreign key references dbo.OnlandVisualTrashAssessmentArea(OnlandVisualTrashAssessmentAreaID),
	DeletedGeometry geometry null,
	AdjustmentDate datetime not null,
	AdjustedByPersonID int not null
		constraint FK_TrashGeneratingUnitAdjustment_Person_AdjustedByPersonID_PersonID
		foreign key references dbo.Person(PersonId),
	IsProcessed bit not null,
	ProcessedDate datetime null,
	constraint CK_TrashGeneratingUnitAdjustment_ExclusiveOrDelineationAssessmentDeletedGeometry
		check (
			(AdjustedDelineationID is not null and AdjustedOnlandVisualTrashAssessmentAreaID is null and DeletedGeometry is null)
				OR
			(AdjustedDelineationID is null and AdjustedOnlandVisualTrashAssessmentAreaID is not null and DeletedGeometry is null)
				OR
			(AdjustedDelineationID is null and AdjustedOnlandVisualTrashAssessmentAreaID is null and DeletedGeometry is not null)
		)
)
