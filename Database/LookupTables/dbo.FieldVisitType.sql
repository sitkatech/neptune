delete from dbo.FieldVisitType

insert into dbo.FieldVisitType(FieldVisitTypeID, FieldVisitTypeName, FieldVisitTypeDisplayName)
values
(1, 'DryWeather', 'Dry Weather'),
(2, 'WetWeather', 'Wet Weather'),
(3, 'PostStormAssessment', 'Post-Storm Assessment')