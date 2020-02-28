Create table dbo.LoadGeneratingUnit(
LoadGeneratingUnitID int not null identity(1,1) constraint PK_LoadGeneratingUnit_LoadGeneratingUnitID primary key,
LoadGeneratingUnitGeometry geometry not null,
LSPCBasinID int null constraint FK_LoadGeneratingUnit_LSPCBasin_LSPCBasinID foreign key references dbo.LSPCBasin(LSPCBasinID),
RegionalSubbasinID int null constraint FK_LoadGeneratingUnit_RegionalSubbasin_RegionalSubbasinID foreign key references dbo.RegionalSubbasin(RegionalSubbasinID),
DelineationID int null constraint FK_LoadGeneratingUnit_Delineation_DelineationID foreign key references dbo.Delineation(DelineationID),
WaterQualityManagementPlanID int null constraint FK_LoadGeneratingUnit_WaterQualityManagementPlan_WaterQualityManagementPlanID foreign key references dbo.WaterQualityManagementPlan(WaterQualityManagementPlanID)
)

INSERT INTO [dbo].[geometry_columns]
           ([f_table_catalog]
           ,[f_table_schema]
           ,[f_table_name]
           ,[f_geometry_column]
           ,[coord_dimension]
           ,[srid]
           ,[geometry_type])
     VALUES
('Neptune', 'dbo', 'LoadGeneratingUnit', 'LoadGeneratingUnitGeometry', 2, 2771, 'MULTIPOLYGON')