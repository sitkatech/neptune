SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DelineationStaging](
	[DelineationStagingID] [int] IDENTITY(1,1) NOT NULL,
	[DelineationStagingGeometry] [geometry] NOT NULL,
	[UploadedByPersonID] [int] NULL,
 CONSTRAINT [PK_DelineationStaging_DelineationStagingID] PRIMARY KEY CLUSTERED 
(
	[DelineationStagingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[DelineationStaging]  WITH CHECK ADD  CONSTRAINT [FK_DelineationStaging_Person_UploadedByPersonID_PersonID] FOREIGN KEY([UploadedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[DelineationStaging] CHECK CONSTRAINT [FK_DelineationStaging_Person_UploadedByPersonID_PersonID]