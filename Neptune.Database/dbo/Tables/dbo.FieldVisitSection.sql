CREATE TABLE [dbo].[FieldVisitSection](
	[FieldVisitSectionID] [int] NOT NULL CONSTRAINT [PK_FieldVisitSection_FieldVisitSectionID] PRIMARY KEY,
	[FieldVisitSectionName] [varchar](100) CONSTRAINT [AK_FieldVisitSection_FieldVisitSectionName] UNIQUE,
	[FieldVisitSectionDisplayName] [varchar](100) CONSTRAINT [AK_FieldVisitSection_FieldVisitSectionDisplayName] UNIQUE,
	[SectionHeader] [varchar](100),
	[SortOrder] [int] NOT NULL,
)
