SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPOperationMonth](
	[TreatmentBMPOperationMonthID] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[OperationMonth] [int] NOT NULL,
 CONSTRAINT [PK_TreatmentBMPOperationMonth_TreatmentBMPOperationMonthID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPOperationMonthID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPOperationMonth_TreatmentBMPID_OperationMonth] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPID] ASC,
	[OperationMonth] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPOperationMonth]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPOperationMonth_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[TreatmentBMPOperationMonth] CHECK CONSTRAINT [FK_TreatmentBMPOperationMonth_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[TreatmentBMPOperationMonth]  WITH CHECK ADD  CONSTRAINT [CK_TreatmentBMPOperationMonth_Between1and12] CHECK  (([OperationMonth]>=(1) AND [OperationMonth]<=(12)))
GO
ALTER TABLE [dbo].[TreatmentBMPOperationMonth] CHECK CONSTRAINT [CK_TreatmentBMPOperationMonth_Between1and12]