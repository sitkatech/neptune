CREATE TABLE [dbo].[TimeOfConcentration](
	[TimeOfConcentrationID] [int] NOT NULL CONSTRAINT [PK_TimeOfConcentration_TimeOfConcentrationID] PRIMARY KEY,
	[TimeOfConcentrationName] [varchar](100) CONSTRAINT [AK_TimeOfConcentration_TimeOfConcentrationName] UNIQUE,
	[TimeOfConcentrationDisplayName] [varchar](100) CONSTRAINT [AK_TimeOfConcentration_TimeOfConcentrationDisplayName] UNIQUE
)
