exec sp_rename 'dbo.PK_HydromodificationApplies_HydromodificationAppliesID', 'PK_HydromodificationAppliesType_HydromodificationAppliesTypeID', 'OBJECT';
exec sp_rename 'dbo.AK_HydromodificationApplies_HydromodificationAppliesDisplayName', 'AK_HydromodificationAppliesType_HydromodificationAppliesTypeDisplayName', 'OBJECT';
exec sp_rename 'dbo.AK_HydromodificationApplies_HydromodificationAppliesName', 'AK_HydromodificationAppliesType_HydromodificationAppliesTypeName', 'OBJECT';
exec sp_rename 'dbo.FK_WaterQualityManagementPlan_HydromodificationApplies_HydromodificationAppliesID', 'FK_WaterQualityManagementPlan_HydromodificationAppliesType_HydromodificationAppliesTypeID', 'OBJECT';
exec sp_rename 'dbo.WaterQualityManagementPlan.HydromodificationAppliesID', 'HydromodificationAppliesTypeID', 'COLUMN';
exec sp_rename 'dbo.HydromodificationApplies.HydromodificationAppliesID', 'HydromodificationAppliesTypeID', 'COLUMN';
exec sp_rename 'dbo.HydromodificationApplies.HydromodificationAppliesName', 'HydromodificationAppliesTypeName', 'COLUMN';
exec sp_rename 'dbo.HydromodificationApplies.HydromodificationAppliesDisplayName', 'HydromodificationAppliesTypeDisplayName', 'COLUMN';
exec sp_rename 'dbo.HydromodificationApplies', 'HydromodificationAppliesType';