SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPObservationDetailType](
	[TreatmentBMPObservationDetailTypeID] [int] NOT NULL,
	[TreatmentBMPObservationDetailTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TreatmentBMPObservationDetailTypeDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ObservationTypeID] [int] NOT NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [PK_TreatmentBMPObservationDetailType_TreatmentBMPObservationDetailTypeID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPObservationDetailTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPObservationDetailType_TreatmentBMPObservationDetailTypeName] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPObservationDetailTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPObservationDetailType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPObservationDetailType_ObservationType_ObservationTypeID] FOREIGN KEY([ObservationTypeID])
REFERENCES [dbo].[ObservationType] ([ObservationTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPObservationDetailType] CHECK CONSTRAINT [FK_TreatmentBMPObservationDetailType_ObservationType_ObservationTypeID]