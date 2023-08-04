CREATE TABLE [dbo].[FieldVisitStatus](
	[FieldVisitStatusID] [int] NOT NULL CONSTRAINT [PK_FieldVisitStatus_FieldVisitStatusID] PRIMARY KEY,
	[FieldVisitStatusName] [varchar](100) CONSTRAINT [AK_FieldVisitStatus_FieldVisitStatusName] UNIQUE,
	[FieldVisitStatusDisplayName] [varchar](100) CONSTRAINT [AK_FieldVisitStatus_FieldVisitStatusDisplayName] UNIQUE
)
