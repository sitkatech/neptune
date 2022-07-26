--generated by exec dbo.pGeospatial_SpatialIndexCreate

IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'SPATIAL_Parcel_ParcelGeometry')
                          create spatial index [SPATIAL_Parcel_ParcelGeometry] on [Neptune].[dbo].[Parcel]([ParcelGeometry])
                          with (BOUNDING_BOX=(1.82674e+006, 636160, 1.89215e+006, 698757))
IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'SPATIAL_Parcel_ParcelGeometry4326')
                          create spatial index [SPATIAL_Parcel_ParcelGeometry4326] on [Neptune].[dbo].[Parcel]([ParcelGeometry4326])
                          with (BOUNDING_BOX=(-119, 33, -117, 34))

IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'SPATIAL_WaterQualityManagementPlan_WaterQualityManagementPlanBoundary')
                          create spatial index [SPATIAL_WaterQualityManagementPlan_WaterQualityManagementPlanBoundary] on [Neptune].[dbo].[WaterQualityManagementPlan]([WaterQualityManagementPlanBoundary])
                          with (BOUNDING_BOX=(1.83018e+006, 637916, 1.88009e+006, 690975))
IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'SPATIAL_WaterQualityManagementPlan_WaterQualityManagementPlanBoundary4326')
                          create spatial index [SPATIAL_WaterQualityManagementPlan_WaterQualityManagementPlanBoundary4326] on [Neptune].[dbo].[WaterQualityManagementPlan]([WaterQualityManagementPlanBoundary4326])
                          with (BOUNDING_BOX=(-119, 33, -117, 34))
