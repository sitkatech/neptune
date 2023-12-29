CREATE TABLE [dbo].[OVTASection](
	[OVTASectionID] [int] NOT NULL CONSTRAINT [PK_OVTASection_OVTASectionID] PRIMARY KEY,
	[OVTASectionName] [varchar](50) CONSTRAINT [AK_OVTASection_OVTASectionName] UNIQUE,
	[OVTASectionDisplayName] [varchar](50) CONSTRAINT [AK_OVTASection_OVTASectionDisplayName] UNIQUE,
	[SectionHeader] [varchar](100),
	[SortOrder] [int] NOT NULL,
	[HasCompletionStatus] [bit] NOT NULL
)
