ALTER TABLE dbo.DelineationStaging DROP CONSTRAINT AK_DelineationStaging_TreatmentBMPName_StormwaterJurisdictionID
GO

ALTER TABLE dbo.DelineationStaging ADD  CONSTRAINT AK_DelineationStaging_TreatmentBMPName_StormwaterJurisdictionID UNIQUE NONCLUSTERED 
(
	TreatmentBMPName,
	StormwaterJurisdictionID,
	UploadedByPersonID 
)

