delete from dbo.ObservationValueType
go

INSERT dbo.ObservationValueType (ObservationValueTypeID, ObservationValueTypeName, ObservationValueTypeDisplayName, SortOrder)
VALUES (1, 'integerType', 'Integer', 10),
(2, 'stringType', 'Test', 20),
(3, 'booleanType', 'Yes/No', 30),
(4, 'floatType', 'Decimal', 40)
