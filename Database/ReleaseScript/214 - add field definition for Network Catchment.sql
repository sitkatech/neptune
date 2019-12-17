INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel) 
VALUES 
(76, N'NetworkCatchment', N'Network Catchment', N'', 1)

INSERT dbo.FieldDefinitionData (FieldDefinitionID)
VALUES
(76)