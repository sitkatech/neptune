MERGE INTO dbo.StormwaterJurisdictionPublicWQMPVisibilityType AS Target
USING (VALUES
(1, 'ActiveAndInactive', 'Active and Inactive'),
(2, 'ActiveOnly', 'Active Only'),
(3, 'None', 'None')
)
AS Source (StormwaterJurisdictionPublicWQMPVisibilityTypeID, StormwaterJurisdictionPublicWQMPVisibilityTypeName, StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName)
ON Target.StormwaterJurisdictionPublicWQMPVisibilityTypeID = Source.StormwaterJurisdictionPublicWQMPVisibilityTypeID
WHEN MATCHED THEN
UPDATE SET
	StormwaterJurisdictionPublicWQMPVisibilityTypeName = Source.StormwaterJurisdictionPublicWQMPVisibilityTypeName,
	StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName = Source.StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (StormwaterJurisdictionPublicWQMPVisibilityTypeID, StormwaterJurisdictionPublicWQMPVisibilityTypeName, StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName)
	VALUES (StormwaterJurisdictionPublicWQMPVisibilityTypeID, StormwaterJurisdictionPublicWQMPVisibilityTypeName, StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;