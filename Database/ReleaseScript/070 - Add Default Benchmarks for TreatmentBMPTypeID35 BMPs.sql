-- create default benchmark records for TreatmentBMPTypeID 35
insert into dbo.TreatmentBMPBenchmarkAndThreshold ([TenantID]
      ,[TreatmentBMPID]
      ,[TreatmentBMPTypeAssessmentObservationTypeID]
      ,[TreatmentBMPTypeID]
      ,[TreatmentBMPAssessmentObservationTypeID]
      ,[BenchmarkValue]
      ,[ThresholdValue])
select 2 as TenantID,
t.TreatmentBMPID,
tta.TreatmentBMPTypeAssessmentObservationTypeID,
tt.TreatmentBMPTypeID,
tta.TreatmentBMPAssessmentObservationTypeID,
0 as BenchmarkValue,
40 as ThresholdValue 
from dbo.TreatmentBMP t join dbo.TreatmentBMPType tt on t.TreatmentBMPTypeID = tt.TreatmentBMPTypeID 
join dbo.TreatmentBMPTypeAssessmentObservationType tta on tt.TreatmentBMPTypeID = tta.TreatmentBMPTypeID and tta.TreatmentBMPAssessmentObservationTypeID = 34 
left join dbo.TreatmentBMPBenchmarkAndThreshold tbbat on t.TreatmentBMPID = tbbat.TreatmentBMPID and tta.TreatmentBMPAssessmentObservationTypeID = tbbat.TreatmentBMPAssessmentObservationTypeID
where t.TreatmentBMPTypeID = 35 and tbbat.TreatmentBMPTypeAssessmentObservationTypeID is null
