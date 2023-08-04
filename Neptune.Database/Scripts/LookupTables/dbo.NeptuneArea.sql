MERGE INTO dbo.NeptuneArea AS Target
USING (VALUES
(1, 'Trash', 'Trash Module', 30, 1),
(2, 'OCStormwaterTools', 'Inventory Module', 10, 1),
(3, 'Modeling', 'Modeling Module', 20, 1),
(4, 'Planning', 'Planning Module', 40, 1)
)
AS Source (NeptuneAreaID, NeptuneAreaName, NeptuneAreaDisplayName, SortOrder, ShowOnPrimaryNavigation)
ON Target.NeptuneAreaID = Source.NeptuneAreaID
WHEN MATCHED THEN
UPDATE SET
	NeptuneAreaName = Source.NeptuneAreaName,
	NeptuneAreaDisplayName = Source.NeptuneAreaDisplayName,
	SortOrder = Source.SortOrder,
	ShowOnPrimaryNavigation = Source.ShowOnPrimaryNavigation
WHEN NOT MATCHED BY TARGET THEN
	INSERT (NeptuneAreaID, NeptuneAreaName, NeptuneAreaDisplayName, SortOrder, ShowOnPrimaryNavigation)
	VALUES (NeptuneAreaID, NeptuneAreaName, NeptuneAreaDisplayName, SortOrder, ShowOnPrimaryNavigation)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;