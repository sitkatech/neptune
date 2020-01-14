INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel) 
VALUES 
(71, N'LandUse', N'Land Use', N'', 1),
(72, N'Area', N'Area', N'', 1),
(73, N'ImperviousCover', N'Impervious Cover', N'', 1),
(74, N'GrossArea', N'Gross Area', N'', 1),
(75, N'LandUseStatistics', N'Land Use Statistics', N'', 1)

GO

Insert dbo.FieldDefinitionData(FieldDefinitionID) values
(71),(72),(73),(74),(75)