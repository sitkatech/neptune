create table dbo.FieldVisitType(
FieldVisitTypeID int not null constraint PK_FieldVisitType_FieldVisitTypeID primary key,
FieldVisitTypeName varchar(40) not null constraint AK_FieldVisitType_FieldVisitTypeName unique,
FieldVisitTypeDisplayName varchar(40) not null )

insert into dbo.FieldVisitType(FieldVisitTypeID, FieldVisitTypeName, FieldVisitTypeDisplayName)
values
(1, 'DryWeather', 'Dry Weather'),
(2, 'WetWeather', 'Wet Weather'),
(3, 'PostStormAssessment', 'Post-Storm Assessment')