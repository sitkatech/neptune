delete from dbo.StormwaterAssessmentType
go

INSERT 
dbo.StormwaterAssessmentType (StormwaterAssessmentTypeID, StormwaterAssessmentTypeName, StormwaterAssessmentTypeDisplayName, SortOrder) VALUES
 (1, N'Regular', N'Regular', 10),
 (2, N'Validation', N'Validation', 20)
