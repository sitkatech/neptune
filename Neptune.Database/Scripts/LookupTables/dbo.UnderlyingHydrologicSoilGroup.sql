MERGE INTO dbo.UnderlyingHydrologicSoilGroup AS Target
USING (VALUES
(1, 'A', 'A'),
(2, 'B', 'B'),
(3, 'C', 'C'),
(4, 'D', 'D'),
(5, 'Liner', 'Liner')
)
AS Source (UnderlyingHydrologicSoilGroupID, UnderlyingHydrologicSoilGroupName, UnderlyingHydrologicSoilGroupDisplayName)
ON Target.UnderlyingHydrologicSoilGroupID = Source.UnderlyingHydrologicSoilGroupID
WHEN MATCHED THEN
UPDATE SET
	UnderlyingHydrologicSoilGroupName = Source.UnderlyingHydrologicSoilGroupName,
	UnderlyingHydrologicSoilGroupDisplayName = Source.UnderlyingHydrologicSoilGroupDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (UnderlyingHydrologicSoilGroupID, UnderlyingHydrologicSoilGroupName, UnderlyingHydrologicSoilGroupDisplayName)
	VALUES (UnderlyingHydrologicSoilGroupID, UnderlyingHydrologicSoilGroupName, UnderlyingHydrologicSoilGroupDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;