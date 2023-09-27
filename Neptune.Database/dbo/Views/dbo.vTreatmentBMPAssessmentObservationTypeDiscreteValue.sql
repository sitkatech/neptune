Create View dbo.vTreatmentBMPAssessmentObservationTypeDiscreteValue
as

select  tbaot.TreatmentBMPAssessmentObservationTypeID, tbaot.TreatmentBMPAssessmentObservationTypeName, tbaot.TreatmentBMPAssessmentObservationTypeSchema, 
        ots.ObservationTypeSpecificationID, ots.ObservationTypeSpecificationName, ots.ObservationTypeSpecificationDisplayName, 
        otcm.ObservationTypeCollectionMethodID, otcm.ObservationTypeCollectionMethodName, otcm.ObservationTypeCollectionMethodDisplayName, otcm.ObservationTypeCollectionMethodDescription,
        AssessmentDescription, BenchmarkDescription, ThresholdDescription, MeasurementUnitLabel, MeasurementUnitTypeID, MinimumNumberOfObservations, MaximumNumberOfObservations, 
        MinimumValueOfObservations, MaximumValueOfObservations, string_agg(p.PropertiesToObserve, ', ') as PropertiesToObserve
from dbo.TreatmentBMPAssessmentObservationType tbaot
join dbo.ObservationTypeSpecification ots on tbaot.ObservationTypeSpecificationID = ots.ObservationTypeSpecificationID
join dbo.ObservationTypeCollectionMethod otcm on ots.ObservationTypeCollectionMethodID = otcm.ObservationTypeCollectionMethodID
cross apply openjson(TreatmentBMPAssessmentObservationTypeSchema) 
with
(
    PropertiesToObserve nvarchar(max) '$.PropertiesToObserve' as json,
    AssessmentDescription  varchar(1000) '$.AssessmentDescription',
    BenchmarkDescription  varchar(1000) '$.BenchmarkDescription',
    ThresholdDescription  varchar(1000) '$.ThresholdDescription',
    MeasurementUnitLabel  varchar(1000) '$.MeasurementUnitLabel',
    MeasurementUnitTypeID  int '$.MeasurementUnitTypeID',
    MinimumNumberOfObservations  int '$.MinimumNumberOfObservations',
    MaximumNumberOfObservations  int '$.MaximumNumberOfObservations',
    MinimumValueOfObservations  int '$.MinimumValueOfObservations',
    MaximumValueOfObservations  int '$.MaximumValueOfObservations'
) as tbaots
cross apply openjson(tbaots.PropertiesToObserve) 
with
(
    PropertiesToObserve varchar(1000) '$'
) as p
where otcm.ObservationTypeCollectionMethodID = 1 -- discrete value
group by tbaot.TreatmentBMPAssessmentObservationTypeID, tbaot.TreatmentBMPAssessmentObservationTypeName, tbaot.TreatmentBMPAssessmentObservationTypeSchema, 
        ots.ObservationTypeSpecificationID, ots.ObservationTypeSpecificationName, ots.ObservationTypeSpecificationDisplayName, 
        otcm.ObservationTypeCollectionMethodID, otcm.ObservationTypeCollectionMethodName, otcm.ObservationTypeCollectionMethodDisplayName, otcm.ObservationTypeCollectionMethodDescription,
        AssessmentDescription, BenchmarkDescription, ThresholdDescription, MeasurementUnitLabel, MeasurementUnitTypeID, MinimumNumberOfObservations, MaximumNumberOfObservations, 
        MinimumValueOfObservations, MaximumValueOfObservations

GO