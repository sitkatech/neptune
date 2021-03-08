DECLARE @constructedWetlandID INT;
DECLARE @naturalTreatmentSystemsID INT;

SELECT @constructedWetlandID = TreatmentBMPTypeID FROM TreatmentBMPType WHERE TreatmentBMPTypeName = 'Constructed Wetland'

INSERT INTO dbo.TreatmentBMPType (
	[TreatmentBMPTypeName],
	[TreatmentBMPTypeDescription],
	[IsAnalyzedInModelingModule],
	[TreatmentBMPModelingTypeID]
)
SELECT
	'Natural Treatment Systems',
	TreatmentBMPTypeDescription,
	IsAnalyzedInModelingModule,
	TreatmentBMPModelingTypeID
FROM dbo.TreatmentBMPType
WHERE TreatmentBMPTypeID = @constructedWetlandID

SELECT @naturalTreatmentSystemsID = TreatmentBMPTypeID from TreatmentBMPType where TreatmentBMPTypeName = 'Natural Treatment Systems'

INSERT INTO dbo.TreatmentBMPTypeCustomAttributeType ([TreatmentBMPTypeID], [CustomAttributeTypeID], [SortOrder])
SELECT @naturalTreatmentSystemsID, CustomAttributeTypeID, SortOrder
FROM dbo.TreatmentBMPTypeCustomAttributeType
WHERE TreatmentBMPTYpeID = @constructedWetlandID

INSERT INTO dbo.TreatmentBMPTypeAssessmentObservationType (
	[TreatmentBMPTypeID],
	[TreatmentBMPAssessmentObservationTypeID],
	[AssessmentScoreWeight],
	[DefaultThresholdValue],
	[DefaultBenchmarkValue],
	[OverrideAssessmentScoreIfFailing],
	[SortOrder])
SELECT 
	@naturalTreatmentSystemsID,
	[TreatmentBMPAssessmentObservationTypeID],
	[AssessmentScoreWeight],
	[DefaultThresholdValue],
	[DefaultBenchmarkValue],
	[OverrideAssessmentScoreIfFailing],
	[SortOrder]
FROM dbo.TreatmentBMPTypeAssessmentObservationType
WHERE TreatmentBMPTypeID = @constructedWetlandID