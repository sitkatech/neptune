CREATE TABLE [dbo].[TrainingVideo](
	[TrainingVideoID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TrainingVideo_TrainingVideoID] PRIMARY KEY,
	[VideoName] [varchar](100),
	[VideoDescription] [varchar](500) NULL,
	[VideoURL] [varchar](100)
)
