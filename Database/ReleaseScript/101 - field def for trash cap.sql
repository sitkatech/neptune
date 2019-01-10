INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel) 
values (57, N'TrashCaptureStatus', N'Trash Capture Status', N'Indicates the ability of this BMP to capture trash.', 1)

Insert dbo.FieldDefinitionData([FieldDefinitionID], [FieldDefinitionDataValue], [FieldDefinitionLabel])
values (57, null, null)