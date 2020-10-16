INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel) 
VALUES 
(114, 'DelineationStatus', N'Delineation Status', N'Indicates whether the delineation is verified or not.', 1)

INSERT [dbo].[FieldDefinitionData] (FieldDefinitionID) 
VALUES 
(114)


update dbo.FieldDefinitionData
set FieldDefinitionLabel = null
where FieldDefinitionID in (106, 107, 108)
