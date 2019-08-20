-- Fix the geometry_columns primary key
/****** Object:  Index [PK_geometry_columns_f_table_catalog_f_table_schema_f_table_name_f_geometry_column]    Script Date: 8/20/2019 12:34:08 PM ******/
ALTER TABLE [dbo].[geometry_columns] DROP CONSTRAINT [PK_geometry_columns_f_table_catalog_f_table_schema_f_table_name_f_geometry_column] WITH ( ONLINE = OFF )
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [PK_geometry_columns_f_table_catalog_f_table_schema_f_table_name_f_geometry_column]    Script Date: 8/20/2019 12:34:08 PM ******/
ALTER TABLE [dbo].[geometry_columns] ADD  CONSTRAINT [PK_geometry_columns_f_table_catalog_f_table_schema_f_table_name_f_geometry_column] PRIMARY KEY CLUSTERED 
(
	[f_table_catalog] ASC,
	[f_table_schema] ASC,
	[f_table_name] ASC,
	[f_geometry_column] ASC
)ON [PRIMARY]
GO

-- script out the "reprojected" tables (needed so GDAL will use the correct SRID

/****** Object:  Table [dbo].[delineationreprojected]    Script Date: 8/20/2019 12:40:13 PM ******/
CREATE TABLE [dbo].[delineationreprojected](
	[ogr_fid] [int] IDENTITY(1,1) NOT NULL,
	[ogr_geometry] [geometry] NULL,
	[delineationid] [numeric](10, 0) NULL,
	[delineationtypeid] [numeric](10, 0) NULL,
	[isverified] [varchar](1) NULL,
	[datelastverified] [datetime] NULL,
	[verifiedbypersonid] [numeric](10, 0) NULL,
	[treatmentbmpid] [numeric](10, 0) NULL,
	[datelastmodified] [datetime] NULL,
	[delineationgeometry4326] [image] NULL,
 CONSTRAINT [PK_delineationreprojected] PRIMARY KEY CLUSTERED 
(
	[ogr_fid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[backbonesegmentreprojected](
	[ogr_fid] [int] IDENTITY(1,1) NOT NULL,
	[ogr_geometry] [geometry] NULL,
	[backbonesegmentid] [numeric](10, 0) NULL,
	[catchidn] [numeric](10, 0) NULL,
	[networkcatchmentid] [numeric](10, 0) NULL,
	[backbonesegmenttypeid] [numeric](10, 0) NULL,
	[downstreambackbonesegmentid] [numeric](10, 0) NULL,
	[streamname] [varchar](max) NULL,
	[backbonesegmentgeometry4326] [image] NULL,
 CONSTRAINT [PK_backbonesegmentreprojected] PRIMARY KEY CLUSTERED 
(
	[ogr_fid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[landuseblockreprojected](
	[ogr_fid] [int] IDENTITY(1,1) NOT NULL,
	[ogr_geometry] [geometry] NULL,
	[landuseblockid] [numeric](10, 0) NULL,
	[prioritylandusetypeid] [numeric](10, 0) NULL,
	[landusedescription] [varchar](500) NULL,
	[trashgenerationrate] [numeric](4, 1) NULL,
	[landusefortgr] [varchar](80) NULL,
	[medianhouseholdincomeresidential] [float] NULL,
	[medianhouseholdincomeretail] [float] NULL,
	[stormwaterjurisdictionid] [numeric](10, 0) NULL,
	[permittypeid] [numeric](10, 0) NULL,
	[landuseblockgeometry4326] [image] NULL,
 CONSTRAINT [PK_landuseblockreprojected] PRIMARY KEY CLUSTERED 
(
	[ogr_fid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[networkcatchmentreprojected](
	[ogr_fid] [int] IDENTITY(1,1) NOT NULL,
	[ogr_geometry] [geometry] NULL,
	[networkcatchmentid] [numeric](10, 0) NULL,
	[drainid] [varchar](10) NULL,
	[watershed] [varchar](100) NULL,
	[ocsurveycatchmentid] [numeric](10, 0) NULL,
	[ocsurveydownstreamcatchmentid] [numeric](10, 0) NULL,
	[catchmentgeometry4326] [image] NULL,
 CONSTRAINT [PK_networkcatchmentreprojected] PRIMARY KEY CLUSTERED 
(
	[ogr_fid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[onlandvisualtrashassessmentareareprojected](
	[ogr_fid] [int] IDENTITY(1,1) NOT NULL,
	[ogr_geometry] [geometry] NULL,
	[onlandvisualtrashassessmentareaid] [numeric](10, 0) NULL,
	[onlandvisualtrashassessmentareaname] [varchar](100) NULL,
	[stormwaterjurisdictionid] [numeric](10, 0) NULL,
	[onlandvisualtrashassessmentbaselinescoreid] [numeric](10, 0) NULL,
	[assessmentareadescription] [varchar](500) NULL,
	[transectline] [image] NULL,
	[onlandvisualtrashassessmentprogressscoreid] [numeric](10, 0) NULL,
	[onlandvisualtrashassessmentareageometry4326] [image] NULL,
	[transectline4326] [image] NULL,
 CONSTRAINT [PK_onlandvisualtrashassessmentareareprojected] PRIMARY KEY CLUSTERED 
(
	[ogr_fid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[onlandvisualtrashassessmentobservationreprojected](
	[ogr_fid] [int] IDENTITY(1,1) NOT NULL,
	[ogr_geometry] [geometry] NULL,
	[onlandvisualtrashassessmentobservationid] [numeric](10, 0) NULL,
	[onlandvisualtrashassessmentid] [numeric](10, 0) NULL,
	[note] [varchar](500) NULL,
	[observationdatetime] [datetime] NULL,
	[locationpoint4326] [image] NULL,
 CONSTRAINT [PK_onlandvisualtrashassessmentobservationreprojected] PRIMARY KEY CLUSTERED 
(
	[ogr_fid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[stormwaterjurisdictionreprojected](
	[ogr_fid] [int] IDENTITY(1,1) NOT NULL,
	[ogr_geometry] [geometry] NULL,
	[stormwaterjurisdictionid] [numeric](10, 0) NULL,
	[organizationid] [numeric](10, 0) NULL,
	[stateprovinceid] [numeric](10, 0) NULL,
	[istransportationjurisdiction] [varchar](1) NULL,
	[stormwaterjurisdictiongeometry4326] [image] NULL,
 CONSTRAINT [PK_stormwaterjurisdictionreprojected] PRIMARY KEY CLUSTERED 
(
	[ogr_fid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[transectlinereprojected](
	[ogr_fid] [int] IDENTITY(1,1) NOT NULL,
	[ogr_geometry] [geometry] NULL,
	[onlandvisualtrashassessmentareaid] [numeric](10, 0) NULL,
	[onlandvisualtrashassessmentareaname] [varchar](100) NULL,
	[stormwaterjurisdictionid] [numeric](10, 0) NULL,
	[onlandvisualtrashassessmentareageometry] [image] NULL,
	[onlandvisualtrashassessmentbaselinescoreid] [numeric](10, 0) NULL,
	[assessmentareadescription] [varchar](500) NULL,
	[onlandvisualtrashassessmentprogressscoreid] [numeric](10, 0) NULL,
	[onlandvisualtrashassessmentareageometry4326] [image] NULL,
	[transectline4326] [image] NULL,
 CONSTRAINT [PK_transectlinereprojected] PRIMARY KEY CLUSTERED 
(
	[ogr_fid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[treatmentbmpreprojected](
	[ogr_fid] [int] IDENTITY(1,1) NOT NULL,
	[ogr_geometry] [geometry] NULL,
	[treatmentbmpid] [numeric](10, 0) NULL,
	[treatmentbmpname] [varchar](200) NULL,
	[treatmentbmptypeid] [numeric](10, 0) NULL,
	[stormwaterjurisdictionid] [numeric](10, 0) NULL,
	[notes] [varchar](1000) NULL,
	[systemofrecordid] [varchar](100) NULL,
	[yearbuilt] [numeric](10, 0) NULL,
	[ownerorganizationid] [numeric](10, 0) NULL,
	[waterqualitymanagementplanid] [numeric](10, 0) NULL,
	[treatmentbmplifespantypeid] [numeric](10, 0) NULL,
	[treatmentbmplifespanenddate] [datetime] NULL,
	[requiredfieldvisitsperyear] [numeric](10, 0) NULL,
	[requiredpoststormfieldvisitsperyear] [numeric](10, 0) NULL,
	[inventoryisverified] [varchar](1) NULL,
	[dateoflastinventoryverification] [datetime] NULL,
	[inventoryverifiedbypersonid] [numeric](10, 0) NULL,
	[inventorylastchangeddate] [datetime] NULL,
	[trashcapturestatustypeid] [numeric](10, 0) NULL,
	[sizingbasistypeid] [numeric](10, 0) NULL,
	[trashcaptureeffectiveness] [numeric](10, 0) NULL,
	[locationpoint4326] [image] NULL,
 CONSTRAINT [PK_treatmentbmpreprojected] PRIMARY KEY CLUSTERED 
(
	[ogr_fid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[watershedreprojected](
	[ogr_fid] [int] IDENTITY(1,1) NOT NULL,
	[ogr_geometry] [geometry] NULL,
	[watershedid] [numeric](10, 0) NULL,
	[watershedname] [varchar](50) NULL,
	[watershedgeometry4326] [image] NULL,
 CONSTRAINT [PK_watershedreprojected] PRIMARY KEY CLUSTERED 
(
	[ogr_fid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- ensure that geometry_columns has the correct records
truncate table dbo.geometry_columns

INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'BackboneSegment', N'BackboneSegmentGeometry', 2, 2771, N'MULTIPOLYGON')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'backbonesegmentreprojected', N'ogr_geometry', 2, 4326, N'GEOMETRY')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'Delineation', N'DelineationGeometry', 2, 2771, N'MULTIPOLYGON')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'delineationreprojected', N'ogr_geometry', 2, 4326, N'GEOMETRY')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'DelineationStaging', N'DelineationStagingGeometry', 2, 2771, N'POLYGON')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'LandUseBlock', N'LandUseBlockGeometry', 2, 2771, N'MULTIPOLYGON')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'landuseblockreprojected', N'ogr_geometry', 2, 4326, N'GEOMETRY')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'LandUseBlockStaging', N'LandUseBlockStagingGeometry', 2, 2771, N'MULTIPOLYGON')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'NetworkCatchment', N'NetworkCatchmentGeometry', 2, 2771, N'MULTIPOLYGON')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'networkcatchmentreprojected', N'ogr_geometry', 2, 4326, N'GEOMETRY')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'OnlandVisualTrashAssessmentArea', N'OnlandVisualTrashAssessmentAreaGeometry', 2, 2771, N'MULTIPOLYGON')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'OnlandVisualTrashAssessmentArea', N'TransectLine', 2, 2771, N'LINESTRING')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'onlandvisualtrashassessmentareareprojected', N'ogr_geometry', 2, 4326, N'GEOMETRY')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'OnlandVisualTrashAssessmentObservation', N'LocationPoint', 2, 2771, N'POINT')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'onlandvisualtrashassessmentobservationreprojected', N'ogr_geometry', 2, 4326, N'GEOMETRY')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'StormwaterJurisdiction', N'StormwaterJurisdictionGeometry', 2, 2771, N'MULTIPOLYGON')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'stormwaterjurisdictionreprojected', N'ogr_geometry', 2, 4326, N'GEOMETRY')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'transectlinereprojected', N'ogr_geometry', 2, 4326, N'GEOMETRY')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'TrashGeneratingUnit4326', N'TrashGeneratingUnit4326Geometry', 2, 4326, N'MULTIPOLYGON')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'TreatmentBMP', N'LocationPoint', 2, 2771, N'POINT')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'treatmentbmpreprojected', N'ogr_geometry', 2, 4326, N'GEOMETRY')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'vGeoServerTrashGeneratingUnit', N'TrashGeneratingUnitGeometry', 2, 4326, N'MULTIPOLYGON')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'Watershed', N'WatershedGeometry', 2, 2771, N'MULTIPOLYGON')
GO
INSERT [dbo].[geometry_columns] ([f_table_catalog], [f_table_schema], [f_table_name], [f_geometry_column], [coord_dimension], [srid], [geometry_type]) VALUES (N'Neptune', N'dbo', N'watershedreprojected', N'ogr_geometry', 2, 4326, N'GEOMETRY')
GO

