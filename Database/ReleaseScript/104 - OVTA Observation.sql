Create Table Dbo.OnlandVisualTrashAssessmentObservation(
OnlandVisualTrashAssessmentObservationID int not null identity (1,1)
	constraint PK_OnlandVisualTrashAssessmentObservation_OnlandVisualTrashAssessmentObservationID
		primary key,
OnlandVisualTrashAssessmentID int not null
	constraint FK_OnlandVisualTrashAssessmentObservation_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID
		foreign key references dbo.OnlandVisualTrashAssessment(OnlandVisualTrashAssessmentID),
LocationPoint geometry not null,
Note varchar(500) null,
ObservationDatetime datetime not null,
Constraint CK_LocationIsAPoint check (LocationPoint.STGeometryType() = 'Point')
)