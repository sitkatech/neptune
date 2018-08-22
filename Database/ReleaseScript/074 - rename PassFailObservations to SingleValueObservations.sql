update dbo.TreatmentBMPObservation
set ObservationData = replace(ObservationData, 'PassFailObservations', 'SingleValueObservations')
where ObservationData like '%PassFailObservations%'