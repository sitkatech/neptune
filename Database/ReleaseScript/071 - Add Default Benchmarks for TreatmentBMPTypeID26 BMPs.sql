-- create default benchmark records for TreatmentBMPTypeID 26
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
50 as ThresholdValue 
from dbo.TreatmentBMP t join dbo.TreatmentBMPType tt on t.TreatmentBMPTypeID = tt.TreatmentBMPTypeID 
join dbo.TreatmentBMPTypeAssessmentObservationType tta on tt.TreatmentBMPTypeID = tta.TreatmentBMPTypeID and tta.TreatmentBMPAssessmentObservationTypeID = 33 
left join dbo.TreatmentBMPBenchmarkAndThreshold tbbat on t.TreatmentBMPID = tbbat.TreatmentBMPID and tta.TreatmentBMPAssessmentObservationTypeID = tbbat.TreatmentBMPAssessmentObservationTypeID
where t.TreatmentBMPTypeID = 26 and tbbat.TreatmentBMPTypeAssessmentObservationTypeID is null
