Create View dbo.vTreatmentBMPAssessmentObservationTypePassFail
as

select  tbaot.TreatmentBMPAssessmentObservationTypeID, tbaot.TreatmentBMPAssessmentObservationTypeName, tbaot.TreatmentBMPAssessmentObservationTypeSchema, 
        ots.ObservationTypeSpecificationID, ots.ObservationTypeSpecificationName, ots.ObservationTypeSpecificationDisplayName, 
        otcm.ObservationTypeCollectionMethodID, otcm.ObservationTypeCollectionMethodName, otcm.ObservationTypeCollectionMethodDisplayName, otcm.ObservationTypeCollectionMethodDescription,
        AssessmentDescription, PassingScoreLabel, FailingScoreLabel, string_agg(p.PropertiesToObserve, ', ') as PropertiesToObserve
from dbo.TreatmentBMPAssessmentObservationType tbaot
join dbo.ObservationTypeSpecification ots on tbaot.ObservationTypeSpecificationID = ots.ObservationTypeSpecificationID
join dbo.ObservationTypeCollectionMethod otcm on ots.ObservationTypeCollectionMethodID = otcm.ObservationTypeCollectionMethodID
cross apply openjson(TreatmentBMPAssessmentObservationTypeSchema) 
with
(
    PropertiesToObserve nvarchar(max) '$.PropertiesToObserve' as json,
    AssessmentDescription  varchar(1000) '$.AssessmentDescription',
    PassingScoreLabel  varchar(1000) '$.PassingScoreLabel',
    FailingScoreLabel  varchar(1000) '$.FailingScoreLabel'
) as tbaots
cross apply openjson(tbaots.PropertiesToObserve) 
with
(
    PropertiesToObserve varchar(1000) '$'
) as p
where otcm.ObservationTypeCollectionMethodID = 3 -- pass fail
group by tbaot.TreatmentBMPAssessmentObservationTypeID, tbaot.TreatmentBMPAssessmentObservationTypeName, tbaot.TreatmentBMPAssessmentObservationTypeSchema, 
        ots.ObservationTypeSpecificationID, ots.ObservationTypeSpecificationName, ots.ObservationTypeSpecificationDisplayName, 
        otcm.ObservationTypeCollectionMethodID, otcm.ObservationTypeCollectionMethodName, otcm.ObservationTypeCollectionMethodDisplayName, otcm.ObservationTypeCollectionMethodDescription,
        AssessmentDescription, PassingScoreLabel, FailingScoreLabel

GO