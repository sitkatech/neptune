SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObservationTargetType](
	[ObservationTargetTypeID] [int] NOT NULL,
	[ObservationTargetTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ObservationTargetTypeDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SortOrder] [int] NOT NULL,
	[ObservationTargetTypeDescription] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_ObservationTargetType_ObservationTargetTypeID] PRIMARY KEY CLUSTERED 
(
	[ObservationTargetTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ObservationTargetType_ObservationTargetTypeDisplayName] UNIQUE NONCLUSTERED 
(
	[ObservationTargetTypeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ObservationTargetType_ObservationTargetTypeName] UNIQUE NONCLUSTERED 
(
	[ObservationTargetTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
