MERGE INTO dbo.StormwaterJurisdictionPublicBMPVisibilityType AS Target
USING (VALUES
(1, 'VerifiedOnly', 'Verified Only'),
(2, 'None', 'None')
)
AS Source (StormwaterJurisdictionPublicBMPVisibilityTypeID, StormwaterJurisdictionPublicBMPVisibilityTypeName, StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName)
ON Target.StormwaterJurisdictionPublicBMPVisibilityTypeID = Source.StormwaterJurisdictionPublicBMPVisibilityTypeID
WHEN MATCHED THEN
UPDATE SET
	StormwaterJurisdictionPublicBMPVisibilityTypeName = Source.StormwaterJurisdictionPublicBMPVisibilityTypeName,
	StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName = Source.StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (StormwaterJurisdictionPublicBMPVisibilityTypeID, StormwaterJurisdictionPublicBMPVisibilityTypeName, StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName)
	VALUES (StormwaterJurisdictionPublicBMPVisibilityTypeID, StormwaterJurisdictionPublicBMPVisibilityTypeName, StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;