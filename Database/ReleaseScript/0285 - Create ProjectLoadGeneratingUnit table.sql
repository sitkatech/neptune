Create table dbo.ProjectLoadGeneratingUnit(
ProjectLoadGeneratingUnitID int not null identity(1,1) constraint PK_ProjectLoadGeneratingUnit_ProjectLoadGeneratingUnitID primary key,
ProjectLoadGeneratingUnitGeometry geometry not null,
ProjectID int not null constraint FK_ProjectLoadGeneratingUnit_Project_ProjectID foreign key references dbo.Project (ProjectID),
ModelBasinID int null constraint FK_ProjectLoadGeneratingUnit_ModelBasin_ModelBasinID foreign key references dbo.ModelBasin(ModelBasinID),
RegionalSubbasinID int null constraint FK_ProjectLoadGeneratingUnit_RegionalSubbasin_RegionalSubbasinID foreign key references dbo.RegionalSubbasin(RegionalSubbasinID),
DelineationID int null constraint FK_ProjectLoadGeneratingUnit_Delineation_DelineationID foreign key references dbo.Delineation(DelineationID),
WaterQualityManagementPlanID int null constraint FK_ProjectLoadGeneratingUnit_WaterQualityManagementPlan_WaterQualityManagementPlanID foreign key references dbo.WaterQualityManagementPlan(WaterQualityManagementPlanID),
IsEmptyResponseFromHRUService bit null
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
('Neptune', 'dbo', 'ProjectLoadGeneratingUnit', 'ProjectLoadGeneratingUnitGeometry', 2, 2771, 'MULTIPOLYGON')