Create View dbo.vTreatmentBMPObservation
as

select tbo.TreatmentBMPObservationID, tbo.TreatmentBMPAssessmentID, tbo.TreatmentBMPAssessmentObservationTypeID, obsvals.ObservationValue, obsvals.Notes
from dbo.TreatmentBMPObservation tbo
cross apply openjson(tbo.ObservationData) 
with
(
    Observations nvarchar(max) '$.SingleValueObservations' AS JSON
) as obs
cross apply openjson(obs.Observations)
with
(
    ObservationValue varchar(1000) '$.ObservationValue',
    Notes  varchar(1000) '$.Notes'
) as obsvals


GO