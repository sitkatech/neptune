INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel) 
VALUES 
(62, 'SizingBasis', 'Sizing Basis', 'Indicates whether this BMP is sized for full trash capture, water quality improvement, or otherwise.', 1)

Insert into dbo.FieldDefinitionData (FieldDefinitionID)
values (62)