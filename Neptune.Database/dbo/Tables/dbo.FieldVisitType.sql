CREATE TABLE [dbo].[FieldVisitType](
	[FieldVisitTypeID] [int] NOT NULL CONSTRAINT [PK_FieldVisitType_FieldVisitTypeID] PRIMARY KEY,
	[FieldVisitTypeName] [varchar](100) CONSTRAINT [AK_FieldVisitType_FieldVisitTypeName] UNIQUE,
	[FieldVisitTypeDisplayName] [varchar](100) CONSTRAINT [AK_FieldVisitType_FieldVisitTypeDisplayName] UNIQUE
)
