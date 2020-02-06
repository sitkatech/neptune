INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel) 
VALUES 
(110, 'Watershed', 'Watershed', '', 1),
(111, 'DesignStormwaterDepth', 'Design Stormwater Depth', '', 1)


INSERT [dbo].[FieldDefinitionData] (FieldDefinitionID) 
VALUES 
(110),
(111)
