SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Delineation](
	[DelineationID] [int] IDENTITY(1,1) NOT NULL,
	[DelineationGeometry] [geometry] NOT NULL,
	[DelineationTypeID] [int] NOT NULL,
	[IsVerified] [bit] NOT NULL,
	[DateLastVerified] [datetime] NULL,
	[VerifiedByPersonID] [int] NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[DateLastModified] [datetime] NOT NULL,
	[DelineationGeometry4326] [geometry] NULL,
	[HasDiscrepancies] [bit] NOT NULL,
 CONSTRAINT [PK_Delineation_DelineationID] PRIMARY KEY CLUSTERED 
(
	[DelineationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Delineation_TreatmentBMPID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Delineation]  WITH CHECK ADD  CONSTRAINT [FK_Delineation_DelineationType_DelineationTypeID] FOREIGN KEY([DelineationTypeID])
REFERENCES [dbo].[DelineationType] ([DelineationTypeID])
GO
ALTER TABLE [dbo].[Delineation] CHECK CONSTRAINT [FK_Delineation_DelineationType_DelineationTypeID]
GO
ALTER TABLE [dbo].[Delineation]  WITH CHECK ADD  CONSTRAINT [FK_Delineation_Person_VerifiedByPersonID_PersonID] FOREIGN KEY([VerifiedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[Delineation] CHECK CONSTRAINT [FK_Delineation_Person_VerifiedByPersonID_PersonID]
GO
ALTER TABLE [dbo].[Delineation]  WITH CHECK ADD  CONSTRAINT [FK_Delineation_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[Delineation] CHECK CONSTRAINT [FK_Delineation_TreatmentBMP_TreatmentBMPID]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
CREATE SPATIAL INDEX [SPATIAL_Delineation_DelineationGeometry] ON [dbo].[Delineation]
(
	[DelineationGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]