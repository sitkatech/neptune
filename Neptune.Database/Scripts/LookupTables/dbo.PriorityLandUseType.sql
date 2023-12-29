MERGE INTO dbo.PriorityLandUseType AS Target
USING (VALUES
(1,'Commercial','Commercial', '#c2fbfc'),
(2,'HighDensityResidential','High Density Residential','#c0d6fc'),
(3,'Industrial','Industrial','#b4fcb3'),
(4,'MixedUrban','Mixed Urban','#fcb6b9'),
(5,'CommercialRetail','Commercial-Retail','#f2cafc'),
(6,'Public Transportation Stations','Public Transportation Stations','#fcd6b6'),
(7, 'ALU', 'ALU', '#ffffff')
)
AS Source (PriorityLandUseTypeID, PriorityLandUseTypeName, PriorityLandUseTypeDisplayName, MapColorHexCode)
ON Target.PriorityLandUseTypeID = Source.PriorityLandUseTypeID
WHEN MATCHED THEN
UPDATE SET
	PriorityLandUseTypeName = Source.PriorityLandUseTypeName,
	PriorityLandUseTypeDisplayName = Source.PriorityLandUseTypeDisplayName,
	MapColorHexCode = Source.MapColorHexCode
WHEN NOT MATCHED BY TARGET THEN
	INSERT (PriorityLandUseTypeID, PriorityLandUseTypeName, PriorityLandUseTypeDisplayName, MapColorHexCode)
	VALUES (PriorityLandUseTypeID, PriorityLandUseTypeName, PriorityLandUseTypeDisplayName, MapColorHexCode)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;