delete from dbo.ObservationTypeSpecification
go

INSERT 
dbo.ObservationTypeSpecification (ObservationTypeSpecificationID, ObservationTypeSpecificationName, ObservationTypeSpecificationDisplayName, SortOrder, ObservationTypeCollectionMethodID, ObservationTargetTypeID, ObservationThresholdTypeID) VALUES
(1, N'Regular', N'Regular', 10, 1, 1, 1)
