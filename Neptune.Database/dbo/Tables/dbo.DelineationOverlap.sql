CREATE TABLE [dbo].[DelineationOverlap](
	[DelineationOverlapID] [int] NOT NULL,
	[DelineationID] [int] NOT NULL,
	[OverlappingDelineationID] [int] NOT NULL,
	[OverlappingGeometry] [geometry] NOT NULL,
 CONSTRAINT [PK_DelineationOverlap_DelineationOverlapID] PRIMARY KEY CLUSTERED 
(
	[DelineationOverlapID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[DelineationOverlap]  WITH CHECK ADD  CONSTRAINT [FK_DelineationOverlap_Delineation_DelineationID] FOREIGN KEY([DelineationID])
REFERENCES [dbo].[Delineation] ([DelineationID])
GO
ALTER TABLE [dbo].[DelineationOverlap] CHECK CONSTRAINT [FK_DelineationOverlap_Delineation_DelineationID]
GO
ALTER TABLE [dbo].[DelineationOverlap]  WITH CHECK ADD  CONSTRAINT [FK_DelineationOverlap_Delineation_OverlappingDelineationID_DelineationID] FOREIGN KEY([OverlappingDelineationID])
REFERENCES [dbo].[Delineation] ([DelineationID])
GO
ALTER TABLE [dbo].[DelineationOverlap] CHECK CONSTRAINT [FK_DelineationOverlap_Delineation_OverlappingDelineationID_DelineationID]