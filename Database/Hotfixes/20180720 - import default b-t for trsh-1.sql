-- blast the manually entered defaults
delete from dbo.TreatmentBMPBenchmarkAndThreshold
where TreatmentBMPTypeID = 35 and TreatmentBMPAssessmentObservationTypeID = 34 -- trash screen / material accumulation

-- create default records
insert into TreatmentBMPBenchmarkAndThreshold ([TenantID]
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
40 as ThresholdValue from TreatmentBMP t join TreatmentBMPType tt on t.TreatmentBMPTypeID = tt.TreatmentBMPTypeID join TreatmentBMPTypeAssessmentObservationType tta on tt.TreatmentBMPTypeID = tta.TreatmentBMPTypeID
where tt.TreatmentBMPTypeID = 35 and tta.TreatmentBMPAssessmentObservationTypeID = 34
