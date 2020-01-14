SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPType](
	[TreatmentBMPTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentBMPTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TreatmentBMPTypeDescription] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DelineationShouldBeReconciled] [bit] NOT NULL,
	[TreatmentBMPModelingTypeID] [int] NULL,
 CONSTRAINT [PK_TreatmentBMPType_TreatmentBMPTypeID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPType_TreatmentBMPTypeName] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPType_TreatmentBMPModelingType_TreatmentBMPModelingTypeID] FOREIGN KEY([TreatmentBMPModelingTypeID])
REFERENCES [dbo].[TreatmentBMPModelingType] ([TreatmentBMPModelingTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPType] CHECK CONSTRAINT [FK_TreatmentBMPType_TreatmentBMPModelingType_TreatmentBMPModelingTypeID]