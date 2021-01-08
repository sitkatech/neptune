INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel) 
VALUES 
(116, 'ModeledPerformance', N'Modeled Performance', N'If the Detailed modeling approach is used, then this table represents the portion of the site that drains to inventoried BMPs; If the Simplified modeling approach is used, then this table represents the inventoried parcel(s)', 1)

INSERT [dbo].[FieldDefinitionData] (FieldDefinitionID) 
VALUES 
(116)

