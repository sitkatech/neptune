DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '016 - Add CustomAttributeTypes for modeling attributes'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    insert into dbo.CustomAttributeType (CustomAttributeTypeName, CustomAttributeDataTypeID, MeasurementUnitTypeID, IsRequired, CustomAttributeTypeDescription, CustomAttributeTypePurposeID, CustomAttributeTypeOptionsSchema)
values ('Average Diverted Flowrate',3, 19, 0, 'Average actual diverted flowrate over the months of operation.', 1, null),
('Average Treatment Flowrate',3, 18, 0, 'Average actual treated flowrate over the months of operation.', 1, null),
('Design Dry Weather Treatment Capacity',3,18, 0, 'Flow treatment capacity of the BMP.', 1, null),
('Design Low Flow Diversion Capacity',3, 19, 0, 'The physical capacity of the low flow diversion or the maximum permitted flow.', 1, null),
('Design Media Filtration Rate',3, 10,0, 'Design filtration rate through the media bed. This may be controlled by the media permeability or by an outlet control on the underdrain system.', 1, null),
('Diversion Rate', 3, 18, 0, 'Flowrate diverted into the BMP.', 1, null),
('Drawdown Time For Detention Volume',3,23, 0, 'Time for the basin to fully draw own after the end of a storm if there is no further inflow.', 1, null),
('Drawdown Time For WQ Detention Volume',3,23, 0, 'Time for water quality surcharge volume to draw down after the end of a storm if there is no further inflow.', 1, null),
('Dry Weather Flow Override',5, null, 0, 'Indicates if the modeled values for Dry Weather Flow have been overridden', 1, '["No - As Modeled", "Yes - DWF Effectively Eliminated"]'),
('Effective Footprint',3,2, 0, 'Average actual diverted flowrate over the months of operation.', 1, null),
('Effective Retention Depth',3,8, 0, 'Depth of water stored in shallow surface depression or media/rock sump for infiltration to occur.', 1, null),
('Infiltration Discharge Rate',3,18, 0, 'Design or tested infiltration flowrate of the drywell. This is specified in cubic feet per section, rather than inches per hour.', 1, null),
('Infiltration Surface Area',3,2, 0, 'Surface area through which infiltration can occur in the system. If infiltration will occur into the sidewalls of a BMP, it is appropriate to include half of the sidewall area as as part of the infiltration surface area.', 1, null),
('Media Bed Footprint',3,2, 0, 'Surface area of the media bed of the BMP.', 1, null),
('Modeled Months Of Operation',5, null, 0, 'This defines the months that the facility is operational. For modeling purposes, this can be "Summer," "Winter," or "Both."', 1, '["Both", "Summer", "Winter"]'),
('Permanent Pool Or Wetland Volume',3, 15, 0, 'Constructed wetland or permanent pool volume below discharge elevation.', 1, null),
('Routing Configuration', 5, null, 0, 'This specifies whether the BMP receives all flow from the drainage area (online), or if there is a diversion structure that limits the flow into the BMP (offline).', 1,'["Offline", "Online"]'),
('Storage Volume Below Lowest Outlet Elevation',3, 15, 0, 'The volume of water stored below the lowest outlet (e.g., underdrain, orifice) of the system.', 1, null),
('Summer Harvested Water Demand',3, 19, 0, 'Average daily harvested water demand from May through October.', 1, null),
('Time Of Concentration',5,17, 0, 'The time required for the entire drainage to begin contributing runoff to the BMP. This value must be less than 60 minutes. See TGD guidance.', 1, '[ "5", "10", "15", "20", "30", "45", "60"]'),
('Total Effective BMP Volume',3, 15, 0, 'The volume of the BMP available for water quality purposes. This includes ponding volume and the available pore volume in media layers and/or in gravel storage layers. It does not include flow control volumes or other volume that is not designed for water quality purposes. ', 1, null),
('Total Effective Drywell BMP Volume',3, 15, 0, 'The volume of the BMP available for water quality purposes. This includes the volume in any pre-treatment chamber as well as the volume in the well itself.', 1, null),
('Treatment Rate',3,18, 0, 'The flowrate at which the BMP can provide treatment of runoff.', 1, null),
('Underlying Hydrologic Soil Group',5, null, 0, 'Choose the soil group that best represents the soils underlying the BMP. This is used to estimate a default infiltration rate (A = XX, B = XX, C=XX, D=XX)', 1,'["A", "B", "C", "D", "Liner"]'),
('Underlying Infiltration Rate',3, 10, 0, 'The underlying infiltration rate below the BMP. This refers to the underlying soil, not engineered media.', 1, null),
('Water Quality Detention Volume',3, 15, 0, 'Extended detention surcharge storage above permanent pool volume. Extended detention is > 24-hour drawdown time.', 1, null),
('Wetted Footprint',3,2, 0, 'Wetted footprint when BMP is half full.', 1, null),
('Winter Harvested Water Demand',3, 19, 0, 'Average daily harvested water demand from November through April. This should be averaged to account for any shutdowns during wet weather and reduction in demand during the winter season.', 1, null)


    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Mack Peters', @MigrationName, '016 - Add CustomAttributeTypes for modeling attributes'
END