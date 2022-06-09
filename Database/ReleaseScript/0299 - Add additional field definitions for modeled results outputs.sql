insert into dbo.FieldDefinitionType (FieldDefinitionTypeID, FieldDefinitionTypeName, FieldDefinitionTypeDisplayName)
values
	(122, 'SiteRunoff', 'Site Runoff'),
	(123, 'TreatedAndDischarged', 'Treated and Discharged'),
	(124, 'RetainedOrRecycled', 'Retained or Recycled'),
	(125, 'Untreated(BypassOrOverflow)', 'Untreated (Bypass or Overflow)'),
	(126, 'TotalSuspendedSolids', 'Total Suspended Solids'),
	(127, 'TotalNitrogen', 'Total Nitrogen'),
	(128, 'TotalPhosphorous', 'Total Phosphorous'),
	(129, 'FecalColiform', 'Fecal Coliform'),
	(130, 'TotalCopper', 'Total Copper'),
	(131, 'TotalLead', 'Total Lead'),
	(132, 'TotalZinc', 'Total Zinc'),
    (133, 'OCTAWatershed', 'OCTA Watershed')
go

insert into dbo.FieldDefinition (FieldDefinitionTypeID, FieldDefinitionValue)
values 
	(122, 'Site Runoff Definition'),
	(123, 'Treated and Discharged Definition'),
	(124, 'Retained or Recycled Definition'),
	(125, 'Untreated (Bypass or Overflow) Definition'),
	(126, 'Total Suspended Solids Definition'),
	(127, 'Total Nitrogen Definition'),
	(128, 'Total Phosphorous Definition'),
	(129, 'Fecal Coliform Definition'),
	(130, 'Total Copper Definition'),
	(131, 'Total Lead Definition'),
	(132, 'Total Zinc Definition'),
    (133, 'OCTA Watershed')