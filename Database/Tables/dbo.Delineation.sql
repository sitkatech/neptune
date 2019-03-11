SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Delineation](
	[DelineationID] [int] IDENTITY(1,1) NOT NULL,
	[DelineationGeometry] [geometry] NOT NULL,
	[DelineationTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Delineation_DelineationID] PRIMARY KEY CLUSTERED 
(
	[DelineationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Delineation]  WITH CHECK ADD  CONSTRAINT [FK_Delineation_DelineationType_DelineationTypeID] FOREIGN KEY([DelineationTypeID])
REFERENCES [dbo].[DelineationType] ([DelineationTypeID])
GO
ALTER TABLE [dbo].[Delineation] CHECK CONSTRAINT [FK_Delineation_DelineationType_DelineationTypeID]