CREATE UNIQUE NONCLUSTERED INDEX AK_TreatmentBMP_DelineationID
ON dbo.TreatmentBMP(DelineationID)
WHERE DelineationID IS NOT NULL