delete from dbo.ObservationTypeCollectionMethod
go

INSERT 
dbo.ObservationTypeCollectionMethod (ObservationTypeCollectionMethodID, ObservationTypeCollectionMethodName, ObservationTypeCollectionMethodDisplayName, SortOrder) VALUES
 (1, N'SingleValue', N'Single Value', 10),
 (2, N'MultipleTimeValue', N'Multiple Time Value', 20),
 (3, N'YesNo', N'Yes/No', 30),
 (4, N'PercentValue', N'Percent Value', 40)
